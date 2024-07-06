using System.Text.Json.Serialization;

using AnthropicClient.Utils;

namespace AnthropicClient.Models;

/// <summary>
/// Represents text content that is part of a message.
/// </summary>
public class TextContent : Content
{
  /// <summary>
  /// Gets the text of the content.
  /// </summary>
  public string Text { get; init; } = string.Empty;

  [JsonConstructor]
  internal TextContent() : base(ContentType.Text)
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="TextContent"/> class.
  /// </summary>
  /// <param name="text">The text of the content.</param>
  /// <exception cref="ArgumentNullException">Thrown when the text is null.</exception>
  /// <returns>A new instance of the <see cref="TextContent"/> class.</returns>
  public TextContent(string text) : base(ContentType.Text)
  {
    ArgumentValidator.ThrowIfNull(text, nameof(text));

    Text = text;
  }
}