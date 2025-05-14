using Newtonsoft.Json;

namespace AnthropicClient.Models;

/// <summary>
/// Represents a response to a token count request.
/// </summary>
public class TokenCountResponse
{
  /// <summary>
  ///  The number of input tokens counted.
  /// </summary>
  [JsonProperty("input_tokens")]
  public int InputTokens { get; init; }
}


