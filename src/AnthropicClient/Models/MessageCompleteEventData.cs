using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

public class MessageCompleteEventData : EventData
{
  public AnthropicHeaders Headers { get; init; } = new();
  public ChatResponse Message { get; init; } = new();

  public MessageCompleteEventData(ChatResponse message, AnthropicHeaders headers) : base(EventType.MessageComplete)
  {
    Message = message;
    Headers = headers;
  }
}