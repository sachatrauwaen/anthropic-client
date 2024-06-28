using System.Text.Json.Serialization;

using AnthropicClient.Utils;

namespace AnthropicClient.Models;

/// <summary>
/// Represents tool result content that is part of a chat message.
/// </summary>
public class ToolResultContent : Content
{
  /// <summary>
  /// Gets the tool use ID of the content.
  /// </summary>
  [JsonPropertyName("tool_use_id")]
  public string ToolUseId { get; set; } = string.Empty;

  /// <summary>
  /// Gets the content of the tool result.
  /// </summary>
  public string Content { get; set; } = string.Empty;

  [JsonConstructor]
  internal ToolResultContent() : base(ContentType.ToolResult) { }

  /// <summary>
  /// Initializes a new instance of the <see cref="ToolResultContent"/> class.
  /// </summary>
  /// <param name="toolUseId">The tool use ID of the content.</param>
  /// <param name="content">The content of the tool result.</param>
  /// <exception cref="ArgumentNullException">Thrown when the tool use ID or content is null.</exception>
  /// <returns>A new instance of the <see cref="ToolResultContent"/> class.</returns>
  public ToolResultContent(string toolUseId, string content) : base(ContentType.ToolResult)
  {
    ArgumentValidator.ThrowIfNull(toolUseId, nameof(toolUseId));
    ArgumentValidator.ThrowIfNull(content, nameof(content));

    ToolUseId = toolUseId;
    Content = content;
  }
}