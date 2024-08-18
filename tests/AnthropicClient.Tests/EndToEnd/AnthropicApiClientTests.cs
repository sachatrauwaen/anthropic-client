namespace AnthropicClient.Tests.EndToEnd;

public class ClientTests(ConfigurationFixture configFixture) : EndToEndTest(configFixture)
{
  private string GetTestFilePath(string fileName) =>
    Path.Combine(Directory.GetCurrentDirectory(), "Files", fileName);

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
    var imagePath = GetTestFilePath("elephant.jpg");
    var mediaType = "image/jpeg";
    var bytes = await File.ReadAllBytesAsync(imagePath);
    var base64Data = Convert.ToBase64String(bytes);

    var request = new MessageRequest(
      model: AnthropicModels.Claude3Haiku,
      messages: [
        new(MessageRole.User, [
          new ImageContent(mediaType, base64Data),
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

  [Fact]
  public async Task CreateMessageAsync_WhenSystemMessagesContainCacheControl_ItShouldUseCache()
  {
    var httpClient = new HttpClient();
    httpClient.DefaultRequestHeaders.Add("anthropic-beta", "prompt-caching-2024-07-31");

    var client = CreateClient(httpClient);

    var storyPath = GetTestFilePath("story.txt");
    var storyText = await File.ReadAllTextAsync(storyPath);

    var request = new MessageRequest(
      model: AnthropicModels.Claude3Haiku,
      systemMessages: [
        new("You are a helpful assistant who can answer questions about the following text:"),
        new(storyText, new EphemeralCacheControl())
      ],
      messages: [
        new(MessageRole.User, [
          new TextContent("Give me a one sentence summary of this story.")
        ]),
      ]
    );

    var resultOne = await client.CreateMessageAsync(request);

    resultOne.IsSuccess.Should().BeTrue();
    resultOne.Value.Should().BeOfType<MessageResponse>();
    resultOne.Value.Content.Should().NotBeNullOrEmpty();
    resultOne.Value.Usage.Should().Match<Usage>(u => u.CacheCreationInputTokens > 0 || u.CacheReadInputTokens > 0);

    request.Messages.Add(new(MessageRole.Assistant, resultOne.Value.Content));
    request.Messages.Add(new(MessageRole.User, [new TextContent("What is the main theme of this story?")]));

    var resultTwo = await client.CreateMessageAsync(request);

    resultTwo.IsSuccess.Should().BeTrue();
    resultTwo.Value.Should().BeOfType<MessageResponse>();
    resultTwo.Value.Content.Should().NotBeNullOrEmpty();
    resultTwo.Value.Usage.CacheReadInputTokens.Should().BeGreaterThan(0);
  }

  [Fact]
  public async Task CreateMessageAsync_WhenMessagesContainCacheControl_ItShouldUseCache()
  {
    var httpClient = new HttpClient();
    httpClient.DefaultRequestHeaders.Add("anthropic-beta", "prompt-caching-2024-07-31");

    var client = CreateClient(httpClient);

    var storyPath = GetTestFilePath("story.txt");
    var storyText = await File.ReadAllTextAsync(storyPath);

    var request = new MessageRequest(
      model: AnthropicModels.Claude3Haiku,
      messages: [
        new(MessageRole.User, [
          new TextContent("Give me a one sentence summary of this story."),
          new TextContent(storyText, new EphemeralCacheControl())
        ]),
      ]
    );

    var resultOne = await client.CreateMessageAsync(request);

    resultOne.IsSuccess.Should().BeTrue();
    resultOne.Value.Should().BeOfType<MessageResponse>();
    resultOne.Value.Content.Should().NotBeNullOrEmpty();
    resultOne.Value.Usage.Should().Match<Usage>(u => u.CacheCreationInputTokens > 0 || u.CacheReadInputTokens > 0);

    request.Messages.Add(new(MessageRole.Assistant, resultOne.Value.Content));
    request.Messages.Add(new(MessageRole.User, [new TextContent("What is the main theme of this story?")]));

    var resultTwo = await client.CreateMessageAsync(request);

    resultTwo.IsSuccess.Should().BeTrue();
    resultTwo.Value.Should().BeOfType<MessageResponse>();
    resultTwo.Value.Content.Should().NotBeNullOrEmpty();
    resultTwo.Value.Usage.CacheReadInputTokens.Should().BeGreaterThan(0);
  }

  [Fact]
  public async Task CreateMessageAsync_WhenToolsContainCacheControl_ItShouldUseCache()
  {
    var httpClient = new HttpClient();
    httpClient.DefaultRequestHeaders.Add("anthropic-beta", "prompt-caching-2024-07-31");

    var client = CreateClient(httpClient);

    var func = (string ticker) => ticker;

    var tools = Enumerable
      .Range(0, 50)
      .Select(i => Tool.CreateFromFunction($"tool-{i}", $"Tool {i}", func))
      .ToList();

    tools.Last().CacheControl = new EphemeralCacheControl();

    var request = new MessageRequest(
      model: AnthropicModels.Claude3Haiku,
      messages: [
        new(MessageRole.User, [
          new TextContent("Hi could you tell me your name?"),
        ]),
      ],
      tools: tools
    );

    var resultOne = await client.CreateMessageAsync(request);

    resultOne.IsSuccess.Should().BeTrue();
    resultOne.Value.Should().BeOfType<MessageResponse>();
    resultOne.Value.Content.Should().NotBeNullOrEmpty();
    resultOne.Value.Usage.Should().Match<Usage>(u => u.CacheCreationInputTokens > 0 || u.CacheReadInputTokens > 0);

    request.Messages.Add(new(MessageRole.Assistant, resultOne.Value.Content));
    request.Messages.Add(new(MessageRole.User, [new TextContent("Could you tell me the stock price for AAPL?")]));

    var resultTwo = await client.CreateMessageAsync(request);

    resultTwo.IsSuccess.Should().BeTrue();
    resultTwo.Value.Should().BeOfType<MessageResponse>();
    resultTwo.Value.Content.Should().NotBeNullOrEmpty();
    resultTwo.Value.Usage.CacheReadInputTokens.Should().BeGreaterThan(0);
  }
}