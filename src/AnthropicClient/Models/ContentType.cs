namespace AnthropicClient.Models;

/// <summary>
/// Represents the content type.
/// </summary>
public static class ContentType
{
  /// <summary>
  /// Represents the text content type.
  /// </summary>
  public const string Text = "text";

  /// <summary>
  /// Represents the image content type.
  /// </summary>
  public const string Image = "image";

  /// <summary>
  /// Represents the tool use content type.
  /// </summary>
  public const string ToolUse = "tool_use";

  /// <summary>
  /// Represents the tool result content type.
  /// </summary>
  public const string ToolResult = "tool_result";
}