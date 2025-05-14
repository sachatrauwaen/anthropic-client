using Newtonsoft.Json;

namespace AnthropicClient.Models;

/// <summary>
/// Represents data for an error event.
/// </summary>
public class ErrorEventData : EventData
{
  /// <summary>
  /// Gets the error.
  /// </summary>
  public Error Error { get; init; } = default!;

  [JsonConstructor]
  internal ErrorEventData() : base(EventType.Error)
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="ErrorEventData"/> class.
  /// </summary>
  /// <param name="error">The error.</param>
  /// <returns>A new instance of the <see cref="ErrorEventData"/> class.</returns>
  public ErrorEventData(Error error) : base(EventType.Error)
  {
    Error = error;
  }
}


