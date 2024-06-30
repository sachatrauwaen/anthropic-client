namespace AnthropicClient.Tests.Unit.Models;

public class ContentDeltaEventDataTests : SerializationTest
{
  private readonly string _testJsonWithTextDelta = @"{
    ""type"": ""content_block_delta"",
    ""index"": 0,
    ""delta"": {
      ""type"": ""text_delta"",
      ""text"": ""Hello World!""
    }
  }";

  private readonly string _testJsonWithInputDelta = @"{
    ""type"": ""content_block_delta"",
    ""index"": 0,
    ""delta"": {
      ""type"": ""input_json_delta"",
      ""partial_json"": ""Hello""
    }
  }";

  [Fact]
  public void Constructor_WhenCalled_ItShouldInitializeProperties()
  {
    var index = 0;
    var text = "Hello World!";
    var textDelta = new TextDelta(text);
    var contentDeltaEventData = new ContentDeltaEventData(index, textDelta);

    contentDeltaEventData.Type.Should().Be("content_block_delta");
    contentDeltaEventData.Delta.Should().BeSameAs(textDelta);
  }

  [Fact]
  public void JsonSerialization_WhenSerializedWithTextDelta_ItShouldHaveExpectedShape()
  {
    var index = 0;
    var text = "Hello World!";
    var textDelta = new TextDelta(text);
    var contentDeltaEventData = new ContentDeltaEventData(index, textDelta);

    var actual = Serialize(contentDeltaEventData);

    JsonAssert.Equal(_testJsonWithTextDelta, actual);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserializedWithTextDelta_ItShouldHaveExpectedProperties()
  {
    var index = 0;
    var text = "Hello World!";
    var textDelta = new TextDelta(text);
    var contentDeltaEventData = new ContentDeltaEventData(index, textDelta);

    var actual = Deserialize<ContentDeltaEventData>(_testJsonWithTextDelta);

    actual.Should().BeEquivalentTo(contentDeltaEventData);
  }

  [Fact]
  public void JsonSerialization_WhenSerializedWithInputDelta_ItShouldHaveExpectedShape()
  {
    var index = 0;
    var input = "Hello";
    var inputDelta = new JsonDelta(input);
    var contentDeltaEventData = new ContentDeltaEventData(index, inputDelta);

    var actual = Serialize(contentDeltaEventData);

    JsonAssert.Equal(_testJsonWithInputDelta, actual);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserializedWithInputDelta_ItShouldHaveExpectedProperties()
  {
    var index = 0;
    var input = "Hello";
    var inputDelta = new JsonDelta(input);
    var contentDeltaEventData = new ContentDeltaEventData(index, inputDelta);

    var actual = Deserialize<ContentDeltaEventData>(_testJsonWithInputDelta);

    actual.Should().BeEquivalentTo(contentDeltaEventData);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserializedWithUnknownDeltaType_ItShouldThrowJsonException()
  {
    var json = @"{
      ""type"": ""content_block_delta"",
      ""index"": 0,
      ""delta"": {
        ""type"": ""unknown_delta"",
        ""text"": ""Hello World!""
      }
    }";

    var action = () => Deserialize<ContentDeltaEventData>(json);

    action.Should().Throw<JsonException>();
  }
}