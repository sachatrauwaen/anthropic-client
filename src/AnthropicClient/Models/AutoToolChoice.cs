namespace AnthropicClient.Models;

/// <summary>
/// Represents the auto tool choice mode.
/// </summary>
public class AutoToolChoice : ToolChoice
{
  /// <summary>
  /// Initializes a new instance of the <see cref="AutoToolChoice"/> class.
  /// </summary>
  /// <returns>A new instance of the <see cref="AutoToolChoice"/> class.</returns>
  public AutoToolChoice() : base(ToolChoiceType.Auto) { }
}