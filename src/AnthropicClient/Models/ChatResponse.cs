using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

/// <summary>
/// Represents a chat response.
/// </summary>
public class ChatResponse
{
  /// <summary>
  /// Gets the ID of the chat response.
  /// </summary>
  public string Id { get; set; } = string.Empty;

  /// <summary>
  /// Gets the model used for the chat response.
  /// </summary>
  public string Model { get; set; } = string.Empty;

  /// <summary>
  /// Gets the role of the chat response.
  /// </summary>
  public string Role { get; set; } = string.Empty;

  /// <summary>
  /// Gets the stop reason of the chat response.
  /// </summary>
  [JsonPropertyName("stop_reason")]
  public string StopReason { get; set; } = string.Empty;

  /// <summary>
  /// Gets the stop sequence of the chat response.
  /// </summary>
  [JsonPropertyName("stop_sequence")]
  public string StopSequence { get; set; } = string.Empty;

  /// <summary>
  /// Gets the type of the chat response.
  /// </summary>
  public string Type { get; set; } = string.Empty;

  /// <summary>
  /// Gets the usage of the chat response.
  /// </summary>
  public ChatUsage Usage { get; set; } = new();

  /// <summary>
  /// Gets the contents of the chat response.
  /// </summary>
  public List<Content> Content { get; set; } = [];
}