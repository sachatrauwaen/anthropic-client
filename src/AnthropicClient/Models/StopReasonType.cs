namespace AnthropicClient.Models;

/// <summary>
/// Represents the stop reason type.
/// </summary>
public static class StopReasonType
{
  /// <summary>
  /// Represents the end_turn stop reason.
  /// </summary>
  public const string EndTurn = "end_turn";

  /// <summary>
  /// Represents the max_tokens stop reason.
  /// </summary>
  public const string MaxTokens = "max_tokens";

  /// <summary>
  /// Represents the stop_sequence stop reason.
  /// </summary>
  public const string StopSequence = "stop_sequence";

  /// <summary>
  /// Represents the tool_use stop reason.
  /// </summary>
  public const string ToolUse = "tool_use";
}


