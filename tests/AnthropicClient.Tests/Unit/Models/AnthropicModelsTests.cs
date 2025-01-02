namespace AnthropicClient.Tests.Unit.Models;

public class AnthropicModelsTests
{
  [Fact]
  public void Claude3Opus_WhenCalled_ItShouldReturnExpectedValue()
  {
    var expected = "claude-3-opus-20240229";

    var actual = AnthropicModels.Claude3Opus;

    actual.Should().Be(expected);
  }

  [Fact]
  public void Claude3Opus20241022_WhenCalled_ItShouldReturnExpectedValue()
  {
    var expected = "claude-3-opus-20240229";

    var actual = AnthropicModels.Claude3Opus20241022;

    actual.Should().Be(expected);
  }

  [Fact]
  public void Claude3OpusLatest_WhenCalled_ItShouldReturnExpectedValue()
  {
    var expected = "claude-3-opus-latest";

    var actual = AnthropicModels.Claude3OpusLatest;

    actual.Should().Be(expected);
  }

  [Fact]
  public void Claude3Sonnet_WhenCalled_ItShouldReturnExpectedValue()
  {
    var expected = "claude-3-sonnet-20240229";

    var actual = AnthropicModels.Claude3Sonnet;

    actual.Should().Be(expected);
  }

  [Fact]
  public void Claude3Sonnet20240229_WhenCalled_ItShouldReturnExpectedValue()
  {
    var expected = "claude-3-sonnet-20240229";

    var actual = AnthropicModels.Claude3Sonnet20240229;

    actual.Should().Be(expected);
  }

  [Fact]
  public void Claude35Sonnet_WhenCalled_ItShouldReturnExpectedValue()
  {
    var expected = "claude-3-5-sonnet-20240620";

    var actual = AnthropicModels.Claude35Sonnet;

    actual.Should().Be(expected);
  }

  [Fact]
  public void Claude35Sonnet20240620_WhenCalled_ItShouldReturnExpectedValue()
  {
    var expected = "claude-3-5-sonnet-20240620";

    var actual = AnthropicModels.Claude35Sonnet20240620;

    actual.Should().Be(expected);
  }

  [Fact]
  public void Claude35Sonnet20241022_WhenCalled_ItShouldReturnExpectedValue()
  {
    var expected = "claude-3-5-sonnet-20241022";

    var actual = AnthropicModels.Claude35Sonnet20241022;

    actual.Should().Be(expected);
  }

  [Fact]
  public void Claude35SonnetLatest_WhenCalled_ItShouldReturnExpectedValue()
  {
    var expected = "claude-3-5-sonnet-latest";

    var actual = AnthropicModels.Claude35SonnetLatest;

    actual.Should().Be(expected);
  }

  [Fact]
  public void Claude3Haiku_WhenCalled_ItShouldReturnExpectedValue()
  {
    var expected = "claude-3-haiku-20240307";

    var actual = AnthropicModels.Claude3Haiku;

    actual.Should().Be(expected);
  }

  [Fact]
  public void Claude3Haiku20240307_WhenCalled_ItShouldReturnExpectedValue()
  {
    var expected = "claude-3-haiku-20240307";

    var actual = AnthropicModels.Claude3Haiku20240307;

    actual.Should().Be(expected);
  }

  [Fact]
  public void Claude35Haiku_WhenCalled_ItShouldReturnExpectedValue()
  {
    var expected = "claude-3-5-haiku-20241022";

    var actual = AnthropicModels.Claude35Haiku20241022;

    actual.Should().Be(expected);
  }

  [Fact]
  public void Claude35HaikuLatest_WhenCalled_ItShouldReturnExpectedValue()
  {
    var expected = "claude-3-5-haiku-latest";

    var actual = AnthropicModels.Claude35HaikuLatest;

    actual.Should().Be(expected);
  }

  [Theory]
  [InlineData("claude-3-opus-20240229", true)]
  [InlineData("claude-3-opus-latest", true)]
  [InlineData("claude-3-sonnet-20240229", true)]
  [InlineData("claude-3-5-sonnet-20240620", true)]
  [InlineData("claude-3-5-sonnet-20241022", true)]
  [InlineData("claude-3-5-sonnet-latest", true)]
  [InlineData("claude-3-haiku-20240307", true)]
  [InlineData("claude-3-5-haiku-20241022", true)]
  [InlineData("claude-3-5-haiku-latest", true)]
  [InlineData("invalid", false)]
  public void IsValidModel_WhenCalled_ItShouldReturnExpectedValue(string modelId, bool expected)
  {
    var actual = AnthropicModels.IsValidModel(modelId);

    actual.Should().Be(expected);
  }
}