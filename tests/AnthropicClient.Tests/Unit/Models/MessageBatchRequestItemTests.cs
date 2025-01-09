namespace AnthropicClient.Tests.Unit.Models;

public class MessageBatchRequestItemTests : SerializationTest
{
  [Fact]
  public void Constructor_WhenCalled_ItShouldReturnInstanceWithPropertiesSet()
  {
    var customId = "custom_id";
    var messageRequest = new MessageRequest();

    var result = new MessageBatchRequestItem(customId, messageRequest);

    result.Should().BeOfType<MessageBatchRequestItem>();
    result.CustomId.Should().Be(customId);
    result.Params.Should().BeSameAs(messageRequest);
  }

  [Theory]
  [InlineData("")]
  [InlineData(" ")]
  [InlineData(null)]
  public void Constructor_WhenCalledAndCustomIdIsInvalid_ItShouldThrowException(string? customId)
  {
    var act = () => new MessageBatchRequestItem(customId!, new MessageRequest());

    act.Should().Throw<ArgumentException>();
  }

  [Fact]
  public void Constructor_WhenCalledAndMessageRequestIsNull_ItShouldThrowException()
  {
    var act = () => new MessageBatchRequestItem("custom_id", null!);

    act.Should().Throw<ArgumentException>();
  }
}