namespace AnthropicClient.Tests.Unit.Models;

public class ToolUseContentTests : SerializationTest
{
  [Fact]
  public void Constructor_WhenCalled_ItShouldInitializeProperties()
  {
    var id = Guid.NewGuid().ToString();
    var name = "name";
    var input = new Dictionary<string, object?> { { "name", "input" } };

    var actual = new ToolUseContent()
    {
      Id = id,
      Name = name,
      Input = input
    };

    actual.Id.Should().Be(id);
    actual.Name.Should().Be(name);
    actual.Input.Should().BeEquivalentTo(input);
    actual.Type.Should().Be("tool_use");
  }

  [Fact]
  public void CacheControl_WhenCalledToSetCacheControl_ItShouldSetCacheControl()
  {
    var id = Guid.NewGuid().ToString();
    var name = "name";
    var input = new Dictionary<string, object?> { { "name", "input" } };
    var cacheControl = new EphemeralCacheControl();

    var toolUseContent = new ToolUseContent()
    {
      Id = id,
      Name = name,
      Input = input
    };

    toolUseContent.CacheControl = cacheControl;

    toolUseContent.CacheControl.Should().BeSameAs(cacheControl);
  }

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldReturnJsonString()
  {
    var id = Guid.NewGuid().ToString();
    var name = "name";
    var input = new Dictionary<string, object?> { { "name", "input" } };

    var expectedJson = @$"{{
      ""id"": ""{id}"",
      ""name"": ""{name}"",
      ""input"": {{ ""name"": ""input"" }},
      ""type"": ""tool_use""
    }}";

    var toolUseContent = new ToolUseContent()
    {
      Id = id,
      Name = name,
      Input = input
    };

    var actual = Serialize(toolUseContent);

    JsonAssert.Equal(expectedJson, actual);
  }

  [Fact]
  public void JsonSerialization_WhenSerializedWithCacheControl_ItShouldReturnJsonString()
  {
    var id = Guid.NewGuid().ToString();
    var name = "name";
    var input = new Dictionary<string, object?> { { "name", "input" } };
    var cacheControl = new EphemeralCacheControl();

    var expectedJson = @$"{{
      ""id"": ""{id}"",
      ""name"": ""{name}"",
      ""input"": {{ ""name"": ""input"" }},
      ""cache_control"": {{ ""type"": ""ephemeral"" }},
      ""type"": ""tool_use""
    }}";

    var toolUseContent = new ToolUseContent()
    {
      Id = id,
      Name = name,
      Input = input,
      CacheControl = cacheControl
    };

    var actual = Serialize(toolUseContent);

    JsonAssert.Equal(expectedJson, actual);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldReturnToolUseContent()
  {
    var id = Guid.NewGuid().ToString();
    var name = "name";
    var input = new Dictionary<string, object?> { { "name", "input" } };

    var json = @$"{{
      ""id"": ""{id}"",
      ""name"": ""{name}"",
      ""input"": {{ ""name"": ""input"" }},
      ""type"": ""tool_use""
    }}";

    var expected = new ToolUseContent()
    {
      Id = id,
      Name = name,
      Input = input
    };

    var actual = Deserialize<ToolUseContent>(json);

    actual!.Id.Should().Be(expected.Id);
    actual.Name.Should().Be(expected.Name);

    var actualInput = actual.Input.GetValueOrDefault("name")!.ToString();
    var expectedInput = expected.Input.GetValueOrDefault("name")!.ToString();
    actualInput.Should().Be(expectedInput);

    actual.Type.Should().Be(expected.Type);
  }
}