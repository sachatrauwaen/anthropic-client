namespace AnthropicClient.Tests.Unit.Models;

public class MessageCompleteEventDataTests
{
  [Fact]
  public void Constructor_WhenCalled_ItShouldInitializeProperties()
  {
    var expectedMessage = new ChatResponse();
    var expectedHeaders = new AnthropicHeaders();

    var messageCompleteEventData = new MessageCompleteEventData(expectedMessage, expectedHeaders);

    messageCompleteEventData.Message.Should().BeSameAs(expectedMessage);
    messageCompleteEventData.Headers.Should().BeSameAs(expectedHeaders);
  }
}