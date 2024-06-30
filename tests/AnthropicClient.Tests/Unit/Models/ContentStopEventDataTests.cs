namespace AnthropicClient.Tests.Unit.Models;

public class ContentStopEventDataTests : SerializationTest
{
  private readonly string _testJson = @"{
    ""type"": ""content_block_stop"",
    ""index"": 0
  }";

  [Fact]
  public void Constructor_WhenCalled_ItShouldInitializeProperties()
  {
    var index = 0;
    var contentStopEventData = new ContentStopEventData(index);

    contentStopEventData.Type.Should().Be("content_block_stop");
    contentStopEventData.Index.Should().Be(index);
  }

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldHaveExpectedShape()
  {
    var index = 0;
    var contentStopEventData = new ContentStopEventData(index);

    var actual = Serialize(contentStopEventData);

    JsonAssert.Equal(_testJson, actual);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldHaveExpectedProperties()
  {
    var index = 0;
    var contentStopEventData = new ContentStopEventData(index);

    var actual = Deserialize<ContentStopEventData>(_testJson);

    actual.Should().BeEquivalentTo(contentStopEventData);
  }
}