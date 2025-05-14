using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AnthropicClient.Models;

/// <summary>
/// The type of content.
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
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

  /// <summary>
  /// Represents the document content type.
  /// </summary>
  public const string Document = "document";
}


