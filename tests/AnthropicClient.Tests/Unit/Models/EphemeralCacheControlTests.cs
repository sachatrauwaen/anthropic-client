namespace AnthropicClient.Tests.Unit.Models;

public class EphemeralCacheControlTests
{
  [Fact]
  public void Constructor_WhenCalled_ItShouldInitializeType()
  {
    new EphemeralCacheControl().Type.Should().Be(CacheControlType.Ephemeral);
  }
}