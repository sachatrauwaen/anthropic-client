using AnthropicClient.Utils;

namespace AnthropicClient.Models;

/// <summary>
/// Represents the cache control to be used for content.
/// </summary>
public abstract class CacheControl
{
  /// <summary>
  /// Gets the type of the cache control.
  /// </summary>
  public string Type { get; init; } = string.Empty;

  /// <summary>
  /// Initializes a new instance of the <see cref="CacheControl"/> class.
  /// </summary>
  /// <param name="type">The type of the cache control.</param>
  /// <returns>A new instance of the <see cref="CacheControl"/> class.</returns>
  /// <exception cref="ArgumentException">Thrown when the type is null or whitespace.</exception>
  protected CacheControl(string type)
  {
    ArgumentValidator.ThrowIfNullOrWhitespace(type, nameof(type));

    Type = type;
  }
}