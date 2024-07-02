namespace AnthropicClient.Tests.Unit.Models;

public class InvalidRequestErrorTests : SerializationTest
{
  [Fact]
  public void Constructor_WhenCalled_ItShouldInitializeProperties()
  {
    var message = "message";

    var error = new InvalidRequestError(message);

    error.Type.Should().Be("invalid_request_error");
    error.Message.Should().Be(message);
  }

  [Fact]
  public void Serialization_WhenSerialized_ItShouldHaveExpectedShape()
  {
    var message = "message";

    var error = new InvalidRequestError(message);

    var serialized = Serialize(error);

    JsonAssert.Equal(
      @"{
        ""type"": ""invalid_request_error"",
        ""message"": ""message""
      }",
      serialized
    );
  }

  [Fact]
  public void Deserialization_WhenDeserialized_ItShouldHaveExpectedProperties()
  {
    var json = @"{
      ""type"": ""invalid_request_error"",
      ""message"": ""message""
    }";

    var error = Deserialize<InvalidRequestError>(json);

    error!.Type.Should().Be("invalid_request_error");
    error.Message.Should().Be("message");
  }
}