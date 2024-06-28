namespace AnthropicClient.Models;

/// <summary>
/// Represents a message request.
/// </summary>
public abstract class MessageRequest
{
  /// <summary>
  /// Gets a value indicating whether the message should be streamed.
  /// </summary>
  public bool Stream { get; init; }

  /// <summary>
  /// Initializes a new instance of the <see cref="MessageRequest"/> class.
  /// </summary>
  /// <param name="stream">A value indicating whether the message should be streamed.</param>
  /// <returns>A new instance of the <see cref="MessageRequest"/> class.</returns>
  public MessageRequest(bool stream = false)
  {
    Stream = stream;
  }
}