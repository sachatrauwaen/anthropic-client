using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

/// <summary>
/// Represents the usage of a chat response.
/// </summary>
public class ChatUsage
{
  /// <summary>
  /// Gets the number of input tokens used.
  /// </summary>
  [JsonPropertyName("input_tokens")]
  public int InputTokens { get; set; }

  /// <summary>
  /// Gets the number of output tokens used.
  /// </summary>
  [JsonPropertyName("output_tokens")]
  public int OutputTokens { get; set; }
}