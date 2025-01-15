using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

/// <summary>
/// Represents a message batch result item.
/// </summary>
public class MessageBatchResultItem
{
  /// <summary>
  /// Gets the custom ID of the message batch result item.
  /// </summary>
  [JsonPropertyName("custom_id")]
  public string CustomId { get; init; } = string.Empty;

  /// <summary>
  /// Gets the result of the message batch result item.
  /// </summary>
  public MessageBatchResult Result { get; init; } = default!;
}