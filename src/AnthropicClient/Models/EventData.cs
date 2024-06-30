namespace AnthropicClient.Models;

/// <summary>
/// Represents data for an event.
/// </summary>
public abstract class EventData
{
  /// <summary>
  /// Gets the type of the event.
  /// </summary>
  public string Type { get; init; }

  /// <summary>
  /// Initializes a new instance of the <see cref="EventData"/> class.
  /// </summary>
  /// <param name="type">The type of the event.</param>
  /// <returns>A new instance of the <see cref="EventData"/> class.</returns>
  protected EventData(string type)
  {
    Type = type;
  }
}