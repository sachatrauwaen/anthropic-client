namespace AnthropicClient.Models;

public static class EventType
{
  public const string Error = "error";
  public const string Ping = "ping";
  public const string MessageStart = "message_start";
  public const string MessageDelta = "message_delta";
  public const string MessageStop = "message_stop";
  public const string ContentBlockStart = "content_block_start";
  public const string ContentBlockDelta = "content_block_delta";
  public const string ContentBlockStop = "content_block_stop";
}