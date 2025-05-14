using Newtonsoft.Json;

using AnthropicClient.Utils;

namespace AnthropicClient.Models;

/// <summary>
/// Represents an input property.
/// </summary>
public class InputProperty
{
  /// <summary>
  /// Gets the type of the input property.
  /// </summary>
  public string Type { get; init; } = string.Empty;

  /// <summary>
  /// Gets the description of the input property.
  /// </summary>
  public string Description { get; init; } = string.Empty;

  [JsonConstructor]
  internal InputProperty()
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="InputProperty"/> class.
  /// </summary>
  /// <param name="type">The type of the input property.</param>
  /// <param name="description">The description of the input property.</param>
  /// <exception cref="ArgumentNullException">Thrown when the type or description is null.</exception>
  /// <returns>A new instance of the <see cref="InputProperty"/> class.</returns>
  public InputProperty(string type, string description)
  {
    ArgumentValidator.ThrowIfNull(type, nameof(type));
    ArgumentValidator.ThrowIfNull(description, nameof(description));

    Type = type;
    Description = description;
  }
}


