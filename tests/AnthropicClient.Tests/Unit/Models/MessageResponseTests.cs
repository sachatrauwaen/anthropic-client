namespace AnthropicClient.Tests.Unit.Models;

public class MessageResponseTests : SerializationTest
{
  [Fact]
  public void Constructor_WhenCalled_ShouldInitializeProperties()
  {
    var expectedId = "id";
    var expectedModel = "model";
    var expectedRole = "role";
    var expectedStopReason = "stop reason";
    var expectedStopSequence = "stop sequence";
    var expectedType = "type";
    var expectedUsage = new Usage
    {
      InputTokens = 1,
      OutputTokens = 2
    };
    var expectedContent = new List<Content>
    {
      new TextContent("text content"),
    };

    var messageResponse = new MessageResponse
    {
      Id = expectedId,
      Model = expectedModel,
      Role = expectedRole,
      StopReason = expectedStopReason,
      StopSequence = expectedStopSequence,
      Type = expectedType,
      Usage = expectedUsage,
      Content = expectedContent
    };

    messageResponse.Id.Should().Be(expectedId);
    messageResponse.Model.Should().Be(expectedModel);
    messageResponse.Role.Should().Be(expectedRole);
    messageResponse.StopReason.Should().Be(expectedStopReason);
    messageResponse.StopSequence.Should().Be(expectedStopSequence);
    messageResponse.Type.Should().Be(expectedType);
    messageResponse.Usage.Should().BeEquivalentTo(expectedUsage);
    messageResponse.Content.Should().BeEquivalentTo(expectedContent);
  }

  [Fact]
  public void JsonSerialization_WhenCalled_ItShouldSerializeCorrectly()
  {
    var expectedJson = @"{
      ""id"": ""id"",
      ""model"": ""model"",
      ""role"": ""role"",
      ""stop_reason"": ""stop reason"",
      ""stop_sequence"": ""stop sequence"",
      ""type"": ""type"",
      ""usage"": { ""input_tokens"": 1, ""output_tokens"": 2 },
      ""content"": [
        { ""text"": ""text content"", ""type"": ""text"" }
      ]
    }";

    var messageResponse = new MessageResponse
    {
      Id = "id",
      Model = "model",
      Role = "role",
      StopReason = "stop reason",
      StopSequence = "stop sequence",
      Type = "type",
      Usage = new Usage
      {
        InputTokens = 1,
        OutputTokens = 2
      },
      Content =
      [
        new TextContent("text content"),
      ]
    };

    var actual = Serialize(messageResponse);

    JsonAssert.Equal(expectedJson, actual);
  }

  [Fact]
  public void JsonDeserialization_WhenCalled_ItShouldDeserializeCorrectly()
  {
    var json = @"{
      ""id"": ""id"",
      ""model"": ""model"",
      ""role"": ""role"",
      ""stop_reason"": ""stop reason"",
      ""stop_sequence"": ""stop sequence"",
      ""type"": ""type"",
      ""usage"": { ""input_tokens"": 1, ""output_tokens"": 2 },
      ""content"": [
        { ""text"": ""text content"", ""type"": ""text"" }
      ]
    }";

    var messageResponse = Deserialize<MessageResponse>(json);

    messageResponse!.Id.Should().Be("id");
    messageResponse.Model.Should().Be("model");
    messageResponse.Role.Should().Be("role");
    messageResponse.StopReason.Should().Be("stop reason");
    messageResponse.StopSequence.Should().Be("stop sequence");
    messageResponse.Type.Should().Be("type");
    messageResponse.Usage.Should().BeEquivalentTo(new Usage
    {
      InputTokens = 1,
      OutputTokens = 2
    });
    messageResponse.Content.Should().BeEquivalentTo(new List<Content>
    {
      new TextContent("text content"),
    });
  }
}