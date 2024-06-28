using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

/// <summary>
/// Represents a permission error.
/// </summary>
public class PermissionError : Error
{
  [JsonConstructor]
  internal PermissionError() : base(ErrorType.PermissionError)
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="PermissionError"/> class.
  /// </summary>
  /// <param name="message">The message of the error.</param>
  /// <returns>A new instance of the <see cref="PermissionError"/> class.</returns>
  public PermissionError(string message) : base(ErrorType.PermissionError)
  {
    Message = message;
  }
}