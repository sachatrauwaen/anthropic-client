namespace AnthropicClient.Models;

/// <summary>
/// Represents tool use content that is part of a message.
/// </summary>
public class ToolUseContent : Content
{
  /// <summary>
  /// Gets the ID of the tool use.
  /// </summary>
  public string Id { get; init; } = string.Empty;

  /// <summary>
  /// Gets the name of the tool.
  /// </summary>
  public string Name { get; init; } = string.Empty;

  /// <summary>
  /// Gets the input of the tool.
  /// </summary>
  public Dictionary<string, object?> Input { get; init; } = [];

  /// <summary>
  /// Initializes a new instance of the <see cref="ToolUseContent"/> class.
  /// </summary>
  /// <returns>A new instance of the <see cref="ToolUseContent"/> class.</returns>
  public ToolUseContent() : base(ContentType.ToolUse)
  {
  }
}