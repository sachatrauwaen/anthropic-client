using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

/// <summary>
/// Represents an error response from the Anthropic API.
/// </summary>
public class AnthropicError
{
  /// <summary>
  /// The type of the error.
  /// </summary>
  public string Type { get; init; } = "error";

  /// <summary>
  /// The error object.
  /// </summary>
  public Error Error { get; init; } = new ApiError();

  [JsonConstructor]
  internal AnthropicError()
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="AnthropicError"/> class.
  /// </summary>
  /// <param name="error">The error as an <see cref="Error"/> object.</param>
  /// <returns>A new instance of the <see cref="AnthropicError"/> class.</returns>
  public AnthropicError(Error error)
  {
    Error = error;
  }
}