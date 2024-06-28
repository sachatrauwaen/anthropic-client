using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

/// <summary>
/// Represents a rate_limit error.
/// </summary>
public class RateLimitError : Error
{
  [JsonConstructor]
  internal RateLimitError() : base(ErrorType.RateLimitError)
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="RateLimitError"/> class.
  /// </summary>
  /// <param name="message">The message of the error.</param>
  /// <returns>A new instance of the <see cref="RateLimitError"/> class.</returns>
  public RateLimitError(string message) : base(ErrorType.RateLimitError)
  {
    Message = message;
  }
}