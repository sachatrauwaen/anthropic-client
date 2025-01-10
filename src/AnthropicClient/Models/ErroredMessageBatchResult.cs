namespace AnthropicClient.Models;

/// <summary>
/// Represents a message batch result that contains an error response.
/// </summary>
public class ErroredMessageBatchResult : MessageBatchResult
{
  /// <summary>
  /// Gets the error of the message batch result.
  /// </summary>
  public AnthropicError Error { get; init; } = new AnthropicError();

  /// <summary>
  /// Initializes a new instance of the <see cref="ErroredMessageBatchResult"/> class.
  /// </summary>
  public ErroredMessageBatchResult() : base(MessageBatchResultType.Errored)
  {
  }
}
