using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

public class MessageStartEventData : EventData
{
  public ChatMessage Message { get; init; } = new();

  [JsonConstructor]
  internal MessageStartEventData() : base(EventType.MessageStart)
  {
  }

  public MessageStartEventData(ChatMessage message) : base(EventType.MessageStart)
  {
    Message = message;
  }
}