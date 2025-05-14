using Newtonsoft.Json;

namespace AnthropicClient.Models;

/// <summary>
/// Represents an authentication_error response from the Anthropic API.
/// </summary>
public class AuthenticationError : Error
{
  [JsonConstructor]
  internal AuthenticationError() : base(ErrorType.AuthenticationError)
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="AuthenticationError"/> class.
  /// </summary>
  /// <param name="message">The error message.</param>
  /// <returns>A new instance of the <see cref="AuthenticationError"/> class.</returns>
  public AuthenticationError(string message) : base(ErrorType.AuthenticationError)
  {
    Message = message;
  }
}


