using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

public class ContentStartEventData : EventData
{
  public int Index { get; init; }

  [JsonPropertyName("content_block")]
  public Content ContentBlock { get; init; } = default!;

  [JsonConstructor]
  internal ContentStartEventData() : base(EventType.ContentBlockStart)
  {
  }

  public ContentStartEventData(int index, Content contentBlock) : base(EventType.ContentBlockStart)
  {
    Index = index;
    ContentBlock = contentBlock;
  }
}