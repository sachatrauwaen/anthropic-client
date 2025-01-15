namespace AnthropicClient.Tests.Unit.Models;

public class MessageBatchResultItemTests
{
  [Fact]
  public void Constructor_WhenCalled_ItShouldReturnInstanceWithPropertiesSet()
  {
    var result = new MessageBatchResultItem();

    result.Should().BeOfType<MessageBatchResultItem>();
    result.CustomId.Should().BeEmpty();
    result.Result.Should().Be(default);
  }
}