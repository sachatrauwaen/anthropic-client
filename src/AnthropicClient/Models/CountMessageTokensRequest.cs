using System.Text.Json.Serialization;

using AnthropicClient.Utils;

namespace AnthropicClient.Models;

/// <summary>
/// Represents a request to count the number of tokens in a message.
/// </summary>
public class CountMessageTokensRequest
{
  /// <summary>
  /// Gets the model ID to be used for the request.
  /// </summary>
  public string Model { get; init; } = string.Empty;

  /// <summary>
  /// Gets the messages to count the number of tokens in.
  /// </summary>
  public List<Message> Messages { get; init; } = [];

  /// <summary>
  /// Gets the tool choice mode to use for the request.
  /// </summary>
  [JsonPropertyName("tool_choice")]
  public ToolChoice? ToolChoice { get; init; } = null;

  /// <summary>
  /// Gets the tools to use for the request.
  /// </summary>
  public List<Tool>? Tools { get; init; } = null;

  /// <summary>
  /// Gets the system prompt to use for the request.
  /// </summary>
  [JsonPropertyName("system")]
  public List<TextContent>? SystemPrompt { get; init; } = null;

  /// <summary>
  /// Initializes a new instance of the <see cref="CountMessageTokensRequest"/> class.
  /// </summary>
  /// <param name="model">The model ID to use for the request.</param>
  /// <param name="messages">The messages to count the number of tokens in.</param>
  /// <param name="toolChoice">The tool choice mode to use for the request.</param>
  /// <param name="tools">The tools to use for the request.</param>
  /// <param name="systemPrompt">The system prompt to use for the request.</param>
  /// <exception cref="ArgumentNullException">Thrown when <paramref name="model"/> or <paramref name="messages"/> is null.</exception>
  /// <exception cref="ArgumentException">Thrown when <paramref name="messages"/> is empty.</exception>
  /// <returns>A new instance of the <see cref="CountMessageTokensRequest"/> class.</returns>
  public CountMessageTokensRequest(
    string model,
    List<Message> messages,
    ToolChoice? toolChoice = null,
    List<Tool>? tools = null,
    List<TextContent>? systemPrompt = null
  )
  {
    ArgumentValidator.ThrowIfNull(model, nameof(model));
    ArgumentValidator.ThrowIfNull(messages, nameof(messages));

    if (messages.Count < 1)
    {
      throw new ArgumentException("Messages must contain at least one message");
    }

    Model = model;
    Messages = messages;
    ToolChoice = toolChoice;
    Tools = tools;
    SystemPrompt = systemPrompt;
  }
}