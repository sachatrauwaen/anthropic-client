namespace AnthropicClient.Tests.Unit.Models;

public class ToolResultContentTests : SerializationTest
{
  [Fact]
  public void Constructor_WhenCalled_ItShouldInitializeProperties()
  {
    var toolUseId = Guid.NewGuid().ToString();
    var content = "content";

    var actual = new ToolResultContent(toolUseId, content);

    actual.ToolUseId.Should().Be(toolUseId);
    actual.Content.Should().Be(content);
    actual.Type.Should().Be("tool_result");
  }

  [Fact]
  public void Constructor_WhenCalledAndToolUseIdIsNull_ItShouldThrowArgumentNullException()
  {
    var content = "content";

    var action = () => new ToolResultContent(null!, content);

    action.Should().Throw<ArgumentNullException>();
  }

  [Fact]
  public void Constructor_WhenCalledAndContentIsNull_ItShouldThrowArgumentNullException()
  {
    var toolUseId = Guid.NewGuid().ToString();

    var action = () => new ToolResultContent(toolUseId, null!);

    action.Should().Throw<ArgumentNullException>();
  }

  [Fact]
  public void Constructor_WhenCalledAndGivenCacheControl_ItShouldInitializeProperties()
  {
    var toolUseId = Guid.NewGuid().ToString();
    var content = "content";
    var cacheControl = new EphemeralCacheControl();

    var actual = new ToolResultContent(toolUseId, content, cacheControl);

    actual.ToolUseId.Should().Be(toolUseId);
    actual.Content.Should().Be(content);
    actual.CacheControl.Should().BeSameAs(cacheControl);
    actual.Type.Should().Be("tool_result");
  }

  [Fact]
  public void Constructor_WhenCalledAndGivenCacheControlAndToolUseIdIsNull_ItShouldThrowArgumentNullException()
  {
    var content = "content";
    var cacheControl = new EphemeralCacheControl();

    var action = () => new ToolResultContent(null!, content, cacheControl);

    action.Should().Throw<ArgumentNullException>();
  }

  [Fact]
  public void Constructor_WhenCalledAndGivenCacheControlAndContentIsNull_ItShouldThrowArgumentNullException()
  {
    var toolUseId = Guid.NewGuid().ToString();
    var cacheControl = new EphemeralCacheControl();

    var action = () => new ToolResultContent(toolUseId, null!, cacheControl);

    action.Should().Throw<ArgumentNullException>();
  }

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldReturnJsonString()
  {
    var toolUseId = Guid.NewGuid().ToString();
    var content = "content";

    var expectedJson = @$"{{
      ""tool_use_id"": ""{toolUseId}"",
      ""content"": ""{content}"",
      ""type"": ""tool_result""
    }}";

    var toolResultContent = new ToolResultContent
    {
      ToolUseId = toolUseId,
      Content = content
    };

    var actual = Serialize(toolResultContent);

    JsonAssert.Equal(expectedJson, actual);
  }

  [Fact]
  public void JsonSerialization_WhenSerializedWithCacheControl_ItShouldReturnJsonString()
  {
    var toolUseId = Guid.NewGuid().ToString();
    var content = "content";
    var cacheControl = new EphemeralCacheControl();

    var expectedJson = @$"{{
      ""tool_use_id"": ""{toolUseId}"",
      ""content"": ""{content}"",
      ""type"": ""tool_result"",
      ""cache_control"": {{
        ""type"": ""ephemeral""
      }}
    }}";

    var toolResultContent = new ToolResultContent
    {
      ToolUseId = toolUseId,
      Content = content,
      CacheControl = cacheControl
    };

    var actual = Serialize(toolResultContent);

    JsonAssert.Equal(expectedJson, actual);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldReturnToolResultContent()
  {
    var toolUseId = Guid.NewGuid().ToString();
    var content = "content";

    var json = @$"{{
      ""tool_use_id"": ""{toolUseId}"",
      ""content"": ""{content}"",
      ""type"": ""tool_result""
    }}";

    var actual = Deserialize<ToolResultContent>(json);

    actual!.ToolUseId.Should().Be(toolUseId);
    actual.Content.Should().Be(content);
    actual.Type.Should().Be("tool_result");
  }
}