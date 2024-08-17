using System.Text.Json.Serialization;

using AnthropicClient.Utils;

namespace AnthropicClient.Models;

/// <summary>
/// Represents tool result content that is part of a message.
/// </summary>
public class ToolResultContent : Content
{
  /// <summary>
  /// Gets the tool use ID of the content.
  /// </summary>
  [JsonPropertyName("tool_use_id")]
  public string ToolUseId { get; init; } = string.Empty;

  /// <summary>
  /// Gets the content of the tool result.
  /// </summary>
  public string Content { get; init; } = string.Empty;

  [JsonConstructor]
  internal ToolResultContent() : base(ContentType.ToolResult) { }

  private void Validate(string toolUseId, string content)
  {
    ArgumentValidator.ThrowIfNull(toolUseId, nameof(toolUseId));
    ArgumentValidator.ThrowIfNull(content, nameof(content));
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="ToolResultContent"/> class.
  /// </summary>
  /// <param name="toolUseId">The tool use ID of the content.</param>
  /// <param name="content">The content of the tool result.</param>
  /// <exception cref="ArgumentNullException">Thrown when the tool use ID or content is null.</exception>
  /// <returns>A new instance of the <see cref="ToolResultContent"/> class.</returns>
  public ToolResultContent(string toolUseId, string content) : base(ContentType.ToolResult)
  {
    Validate(toolUseId, content);

    ToolUseId = toolUseId;
    Content = content;
  }

    /// <summary>
  /// Initializes a new instance of the <see cref="ToolResultContent"/> class.
  /// </summary>
  /// <param name="toolUseId">The tool use ID of the content.</param>
  /// <param name="content">The content of the tool result.</param>
  /// <param name="cacheControl">The cache control to be used for the content.</param>
  /// <exception cref="ArgumentNullException">Thrown when the tool use ID or content is null.</exception>
  /// <returns>A new instance of the <see cref="ToolResultContent"/> class.</returns>
  public ToolResultContent(string toolUseId, string content, CacheControl cacheControl) : base(ContentType.ToolResult)
  {
    Validate(toolUseId, content);

    ToolUseId = toolUseId;
    Content = content;
    CacheControl = cacheControl;
  }
}