namespace AnthropicClient.Tests.Unit.Models;

public class ContentStartEventDataTests : SerializationTest
{
  private readonly string _testJsonWithTextBlock = @"{
    ""type"": ""content_block_start"",
    ""index"": 0,
    ""content_block"": {
      ""type"": ""text"",
      ""text"": ""Hello World!""
    }
  }";

  private readonly string _testJsonWithToolUseBlock = @"{
    ""type"": ""content_block_start"",
    ""index"": 0,
    ""content_block"": {
      ""type"": ""tool_use"",
      ""id"": ""toolu_01T1x1fJ34qAmk2tNTrN7Up6"",
      ""name"": ""get_weather"",
      ""input"": {}}
  }";

  [Fact]
  public void Constructor_WhenCalled_ItShouldInitializeProperties()
  {
    var index = 0;
    var textContent = new TextContent("Hello World!");
    var contentStartEventData = new ContentStartEventData(index, textContent);

    contentStartEventData.Type.Should().Be("content_block_start");
    contentStartEventData.Index.Should().Be(index);
    contentStartEventData.ContentBlock.Should().BeSameAs(textContent);
  }

  [Fact]
  public void JsonSerialization_WhenSerializedWithTextContent_ItShouldHaveExpectedShape()
  {
    var index = 0;
    var textContent = new TextContent("Hello World!");
    var contentStartEventData = new ContentStartEventData(index, textContent);

    var actual = Serialize(contentStartEventData);

    JsonAssert.Equal(_testJsonWithTextBlock, actual);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserializedWithTextContent_ItShouldHaveExpectedProperties()
  {
    var index = 0;
    var textContent = new TextContent("Hello World!");
    var contentStartEventData = new ContentStartEventData(index, textContent);

    var actual = Deserialize<ContentStartEventData>(_testJsonWithTextBlock);

    actual.Should().BeEquivalentTo(contentStartEventData);
  }

  [Fact]
  public void JsonSerialization_WhenSerializedWithToolUseContent_ItShouldHaveExpectedShape()
  {
    var index = 0;
    var toolUseContent = new ToolUseContent()
    {
      Id = "toolu_01T1x1fJ34qAmk2tNTrN7Up6",
      Name = "get_weather",
      Input = []
    };

    var contentStartEventData = new ContentStartEventData(index, toolUseContent);

    var actual = Serialize(contentStartEventData);

    JsonAssert.Equal(_testJsonWithToolUseBlock, actual);
  }
}