using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

/// <summary>
/// Represents data for a content_block_delta event.
/// </summary>
public class ContentDeltaEventData : EventData
{
  /// <summary>
  /// Gets the index of the content block.
  /// </summary>
  public int Index { get; init; }

  /// <summary>
  /// Gets the content delta.
  /// </summary>
  public ContentDelta Delta { get; init; } = default!;

  [JsonConstructor]
  internal ContentDeltaEventData() : base(EventType.ContentBlockDelta)
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="ContentDeltaEventData"/> class.
  /// </summary>
  /// <param name="index">The index of the content block.</param>
  /// <param name="delta">The content delta.</param>
  /// <returns>A new instance of the <see cref="ContentDeltaEventData"/> class.</returns>
  public ContentDeltaEventData(int index, ContentDelta delta) : base(EventType.ContentBlockDelta)
  {
    Index = index;
    Delta = delta;
  }
}