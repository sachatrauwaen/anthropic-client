using Newtonsoft.Json;

using AnthropicClient.Utils;

namespace AnthropicClient.Models;

/// <summary>
/// Represents a message request.
/// </summary>
public abstract class BaseMessageRequest
{
  /// <summary>
  /// Gets the model ID to use for the request.
  /// </summary>
  public string Model { get; init; } = string.Empty;

  // TODO: I do not like this. I would prefer to have a single property that is a list of TextContent objects.
  // This approach was taken to maintain compatibility with the API. As someone could be using the System property
  // and changing it to a list of TextContent objects would break their code.
  // However if an opportunity arises for a breaking change release, this should be changed.

  /// <summary>
  /// Gets the system message that will be used as the system prompt if no system messages are provided.
  /// </summary>
  [JsonIgnore]
  public string? System { get; init; } = null;

  /// <summary>
  /// Gets the system messages to send to the model to be used as the system prompt.
  /// </summary>
  [JsonIgnore]
  public List<TextContent>? SystemMessages { get; init; } = null;

  /// <summary>
  /// Gets the system prompt that will be used for the request. 
  /// If will return the system messages if they are provided, otherwise it will return the system message.
  /// If neither are provided, it will return null.
  /// </summary>
  [JsonProperty("system")]
  public List<TextContent>? SystemPrompt => GetSystemPrompt();

  private List<TextContent>? GetSystemPrompt()
  {
    if (SystemMessages is not null)
    {
      return SystemMessages;
    }

    if (System is not null)
    {
      return [new TextContent(System)];
    }

    return null;
  }

  /// <summary>
  /// Gets the messages to send to the model.
  /// </summary>
  public List<Message> Messages { get; init; } = [];

  /// <summary>
  /// Gets the maximum number of tokens to generate.
  /// </summary>
  [JsonProperty("max_tokens")]
  public int MaxTokens { get; init; } = 1024;

  /// <summary>
  /// Gets the metadata to include with the request.
  /// </summary>
  public Dictionary<string, object>? Metadata { get; init; } = null;

  /// <summary>
  /// Gets the prompt stop sequences.
  /// </summary>
  [JsonProperty("stop_sequences")]
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
  [JsonProperty("tool_choice")]
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
  internal BaseMessageRequest() { }

  /// <summary>
  /// Initializes a new instance of the <see cref="BaseMessageRequest"/> class.
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
  /// <param name="stream">A value indicating whether the message should be streamed.</param>
  /// <param name="stopSequences">The prompt stop sequences.</param>
  /// <param name="systemMessages">The system messages to use for the request.</param>
  /// <exception cref="ArgumentNullException">Thrown when the model or messages is null.</exception>
  /// <exception cref="ArgumentException">Thrown when the messages contain no messages.</exception>
  /// <exception cref="ArgumentException">Thrown when the max tokens is less than one.</exception>
  /// <exception cref="ArgumentException">Thrown when the temperature is less than zero or greater than one.</exception>
  /// <returns>A new instance of the <see cref="BaseMessageRequest"/> class.</returns>
  protected BaseMessageRequest(
    string model,
    List<Message> messages,
    int maxTokens,
    string? system,
    Dictionary<string, object>? metadata,
    decimal temperature,
    int? topK,
    decimal? topP,
    ToolChoice? toolChoice,
    List<Tool>? tools,
    bool stream,
    List<string>? stopSequences,
    List<TextContent>? systemMessages
  )
  {
    ArgumentValidator.ThrowIfNull(model, nameof(model));
    ArgumentValidator.ThrowIfNull(messages, nameof(messages));

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
    SystemMessages = systemMessages;
    Metadata = metadata;
    Temperature = temperature;
    TopK = topK;
    TopP = topP;
    ToolChoice = toolChoice;
    Tools = tools;
    Stream = stream;
    StopSequences = stopSequences ?? [];
  }
}


