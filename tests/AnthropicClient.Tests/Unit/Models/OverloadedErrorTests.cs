namespace AnthropicClient.Tests.Unit.Models;

public class OverloadedErrorTests : SerializationTest
{
  private readonly string _testJson = @"{ 
    ""message"": ""message"", 
    ""type"": ""overloaded_error"" 
  }";

  [Fact]
  public void Constructor_WhenCalled_ItShouldInitializeProperties()
  {
    var expectedMessage = "message";

    var actual = new OverloadedError(expectedMessage);

    actual.Message.Should().Be(expectedMessage);
    actual.Type.Should().Be("overloaded_error");
  }

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldHaveExpectedShape()
  {
    var error = new OverloadedError("message");

    var actual = Serialize(error);

    JsonAssert.Equal(_testJson, actual);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldHaveExpectedShape()
  {
    var expected = new OverloadedError("message");

    var actual = Deserialize<OverloadedError>(_testJson);

    actual.Should().BeEquivalentTo(expected);
  }
}