namespace AnthropicClient.Tests.Unit.Models;

public class NotFoundErrorTests : SerializationTest
{
  private readonly string _testJson = @"{ 
    ""message"": ""message"", 
    ""type"": ""not_found_error"" 
  }";

  [Fact]
  public void Constructor_WhenCalled_ItShouldInitializeProperties()
  {
    var expectedMessage = "message";

    var actual = new NotFoundError(expectedMessage);

    actual.Message.Should().Be(expectedMessage);
    actual.Type.Should().Be("not_found_error");
  }

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldHaveExpectedShape()
  {
    var error = new NotFoundError("message");

    var actual = Serialize(error);

    JsonAssert.Equal(_testJson, actual);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldHaveExpectedShape()
  {
    var expected = new NotFoundError("message");

    var actual = Deserialize<NotFoundError>(_testJson);

    actual.Should().BeEquivalentTo(expected);
  }
}