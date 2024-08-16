using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

/// <summary>
/// Represents a message request.
/// </summary>
public class StreamMessageRequest : BaseMessageRequest
{
  [JsonConstructor]
  internal StreamMessageRequest() : base() { }

  /// <summary>
  /// Initializes a new instance of the <see cref="StreamMessageRequest"/> class.
  /// </summary>
  /// <param name="model">The model ID to use for the request.</param>
  /// <param name="messages">The messages to send to the model.</param>
  /// <param name="maxTokens">The maximum number of tokens to generate.</param>
  /// <param name="system">The system prompt to use for the request.</param>
  /// <param name="metadata">The metadata to include with the request.</param>
  /// <param name="temperature">The temperature to use for the request.</param>
  /// <param name="topK">The top-K value to use for the request.</param>
  /// <param name="topP">The top-P value to use for the request.</param>
  /// <param name="toolChoice">The tool choice mode to use for the request.</param>
  /// <param name="tools">The tools to use for the request.</param>
  /// <param name="stopSequences">The prompt stop sequences.</param>
  /// <param name="systemMessages">The system messages to include with the request.</param>
  /// <exception cref="ArgumentException">Thrown when the model ID is invalid.</exception>
  /// <exception cref="ArgumentNullException">Thrown when the model or messages is null.</exception>
  /// <exception cref="ArgumentException">Thrown when the messages contain no messages.</exception>
  /// <exception cref="ArgumentException">Thrown when the max tokens is less than one.</exception>
  /// <exception cref="ArgumentException">Thrown when the temperature is less than zero or greater than one.</exception>
  /// <returns>A new instance of the <see cref="StreamMessageRequest"/> class.</returns>
  public StreamMessageRequest(
    string model,
    List<Message> messages,
    int maxTokens = 1024,
    string? system = null,
    Dictionary<string, object>? metadata = null,
    decimal temperature = 0.0m,
    int? topK = null,
    decimal? topP = null,
    ToolChoice? toolChoice = null,
    List<Tool>? tools = null,
    List<string>? stopSequences = null,
    List<TextContent>? systemMessages = null
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
    true,
    stopSequences,
    systemMessages
  )
  {
  }
}