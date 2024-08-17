namespace AnthropicClient.Tests.Unit.Models;

public class CacheControlTypeTests
{
  [Fact]
  public void Ephemeral_WhenCalled_ItShouldReturnExpectedValue()
  {
    CacheControlType.Ephemeral.Should().Be("ephemeral");
  }
}