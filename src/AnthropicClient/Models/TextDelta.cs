using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

/// <summary>
/// Represents a text delta.
/// </summary>
public class TextDelta : ContentDelta
{
  /// <summary>
  /// Gets the text.
  /// </summary>
  public string Text { get; set; } = string.Empty;

  [JsonConstructor]
  internal TextDelta() : base(ContentDeltaType.TextDelta)
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="TextDelta"/> class.
  /// </summary>
  /// <param name="text">The text.</param>
  /// <returns>A new instance of the <see cref="TextDelta"/> class.</returns>
  public TextDelta(string text) : base(ContentDeltaType.TextDelta)
  {
    Text = text;
  }
}