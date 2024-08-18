namespace AnthropicClient.Tests.Unit.Models;

public class UsageTests : SerializationTest
{
  [Fact]
  public void Constructor_WhenCalled_ShouldInitializeProperties()
  {
    var expectedInputTokens = 1;
    var expectedOutputTokens = 2;
    var expectedCacheCreationInputTokens = 3;
    var expectedCacheReadInputTokens = 4;

    var usage = new Usage
    {
      InputTokens = expectedInputTokens,
      OutputTokens = expectedOutputTokens,
      CacheCreationInputTokens = expectedCacheCreationInputTokens,
      CacheReadInputTokens = expectedCacheReadInputTokens
    };

    usage.InputTokens.Should().Be(expectedInputTokens);
    usage.OutputTokens.Should().Be(expectedOutputTokens);
    usage.CacheCreationInputTokens.Should().Be(expectedCacheCreationInputTokens);
    usage.CacheReadInputTokens.Should().Be(expectedCacheReadInputTokens);
  }

  [Fact]
  public void JsonSerialization_WhenCalled_ItShouldSerializeCorrectly()
  {
    var expectedJson = @"{ 
      ""input_tokens"": 1, 
      ""output_tokens"": 2,
      ""cache_creation_input_tokens"": 3,
      ""cache_read_input_tokens"": 4
    }";

    var usage = new Usage
    {
      InputTokens = 1,
      OutputTokens = 2,
      CacheCreationInputTokens = 3,
      CacheReadInputTokens = 4
    };

    var actual = Serialize(usage);

    JsonAssert.Equal(expectedJson, actual);
  }

  [Fact]
  public void JsonDeserialization_WhenCalled_ItShouldDeserializeCorrectly()
  {
    var json = @"{ 
      ""input_tokens"": 1, 
      ""output_tokens"": 2,
      ""cache_creation_input_tokens"": 3,
      ""cache_read_input_tokens"": 4
    }";

    var usage = Deserialize<Usage>(json);

    usage!.InputTokens.Should().Be(1);
    usage.OutputTokens.Should().Be(2);
    usage.CacheCreationInputTokens.Should().Be(3);
    usage.CacheReadInputTokens.Should().Be(4);
  }
}