using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

/// <summary>
/// Represents data for a content_block_start event.
/// </summary>
public class ContentStartEventData : EventData
{
  /// <summary>
  /// Gets the index of the content block.
  /// </summary>
  public int Index { get; init; }

  /// <summary>
  /// Gets the content block.
  /// </summary>
  [JsonPropertyName("content_block")]
  public Content ContentBlock { get; init; } = default!;

  [JsonConstructor]
  internal ContentStartEventData() : base(EventType.ContentBlockStart)
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="ContentStartEventData"/> class.
  /// </summary>
  /// <param name="index">The index of the content block.</param>
  /// <param name="contentBlock">The content block.</param>
  /// <returns>A new instance of the <see cref="ContentStartEventData"/> class.</returns>
  public ContentStartEventData(int index, Content contentBlock) : base(EventType.ContentBlockStart)
  {
    Index = index;
    ContentBlock = contentBlock;
  }
}