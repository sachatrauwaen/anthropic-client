namespace AnthropicClient.Models;

/// <summary>
/// Represents data for a ping event.
/// </summary>
public class PingEventData : EventData
{
  /// <summary>
  /// Initializes a new instance of the <see cref="PingEventData"/> class.
  /// </summary>
  /// <returns>A new instance of the <see cref="PingEventData"/> class.</returns>
  public PingEventData() : base(EventType.Ping)
  {
  }
}


