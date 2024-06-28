using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

public class ContentDeltaEventData : EventData
{
  public int Index { get; init; }
  public ContentDelta Delta { get; init; } = default!;

  [JsonConstructor]
  internal ContentDeltaEventData() : base(EventType.ContentBlockDelta)
  {
  }

  public ContentDeltaEventData(int index, ContentDelta delta) : base(EventType.ContentBlockDelta)
  {
    Index = index;
    Delta = delta;
  }
}