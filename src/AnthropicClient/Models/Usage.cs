using Newtonsoft.Json;

namespace AnthropicClient.Models;

/// <summary>
/// Represents the usage of a response.
/// </summary>
public class Usage
{
  /// <summary>
  /// Gets the number of input tokens used.
  /// </summary>
  [JsonProperty("input_tokens")]
  public int InputTokens { get; init; }

  /// <summary>
  /// Gets the number of output tokens used.
  /// </summary>
  [JsonProperty("output_tokens")]
  public int OutputTokens { get; init; }

  /// <summary>
  /// Gets the number of tokens written to the cache when creating a new entry
  /// </summary>
  [JsonProperty("cache_creation_input_tokens")]
  public int CacheCreationInputTokens { get; init; }

  /// <summary>
  /// Gets the number of tokens retrieved from the cache for the request.
  /// </summary>
  [JsonProperty("cache_read_input_tokens")]
  public int CacheReadInputTokens { get; init; }
}


