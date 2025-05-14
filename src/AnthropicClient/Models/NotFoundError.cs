using Newtonsoft.Json;

namespace AnthropicClient.Models;

/// <summary>
/// Represents a not_found error.
/// </summary>
public class NotFoundError : Error
{
  [JsonConstructor]
  internal NotFoundError() : base(ErrorType.NotFoundError)
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="NotFoundError"/> class.
  /// </summary>
  /// <param name="message">The message of the error.</param>
  /// <returns>A new instance of the <see cref="NotFoundError"/> class.</returns>
  public NotFoundError(string message) : base(ErrorType.NotFoundError)
  {
    Message = message;
  }
}


