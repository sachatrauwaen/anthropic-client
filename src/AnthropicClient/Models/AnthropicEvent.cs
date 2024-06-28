using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

public class AnthropicEvent
{
  public string Type { get; init; } = string.Empty;
  public EventData Data { get; init; } = default!;

  [JsonConstructor]
  internal AnthropicEvent()
  {
  }

  public AnthropicEvent(string type, EventData data)
  {
    Type = type;
    Data = data;
  }
}