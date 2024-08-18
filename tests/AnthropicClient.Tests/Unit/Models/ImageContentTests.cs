namespace AnthropicClient.Tests.Unit.Models;

public class ImageContentTests : SerializationTest
{
  private readonly string _testJson = @"{
    ""source"": { 
      ""media_type"": ""image/png"",
      ""data"": ""data"",
      ""type"": ""base64""
    },
    ""type"": ""image""
  }";

  private readonly string _testJsonWithCacheControl = @"{
    ""source"": { 
      ""media_type"": ""image/png"",
      ""data"": ""data"",
      ""type"": ""base64""
    },
    ""cache_control"": { ""type"": ""ephemeral"" },
    ""type"": ""image""
  }";

  [Fact]
  public void Constructor_WhenCalled_ItShouldInitializeSource()
  {
    var expectedMediaType = "image/png";
    var expectedData = "data";

    var result = new ImageContent(expectedMediaType, expectedData);

    result.Source.Should().BeEquivalentTo(new ImageSource(expectedMediaType, expectedData));
  }

  [Fact]
  public void Constructor_WhenCalledAndMediaTypeIsNull_ItShouldThrowArgumentNullException()
  {
    var expectedData = "data";

    var action = () => new ImageContent(null!, expectedData);

    action.Should().Throw<ArgumentNullException>();
  }

  [Fact]
  public void Constructor_WhenCalledAndDataIsNull_ItShouldThrowArgumentNullException()
  {
    var expectedMediaType = "image/png";

    var action = () => new ImageContent(expectedMediaType, null!);

    action.Should().Throw<ArgumentNullException>();
  }

  [Fact]
  public void Constructor_WhenCalledWithInvalidMediaType_ItShouldThrowArgumentException()
  {
    var expectedMediaType = "invalid";
    var expectedData = "data";

    var action = () => new ImageContent(expectedMediaType, expectedData);

    action.Should().Throw<ArgumentException>();
  }

  [Fact]
  public void Constructor_WhenCalledWithCacheControl_ItShouldInitializeSourceAndCacheControl()
  {
    var expectedMediaType = "image/png";
    var expectedData = "data";
    var cacheControl = new EphemeralCacheControl();

    var result = new ImageContent(expectedMediaType, expectedData, cacheControl);

    result.Source.Should().BeEquivalentTo(new ImageSource(expectedMediaType, expectedData));
    result.CacheControl.Should().BeSameAs(cacheControl);
  }

  [Fact]
  public void Constructor_WhenCalledWithCacheControlAndMediatTypeIsNull_ItShouldThrowArgumentNullException()
  {
    var expectedData = "data";
    var cacheControl = new EphemeralCacheControl();

    var action = () => new ImageContent(null!, expectedData, cacheControl);

    action.Should().Throw<ArgumentNullException>();
  }

  [Fact]
  public void Constructor_WhenCalledWithCacheControlAndDataIsNull_ItShouldThrowArgumentNullException()
  {
    var expectedMediaType = "image/png";
    var cacheControl = new EphemeralCacheControl();

    var action = () => new ImageContent(expectedMediaType, null!, cacheControl);

    action.Should().Throw<ArgumentNullException>();
  }

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldHaveExpectedShape()
  {
    var content = new ImageContent("image/png", "data");

    var actual = Serialize(content);

    JsonAssert.Equal(_testJson, actual);
  }

  [Fact]
  public void JsonSerialization_WhenSerializedWithCacheControl_ItShouldHaveExpectedShape()
  {
    var content = new ImageContent("image/png", "data", new EphemeralCacheControl());

    var actual = Serialize(content);

    JsonAssert.Equal(_testJsonWithCacheControl, actual);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldHaveExpectedShape()
  {
    var expected = new ImageContent("image/png", "data");

    var actual = Deserialize<ImageContent>(_testJson);

    actual.Should().BeEquivalentTo(expected);
  }
}