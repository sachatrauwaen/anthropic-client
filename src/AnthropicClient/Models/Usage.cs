using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

/// <summary>
/// Represents the usage of a response.
/// </summary>
public class Usage
{
  /// <summary>
  /// Gets the number of input tokens used.
  /// </summary>
  [JsonPropertyName("input_tokens")]
  public int InputTokens { get; init; }

  /// <summary>
  /// Gets the number of output tokens used.
  /// </summary>
  [JsonPropertyName("output_tokens")]
  public int OutputTokens { get; init; }
}