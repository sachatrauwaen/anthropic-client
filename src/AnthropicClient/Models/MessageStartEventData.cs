using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

/// <summary>
/// Represents data for a message_start event.
/// </summary>
public class MessageStartEventData : EventData
{
  /// <summary>
  /// Gets the message.
  /// </summary>
  public MessageResponse Message { get; init; } = new();

  [JsonConstructor]
  internal MessageStartEventData() : base(EventType.MessageStart)
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="MessageStartEventData"/> class.
  /// </summary>
  /// <param name="message">The message.</param>
  /// <returns>A new instance of the <see cref="MessageStartEventData"/> class.</returns>
  public MessageStartEventData(MessageResponse message) : base(EventType.MessageStart)
  {
    Message = message;
  }
}