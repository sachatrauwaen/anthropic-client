namespace AnthropicClient.Models;

public class MessageStopEventData : EventData
{
  public MessageStopEventData() : base(EventType.MessageStop)
  {
  }
}