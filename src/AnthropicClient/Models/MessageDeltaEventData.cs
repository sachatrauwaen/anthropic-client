using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

/// <summary>
/// Represents data for a message_delta event.
/// </summary>
public class MessageDeltaEventData : EventData
{
  /// <summary>
  /// Gets the message delta.
  /// </summary>
  public MessageDelta Delta { get; init; } = new();

  /// <summary>
  /// Gets the chat usage.
  /// </summary>
  public ChatUsage Usage { get; init; } = new();

  [JsonConstructor]
  internal MessageDeltaEventData() : base(EventType.MessageDelta)
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="MessageDeltaEventData"/> class.
  /// </summary>
  /// <param name="delta">The message delta.</param>
  /// <param name="usage">The chat usage.</param>
  /// <returns>A new instance of the <see cref="MessageDeltaEventData"/> class.</returns>
  public MessageDeltaEventData(MessageDelta delta, ChatUsage usage) : base(EventType.MessageDelta)
  {
    Delta = delta;
    Usage = usage;
  }
}

/// <summary>
/// Represents a message delta.
/// </summary>
public class MessageDelta
{
  /// <summary>
  /// Gets the stop reason.
  /// </summary>
  [JsonPropertyName("stop_reason")]
  public string StopReason { get; init; } = string.Empty;

  /// <summary>
  /// Gets the stop sequence.
  /// </summary>
  [JsonPropertyName("stop_sequence")]
  public string StopSequence { get; init; } = string.Empty;

  [JsonConstructor]
  internal MessageDelta()
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="MessageDelta"/> class.
  /// </summary>
  /// <param name="stopReason">The stop reason.</param>
  /// <param name="stopSequence">The stop sequence.</param>
  /// <returns>A new instance of the <see cref="MessageDelta"/> class.</returns>
  public MessageDelta(string stopReason, string stopSequence)
  {
    StopReason = stopReason;
    StopSequence = stopSequence;
  }
}