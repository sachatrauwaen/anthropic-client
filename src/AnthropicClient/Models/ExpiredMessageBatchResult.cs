namespace AnthropicClient.Models;

/// <summary>
/// Represents a message batch result that has expired.
/// </summary>
public class ExpiredMessageBatchResult : MessageBatchResult
{
  /// <summary>
  /// Initializes a new instance of the <see cref="ExpiredMessageBatchResult"/> class.
  /// </summary>
  public ExpiredMessageBatchResult() : base(MessageBatchResultType.Expired)
  {
  }
}


