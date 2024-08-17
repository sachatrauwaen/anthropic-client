namespace AnthropicClient.Tests.Unit.Models;

public class TextContentTests : SerializationTest
{
  private readonly string _testJson = @"{ ""text"": ""text"", ""type"": ""text"" }";
  private readonly string _testJsonWithCacheControl = @"{ 
    ""text"": ""text"", 
    ""cache_control"": { ""type"": ""ephemeral"" }, 
    ""type"": ""text"" 
  }";

  [Fact]
  public void Constructor_WhenCalled_ItShouldInitializeText()
  {
    var expectedText = "text";

    var result = new TextContent(expectedText);

    result.Text.Should().Be(expectedText);
  }

  [Fact]
  public void Constructor_WhenCalledWithCacheControl_ItShouldInitializeProperties()
  {
    var expectedText = "text";
    var cacheControl = new EphemeralCacheControl();

    var result = new TextContent(expectedText, cacheControl);

    result.Text.Should().Be(expectedText);
    result.CacheControl.Should().BeSameAs(cacheControl);
  }

  [Fact]
  public void Constructor_WhenCalledAndTextIsNull_ItShouldThrowArgumentNullException()
  {
    var action = () => new TextContent(null!);

    action.Should().Throw<ArgumentNullException>();
  }

  [Fact]
  public void Constructor_WhenCalledWithCacheControlAndTextIsNull_ItShouldThrowArgumentNullException()
  {
    var cacheControl = new EphemeralCacheControl();

    var action = () => new TextContent(null!, cacheControl);

    action.Should().Throw<ArgumentNullException>();
  }

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldHaveExpectedShape()
  {
    var content = new TextContent("text");

    var actual = Serialize(content);

    JsonAssert.Equal(_testJson, actual);
  }

  [Fact]
  public void JsonSerialization_WhenSerializedWithCacheControl_ItShouldHaveExpectedShape()
  {
    var content = new TextContent("text", new EphemeralCacheControl());

    var actual = Serialize(content);

    JsonAssert.Equal(_testJsonWithCacheControl, actual);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldHaveExpectedShape()
  {
    var expected = new TextContent("text");

    var actual = Deserialize<TextContent>(_testJson);

    actual.Should().BeEquivalentTo(expected);
  }
}