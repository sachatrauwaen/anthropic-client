using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

public class ErrorEventData : EventData
{
  public Error Error { get; init; } = default!;

  [JsonConstructor]
  internal ErrorEventData() : base(EventType.Error)
  {
  }

  public ErrorEventData(Error error) : base(EventType.Error)
  {
    Error = error;
  }
}