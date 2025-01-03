using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

using AnthropicClient.Json;
using AnthropicClient.Models;
using AnthropicClient.Utils;

namespace AnthropicClient;

/// <summary>
/// Represents a client for interacting with the Anthropic API.
/// </summary>
public interface IAnthropicApiClient
{
  /// <summary>
  /// Creates a message asynchronously.
  /// </summary>
  /// <param name="request">The message request to create.</param>
  /// <returns>A task that represents the asynchronous operation. The task result contains the response as an <see cref="AnthropicResult{T}"/>.</returns>
  Task<AnthropicResult<MessageResponse>> CreateMessageAsync(MessageRequest request);

  /// <summary>
  /// Creates a message asynchronously and streams the response.
  /// </summary>
  /// <param name="request">The message request to create.</param>
  /// <returns>An asynchronous enumerable that yields the response event by event.</returns>
  IAsyncEnumerable<AnthropicEvent> CreateMessageAsync(StreamMessageRequest request);

  /// <summary>
  /// Counts the tokens in a message asynchronously.
  /// </summary>
  /// <param name="request">The count message tokens request.</param>
  /// <returns>A task that represents the asynchronous operation. The task result contains the response as an <see cref="AnthropicResult{T}"/> where T is <see cref="TokenCountResponse"/>.</returns>
  Task<AnthropicResult<TokenCountResponse>> CountMessageTokensAsync(CountMessageTokensRequest request);
}

/// <inheritdoc cref="IAnthropicApiClient"/>
public class AnthropicApiClient : IAnthropicApiClient
{
  private const string BaseUrl = "https://api.anthropic.com/v1/";
  private const string ApiKeyHeader = "x-api-key";
  private const string MessagesEndpoint = "messages";
  private const string CountTokensEndpoint = "messages/count_tokens";
  private const string JsonContentType = "application/json";
  private const string EventPrefix = "event:";
  private const string DataPrefix = "data:";
  private readonly Dictionary<string, string> _defaultHeaders = new()
  {
    { "anthropic-version", "2023-06-01" },
  };
  private readonly HttpClient _httpClient;

  /// <summary>
  /// Initializes a new instance of the <see cref="AnthropicApiClient"/> class.
  /// </summary>
  /// <param name="apiKey">The API key to use for the client.</param>
  /// <param name="httpClient">The HTTP client to use for the client.</param>
  /// <exception cref="ArgumentNullException">Thrown when the API key or HTTP client is null.</exception>
  /// <returns>A new instance of the <see cref="AnthropicApiClient"/> class.</returns>
  public AnthropicApiClient(string apiKey, HttpClient httpClient)
  {
    ArgumentValidator.ThrowIfNull(apiKey, nameof(apiKey));
    ArgumentValidator.ThrowIfNull(httpClient, nameof(httpClient));

    _httpClient = httpClient;
    _httpClient.BaseAddress = new Uri(BaseUrl);
    _httpClient.DefaultRequestHeaders.Add(ApiKeyHeader, apiKey);
    _httpClient.DefaultRequestHeaders
      .Accept
      .Add(new MediaTypeWithQualityHeaderValue(JsonContentType));

    foreach (var pair in _defaultHeaders)
    {
      _httpClient.DefaultRequestHeaders.Add(pair.Key, pair.Value);
    }
  }

  /// <inheritdoc />
  public async Task<AnthropicResult<MessageResponse>> CreateMessageAsync(MessageRequest request)
  {
    var response = await SendRequestAsync(MessagesEndpoint, request);
    var anthropicHeaders = new AnthropicHeaders(response.Headers);
    var responseContent = await response.Content.ReadAsStringAsync();

    if (response.IsSuccessStatusCode is false)
    {
      var error = Deserialize<AnthropicError>(responseContent) ?? new AnthropicError();
      return AnthropicResult<MessageResponse>.Failure(error, anthropicHeaders);
    }

    var msgResponse = Deserialize<MessageResponse>(responseContent) ?? new MessageResponse();

    if (request.Tools is not null && request.Tools.Count > 0)
    {
      msgResponse.ToolCall = GetToolCall(msgResponse, request.Tools);
    }

    return AnthropicResult<MessageResponse>.Success(msgResponse, anthropicHeaders);
  }

