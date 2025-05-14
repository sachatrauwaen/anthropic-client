namespace AnthropicClient.Models;

/// <summary>
/// Represents the types of message batch results.
/// </summary>
public static class MessageBatchResultType
{
  /// <summary>
  /// Represents a succeeded message batch result.
  /// </summary>
  public const string Succeeded = "succeeded";

  /// <summary>
  /// Represents an errored message batch result.
  /// </summary>
  public const string Errored = "errored";

  /// <summary>
  /// Represents a canceled message batch result.
  /// </summary>
  public const string Canceled = "canceled";

  /// <summary>
  /// Represents an expired message batch result.
  /// </summary>
  public const string Expired = "expired";
}


