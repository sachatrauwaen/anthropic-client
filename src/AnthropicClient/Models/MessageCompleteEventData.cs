namespace AnthropicClient.Models;

public class MessageCompleteEventData : EventData
{
  public AnthropicHeaders Headers { get; init; }
  public ChatResponse Message { get; init; }

  public MessageCompleteEventData(ChatResponse message, AnthropicHeaders headers) : base(EventType.MessageComplete)
  {
    Message = message;
    Headers = headers;
  }
}