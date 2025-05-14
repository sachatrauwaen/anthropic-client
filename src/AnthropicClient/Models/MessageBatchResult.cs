using AnthropicClient.Utils;

namespace AnthropicClient.Models;

/// <summary>
/// Represents a message batch result.
/// </summary>
public abstract class MessageBatchResult
{
  /// <summary>
  /// Gets the type of the message batch result.
  /// </summary>
  public string Type { get; init; } = string.Empty;

  /// <summary>
  /// Initializes a new instance of the <see cref="MessageBatchResult"/> class.
  /// </summary>
  /// <param name="type">The type of the message batch result.</param>
  /// <returns>An instance of the <see cref="MessageBatchResult"/> class.</returns>
  public MessageBatchResult(string type)
  {
    ArgumentValidator.ThrowIfNullOrWhitespace(type, nameof(type));

    Type = type;
  }
}


