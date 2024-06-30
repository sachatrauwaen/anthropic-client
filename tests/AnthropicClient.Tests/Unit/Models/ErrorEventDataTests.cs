namespace AnthropicClient.Tests.Unit.Models;

public class ErrorEventDataTests : SerializationTest
{
  private readonly string _testJson = @"{
    ""type"": ""error"",
    ""error"": {
      ""type"": ""api_error"",
      ""message"": ""An error occurred.""
    }
  }";

  [Fact]
  public void Constructor_WhenCalled_ItShouldInitializeProperties()
  {
    var error = new ApiError("An error occurred.");
    var errorEventData = new ErrorEventData(error);

    errorEventData.Type.Should().Be("error");
    errorEventData.Error.Should().BeSameAs(error);
  }

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldHaveExpectedShape()
  {
    var error = new ApiError("An error occurred.");
    var errorEventData = new ErrorEventData(error);

    var actual = Serialize(errorEventData);

    JsonAssert.Equal(_testJson, actual);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldHaveExpectedProperties()
  {
    var error = new ApiError("An error occurred.");
    var errorEventData = new ErrorEventData(error);

    var actual = Deserialize<ErrorEventData>(_testJson);

    actual.Should().BeEquivalentTo(errorEventData);
  }
}