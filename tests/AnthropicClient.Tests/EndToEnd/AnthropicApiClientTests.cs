namespace AnthropicClient.Tests.EndToEnd;

public class ClientTests(ConfigurationFixture configFixture) : EndToEndTest(configFixture)
{
  [Fact]
  public async Task CreateChatMessage_WhenCalled_ItShouldReturnChatResponse()
  {
    var request = new ChatMessageRequest(
      model: AnthropicModels.Claude3Haiku,
      messages: [new(MessageRole.User, [new TextContent("Hello!")])]
    );

    var result = await _client.CreateChatMessageAsync(request);

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().BeOfType<ChatResponse>();
  }

  [Fact]
  public async Task CreateChatMessage_WhenCalledWithStreamRequest_ItShouldReturnEvents()
  {
    var request = new StreamChatMessageRequest(
      model: AnthropicModels.Claude3Haiku,
      messages: [new(MessageRole.User, [new TextContent("Hello!")])]
    );

    var response = _client.CreateChatMessageAsync(request);
    
    var events = new List<AnthropicEvent>();

    await foreach (var e in response)
    {
      events.Add(e);
    }

    events.Should().NotBeEmpty();
  }

  [Fact]
  public async Task CreateChatMessage_WhenCalledWithStreamRequest_ItShouldYieldAMessageCompleteEvent()
  {
    var request = new StreamChatMessageRequest(
      model: AnthropicModels.Claude3Haiku,
      messages: [new(MessageRole.User, [new TextContent("Hello!")])]
    );

    var response = _client.CreateChatMessageAsync(request);
  
    await foreach (var e in response)
    {
      if (e.Data is MessageCompleteEventData messageCompleteData)
      {
        messageCompleteData.Message.Should().NotBeNull();
        break;
      }
    }
  }
}