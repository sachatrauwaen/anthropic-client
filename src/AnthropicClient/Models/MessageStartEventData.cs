using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

public class MessageStartEventData : EventData
{
  public ChatResponse Message { get; init; } = new();

  [JsonConstructor]
  internal MessageStartEventData() : base(EventType.MessageStart)
  {
  }

  public MessageStartEventData(ChatResponse message) : base(EventType.MessageStart)
  {
    Message = message;
  }
}