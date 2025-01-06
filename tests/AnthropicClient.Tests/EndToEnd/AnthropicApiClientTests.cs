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
    var client = CreateClient(new HttpClient());

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

    var resultOne = await _client.CreateMessageAsync(request);

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
    var client = CreateClient(new HttpClient());

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
    var client = CreateClient(new HttpClient());

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

  [Fact]
  public async Task CreateMessageAsync_WhenProvidedWithPDF_ItShouldReturnResponse()
  {
    var pdfPath = GetTestFilePath("addendum.pdf");
    var bytes = await File.ReadAllBytesAsync(pdfPath);
    var base64Data = Convert.ToBase64String(bytes);

    var request = new MessageRequest(
      model: AnthropicModels.Claude35Sonnet,
      messages: [
        new(MessageRole.User, [new TextContent("What is the title of this paper?")]),
        new(MessageRole.User, [new DocumentContent("application/pdf", base64Data)])
      ]
    );

    var client = CreateClient(new HttpClient());

    var result = await client.CreateMessageAsync(request);

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

    text.Should().Contain("Model Card Addendum: Claude 3.5 Haiku and Upgraded Claude 3.5 Sonnet");
  }

  [Fact]
  public async Task CreateMessageAsync_WhenProvidedWithPDFWithCacheControl_ItShouldUseCache()
  {
    var pdfPath = GetTestFilePath("addendum.pdf");
    var bytes = await File.ReadAllBytesAsync(pdfPath);
    var base64Data = Convert.ToBase64String(bytes);

    var client = CreateClient(new HttpClient());

    var request = new MessageRequest(
      model: AnthropicModels.Claude35Sonnet,
      messages: [
        new(MessageRole.User, [
          new DocumentContent("application/pdf", base64Data, new EphemeralCacheControl()),
          new TextContent("What is the title of this paper?")
        ]),
      ]
    );

    var resultOne = await client.CreateMessageAsync(request);

    resultOne.IsSuccess.Should().BeTrue();
    resultOne.Value.Should().BeOfType<MessageResponse>();
    resultOne.Value.Content.Should().NotBeNullOrEmpty();
    resultOne.Value.Usage.Should().Match<Usage>(u => u.CacheCreationInputTokens > 0 || u.CacheReadInputTokens > 0);

    request.Messages.Add(new(MessageRole.Assistant, resultOne.Value.Content));
    request.Messages.Add(new(MessageRole.User, [new TextContent("What is the main theme of this paper?")]));

    var resultTwo = await client.CreateMessageAsync(request);

    resultTwo.IsSuccess.Should().BeTrue();
    resultTwo.Value.Should().BeOfType<MessageResponse>();
    resultTwo.Value.Content.Should().NotBeNullOrEmpty();
    resultTwo.Value.Usage.CacheReadInputTokens.Should().BeGreaterThan(0);
  }

  [Fact]
  public async Task CountMessageTokensAsync_WhenCalled_ItShouldReturnResponse()
  {
    var request = new CountMessageTokensRequest(
      model: AnthropicModels.Claude3Haiku,
      messages: [
        new(MessageRole.User, [new TextContent("Hello!")])
      ]
    );

    var result = await _client.CountMessageTokensAsync(request);

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().BeOfType<TokenCountResponse>();
    result.Value.InputTokens.Should().BeGreaterThan(0);
  }

  [Fact]
  public async Task ListModelsAsync_WhenCalled_ItShouldReturnResponse()
  {
    var result = await _client.ListModelsAsync();

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().BeOfType<Page<AnthropicModel>>();
    result.Value.Data.Should().HaveCountGreaterThan(0);
  }

  [Fact]
  public async Task ListModelsAsync_WhenCalledWithPagination_ItShouldReturnResponse()
  {
    var result = await _client.ListModelsAsync(new PagingRequest(limit: 1));

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().BeOfType<Page<AnthropicModel>>();
    result.Value.Data.Should().HaveCount(1);
  }

  [Fact]
  public async Task ListAllModelsAsync_WhenCalled_ItShouldReturnResponse()
  {
    var responses = await _client.ListAllModelsAsync(limit: 1).ToListAsync();

    responses.Should().HaveCountGreaterThan(0);
  }
}