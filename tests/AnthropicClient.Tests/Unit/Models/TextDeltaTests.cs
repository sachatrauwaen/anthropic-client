namespace AnthropicClient.Tests.Unit.Models;

public class TextDeltaTests : SerializationTest
{
  private readonly string _testJson = @"{
    ""type"": ""text_delta"",
    ""text"": ""Hello World!""
  }";

  [Fact]
  public void Constructor_WhenCalled_ItShouldInitializeProperties()
  {
    var text = "Hello World!";
    
    var textDelta = new TextDelta(text);
    
    textDelta.Type.Should().Be("text_delta");
    textDelta.Text.Should().Be(text);
  }

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldHaveExpectedShape()
  {
    var textDelta = new TextDelta("Hello World!");

    var actual = Serialize(textDelta);
  
    JsonAssert.Equal(_testJson, actual);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldHaveExpectedProperties()
  {
    var expected = new TextDelta("Hello World!");

    var actual = Deserialize<TextDelta>(_testJson);

    actual.Should().BeEquivalentTo(expected);
  }
}