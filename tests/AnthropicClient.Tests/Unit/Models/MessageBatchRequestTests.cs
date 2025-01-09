namespace AnthropicClient.Tests.Unit.Models;

public class MessageBatchRequestTests : SerializationTest
{
  private const string SampleJson = @"{
      ""requests"": [
        {
          ""custom_id"": ""my-first-request"",
          ""params"": {
            ""model"": ""claude-3-5-sonnet-20241022"",
            ""messages"": [
              {""role"": ""user"", ""content"": [{ ""text"": ""Hello, world"", ""type"": ""text"" }]}
            ],
            ""max_tokens"": 1024,
            ""stop_sequences"": [],
            ""temperature"": 0.0,
            ""stream"": false
          }
        },
        {
          ""custom_id"": ""my-second-request"",
          ""params"": {
            ""model"": ""claude-3-5-sonnet-20241022"",
            ""messages"": [
              {""role"": ""user"", ""content"": [{ ""text"": ""Hi again, friend"", ""type"": ""text"" }]}
            ],
            ""max_tokens"": 1024,
            ""stop_sequences"": [],
            ""temperature"": 0.0,
            ""stream"": false
          }
        }
      ]
    }";

  [Fact]
  public void Constructor_WhenCalled_ItShouldReturnInstanceWithPropertiesSet()
  {
    var requests = new List<MessageBatchRequestItem> { new("custom_id", new()) };

    var result = new MessageBatchRequest(requests);

    result.Should().BeOfType<MessageBatchRequest>();
    result.Requests.Should().BeSameAs(requests);
  }

  [Fact]
  public void Constructor_WhenCalledWithEmptyRequests_ItShouldThrowException()
  {
    var act = () => new MessageBatchRequest([]);

    act.Should().Throw<ArgumentException>();
  }

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldHaveExpectedShape()
  {
    var requests = new List<MessageBatchRequestItem>
    {
      new("my-first-request", new()
      {
        Model = "claude-3-5-sonnet-20241022",
        MaxTokens = 1024,
        Messages = [
          new() { Role = "user", Content = [new TextContent("Hello, world")] }
        ]
      }),
      new("my-second-request", new()
      {
        Model = "claude-3-5-sonnet-20241022",
        MaxTokens = 1024,
        Messages = [
          new() { Role = "user", Content = [new TextContent("Hi again, friend")] }
        ]
      })
    };

    var result = new MessageBatchRequest(requests);

    var json = Serialize(result);

    JsonAssert.Equal(SampleJson, json);
  }
}