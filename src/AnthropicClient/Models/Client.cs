using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

using AnthropicClient.Json;
using AnthropicClient.Utils;

namespace AnthropicClient.Models;

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
    var requestContent = new StringContent(Serialize(request), Encoding.UTF8, JsonContentType);
    var response = await _httpClient.PostAsync(MessagesEndpoint, requestContent);
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

  private string Serialize<T>(T obj) => JsonSerializer.Serialize(obj, JsonSerializationOptions.DefaultOptions);
  private T? Deserialize<T>(string json) => JsonSerializer.Deserialize<T>(json, JsonSerializationOptions.DefaultOptions);
  private string GetRequestId(HttpResponseMessage response) => response.Headers.GetValues(RequestIdHeader).FirstOrDefault() ?? string.Empty;
}