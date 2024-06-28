namespace AnthropicClient.Models;

/// <summary>
/// Represents an error.
/// </summary>
public abstract class Error
{
  /// <summary>
  /// Gets the type of the error.
  /// </summary>
  public string Type { get; init; } = string.Empty;

  /// <summary>
  /// Gets the message of the error.
  /// </summary>
  public string Message { get; init; } = string.Empty;

  /// <summary>
  /// Initializes a new instance of the <see cref="Error"/> class.
  /// </summary>
  /// <param name="type">The type of the error.</param>
  /// <returns>A new instance of the <see cref="Error"/> class.</returns>
  protected Error(string type)
  {
    Type = type;
  }
}