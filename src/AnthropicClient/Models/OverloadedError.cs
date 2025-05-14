using Newtonsoft.Json;

namespace AnthropicClient.Models;

/// <summary>
/// Represents an overloaded error.
/// </summary>
public class OverloadedError : Error
{
  [JsonConstructor]
  internal OverloadedError() : base(ErrorType.OverloadedError)
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="OverloadedError"/> class.
  /// </summary>
  /// <param name="message">The message of the error.</param>
  /// <returns>A new instance of the <see cref="OverloadedError"/> class.</returns>
  public OverloadedError(string message) : base(ErrorType.OverloadedError)
  {
    Message = message;
  }
}


