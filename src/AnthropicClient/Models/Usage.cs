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

  /// <summary>
  /// Gets the number of tokens written to the cache when creating a new entry
  /// </summary>
  [JsonPropertyName("cache_creation_input_tokens")]
  public int CacheCreationInputTokens { get; init; }

  /// <summary>
  /// Gets the number of tokens retrieved from the cache for the request.
  /// </summary>
  [JsonPropertyName("cache_read_input_tokens")]
  public int CacheReadInputTokens { get; init; }
}