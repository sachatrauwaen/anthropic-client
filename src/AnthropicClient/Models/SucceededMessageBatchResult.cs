namespace AnthropicClient.Models;

/// <summary>
/// Represents a message batch result that contains a message response.
/// </summary>
public class SucceededMessageBatchResult : MessageBatchResult
{
  /// <summary>
  /// Gets the message of the message batch result.
  /// </summary>
  public MessageResponse Message { get; init; } = new MessageResponse();

  /// <summary>
  /// Initializes a new instance of the <see cref="SucceededMessageBatchResult"/> class.
  /// </summary>
  /// <returns>An instance of the <see cref="SucceededMessageBatchResult"/> class.</returns>
  public SucceededMessageBatchResult() : base(MessageBatchResultType.Succeeded)
  {
  }
}


