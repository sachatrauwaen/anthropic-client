namespace AnthropicClient.Models;

/// <summary>
/// Represents the status of a message batch.
/// </summary>
public static class MessageBatchStatus
{
  /// <summary>
  /// The status of a message batch that is being canceled.
  /// </summary>
  public const string Canceling = "canceling";

  /// <summary>
  /// The status of a message batch that is in progress.
  /// </summary>
  public const string InProgress = "in_progress";

  /// <summary>
  /// The status of a message batch that has ended.
  /// </summary>
  public const string Ended = "ended";
}


