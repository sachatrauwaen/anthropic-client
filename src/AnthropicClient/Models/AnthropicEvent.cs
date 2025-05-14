using Newtonsoft.Json;

namespace AnthropicClient.Models;

/// <summary>
/// Represents an event from the Anthropic API.
/// </summary>
public class AnthropicEvent
{
  /// <summary>
  /// The type of the event.
  /// </summary>
  public string Type { get; init; } = string.Empty;

  /// <summary>
  /// The data associated with the event.
  /// </summary>
  public EventData Data { get; init; } = default!;

  [JsonConstructor]
  internal AnthropicEvent()
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="AnthropicEvent"/> class.
  /// </summary>
  /// <param name="type">The type of the event.</param>
  /// <param name="data">The data associated with the event.</param>
  /// <returns>A new instance of the <see cref="AnthropicEvent"/> class.</returns>
  public AnthropicEvent(string type, EventData data)
  {
    Type = type;
    Data = data;
  }
}


