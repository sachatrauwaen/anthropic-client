namespace AnthropicClient.Tests.Unit.Models;

public class MessageStartEventDataTests : SerializationTest
{
  private readonly string _testJson = @"{
    ""type"": ""message_start"",
    ""message"": {
      ""id"": ""msg_1nZdL29xx5MUA1yADyHTEsnR8uuvGzszyY"",
      ""type"": ""message"",
      ""role"": ""assistant"",
      ""content"": [],
      ""model"": ""claude-3-5-sonnet-20240620"",
      ""stop_reason"": """",
      ""stop_sequence"": """",
      ""usage"": {
        ""input_tokens"": 25,
        ""output_tokens"": 1
      }
    }
  }";

  [Fact]
  public void Constructor_WhenCalled_ItShouldInitializeProperties()
  {
    var expectedMessage = new ChatResponse
    {
      Id = "msg_1nZdL29xx5MUA1yADyHTEsnR8uuvGzszyY",
      Type = "message",
      Role = "assistant",
      Content = [],
      Model = "claude-3-5-sonnet-20240620",
      StopReason = string.Empty,
      StopSequence = string.Empty,
      Usage = new ChatUsage { InputTokens = 25, OutputTokens = 1 }
    };

    var messageStartEventData = new MessageStartEventData(expectedMessage);

    messageStartEventData.Message.Should().BeSameAs(expectedMessage);
  }

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldHaveExpectedShape()
  {
    var expectedMessage = new ChatResponse
    {
      Id = "msg_1nZdL29xx5MUA1yADyHTEsnR8uuvGzszyY",
      Type = "message",
      Role = "assistant",
      Content = [],
      Model = "claude-3-5-sonnet-20240620",
      StopReason = string.Empty,
      StopSequence = string.Empty,
      Usage = new ChatUsage { InputTokens = 25, OutputTokens = 1 }
    };

    var messageStartEventData = new MessageStartEventData(expectedMessage);

    var actualJson = Serialize(messageStartEventData);

    JsonAssert.Equal(_testJson, actualJson);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldHaveExpectedValues()
  {
    var expectedMessage = new ChatResponse
    {
      Id = "msg_1nZdL29xx5MUA1yADyHTEsnR8uuvGzszyY",
      Type = "message",
      Role = "assistant",
      Content = [],
      Model = "claude-3-5-sonnet-20240620",
      StopReason = string.Empty,
      StopSequence = string.Empty,
      Usage = new ChatUsage { InputTokens = 25, OutputTokens = 1 }
    };

    var messageStartEventData = Deserialize<MessageStartEventData>(_testJson);

    messageStartEventData!.Message.Should().BeEquivalentTo(expectedMessage);
  }
}