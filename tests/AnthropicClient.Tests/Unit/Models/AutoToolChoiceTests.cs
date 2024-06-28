namespace AnthropicClient.Tests.Unit.Models;

public class AutoToolChoiceTests : SerializationTest
{
  private readonly string _testJson = @"{
    ""type"": ""auto""
  }";

  [Fact]
  public void Constructor_WhenCalled_ItShouldSetTypeToAuto()
  {
    var expected = "auto";

    var actual = new AutoToolChoice();

    actual.Type.Should().Be(expected);
  }

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldHaveExpectedShape()
  {
    var choice = new AutoToolChoice();

    var actual = Serialize(choice);

    JsonAssert.Equal(_testJson, actual);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldHaveExpectedShape()
  {
    var expected = new AutoToolChoice();

    var actual = Deserialize<AutoToolChoice>(_testJson);

    actual.Should().BeEquivalentTo(expected);
  }
}