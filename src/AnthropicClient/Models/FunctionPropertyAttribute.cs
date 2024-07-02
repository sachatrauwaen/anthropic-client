namespace AnthropicClient.Models;

/// <summary>
/// Attribute to describe a property of a type that is used as a function parameter.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class FunctionPropertyAttribute : Attribute
{
  /// <summary>
  /// Description of the property.
  /// </summary>
  public string Description { get; } = string.Empty;

  /// <summary>
  /// Whether the property is required.
  /// </summary>
  public bool Required { get; } = false;

  /// <summary>
  /// Default value of the property.
  /// </summary>
  public object? DefaultValue { get; } = null;

  /// <summary>
  /// Possible values of the property.
  /// </summary>
  public object[]? PossibleValues { get; } = null;

  /// <summary>
  /// Initializes a new instance of the <see cref="FunctionPropertyAttribute"/> class.
  /// </summary>
  /// <param name="description">Description of the property.</param>
  /// <param name="required">Whether the property is required.</param>
  /// <param name="defaultValue">Default value of the property.</param>
  /// <param name="possibleValues">Possible values of the property.</param>
  /// <returns>A new instance of the <see cref="FunctionPropertyAttribute"/> class.</returns>
  public FunctionPropertyAttribute(
    string description,
    bool required = false,
    object? defaultValue = null,
    object[]? possibleValues = null
  )
  {
    Description = description;
    Required = required;
    DefaultValue = defaultValue;
    PossibleValues = possibleValues;
  }
}