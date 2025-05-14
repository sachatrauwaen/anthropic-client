using Newtonsoft.Json;

using AnthropicClient.Utils;

namespace AnthropicClient.Models;

/// <summary>
/// Represents an image source.
/// </summary>
public class ImageSource
{
  /// <summary>
  /// Gets the media type of the image.
  /// </summary>
  [JsonProperty("media_type")]
  public string MediaType { get; init; } = string.Empty;

  /// <summary>
  /// Gets the data of the image.
  /// </summary>
  public string Data { get; init; } = string.Empty;

  /// <summary>
  /// Gets the type of encoding of the image data.
  /// </summary>
  public string Type { get; init; } = "base64";

  [JsonConstructor]
  internal ImageSource()
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="ImageSource"/> class.
  /// </summary>
  /// <param name="mediaType">The media type of the image.</param>
  /// <param name="data">The data of the image.</param>
  /// <exception cref="ArgumentException">Thrown when the media type is invalid.</exception>
  /// <exception cref="ArgumentNullException">Thrown when the media type or data is null.</exception>
  /// <returns>A new instance of the <see cref="ImageSource"/> class.</returns>
  public ImageSource(string mediaType, string data)
  {
    ArgumentValidator.ThrowIfNull(mediaType, nameof(mediaType));

    if (ImageType.IsValidImageType(mediaType) is false)
    {
      throw new ArgumentException($"Invalid media type: {mediaType}");
    }

    ArgumentValidator.ThrowIfNull(data, nameof(data));

    MediaType = mediaType;
    Data = data;
  }
}


