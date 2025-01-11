namespace AnthropicClient.Tests.Unit.Models;

public class SucceededMessageBatchResultTests : SerializationTest
{
  private const string SampleJson = @"{
    ""message"": {
      ""id"": ""msg_01FqfsLoHwgeFbguDgpz48m7"",
      ""model"": ""claude-3-5-sonnet-20240620"",
      ""role"": ""assistant"",
      ""stop_reason"": ""end_turn"",
      ""type"": ""message"",
      ""usage"": {
        ""input_tokens"": 10,
        ""output_tokens"": 34,
        ""cache_creation_input_tokens"": 0,
        ""cache_read_input_tokens"": 0
      },
      ""content"": [
        {
          ""text"": ""Hello! How can I assist you today? Feel free to ask me any questions or let me know if there\u0027s anything you\u0027d like to chat about."",
          ""type"": ""text""
        }
      ]
    },
    ""type"": ""succeeded""
  }";

  [Fact]
  public void Constructor_WhenCalled_ItShouldReturnAnInstanceWithPropertiesSet()
  {
    var result = new SucceededMessageBatchResult();

    result.Type.Should().Be(MessageBatchResultType.Succeeded);
    result.Message.Should().BeEquivalentTo(new MessageResponse());
  }

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldHaveExpectedShape()
  {
    var result = new SucceededMessageBatchResult
    {
      Message = new MessageResponse
      {
        Id = "msg_01FqfsLoHwgeFbguDgpz48m7",
        Type = "message",
        Role = "assistant",
        Model = "claude-3-5-sonnet-20240620",
        Content = [
            new TextContent()
            {
              Text = "Hello! How can I assist you today? Feel free to ask me any questions or let me know if there's anything you'd like to chat about."
            }
        ],
        StopReason = "end_turn",
        Usage = new()
        {
          InputTokens = 10,
          OutputTokens = 34
        }
      }
    };

    var json = Serialize(result);

    JsonAssert.Equal(SampleJson, json);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldHaveExpectedValues()
  {
    var result = Deserialize<SucceededMessageBatchResult>(SampleJson);

    result!.Type.Should().Be(MessageBatchResultType.Succeeded);
    result.Message.Should().BeEquivalentTo(new MessageResponse
    {
      Id = "msg_01FqfsLoHwgeFbguDgpz48m7",
      Type = "message",
      Role = "assistant",
      Model = "claude-3-5-sonnet-20240620",
      Content = [
          new TextContent()
          {
            Text = "Hello! How can I assist you today? Feel free to ask me any questions or let me know if there's anything you'd like to chat about."
          }
      ],
      StopReason = "end_turn",
      StopSequence = null,
      Usage = new()
      {
        InputTokens = 10,
        OutputTokens = 34
      }
    });
  }
}