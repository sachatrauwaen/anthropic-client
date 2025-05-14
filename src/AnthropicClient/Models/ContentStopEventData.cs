using Newtonsoft.Json;

namespace AnthropicClient.Models;

/// <summary>
/// Represents data for a content_block_stop event.
/// </summary>
public class ContentStopEventData : EventData
{
  /// <summary>
  /// Gets the index of the content block.
  /// </summary>
  public int Index { get; init; }

  [JsonConstructor]
  internal ContentStopEventData() : base(EventType.ContentBlockStop)
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="ContentStopEventData"/> class.
  /// </summary>
  /// <param name="index">The index of the content block.</param>
  /// <returns>A new instance of the <see cref="ContentStopEventData"/> class.</returns>
  public ContentStopEventData(int index) : base(EventType.ContentBlockStop)
  {
    Index = index;
  }
}


