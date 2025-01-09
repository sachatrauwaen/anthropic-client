namespace AnthropicClient.Tests.Unit.Models;

public class MessageBatchStatusTests
{
  [Fact]
  public void Canceling_WhenCalled_ItShouldReturnCancelingStatus()
  {
    MessageBatchStatus.Canceling.Should().Be("canceling");
  }
  
  [Fact]
  public void InProgress_WhenCalled_ItShouldReturnCancelingStatus()
  {
    MessageBatchStatus.InProgress.Should().Be("in_progress");
  }
  
  [Fact]
  public void Ended_WhenCalled_ItShouldReturnCancelingStatus()
  {
    MessageBatchStatus.Ended.Should().Be("ended");
  }
}