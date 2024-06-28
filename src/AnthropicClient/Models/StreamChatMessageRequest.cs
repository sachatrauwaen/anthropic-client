using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

/// <summary>
/// Represents a chat message request.
/// </summary>
public class StreamChatMessageRequest : MessageRequest
{
  [JsonConstructor]
  internal StreamChatMessageRequest() : base() { }

  /// <summary>
  /// Initializes a new instance of the <see cref="StreamChatMessageRequest"/> class.
  /// </summary>
  /// <param name="model">The model ID to use for the request.</param>
  /// <param name="messages">The messages to send to the model.</param>
  /// <param name="maxTokens">The maximum number of tokens to generate.</param>
  /// <param name="system">The system ID to use for the request.</param>
  /// <param name="metadata">The metadata to include with the request.</param>
  /// <param name="temperature">The temperature to use for the request.</param>
  /// <param name="topK">The top-K value to use for the request.</param>
  /// <param name="topP">The top-P value to use for the request.</param>
  /// <param name="toolChoice">The tool choice mode to use for the request.</param>
  /// <param name="tools">The tools to use for the request.</param>
  /// <exception cref="ArgumentException">Thrown when the model ID is invalid.</exception>
  /// <exception cref="ArgumentNullException">Thrown when the model or messages is null.</exception>
  /// <exception cref="ArgumentException">Thrown when the messages contain no messages.</exception>
  /// <exception cref="ArgumentException">Thrown when the max tokens is less than one.</exception>
  /// <exception cref="ArgumentException">Thrown when the temperature is less than zero or greater than one.</exception>
  /// <returns>A new instance of the <see cref="StreamChatMessageRequest"/> class.</returns>
  public StreamChatMessageRequest(
    string model,
    List<ChatMessage> messages,
    int maxTokens = 1024,
    string? system = null,
    Dictionary<string, object>? metadata = null,
    decimal temperature = 0.0m,
    int? topK = null,
    decimal? topP = null,
    ToolChoice? toolChoice = null,
    List<Tool>? tools = null
  ) : base(
    model,
    messages,
    maxTokens,
    system,
    metadata,
    temperature,
    topK,
    topP,
    toolChoice,
    tools,
    true
  )
  {
  }
}