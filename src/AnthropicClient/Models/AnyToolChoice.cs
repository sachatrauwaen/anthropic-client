namespace AnthropicClient.Models;

/// <summary>
/// Represents the any tool choice mode.
/// </summary>
public class AnyToolChoice : ToolChoice
{
  /// <summary>
  /// Initializes a new instance of the <see cref="AnyToolChoice"/> class.
  /// </summary>
  /// <returns>A new instance of the <see cref="AnyToolChoice"/> class.</returns>
  public AnyToolChoice() : base(ToolChoiceType.Any) { }
}


