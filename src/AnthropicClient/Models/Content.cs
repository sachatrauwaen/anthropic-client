using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

/// <summary>
/// Represents part of the content of a message.
/// </summary>
public abstract class Content
{
  /// <summary>
  /// Gets the type of the content.
  /// </summary>
  public string Type { get; init; } = string.Empty;

  /// <summary>
  /// Gets the cache control to be used for the content.
  /// </summary>
  [JsonPropertyName("cache_control")]
  public CacheControl? CacheControl { get; set; }

  [JsonConstructor]
  internal Content()
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="Content"/> class.
  /// </summary>
  /// <param name="type">The type of the content.</param>
  /// <returns>A new instance of the <see cref="Content"/> class.</returns>
  protected Content(string type)
  {
    Type = type;
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="Content"/> class.
  /// </summary>
  /// <param name="type">The type of the content.</param>
  /// <param name="cacheControl">The cache control to be used for the content.</param>
  /// <returns>A new instance of the <see cref="Content"/> class.</returns>
  protected Content(string type, CacheControl cacheControl)
  {
    Type = type;
    CacheControl = cacheControl;
  }
}