namespace AnthropicClient.Tests.Unit.Models;

public class MessageBatchResultTypeTests
{
  [Fact]
  public void Succeeded_WhenCalled_ItShouldReturnExpectedValue()
  {
    var result = MessageBatchResultType.Succeeded;

    result.Should().Be("succeeded");
  }

  [Fact]
  public void Errored_WhenCalled_ItShouldReturnExpectedValue()
  {
    var result = MessageBatchResultType.Errored;

    result.Should().Be("errored");
  }

  [Fact]
  public void Canceled_WhenCalled_ItShouldReturnExpectedValue()
  {
    var result = MessageBatchResultType.Canceled;

    result.Should().Be("canceled");
  }

  [Fact]
  public void Expired_WhenCalled_ItShouldReturnExpectedValue()
  {
    var result = MessageBatchResultType.Expired;

    result.Should().Be("expired");
  }
}