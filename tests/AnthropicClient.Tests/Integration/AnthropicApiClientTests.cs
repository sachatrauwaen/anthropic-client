namespace AnthropicClient.Tests.Integration;

public class AnthropicApiClientTests : IntegrationTest
{
  [Theory]
  [ClassData(typeof(ErrorTestData))]
  public async Task CreateChatMessageAsync_WhenCalledAndErrorReturned_ItShouldHandleError(
    HttpStatusCode statusCode,
    string content,
    Type errorType
  )
  {
    _mockHttpMessageHandler
      .WhenCreateMessageRequest()
      .Respond(
        statusCode,
        "application/json",
        content
      );

    var request = new ChatMessageRequest(
      model: AnthropicModels.Claude3Haiku,
      messages: [new(MessageRole.User, [new TextContent("Hello!")])]
    );

    var result = await Client.CreateChatMessageAsync(request);

    result.IsSuccess.Should().BeFalse();
    result.Error.Should().BeOfType<AnthropicError>();
    
    var actualErrorType = result.Error.Error!.GetType();
    actualErrorType.Should().Be(errorType);
  }

  [Fact]
  public async Task CreateChatMessageAsync_WhenCalledAndMessageCreatedWithTextContent_ItShouldReturnMessage()
  {
    _mockHttpMessageHandler
      .WhenCreateMessageRequest()
      .Respond(
        HttpStatusCode.OK,
        "application/json",
        @"{
            ""content"": [
              {
                ""text"": ""Hi! My name is Claude."",
                ""type"": ""text""
              }
            ],
            ""id"": ""msg_013Zva2CMHLNnXjNJJKqJ2EF"",
            ""model"": ""claude-3-5-sonnet-20240620"",
            ""role"": ""assistant"",
            ""stop_reason"": ""end_turn"",
            ""stop_sequence"": null,
            ""type"": ""message"",
            ""usage"": {
              ""input_tokens"": 10,
              ""output_tokens"": 25
            }
        }"
      );

    var request = new ChatMessageRequest(
      model: AnthropicModels.Claude35Sonnet,
      messages: [new(MessageRole.User, [new TextContent("Hello!")])]
    );

    var result = await Client.CreateChatMessageAsync(request);

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().BeOfType<ChatResponse>();
    result.Error.Should().BeNull();

    var message = result.Value;
    message.Id.Should().Be("msg_013Zva2CMHLNnXjNJJKqJ2EF");
    message.Model.Should().Be("claude-3-5-sonnet-20240620");
    message.Role.Should().Be("assistant");
    message.StopReason.Should().Be("end_turn");
    message.StopSequence.Should().BeNull();
    message.Type.Should().Be("message");
    message.Usage.InputTokens.Should().Be(10);
    message.Usage.OutputTokens.Should().Be(25);
    message.Content.Should().HaveCount(1);
    message.ToolCall.Should().BeNull();
    
