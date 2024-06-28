using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

/// <summary>
/// Represents an invalid_request error.
/// </summary>
public class InvalidRequestError : Error
{
  [JsonConstructor]
  internal InvalidRequestError() : base(ErrorType.InvalidRequestError)
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="InvalidRequestError"/> class.
  /// </summary>
  /// <param name="message">The message of the error.</param>
  /// <returns>A new instance of the <see cref="InvalidRequestError"/> class.</returns>
  public InvalidRequestError(string message) : base(ErrorType.InvalidRequestError)
  {
    Message = message;
  }
}