namespace AnthropicClient.Utils;

/// <summary>
/// Provides methods to validate arguments.
/// </summary>
static class ArgumentValidator
{
  /// <summary>
  /// Throws an <see cref="ArgumentNullException"/> if the value is null.
  /// </summary>
  /// <typeparam name="T">The type of the value.</typeparam>
  /// <param name="value">The value to check.</param>
  /// <param name="name">The name of the value.</param>
  /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
  /// <returns>Nothing.</returns>
  public static void ThrowIfNull<T>(T? value, string name)
  {
    if (value is null)
    {
      throw new ArgumentNullException(name);
    }
  }

  /// <summary>
  /// Throws an <see cref="ArgumentException"/> if the value is null or empty.
  /// </summary>
  /// <param name="value">The value to check.</param>
  /// <param name="name">The name of the value.</param>
  /// <exception cref="ArgumentException">Thrown when the value is null or empty.</exception>
  /// <returns>Nothing.</returns>
  public static void ThrowIfNullOrWhitespace(string value, string name)
  {
    if (string.IsNullOrWhiteSpace(value))
    {
      throw new ArgumentException("Value cannot be null or empty.", name);
    }
  }
}


