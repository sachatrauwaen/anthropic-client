namespace AnthropicClient.Tests.Unit.Models;

public class PermissionErrorTests : SerializationTest
{
  private readonly string _testJson = @"{ 
    ""message"": ""message"", 
    ""type"": ""permission_error"" 
  }";

  [Fact]
  public void Constructor_WhenCalled_ItShouldInitializeProperties()
  {
    var expectedMessage = "message";

    var actual = new PermissionError(expectedMessage);

    actual.Message.Should().Be(expectedMessage);
    actual.Type.Should().Be("permission_error");
  }

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldHaveExpectedShape()
  {
    var error = new PermissionError("message");

    var actual = Serialize(error);

    JsonAssert.Equal(_testJson, actual);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldHaveExpectedShape()
  {
    var expected = new PermissionError("message");

    var actual = Deserialize<PermissionError>(_testJson);

    actual.Should().BeEquivalentTo(expected);
  }
}