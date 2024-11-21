using System.Text.Json.Serialization;

using AnthropicClient.Utils;

namespace AnthropicClient.Models;

/// <summary>
/// Represents a document source.
/// </summary>
public class DocumentSource
{
  /// <summary>
  /// Gets the media type of the document.
  /// </summary>
  [JsonPropertyName("media_type")]
  public string MediaType { get; init; } = string.Empty;

  /// <summary>
  /// Gets the data of the document.
  /// </summary>
  public string Data { get; init; } = string.Empty;

  /// <summary>
  /// Gets the type of encoding of the document data.
  /// </summary>
  public string Type { get; init; } = "base64";

  [JsonConstructor]
  internal DocumentSource()
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="DocumentSource"/> class.
  /// </summary>
  /// <param name="mediaType">The media type of the document.</param>
  /// <param name="data">The data of the document.</param>
  /// <exception cref="ArgumentException">Thrown when the media type is invalid.</exception>
  /// <exception cref="ArgumentNullException">Thrown when the media type or data is null.</exception>
  /// <returns>A new instance of the <see cref="DocumentSource"/> class.</returns>
  public DocumentSource(string mediaType, string data)
  {
    ArgumentValidator.ThrowIfNull(mediaType, nameof(mediaType));
    ArgumentValidator.ThrowIfNull(data, nameof(data));

    MediaType = mediaType;
    Data = data;
  }
}