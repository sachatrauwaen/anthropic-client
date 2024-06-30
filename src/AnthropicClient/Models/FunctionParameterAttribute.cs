using AnthropicClient.Utils;

namespace AnthropicClient.Models;

/// <summary>
/// Attribute to describe a function parameter.
/// </summary>
[AttributeUsage(AttributeTargets.Parameter)]
public sealed class FunctionParameterAttribute : Attribute
{ 
  /// <summary>
  /// The name of the parameter.
  /// </summary>
  public string Name { get; }

  /// <summary>
  /// The description of the parameter.
  /// </summary>
  public string Description { get; }

  /// <summary>
  /// Whether the parameter is required.
  /// </summary>
  public bool Required { get; }

  /// <summary>
  /// Initializes a new instance of the <see cref="FunctionParameterAttribute"/> class.
  /// </summary>
  /// <param name="name">The name of the parameter.</param>
  /// <param name="description">The description of the parameter.</param>
  /// <param name="required">Whether the parameter is required.</param>
  /// <returns>A new instance of the <see cref="FunctionParameterAttribute"/> class.</returns>
  public FunctionParameterAttribute(string description, string name = "", bool required = false)
  {
    ArgumentValidator.ThrowIfNullOrWhitespace(description, nameof(description));
    ArgumentValidator.ThrowIfNull(name, nameof(name));

    Name = name;
    Description = description;
    Required = required;
  }
}