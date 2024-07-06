namespace AnthropicClient.Tests.Unit.Models;

public class UsageTests : SerializationTest
{
  [Fact]
  public void Constructor_WhenCalled_ShouldInitializeProperties()
  {
    var expectedInputTokens = 1;
    var expectedOutputTokens = 2;

    var usage = new Usage
    {
      InputTokens = expectedInputTokens,
      OutputTokens = expectedOutputTokens
    };

    usage.InputTokens.Should().Be(expectedInputTokens);
    usage.OutputTokens.Should().Be(expectedOutputTokens);
  }

  [Fact]
  public void JsonSerialization_WhenCalled_ItShouldSerializeCorrectly()
  {
    var expectedJson = @"{ ""input_tokens"": 1, ""output_tokens"": 2 }";

    var usage = new Usage
    {
      InputTokens = 1,
      OutputTokens = 2
    };

    var actual = Serialize(usage);

    JsonAssert.Equal(expectedJson, actual);
  }

  [Fact]
  public void JsonDeserialization_WhenCalled_ItShouldDeserializeCorrectly()
  {
    var json = @"{ ""input_tokens"": 1, ""output_tokens"": 2 }";

    var usage = Deserialize<Usage>(json);

    usage!.InputTokens.Should().Be(1);
    usage.OutputTokens.Should().Be(2);
  }
}