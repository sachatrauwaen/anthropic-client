namespace AnthropicClient.Tests.Unit.Models;

public class MessageDeltaTests : SerializationTest
{
  private readonly string _testJson = @"{
    ""stop_reason"": ""max_tokens"",
    ""stop_sequence"": ""max_tokens""
  }";

  [Fact]
  public void Constructor_WhenCalled_ItShouldInitializeProperties()
  {
    var expectedStopReason = "max_tokens";
    var expectedStopSequence = "max_tokens";

    var messageDelta = new MessageDelta(expectedStopReason, expectedStopSequence);

    messageDelta.StopReason.Should().Be(expectedStopReason);
    messageDelta.StopSequence.Should().Be(expectedStopSequence);
  }

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldHaveExpectedShape()
  {
    var messageDelta = new MessageDelta("max_tokens", "max_tokens");

    var actualJson = Serialize(messageDelta);

    JsonAssert.Equal(_testJson, actualJson);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldHaveExpectedProperties()
  {
    var expectedStopReason = "max_tokens";
    var expectedStopSequence = "max_tokens";

    var messageDelta = Deserialize<MessageDelta>(_testJson);

    messageDelta!.StopReason.Should().Be(expectedStopReason);
    messageDelta.StopSequence.Should().Be(expectedStopSequence);
  }
}