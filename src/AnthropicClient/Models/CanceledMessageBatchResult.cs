namespace AnthropicClient.Models;

/// <summary>
/// Represents a message batch result that was cancelled.
/// </summary>
public class CanceledMessageBatchResult : MessageBatchResult
{
  /// <summary>
  /// Initializes a new instance of the <see cref="CanceledMessageBatchResult"/> class.
  /// </summary>
  public CanceledMessageBatchResult() : base(MessageBatchResultType.Canceled)
  {
  }
}