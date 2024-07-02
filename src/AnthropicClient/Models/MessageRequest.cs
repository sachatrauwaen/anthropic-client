using System.Text.Json.Serialization;

using AnthropicClient.Utils;

namespace AnthropicClient.Models;

/// <summary>
/// Represents a message request.
/// </summary>
public abstract class MessageRequest
{
  /// <summary>
  /// Gets the model ID to use for the request.
  /// </summary>
  public string Model { get; init; } = string.Empty;

  /// <summary>
  /// Gets the system ID to use for the request.
  /// </summary>
  public string? System { get; init; } = null;

  /// <summary>
  /// Gets the messages to send to the model.
  /// </summary>
  public List<ChatMessage> Messages { get; init; } = [];

  /// <summary>
  /// Gets the maximum number of tokens to generate.
  /// </summary>
  [JsonPropertyName("max_tokens")]
  public int MaxTokens { get; init; } = 1024;

  /// <summary>
  /// Gets the metadata to include with the request.
  /// </summary>
  public Dictionary<string, object>? Metadata { get; init; } = null;

  /// <summary>
  /// Gets the prompt stop sequences.
  /// </summary>
  [JsonPropertyName("stop_sequences")]
  public List<string> StopSequences { get; init; } = [];

  /// <summary>
  /// Gets the temperature to use for the request.
  /// </summary>
  public decimal Temperature { get; init; } = 0.0m;

  /// <summary>
  /// Gets the top-K value to use for the request.
  /// </summary>
  public int? TopK { get; init; } = null;

  /// <summary>
  /// Gets the top-P value to use for the request.
  /// </summary>
  public decimal? TopP { get; init; } = null;

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
  /// Gets a value indicating whether the message should be streamed.
  /// </summary>
  public bool Stream { get; init; }

  [JsonConstructor]
  internal MessageRequest() { }

  /// <summary>
  /// Initializes a new instance of the <see cref="MessageRequest"/> class.
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
  /// <param name="stream">A value indicating whether the message should be streamed.</param>
  /// <exception cref="ArgumentException">Thrown when the model ID is invalid.</exception>
  /// <exception cref="ArgumentNullException">Thrown when the model or messages is null.</exception>
  /// <exception cref="ArgumentException">Thrown when the messages contain no messages.</exception>
  /// <exception cref="ArgumentException">Thrown when the max tokens is less than one.</exception>
  /// <exception cref="ArgumentException">Thrown when the temperature is less than zero or greater than one.</exception>
  /// <returns>A new instance of the <see cref="MessageRequest"/> class.</returns>
  protected MessageRequest(
    string model,
    List<ChatMessage> messages,
    int maxTokens = 1024,
    string? system = null,
    Dictionary<string, object>? metadata = null,
    decimal temperature = 0.0m,
    int? topK = null,
    decimal? topP = null,
    ToolChoice? toolChoice = null,
    List<Tool>? tools = null,
    bool stream = false
  )
  {
    ArgumentValidator.ThrowIfNull(model, nameof(model));
    ArgumentValidator.ThrowIfNull(messages, nameof(messages));

    if (AnthropicModels.IsValidModel(model) is false)
    {
      throw new ArgumentException($"Invalid model ID: {model}");
    }

    if (messages.Count < 1)
    {
      throw new ArgumentException("Messages must contain at least one message");
    }

    if (maxTokens < 1)
    {
      throw new ArgumentException($"Invalid max tokens: {maxTokens}");
    }

    if (temperature < 0.0m || temperature > 1.0m)
    {
      throw new ArgumentException($"Invalid temperature: {temperature}");
    }

    Model = model;
    Messages = messages;
    MaxTokens = maxTokens;
    System = system;
    Metadata = metadata;
    Temperature = temperature;
    TopK = topK;
    TopP = topP;
    ToolChoice = toolChoice;
    Tools = tools;
    Stream = stream;
  }
}