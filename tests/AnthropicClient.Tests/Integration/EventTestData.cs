namespace AnthropicClient.Tests.Integration;

public class EventTestData : IEnumerable<object[]>
{
  public IEnumerator<object[]> GetEnumerator()
  {
    yield return new object[]
    {
      """
      event: message_start
      data: {"type":"message_start","message":{"id":"msg_014p7gG3wDgGV9EUtLvnow3U","type":"message","role":"assistant","model":"claude-3-haiku-20240307","stop_sequence":null,"usage":{"input_tokens":472,"output_tokens":2},"content":[],"stop_reason":null}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.MessageStart,
        Data = new MessageStartEventData()
        {
          Message = new ChatResponse()
          {
            Id = "msg_014p7gG3wDgGV9EUtLvnow3U",
            Type = "message",
            Role = "assistant",
            Model = "claude-3-haiku-20240307",
            StopSequence = null,
            Usage = new ChatUsage()
            {
              InputTokens = 472,
              OutputTokens = 2,
            },
            Content = [],
            StopReason = null,
          },
        },
      },
    };

    yield return new object[]
    {
      """
      event: content_block_start
      data: {"type":"content_block_start","index":0,"content_block":{"type":"text","text":""}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockStart,
        Data = new ContentStartEventData()
        {
          Index = 0,
          ContentBlock = new TextContent()
          {
            Type = "text",
            Text = "",
          },
        },
      },
    };

    yield return new object[]
    {
      """
      event: ping
      data: {"type": "ping"}
      """,
      new AnthropicEvent()
      {
        Type = EventType.Ping,
        Data = new PingEventData(),
      },
    };

    yield return new object[]
    {
      """
      event: content_block_delta
      data: {"type":"content_block_delta","index":0,"delta":{"type":"text_delta","text":"Okay"}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockDelta,
        Data = new ContentDeltaEventData()
        {
          Index = 0,
          Delta = new TextDelta()
          {
            Type = "text_delta",
            Text = "Okay",
          },
        },
      },
    };

    yield return new object[]
    {
      """
      event: content_block_delta
      data: {"type":"content_block_delta","index":0,"delta":{"type":"text_delta","text":","}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockDelta,
        Data = new ContentDeltaEventData()
        {
          Index = 0,
          Delta = new TextDelta()
          {
            Type = "text_delta",
            Text = ",",
          },
        },
      },
    };

    yield return new object[]
    {
      """
      event: content_block_delta
      data: {"type":"content_block_delta","index":0,"delta":{"type":"text_delta","text":" let"}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockDelta,
        Data = new ContentDeltaEventData()
        {
          Index = 0,
          Delta = new TextDelta()
          {
            Type = "text_delta",
            Text = " let",
          },
        },
      },
    };

    yield return new object[]
    {
      """
      event: content_block_delta
      data: {"type":"content_block_delta","index":0,"delta":{"type":"text_delta","text":"'s"}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockDelta,
        Data = new ContentDeltaEventData()
        {
          Index = 0,
          Delta = new TextDelta()
          {
            Type = "text_delta",
            Text = "'s",
          },
        },
      },
    };

    yield return new object[]
    {
      """
      event: content_block_delta
      data: {"type":"content_block_delta","index":0,"delta":{"type":"text_delta","text":" check"}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockDelta,
        Data = new ContentDeltaEventData()
        {
          Index = 0,
          Delta = new TextDelta()
          {
            Type = "text_delta",
            Text = " check",
          },
        },
      },
    };

    yield return new object[]
    {
      """
      event: content_block_delta
      data: {"type":"content_block_delta","index":0,"delta":{"type":"text_delta","text":" the"}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockDelta,
        Data = new ContentDeltaEventData()
        {
          Index = 0,
          Delta = new TextDelta()
          {
            Type = "text_delta",
            Text = " the",
          },
        },
      },
    };

    yield return new object[]
    {
      """
      event: content_block_delta
      data: {"type":"content_block_delta","index":0,"delta":{"type":"text_delta","text":" weather"}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockDelta,
        Data = new ContentDeltaEventData()
        {
          Index = 0,
          Delta = new TextDelta()
          {
            Type = "text_delta",
            Text = " weather",
          },
        },
      },
    };

    yield return new object[]
    {
      """
      event: content_block_delta
      data: {"type":"content_block_delta","index":0,"delta":{"type":"text_delta","text":" for"}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockDelta,
        Data = new ContentDeltaEventData()
        {
          Index = 0,
          Delta = new TextDelta()
          {
            Type = "text_delta",
            Text = " for",
          },
        },
      },
    };

    yield return new object[]
    {
      """
      event: content_block_delta
      data: {"type":"content_block_delta","index":0,"delta":{"type":"text_delta","text":" San"}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockDelta,
        Data = new ContentDeltaEventData()
        {
          Index = 0,
          Delta = new TextDelta()
          {
            Type = "text_delta",
            Text = " San",
          },
        },
      },
    };

    yield return new object[]
    {
      """
      event: content_block_delta
      data: {"type":"content_block_delta","index":0,"delta":{"type":"text_delta","text":" Francisco"}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockDelta,
        Data = new ContentDeltaEventData()
        {
          Index = 0,
          Delta = new TextDelta()
          {
            Type = "text_delta",
            Text = " Francisco",
          },
        },
      },
    };

    yield return new object[]
    {
      """
      event: content_block_delta
      data: {"type":"content_block_delta","index":0,"delta":{"type":"text_delta","text":","}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockDelta,
        Data = new ContentDeltaEventData()
        {
          Index = 0,
          Delta = new TextDelta()
          {
            Type = "text_delta",
            Text = " ,",
          },
        },
      },
    };

    yield return new object[]
    {
      """
      event: content_block_delta
      data: {"type":"content_block_delta","index":0,"delta":{"type":"text_delta","text":" CA"}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockDelta,
        Data = new ContentDeltaEventData()
        {
          Index = 0,
          Delta = new TextDelta()
          {
            Type = "text_delta",
            Text = " CA",
          },
        },
      },
    };

    yield return new object[]
    {
      """
      event: content_block_delta
      data: {"type":"content_block_delta","index":0,"delta":{"type":"text_delta","text":":"}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockDelta,
        Data = new ContentDeltaEventData()
        {
          Index = 0,
          Delta = new TextDelta()
          {
            Type = "text_delta",
            Text = " :",
          },
        },
      },
    };

    yield return new object[]
    {
      """
      event: content_block_stop
      data: {"type":"content_block_stop","index":0}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockStop,
        Data = new ContentStopEventData()
        {
          Index = 0,
        },
      },
    };

    yield return new object[]
    {
      """
      event: content_block_start
      data: {"type":"content_block_start","index":1,"content_block":{"type":"tool_use","id":"toolu_01T1x1fJ34qAmk2tNTrN7Up6","name":"get_weather","input":{}}}
      """,
      new AnthropicEvent()
      {
        Type = EventType.ContentBlockStart,
        Data = new ContentStartEventData()
        {
          Index = 1,
          ContentBlock = new ToolUseContent()
          {
            Type = "tool_use",
            Id = "toolu_01T1x1fJ34qAmk2tNTrN7Up6",
            Name = "get_weather",
            Input = [],
          },
        },
      },
    };
  }

  IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}