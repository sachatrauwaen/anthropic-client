using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

/// <summary>
/// Represents a response.
/// </summary>
public class MessageResponse
{
  /// <summary>
  /// Gets the ID of the response.
  /// </summary>
  public string Id { get; init; } = string.Empty;

  /// <summary>
  /// Gets the model used for the response.
  /// </summary>
  public string Model { get; init; } = string.Empty;

  /// <summary>
  /// Gets the role of the response.
  /// </summary>
  public string Role { get; init; } = string.Empty;

  /// <summary>
  /// Gets the stop reason of the response.
  /// </summary>
  [JsonPropertyName("stop_reason")]
  public string? StopReason { get; init; }

  /// <summary>
  /// Gets the stop sequence of the response.
  /// </summary>
  [JsonPropertyName("stop_sequence")]
  public string? StopSequence { get; init; }

  /// <summary>
  /// Gets the type of the response.
  /// </summary>
  public string Type { get; init; } = string.Empty;

  /// <summary>
  /// Gets the usage of the response.
  /// </summary>
  public Usage Usage { get; init; } = new();

  /// <summary>
  /// Gets the contents of the response.
  /// </summary>
  public List<Content> Content { get; init; } = [];

  /// <summary>
  /// Gets the tool call of the response. If the response does not contain a tool call, this property is null.
  /// </summary>
  [JsonIgnore]
  public ToolCall? ToolCall { get; set; } = null;
}