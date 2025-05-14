namespace AnthropicClient.Models;

/// <summary>
/// Represents data for a message_stop event.
/// </summary>
public class MessageStopEventData : EventData
{
  /// <summary>
  /// Initializes a new instance of the <see cref="MessageStopEventData"/> class.
  /// </summary>
  /// <returns>A new instance of the <see cref="MessageStopEventData"/> class.</returns>
  public MessageStopEventData() : base(EventType.MessageStop)
  {
  }
}


