using Newtonsoft.Json;

namespace AnthropicClient.Models;

/// <summary>
/// Represents an api_error response from the Anthropic API.
/// </summary>
public class ApiError : Error
{
  [JsonConstructor]
  internal ApiError() : base(ErrorType.ApiError)
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="ApiError"/> class.
  /// </summary>
  /// <param name="message">The error message.</param>
  /// <returns>A new instance of the <see cref="ApiError"/> class.</returns>
  public ApiError(string message) : base(ErrorType.ApiError)
  {
    Message = message;
  }
}


