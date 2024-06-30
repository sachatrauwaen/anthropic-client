namespace AnthropicClient.Tests.Unit.Models;

public class JsonDeltaTests : SerializationTest
{
  private readonly string _testJson = @"{
    ""type"": ""input_json_delta"",
    ""partial_json"": ""{\""key\"": \""value\""}""
  }";

  [Fact]
  public void Constructor_WhenCalled_ItShouldInitializeProperties()
  {
    var partialJson = "{\"key\": \"value\"}";

    var jsonDelta = new JsonDelta(partialJson);

    jsonDelta.Type.Should().Be("input_json_delta");
    jsonDelta.PartialJson.Should().Be(partialJson);
  }

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldHaveExpectedShape()
  {
    var jsonDelta = new JsonDelta("{\"key\": \"value\"}");

    var actual = Serialize(jsonDelta);

    JsonAssert.Equal(_testJson, actual);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldHaveExpectedProperties()
  {
    var expected = new JsonDelta("{\"key\": \"value\"}");

    var actual = Deserialize<JsonDelta>(_testJson);

    actual.Should().BeEquivalentTo(expected);
  }
}