namespace AnthropicClient.Tests.Unit.Models;

public class MessageDeltaEventDataTests : SerializationTest
{
  private readonly string _testJson = @"{
    ""type"": ""message_delta"",
    ""delta"": {
      ""stop_reason"": ""max_tokens"",
      ""stop_sequence"": ""max_tokens""
    },
    ""usage"": {
      ""input_tokens"": 1,
      ""output_tokens"": 1, 
      ""cache_creation_input_tokens"": 1,
      ""cache_read_input_tokens"": 1
    }
  }";

  [Fact]
  public void Constructor_WhenCalled_ItShouldInitializeProperties()
  {
    var expectedDelta = new MessageDelta("max_tokens", "max_tokens");
    var expectedUsage = new Usage
    {
      InputTokens = 1,
      OutputTokens = 1,
      CacheCreationInputTokens = 1,
      CacheReadInputTokens = 1,
    };

    var messageDeltaEventData = new MessageDeltaEventData(expectedDelta, expectedUsage);

    messageDeltaEventData.Delta.Should().BeSameAs(expectedDelta);
    messageDeltaEventData.Usage.Should().BeSameAs(expectedUsage);
  }

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldHaveExpectedShape()
  {
    var expectedDelta = new MessageDelta("max_tokens", "max_tokens");
    var expectedUsage = new Usage
    {
      InputTokens = 1,
      OutputTokens = 1,
      CacheCreationInputTokens = 1,
      CacheReadInputTokens = 1,
    };

    var messageDeltaEventData = new MessageDeltaEventData(expectedDelta, expectedUsage);

    var actualJson = Serialize(messageDeltaEventData);

    JsonAssert.Equal(_testJson, actualJson);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldHaveExpectedValues()
  {
    var expectedDelta = new MessageDelta("max_tokens", "max_tokens");
    var expectedUsage = new Usage
    {
      InputTokens = 1,
      OutputTokens = 1,
      CacheCreationInputTokens = 1,
      CacheReadInputTokens = 1,
    };

    var messageDeltaEventData = Deserialize<MessageDeltaEventData>(_testJson);

    messageDeltaEventData!.Delta.Should().BeEquivalentTo(expectedDelta);
    messageDeltaEventData.Usage.Should().BeEquivalentTo(expectedUsage);
  }
}