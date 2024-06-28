using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

using AnthropicClient.Json;
using AnthropicClient.Utils;
using AnthropicClient.Models;

namespace AnthropicClient;

/// <summary>
/// Represents a client for interacting with the Anthropic API.
/// </summary>
public interface IClient
{
  /// <summary>
  /// Creates a chat message asynchronously.
  /// </summary>
  /// <param name="request">The chat message request to create.</param>
  /// <returns>A task that represents the asynchronous operation. The task result contains the chat response as an <see cref="AnthropicResult{T}"/>.</returns>
  Task<AnthropicResult<ChatResponse>> CreateChatMessageAsync(ChatMessageRequest request);

  /// <summary>
  /// Creates a chat message asynchronously and streams the response.
  /// </summary>
  /// <param name="request">The chat message request to create.</param>
  /// <returns>An asynchronous enumerable that yields the chat response line by line.</returns>
  IAsyncEnumerable<AnthropicEvent> CreateChatMessageAsync(StreamChatMessageRequest request);
}

/// <inheritdoc cref="IClient"/>
public class Client : IClient
{
  private const string BaseUrl = "https://api.anthropic.com/v1/";
  private const string ApiKeyHeader = "x-api-key";
  private const string MessagesEndpoint = "messages";
  private const string JsonContentType = "application/json";
  private const string RequestIdHeader = "request-id";
  private readonly Dictionary<string, string> _defaultHeaders = new()
  {
    { "anthropic-version", "2023-06-01" },
  };
  private readonly HttpClient _httpClient;

  /// <summary>
  /// Initializes a new instance of the <see cref="Client"/> class.
  /// </summary>
  /// <param name="apiKey">The API key to use for the client.</param>
  /// <param name="httpClient">The HTTP client to use for the client.</param>
  /// <exception cref="ArgumentNullException">Thrown when the API key or HTTP client is null.</exception>
  /// <returns>A new instance of the <see cref="Client"/> class.</returns>
  public Client(string apiKey, HttpClient httpClient)
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
  public async Task<AnthropicResult<ChatResponse>> CreateChatMessageAsync(ChatMessageRequest request)
  {
    var response = await SendRequestAsync(request);
    var requestId = GetRequestId(response);
    var responseContent = await response.Content.ReadAsStringAsync();

    if (response.IsSuccessStatusCode is false)
    {
      var error = Deserialize<AnthropicError>(responseContent) ?? new AnthropicError();
      return AnthropicResult<ChatResponse>.Failure(error, requestId);
    }

    var chatResponse = Deserialize<ChatResponse>(responseContent) ?? new ChatResponse();
    return AnthropicResult<ChatResponse>.Success(chatResponse, requestId);
  }

  /// <inheritdoc />
  public async IAsyncEnumerable<AnthropicEvent> CreateChatMessageAsync(StreamChatMessageRequest request)
  {
    var response = await SendRequestAsync(request);
    var requestId = GetRequestId(response);

    using var responseContent = await response.Content.ReadAsStreamAsync();
    using var streamReader = new StreamReader(responseContent);

    var currentEvent = new AnthropicEvent();

    // TODO: I'd like to emit custom events unique to this client
    // - "content_block_complete"
    //   - provides the complete content block that was streamed
    //   - useful for streaming in text, but then handling tool calls with their entire input

    // will need to keep track of current content so that we can build it up as relevant
    // deltas are streamed in

    // will want to capture the content block start event
    // then continue to build this up until we get to the content block stop event
    // at that point we should have the complete block and we should be able to yield
    // return the complete block and reset the current content block
    
    // event: content_block_start
    // data: {"type":"content_block_start","index":1,"content_block":{"type":"tool_use","id":"toolu_01T1x1fJ34qAmk2tNTrN7Up6","name":"get_weather","input":{}}}


    do
    {
      var line = await streamReader.ReadLineAsync();
      
      if (line is null)
      {
        break;
      }

      if (line.StartsWith("event:"))
      {
        var eventType = line.Substring("event:".Length).Trim();
        currentEvent = currentEvent with { Type = eventType };
      }

      if (line.StartsWith("data:"))
      {
        var eventData = line.Substring("data:".Length).Trim();
        var eventDataJson = JsonSerializer.Deserialize<EventData>(eventData, JsonSerializationOptions.DefaultOptions);
        currentEvent = currentEvent with { Data = eventDataJson! };
      }

      if (line == string.Empty)
      {
        yield return currentEvent;
        currentEvent = new AnthropicEvent();
      }
    } while (true);
  }

  private async Task<HttpResponseMessage> SendRequestAsync(MessageRequest request)
  {
    var requestContent = new StringContent(Serialize(request), Encoding.UTF8, JsonContentType);
    return await _httpClient.PostAsync(MessagesEndpoint, requestContent);
  }

  private string Serialize<T>(T obj) => JsonSerializer.Serialize(obj, JsonSerializationOptions.DefaultOptions);
  private T? Deserialize<T>(string json) => JsonSerializer.Deserialize<T>(json, JsonSerializationOptions.DefaultOptions);
  private string GetRequestId(HttpResponseMessage response) => response.Headers.GetValues(RequestIdHeader).FirstOrDefault() ?? string.Empty;
}