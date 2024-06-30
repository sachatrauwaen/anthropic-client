namespace AnthropicClient.Tests.Unit.Models;

public class AnthropicEventTests : SerializationTest
{
  private readonly string _testMessageStartJson = @"{
    ""type"": ""message_start"",
    ""data"": {
      ""message"": {
        ""id"": ""msg_014p7gG3wDgGV9EUtLvnow3U"",
        ""type"": ""message"",
        ""role"": ""assistant"",
        ""model"": ""claude-3-haiku-20240307"",
        ""stop_sequence"": """",
        ""usage"": {
          ""input_tokens"": 472,
          ""output_tokens"": 2
        },
        ""content"": [],
        ""stop_reason"": """"
      },
      ""type"": ""message_start""
    }
  }";

  private readonly string _testContentBlockStartJson = @"{
    ""type"": ""content_block_start"",
    ""data"": {
      ""index"": 0,
      ""content_block"": {
        ""type"": ""text"",
        ""text"": ""Okay""
      },
      ""type"": ""content_block_start""
    }
  }";

  private readonly string _testPingJson = @"{
    ""type"": ""ping"",
    ""data"": {
      ""type"": ""ping""
    }
  }";

  private readonly string _testContentBlockDeltaJson = @"{
    ""type"": ""content_block_delta"",
    ""data"": {
      ""index"": 0,
      ""delta"": {
        ""type"": ""text_delta"",
        ""text"": ""Okay""
      },
      ""type"": ""content_block_delta""
    }
  }";

  private readonly string _testContentBlockStopJson = @"{
    ""type"": ""content_block_stop"",
    ""data"": {
      ""index"": 0,
      ""type"": ""content_block_stop""
    }
  }";

  private readonly string _testMessageDeltaJson = @"{
    ""type"": ""message_delta"",
    ""data"": {
      ""delta"": {
        ""stop_reason"": ""tool_use"",
        ""stop_sequence"": """"
      },
      ""usage"": {
        ""output_tokens"": 89,
        ""input_tokens"": 0
      },
      ""type"": ""message_delta""
    }
  }";

  private readonly string _testMessageStopJson = @"{
    ""type"": ""message_stop"",
    ""data"": {
      ""type"": ""message_stop""
    }
  }";

  private readonly string _testErrorJson = @"{
    ""type"": ""error"",
    ""data"": {
      ""error"": {
        ""type"": ""api_error"",
        ""message"": ""An error occurred.""
      },
      ""type"": ""error""
    }
  }";

  [Fact]
  public void Constructor_WhenCalled_ItShouldInitializeProperties()
  {
    var expectedType = EventType.MessageStart;
    var expected = new MessageStartEventData
    {
      Type = EventType.MessageStart,
      Message = new()
    };

    var actual = new AnthropicEvent(expectedType, expected);

    actual.Type.Should().Be(expectedType);
    actual.Data.Should().BeSameAs(expected);
  }

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldHaveExpectedShape()
  {
    var eventData = new MessageStartEventData
    {
      Type = EventType.MessageStart,
      Message = new()
      {
        Id = "msg_014p7gG3wDgGV9EUtLvnow3U",
        Type = "message",
        Role = "assistant",
        Model = "claude-3-haiku-20240307",
        StopSequence = string.Empty,
        Usage = new()
        {
          InputTokens = 472,
          OutputTokens = 2
        },
        Content = [],
        StopReason = string.Empty
      }
    };

    var e = new AnthropicEvent(EventType.MessageStart, eventData);

    var actual = Serialize(e);

    JsonAssert.Equal(_testMessageStartJson, actual);
  }

  [Fact]
  public void JsonDeserialization_WhenMessageStartEventDeserialized_ItShouldHaveBeExpectedType()
  {
    var expected = new MessageStartEventData
    {
      Type = EventType.MessageStart,
      Message = new()
      {
        Id = "msg_014p7gG3wDgGV9EUtLvnow3U",
        Type = "message",
        Role = "assistant",
        Model = "claude-3-haiku-20240307",
        StopSequence = string.Empty,
        Usage = new()
        {
          InputTokens = 472,
          OutputTokens = 2
        },
        Content = [],
        StopReason = string.Empty
      }
    };

    var actual = Deserialize<AnthropicEvent>(_testMessageStartJson);

    actual!.Type.Should().Be(expected.Type);
    actual.Data.Should().BeEquivalentTo(expected);
  }

  [Fact]
  public void JsonSerialization_WhenContentBlockStartEventSerialized_ItShouldHaveExpectedShape()
  {
    var eventData = new ContentStartEventData(0, new TextContent("Okay"));

    var e = new AnthropicEvent(EventType.ContentBlockStart, eventData);

    var actual = Serialize(e);

    JsonAssert.Equal(_testContentBlockStartJson, actual);
  }

  [Fact]
  public void JsonDeserialization_WhenContentBlockStartEventDeserialized_ItShouldHaveExpectedType()
  {
    var expected = new ContentStartEventData(0, new TextContent("Okay"));

    var actual = Deserialize<AnthropicEvent>(_testContentBlockStartJson);

    actual!.Type.Should().Be(expected.Type);
    actual.Data.Should().BeEquivalentTo(expected);
  }

  [Fact]
  public void JsonSerialization_WhenPingEventSerialized_ItShouldHaveExpectedShape()
  {
    var eventData = new PingEventData();

    var e = new AnthropicEvent(EventType.Ping, eventData);

    var actual = Serialize(e);

    JsonAssert.Equal(_testPingJson, actual);
  }

  [Fact]
  public void JsonDeserialization_WhenPingEventDeserialized_ItShouldHaveExpectedType()
  {
    var expected = new PingEventData();

    var actual = Deserialize<AnthropicEvent>(_testPingJson);

    actual!.Type.Should().Be(expected.Type);
    actual.Data.Should().BeEquivalentTo(expected);
  }

  [Fact]
  public void JsonSerialization_WhenContentBlockDeltaEventSerialized_ItShouldHaveExpectedShape()
  {
    var eventData = new ContentDeltaEventData(0, new TextDelta("Okay"));

    var e = new AnthropicEvent(EventType.ContentBlockDelta, eventData);

    var actual = Serialize(e);

    JsonAssert.Equal(_testContentBlockDeltaJson, actual);
  }

  [Fact]
  public void JsonDeserialization_WhenContentBlockDeltaEventDeserialized_ItShouldHaveExpectedType()
  {
    var expected = new ContentDeltaEventData(0, new TextDelta("Okay"));

    var actual = Deserialize<AnthropicEvent>(_testContentBlockDeltaJson);

    actual!.Type.Should().Be(expected.Type);
    actual.Data.Should().BeEquivalentTo(expected);
  }

  [Fact]
  public void JsonSerialization_WhenContentBlockStopEventSerialized_ItShouldHaveExpectedShape()
  {
    var eventData = new ContentStopEventData(0);

    var e = new AnthropicEvent(EventType.ContentBlockStop, eventData);

    var actual = Serialize(e);

    JsonAssert.Equal(_testContentBlockStopJson, actual);
  }

  [Fact]
  public void JsonDeserialization_WhenContentBlockStopEventDeserialized_ItShouldHaveExpectedType()
  {
    var expected = new ContentStopEventData(0);

    var actual = Deserialize<AnthropicEvent>(_testContentBlockStopJson);

    actual!.Type.Should().Be(expected.Type);
    actual.Data.Should().BeEquivalentTo(expected);
  }

  [Fact]
  public void JsonSerialization_WhenMessageDeltaEventSerialized_ItShouldHaveExpectedShape()
  {
    var eventData = new MessageDeltaEventData
    {
      Type = EventType.MessageDelta,
      Delta = new()
      {
        StopReason = "tool_use",
        StopSequence = string.Empty,
      },
      Usage = new()
      {
        OutputTokens = 89,
      }
    };

    var e = new AnthropicEvent(EventType.MessageDelta, eventData);

    var actual = Serialize(e);

    JsonAssert.Equal(_testMessageDeltaJson, actual);
  }

  [Fact]
  public void JsonDeserialization_WhenMessageDeltaEventDeserialized_ItShouldHaveExpectedType()
  {
    var expected = new MessageDeltaEventData
    {
      Type = EventType.MessageDelta,
      Delta = new()
      {
        StopReason = "tool_use",
        StopSequence = string.Empty,
      },
      Usage = new()
      {
        OutputTokens = 89,
        InputTokens = 0
      }
    };

    var actual = Deserialize<AnthropicEvent>(_testMessageDeltaJson);

    actual!.Type.Should().Be(expected.Type);
    actual.Data.Should().BeEquivalentTo(expected);
  }

  [Fact]
  public void JsonSerialization_WhenMessageStopEventSerialized_ItShouldHaveExpectedShape()
  {
    var eventData = new MessageStopEventData();

    var e = new AnthropicEvent(EventType.MessageStop, eventData);

    var actual = Serialize(e);

    JsonAssert.Equal(_testMessageStopJson, actual);
  }

  [Fact]
  public void JsonDeserialization_WhenMessageStopEventDeserialized_ItShouldHaveExpectedType()
  {
    var expected = new MessageStopEventData();

    var actual = Deserialize<AnthropicEvent>(_testMessageStopJson);

    actual!.Type.Should().Be(expected.Type);
    actual.Data.Should().BeEquivalentTo(expected);
  }

  [Fact]
  public void JsonSerialization_WhenErrorEventSerialized_ItShouldHaveExpectedShape()
  {
    var eventData = new ErrorEventData(new ApiError("An error occurred."));

    var e = new AnthropicEvent(EventType.Error, eventData);

    var actual = Serialize(e);

    JsonAssert.Equal(_testErrorJson, actual);
  }

  [Fact]
  public void JsonDeserialization_WhenErrorEventDeserialized_ItShouldHaveExpectedType()
  {
    var expected = new ErrorEventData(new ApiError("An error occurred."));

    var actual = Deserialize<AnthropicEvent>(_testErrorJson);

    actual!.Type.Should().Be(expected.Type);
    actual.Data.Should().BeEquivalentTo(expected);
  }

  [Fact]
  public void JsonDeserialization_WhenTypeUnknown_ItShouldThrow()
  {
    var json = @"{
      ""type"": ""unknown"",
      ""data"": {
        ""type"": ""unknown""
      }
    }";

    Action act = () => Deserialize<AnthropicEvent>(json);

    act.Should().Throw<JsonException>();
  }
}