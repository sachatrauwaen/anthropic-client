namespace AnthropicClient.Tests.Unit.Models;

public class RateLimitErrorTests : SerializationTest
{
  private readonly string _testJson = @"{ 
    ""message"": ""message"", 
    ""type"": ""rate_limit_error"" 
  }";

  [Fact]
  public void Constructor_WhenCalled_ItShouldInitializeProperties()
  {
    var expectedMessage = "message";

    var actual = new RateLimitError(expectedMessage);

    actual.Message.Should().Be(expectedMessage);
    actual.Type.Should().Be("rate_limit_error");
  }

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldHaveExpectedShape()
  {
    var error = new RateLimitError("message");

    var actual = Serialize(error);

    JsonAssert.Equal(_testJson, actual);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldHaveExpectedShape()
  {
    var expected = new RateLimitError("message");

    var actual = Deserialize<RateLimitError>(_testJson);

    actual.Should().BeEquivalentTo(expected);
  }
}