namespace AnthropicClient.Models;

public class PingEventData : EventData
{
  public PingEventData() : base(EventType.Ping)
  {
  }
}