  /// <inheritdoc />
  public async IAsyncEnumerable<AnthropicEvent> CreateMessageAsync(StreamMessageRequest request)
  {
    var response = await SendRequestAsync(MessagesEndpoint, request);

    if (response.IsSuccessStatusCode is false)
    {
      var error = Deserialize<AnthropicError>(await response.Content.ReadAsStringAsync()) ?? new AnthropicError();
      yield return new AnthropicEvent(EventType.Error, new ErrorEventData(error.Error));
      yield break;
    }

    var anthropicHeaders = new AnthropicHeaders(response.Headers);

    using var responseContent = await response.Content.ReadAsStreamAsync();
    using var streamReader = new StreamReader(responseContent);

    MessageResponse? msgResponse = null;
    Content? content = null;
    var toolInputJsonStringBuilder = new StringBuilder();
    var currentEvent = new AnthropicEvent();

    do
    {
      var line = await streamReader.ReadLineAsync();

      // I know...this is not pretty, but here is why...
      // as events are being yielded I want to also
      // build up the complete response
      // so I can yield it as a special event to make tool
      // calling easier to handle

      // initialize response on message start
      if (currentEvent.Type is EventType.MessageStart && currentEvent.Data is MessageStartEventData msgStartData)
      {
        msgResponse = msgStartData.Message;
      }

      // initialize content block on content block start
      if (currentEvent.Type is EventType.ContentBlockStart && currentEvent.Data is ContentStartEventData contentStartData)
      {
        content = contentStartData.ContentBlock;
      }

      // update content block with deltas based on
      // current content type and delta type
      if (currentEvent.Type is EventType.ContentBlockDelta && currentEvent.Data is ContentDeltaEventData contentDeltaData)
      {
        if (content is TextContent textContent && contentDeltaData.Delta is TextDelta textDelta)
        {
          var newText = textContent.Text + textDelta.Text;
          content = new TextContent(newText);
        }

        if (content is ToolUseContent toolUseContent && contentDeltaData.Delta is JsonDelta jsonDelta)
        {
          toolInputJsonStringBuilder.Append(jsonDelta.PartialJson);
        }
      }

      // finalize content block on content block stop
      // and add it to the response
      if (currentEvent.Type is EventType.ContentBlockStop)
      {
        if (content is not null && msgResponse is not null)
        {
          if (content is TextContent textContent)
          {
            msgResponse.Content.Add(textContent);
          }

          if (content is ToolUseContent toolUseContent)
          {
            var input = Deserialize<Dictionary<string, object?>>(toolInputJsonStringBuilder.ToString());
            var newToolUseContent = new ToolUseContent()
            {
              Id = toolUseContent.Id,
              Name = toolUseContent.Name,
              Input = input!,
            };

            msgResponse.Content.Add(newToolUseContent);
          }

          content = null;
        }
      }

      // update response with message delta data
      if (
        currentEvent.Type is EventType.MessageDelta &&
        currentEvent.Data is MessageDeltaEventData msgDeltaData &&
        msgResponse is not null
      )
      {
        var existingUsage = msgResponse.Usage;
        var newUsage = new Usage()
        {
          InputTokens = existingUsage.InputTokens + msgDeltaData.Usage.InputTokens,
          OutputTokens = existingUsage.OutputTokens + msgDeltaData.Usage.OutputTokens,
          CacheCreationInputTokens = existingUsage.CacheCreationInputTokens + msgDeltaData.Usage.CacheCreationInputTokens,
          CacheReadInputTokens = existingUsage.CacheReadInputTokens + msgDeltaData.Usage.CacheReadInputTokens,
        };

        msgResponse = new MessageResponse()
        {
          Id = msgResponse.Id,
          Model = msgResponse.Model,
          Role = msgResponse.Role,
          StopReason = msgDeltaData.Delta.StopReason,
          StopSequence = msgDeltaData.Delta.StopSequence,
          Type = msgResponse.Type,
          Usage = newUsage,
          Content = msgResponse.Content,
        };

        if (request.Tools is not null && request.Tools.Count > 0)
        {
          msgResponse.ToolCall = GetToolCall(msgResponse, request.Tools);
        }
      }

      // yield response on message stop
      if (currentEvent.Type is EventType.MessageStop && msgResponse is not null)
      {
        var eventData = new MessageCompleteEventData(msgResponse, anthropicHeaders);
        yield return new AnthropicEvent(EventType.MessageComplete, eventData);
        msgResponse = null;
      }

      if (line is null)
      {
        if (string.IsNullOrWhiteSpace(currentEvent.Type) is false)
        {
          yield return currentEvent;
          currentEvent = new AnthropicEvent();
        }

        break;
      }

      if (line == string.Empty)
      {
        yield return currentEvent;
        currentEvent = new AnthropicEvent();
      }

      if (line.StartsWith(EventPrefix))
      {
        var eventType = line.Substring(EventPrefix.Length).Trim();
        currentEvent = new AnthropicEvent(eventType, currentEvent.Data);
        continue;
      }

      if (line.StartsWith(DataPrefix))
      {
        var eventData = line.Substring(DataPrefix.Length).Trim();
        var eventDataJson = Deserialize<EventData>(eventData);
        currentEvent = new AnthropicEvent(currentEvent.Type, eventDataJson!);
        continue;
      }
    } while (true);
  }

  /// <inheritdoc />
  public async Task<AnthropicResult<TokenCountResponse>> CountMessageTokensAsync(CountMessageTokensRequest request)
  {
    var response = await SendRequestAsync(CountTokensEndpoint, request);
    var anthropicHeaders = new AnthropicHeaders(response.Headers);
    var responseContent = await response.Content.ReadAsStringAsync();

    if (response.IsSuccessStatusCode is false)
    {
      var error = Deserialize<AnthropicError>(responseContent) ?? new AnthropicError();
      return AnthropicResult<TokenCountResponse>.Failure(error, anthropicHeaders);
    }

    var msgResponse = Deserialize<TokenCountResponse>(responseContent) ?? new TokenCountResponse();

    return AnthropicResult<TokenCountResponse>.Success(msgResponse, anthropicHeaders);
  }

  private ToolCall? GetToolCall(MessageResponse response, List<Tool> tools)
  {
    var toolUse = response.Content.OfType<ToolUseContent>().FirstOrDefault();

    if (toolUse is null)
    {
      return null;
    }

    var tool = tools.FirstOrDefault(t => t.Name == toolUse.Name);

    if (tool is null)
    {
      return null;
    }

    return new ToolCall(tool, toolUse);
  }

  private async Task<HttpResponseMessage> SendRequestAsync<T>(string endpoint, T request)
  {
    var requestJson = Serialize(request);
    var requestContent = new StringContent(requestJson, Encoding.UTF8, JsonContentType);
    return await _httpClient.PostAsync(endpoint, requestContent);
  }

  private string Serialize<T>(T obj) => JsonSerializer.Serialize(obj, JsonSerializationOptions.DefaultOptions);
  private T? Deserialize<T>(string json) => JsonSerializer.Deserialize<T>(json, JsonSerializationOptions.DefaultOptions);
}