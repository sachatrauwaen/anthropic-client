namespace AnthropicClient.Models;

/// <summary>
/// Represents a content delta.
/// </summary>
public abstract class ContentDelta
{
  /// <summary>
  /// Gets the type of the content delta.
  /// </summary>
  public string Type { get; init; }

  /// <summary>
  /// Initializes a new instance of the <see cref="ContentDelta"/> class.
  /// </summary>
  /// <param name="type">The type of the content delta.</param>
  /// <returns>A new instance of the <see cref="ContentDelta"/> class.</returns>
  protected ContentDelta(string type)
  {
    Type = type;
  }
}