using Newtonsoft.Json;

using AnthropicClient.Utils;

namespace AnthropicClient.Models;

/// <summary>
/// Represents an input schema.
/// </summary>
public class InputSchema
{
  /// <summary>
  /// Gets the type of the input schema.
  /// </summary>
  public string Type { get; init; } = "object";

  /// <summary>
  /// Gets the properties of the input schema.
  /// </summary>
  public Dictionary<string, InputProperty> Properties { get; init; } = [];

  /// <summary>
  /// Gets the required properties of the input schema.
  /// </summary>
  public List<string> Required { get; init; } = [];

  [JsonConstructor]
  internal InputSchema()
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="InputSchema"/> class.
  /// </summary>
  /// <param name="properties">The properties of the input schema.</param>
  /// <param name="required">The required properties of the input schema.</param>
  /// <exception cref="ArgumentNullException">Thrown when the properties or required is null.</exception>
  /// <exception cref="ArgumentException">Thrown when the required properties are not present in the properties dictionary.</exception>
  /// <returns>A new instance of the <see cref="InputSchema"/> class.</returns>
  public InputSchema(Dictionary<string, InputProperty> properties, List<string> required)
  {
    ArgumentValidator.ThrowIfNull(properties, nameof(properties));
    ArgumentValidator.ThrowIfNull(required, nameof(required));

    if (required.Any(r => properties.ContainsKey(r) is false))
    {
      throw new ArgumentException("Required properties must be present in the properties dictionary.");
    }

    Properties = properties;
    Required = required;
  }
}