    var textContent = message.Content[0];
    textContent.Should().BeOfType<TextContent>();
    textContent.As<TextContent>().Text.Should().Be("Hi! My name is Claude.");
    textContent.As<TextContent>().Type.Should().Be("text");
  }

  [Fact]
  public async Task CreateChatMessageAsync_WhenCalledAndMessageCreatedWithToolUseContentAndToolIsProvided_ItShouldReturnMessageWithToolCall()
  {
    _mockHttpMessageHandler
      .WhenCreateMessageRequest()
      .Respond(
        HttpStatusCode.OK,
        "application/json",
        @"{
            ""content"": [
              {
                ""id"": ""toolu_01D7FLrfh4GYq7yT1ULFeyMV"",
                ""name"": ""get_stock_price"",
                ""input"": { ""ticker"": ""^GSPC"" },
                ""type"": ""tool_use""
              }
            ],
            ""id"": ""msg_01D7FLrfh4GYq7yT1ULFeyMV"",
            ""model"": ""claude-3-5-sonnet-20240620"",
            ""role"": ""assistant"",
            ""stop_reason"": ""end_turn"",
            ""stop_sequence"": null,
            ""type"": ""message"",
            ""usage"": {
              ""input_tokens"": 10,
              ""output_tokens"": 25
            }
        }"
      );

    var func = (string ticker) => ticker;

    var request = new ChatMessageRequest(
      model: AnthropicModels.Claude35Sonnet,
      messages: [
        new(MessageRole.User, [new TextContent("Hello!")]),
      ],
      tools: [
        Tool.CreateFromFunction("get_stock_price", "Gets the stock price for a given ticker", func)
      ]
    );

    var result = await Client.CreateChatMessageAsync(request);

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().BeOfType<ChatResponse>();
    result.Error.Should().BeNull();

    var message = result.Value;
    message.Id.Should().Be("msg_01D7FLrfh4GYq7yT1ULFeyMV");
    message.Model.Should().Be("claude-3-5-sonnet-20240620");
    message.Role.Should().Be("assistant");
    message.StopReason.Should().Be("end_turn");
    message.StopSequence.Should().BeNull();
    message.Type.Should().Be("message");
    message.Usage.InputTokens.Should().Be(10);
    message.Usage.OutputTokens.Should().Be(25);
    message.Content.Should().HaveCount(1);
    message.ToolCall.Should().NotBeNull();

    var toolUseContent = message.Content[0];
    toolUseContent.Should().BeOfType<ToolUseContent>();

    toolUseContent.As<ToolUseContent>().Id.Should().Be("toolu_01D7FLrfh4GYq7yT1ULFeyMV");
    toolUseContent.As<ToolUseContent>().Name.Should().Be("get_stock_price");
    toolUseContent.As<ToolUseContent>().Type.Should().Be("tool_use");

    var input = toolUseContent.As<ToolUseContent>().Input;
    var ticker = input.GetValueOrDefault("ticker");
    ticker!.ToString().Should().Be("^GSPC");

    var toolCallResult = await message.ToolCall!.InvokeAsync();
    toolCallResult.IsSuccess.Should().BeTrue();
    toolCallResult.Value!.ToString().Should().Be("^GSPC");
    toolCallResult.Error.Should().BeNull();
  }


  [Fact]
  public async Task CreateChatMessageAsync_WhenCalledAndMessageCreatedWithToolUseButNoToolProvided_ItShouldReturnMessageWithoutToolCall()
  {
    _mockHttpMessageHandler
      .WhenCreateMessageRequest()
      .Respond(
        HttpStatusCode.OK,
        "application/json",
        @"{
            ""content"": [
              {
                ""id"": ""toolu_01D7FLrfh4GYq7yT1ULFeyMV"",
                ""name"": ""get_stock_price"",
                ""input"": { ""ticker"": ""^GSPC"" },
                ""type"": ""tool_use""
              }
            ],
            ""id"": ""msg_01D7FLrfh4GYq7yT1ULFeyMV"",
            ""model"": ""claude-3-5-sonnet-20240620"",
            ""role"": ""assistant"",
            ""stop_reason"": ""end_turn"",
            ""stop_sequence"": null,
            ""type"": ""message"",
            ""usage"": {
              ""input_tokens"": 10,
              ""output_tokens"": 25
            }
        }"
      );

    var request = new ChatMessageRequest(
      model: AnthropicModels.Claude35Sonnet,
      messages: [
        new(MessageRole.User, [new TextContent("Hello!")]),
      ]
    );

    var result = await Client.CreateChatMessageAsync(request);

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().BeOfType<ChatResponse>();
    result.Error.Should().BeNull();

    var message = result.Value;
    message.Id.Should().Be("msg_01D7FLrfh4GYq7yT1ULFeyMV");
    message.Model.Should().Be("claude-3-5-sonnet-20240620");
    message.Role.Should().Be("assistant");
    message.StopReason.Should().Be("end_turn");
    message.StopSequence.Should().BeNull();
    message.Type.Should().Be("message");
    message.Usage.InputTokens.Should().Be(10);
    message.Usage.OutputTokens.Should().Be(25);
    message.Content.Should().HaveCount(1);
    message.ToolCall.Should().BeNull();

    var toolUseContent = message.Content[0];
    toolUseContent.Should().BeOfType<ToolUseContent>();

    toolUseContent.As<ToolUseContent>().Id.Should().Be("toolu_01D7FLrfh4GYq7yT1ULFeyMV");
    toolUseContent.As<ToolUseContent>().Name.Should().Be("get_stock_price");
    toolUseContent.As<ToolUseContent>().Type.Should().Be("tool_use");

    var input = toolUseContent.As<ToolUseContent>().Input;
    var ticker = input.GetValueOrDefault("ticker");
    ticker!.ToString().Should().Be("^GSPC");
  }

  [Theory]
  [ClassData(typeof(EventTestData))]
  public async Task CreateChatMessageAsync_WhenCalledAndMessageIsStreamed_ItShouldReturnAllEvents(string eventString, AnthropicEvent anthropicEvent)
  {
    _mockHttpMessageHandler
      .WhenCreateStreamMessageRequest()
      .Respond(
        HttpStatusCode.OK,
        "text/event-stream",
        new MemoryStream(Encoding.UTF8.GetBytes(eventString))
      );

    var request = new StreamChatMessageRequest(
      model: AnthropicModels.Claude35Sonnet,
      messages: [
        new(MessageRole.User, [new TextContent("Hello!")]),
      ]
    );

    var result = Client.CreateChatMessageAsync(request);
    var e = await result.FirstOrDefaultAsync();

    e.Should().BeEquivalentTo(anthropicEvent);
  }
}