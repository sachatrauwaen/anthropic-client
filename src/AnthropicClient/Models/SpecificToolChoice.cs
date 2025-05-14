using Newtonsoft.Json;

using AnthropicClient.Utils;

namespace AnthropicClient.Models;

/// <summary>
/// Represents the specific tool choice mode.
/// </summary>
public class SpecificToolChoice : ToolChoice
{
  /// <summary>
  /// Gets the name of the tool.
  /// </summary>
  public string Name { get; init; } = string.Empty;

  [JsonConstructor]
  internal SpecificToolChoice() : base(ToolChoiceType.Tool)
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="SpecificToolChoice"/> class.
  /// </summary>
  /// <param name="name">The name of the tool.</param>
  /// <exception cref="ArgumentNullException">Thrown when the name is null.</exception>
  /// <returns>A new instance of the <see cref="SpecificToolChoice"/> class.</returns>
  public SpecificToolChoice(string name) : base(ToolChoiceType.Tool)
  {
    ArgumentValidator.ThrowIfNull(name, nameof(name));

    Name = name;
  }
}


