namespace AnthropicClient.Models;

/// <summary>
/// Represents the cache control to be used for content.
/// </summary>
public class EphemeralCacheControl : CacheControl
{
  /// <summary>
  /// Initializes a new instance of the <see cref="EphemeralCacheControl"/> class.
  /// </summary>
  /// <returns>A new instance of the <see cref="EphemeralCacheControl"/> class.</returns>
  public EphemeralCacheControl() : base(CacheControlType.Ephemeral) { }
}