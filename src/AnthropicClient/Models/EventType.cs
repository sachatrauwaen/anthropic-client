namespace AnthropicClient.Models;

/// <summary>
/// Provides constants for event types.
/// </summary>
public static class EventType
{
  /// <summary>
  /// Represents the error event type.
  /// </summary>
  public const string Error = "error";

  /// <summary>
  /// Represents the ping event type.
  /// </summary>
  public const string Ping = "ping";

  /// <summary>
  /// Represents the message_start event type.
  /// </summary>
  public const string MessageStart = "message_start";

  /// <summary>
  /// Represents the message_delta event type.
  /// </summary>
  public const string MessageDelta = "message_delta";

  /// <summary>
  /// Represents the message_stop event type.
  /// </summary>
  public const string MessageStop = "message_stop";

  /// <summary>
  /// Represents the message_complete event type.
  /// </summary>
  public const string MessageComplete = "message_complete";

  /// <summary>
  /// Represents the content_block_start event type.
  /// </summary>
  public const string ContentBlockStart = "content_block_start";

  /// <summary>
  /// Represents the content_block_delta event type.
  /// </summary>
  public const string ContentBlockDelta = "content_block_delta";

  /// <summary>
  /// Represents the content_block_stop event type.
  /// </summary>
  public const string ContentBlockStop = "content_block_stop";
}