using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

public class ContentStopEventData : EventData
{
  public int Index { get; init; }

  [JsonConstructor]
  internal ContentStopEventData() : base(EventType.ContentBlockStop)
  {
  }

  public ContentStopEventData(int index) : base(EventType.ContentBlockStop)
  {
    Index = index;
  }
}