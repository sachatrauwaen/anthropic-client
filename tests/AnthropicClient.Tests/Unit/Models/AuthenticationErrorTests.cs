namespace AnthropicClient.Tests.Unit.Models;

public class AuthenticationErrorTests : SerializationTest
{
  private readonly string _testJson = @"{ 
    ""message"": ""message"", 
    ""type"": ""authentication_error"" 
  }";

  [Fact]
  public void Constructor_WhenCalled_ItShouldInitializeProperties()
  {
    var expectedMessage = "message";

    var actual = new AuthenticationError(expectedMessage);

    actual.Message.Should().Be(expectedMessage);
    actual.Type.Should().Be("authentication_error");
  }

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldHaveExpectedShape()
  {
    var error = new AuthenticationError("message");

    var actual = Serialize(error);

    JsonAssert.Equal(_testJson, actual);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldHaveExpectedShape()
  {
    var expected = new AuthenticationError("message");

    var actual = Deserialize<AuthenticationError>(_testJson);

    actual.Should().BeEquivalentTo(expected);
  }
}