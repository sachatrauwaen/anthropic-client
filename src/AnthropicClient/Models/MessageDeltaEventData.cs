using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

public class MessageDeltaEventData : EventData
{
  public MessageDelta Delta { get; init; }
  public ChatUsage Usage { get; init; }

  [JsonConstructor]
  internal MessageDeltaEventData() : base(EventType.MessageDelta)
  {
  }

  public MessageDeltaEventData(MessageDelta delta, ChatUsage usage) : base(EventType.MessageDelta)
  {
    Delta = delta;
    Usage = usage;
  }
}

public class MessageDelta
{
  [JsonPropertyName("stop_reason")]
  public string StopReason { get; init; } = string.Empty;

  [JsonPropertyName("stop_sequence")]
  public string StopSequence { get; init; } = string.Empty;

  [JsonConstructor]
  internal MessageDelta()
  {
  }

  public MessageDelta(string stopReason, string stopSequence)
  {
    StopReason = stopReason;
    StopSequence = stopSequence;
  }
}