using System.Text.Json.Serialization;

using AnthropicClient.Utils;

namespace AnthropicClient.Models;

/// <summary>
/// Represents image content that is part of a chat message.
/// </summary>
public class ImageContent : Content
{
  /// <summary>
  /// Gets the source of the image.
  /// </summary>
  public ImageSource Source { get; init; } = new();

  [JsonConstructor]
  internal ImageContent()
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="ImageContent"/> class.
  /// </summary>
  /// <param name="mediaType">The media type of the image.</param>
  /// <param name="data">The data of the image.</param>
  /// <exception cref="ArgumentNullException">Thrown when the media type or data is null.</exception>
  /// <returns>A new instance of the <see cref="ImageContent"/> class.</returns>
  public ImageContent(string mediaType, string data) : base(ContentType.Image)
  {
    ArgumentValidator.ThrowIfNull(mediaType, nameof(mediaType));
    ArgumentValidator.ThrowIfNull(data, nameof(data));

    Source = new(mediaType, data);
  }
}