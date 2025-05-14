namespace AnthropicClient.Models;

/// <summary>
/// Represents data for the message_complete event.
/// </summary>
public class MessageCompleteEventData : EventData
{
  /// <summary>
  /// Gets the anthropic headers.
  /// </summary>
  public AnthropicHeaders Headers { get; init; }

  /// <summary>
  /// Gets the response message.
  /// </summary>
  public MessageResponse Message { get; init; }

  /// <summary>
  /// Initializes a new instance of the <see cref="MessageCompleteEventData"/> class.
  /// </summary>
  /// <param name="message">The response message.</param>
  /// <param name="headers">The anthropic headers.</param>
  /// <returns>A new instance of the <see cref="MessageCompleteEventData"/> class.</returns>
  public MessageCompleteEventData(MessageResponse message, AnthropicHeaders headers) : base(EventType.MessageComplete)
  {
    Message = message;
    Headers = headers;
  }
}


