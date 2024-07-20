namespace AnthropicClient.Tests.EndToEnd;

public class ClientTests(ConfigurationFixture configFixture) : EndToEndTest(configFixture)
{
  [Fact]
  public async Task CreateMessageAsync_WhenCalled_ItShouldReturnResponse()
  {
    var request = new MessageRequest(
      model: AnthropicModels.Claude3Haiku,
      messages: [new(MessageRole.User, [new TextContent("Hello!")])]
    );

    var result = await _client.CreateMessageAsync(request);

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().BeOfType<MessageResponse>();
    result.Value.Content.Should().NotBeNullOrEmpty();
  }

  [Fact]
  public async Task CreateMessageAsync_WhenCalledWithStreamRequest_ItShouldReturnEvents()
  {
    var request = new StreamMessageRequest(
      model: AnthropicModels.Claude3Haiku,
      messages: [new(MessageRole.User, [new TextContent("Hello!")])]
    );

    var response = _client.CreateMessageAsync(request);

    var events = new List<AnthropicEvent>();

    await foreach (var e in response)
    {
      events.Add(e);
    }

    events.Should().NotBeEmpty();
  }

  [Fact]
  public async Task CreateMessageAsync_WhenCalledWithStreamRequest_ItShouldYieldAMessageCompleteEvent()
  {
    var request = new StreamMessageRequest(
      model: AnthropicModels.Claude3Haiku,
      messages: [new(MessageRole.User, [new TextContent("Hello!")])]
    );

    var response = _client.CreateMessageAsync(request);

    await foreach (var e in response)
    {
      if (e.Data is MessageCompleteEventData messageCompleteData)
      {
        messageCompleteData.Message.Should().NotBeNull();
        break;
      }
    }
  }

  [Fact]
  public async Task CreateMessageAsync_WhenImageIsSent_ItShouldReturnResponse()
  {
    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "elephant.jpg");
    var mediaType = "image/jpeg";
    var imageBytes = await File.ReadAllBytesAsync(imagePath);
    var base64Image = Convert.ToBase64String(imageBytes);

    var request = new MessageRequest(
      model: AnthropicModels.Claude3Haiku,
      messages: [
        new(MessageRole.User, [
          new ImageContent(mediaType, base64Image),
          new TextContent("What is in this image?")
        ]),
      ]
    );

    var result = await _client.CreateMessageAsync(request);

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().BeOfType<MessageResponse>();
    result.Value.Content.Should().NotBeNullOrEmpty();

    var text = result.Value.Content.Aggregate("", (acc, content) =>
    {
      if (content is TextContent textContent)
      {
        acc += textContent.Text;
      }

      return acc;
    });

    text.Should().Contain("elephant");
  }
}