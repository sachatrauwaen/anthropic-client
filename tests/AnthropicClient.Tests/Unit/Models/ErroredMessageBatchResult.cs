namespace AnthropicClient.Tests.Unit.Models;

public class ErroredMessageBatchResultTests : SerializationTest
{
  private const string SampleJson = @"{
    ""type"": ""errored"",
    ""error"": {
      ""type"": ""error"",
      ""error"": {
        ""type"": ""api_error"",
        ""message"": ""An error occurred.""
      }
    }
  }";

  [Fact]
  public void Constructor_WhenCalled_ItShouldReturnAnInstanceWithPropertiesSet()
  {
    var result = new ErroredMessageBatchResult();

    result.Type.Should().Be(MessageBatchResultType.Errored);
    result.Error.Error.Should().BeOfType<ApiError>();
    result.Error.Error.Message.Should().BeEmpty();
  }

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldHaveExpectedShape()
  {
    var result = new ErroredMessageBatchResult
    {
      Error = new(new ApiError("An error occurred."))
    };

    var json = Serialize(result);

    JsonAssert.Equal(SampleJson, json);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldHaveExpectedProperties()
  {
    var result = Deserialize<ErroredMessageBatchResult>(SampleJson);

    result!.Type.Should().Be(MessageBatchResultType.Errored);
    result.Error.Error.Should().BeOfType<ApiError>();
    result.Error.Error.Message.Should().Be("An error occurred.");
  }
}