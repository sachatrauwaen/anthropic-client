namespace AnthropicClient.Models;

/// <summary>
/// Represents a tool choice mode.
/// </summary>
public abstract class ToolChoice
{
  /// <summary>
  /// Gets the type of the tool choice.
  /// </summary>
  public string Type { get; init; } = string.Empty;

  /// <summary>
  /// Initializes a new instance of the <see cref="ToolChoice"/> class.
  /// </summary>
  /// <param name="type">The type of the tool choice.</param>
  /// <returns>A new instance of the <see cref="ToolChoice"/> class.</returns>
  protected ToolChoice(string type)
  {
    Type = type;
  }
}


