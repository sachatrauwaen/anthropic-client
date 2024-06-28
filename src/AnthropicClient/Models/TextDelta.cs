using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

public class TextDelta : ContentDelta
{
  public string Text { get; set; } = string.Empty;

  [JsonConstructor]
  internal TextDelta() : base(ContentDeltaType.TextDelta)
  {
  }

  public TextDelta(string text) : base(ContentDeltaType.TextDelta)
  {
    Text = text;
  }
}