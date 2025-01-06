namespace AnthropicClient.Tests.Integration;

public class AnthropicApiClientTests : IntegrationTest
{
  [Theory]
  [ClassData(typeof(ErrorTestData))]
  public async Task CreateMessageAsync_WhenCalledAndErrorReturned_ItShouldHandleError(
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

    var request = new MessageRequest(
      model: AnthropicModels.Claude3Haiku,
      messages: [new(MessageRole.User, [new TextContent("Hello!")])]
    );

    var result = await Client.CreateMessageAsync(request);

    result.IsSuccess.Should().BeFalse();
    result.Error.Should().BeOfType<AnthropicError>();

    var actualErrorType = result.Error.Error!.GetType();
    actualErrorType.Should().Be(errorType);
  }

  [Fact]
  public async Task CreateMessageAsync_WhenCalledAndMessageCreatedWithTextContent_ItShouldReturnMessage()
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

    var request = new MessageRequest(
      model: AnthropicModels.Claude35Sonnet,
      messages: [new(MessageRole.User, [new TextContent("Hello!")])]
    );

    var result = await Client.CreateMessageAsync(request);

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().BeOfType<MessageResponse>();

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
  public async Task CreateMessageAsync_WhenCalledAndMessageCreatedWithToolUseContentAndToolIsProvided_ItShouldReturnMessageWithToolCall()
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

    var request = new MessageRequest(
      model: AnthropicModels.Claude35Sonnet,
      messages: [
        new(MessageRole.User, [new TextContent("Hello!")]),
      ],
      tools: [
        Tool.CreateFromFunction("get_stock_price", "Gets the stock price for a given ticker", func)
      ]
    );

    var result = await Client.CreateMessageAsync(request);

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().BeOfType<MessageResponse>();

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
  }


  [Fact]
  public async Task CreateMessageAsync_WhenCalledAndMessageCreatedWithToolUseButNoToolProvided_ItShouldReturnMessageWithoutToolCall()
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

    var request = new MessageRequest(
      model: AnthropicModels.Claude35Sonnet,
      messages: [
        new(MessageRole.User, [new TextContent("Hello!")]),
      ]
    );

    var result = await Client.CreateMessageAsync(request);

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().BeOfType<MessageResponse>();

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
  public async Task CreateMessageAsync_WhenCalledAndMessageIsStreamed_ItShouldHandleAllEventTypes(string eventString, AnthropicEvent anthropicEvent)
  {
    _mockHttpMessageHandler
      .WhenCreateStreamMessageRequest()
      .Respond(
        HttpStatusCode.OK,
        "text/event-stream",
        new MemoryStream(Encoding.UTF8.GetBytes(eventString))
      );

    var request = new StreamMessageRequest(
      model: AnthropicModels.Claude35Sonnet,
      messages: [
        new(MessageRole.User, [new TextContent("Hello!")]),
      ]
    );

    var result = Client.CreateMessageAsync(request);
    var e = await result.FirstOrDefaultAsync();

    e.Should().BeEquivalentTo(anthropicEvent);
  }

  [Fact]
  public async Task CreateMessageAsync_WhenCalledAndMessageIsStreamed_ItShouldReturnAllExpectedEvents()
  {
    var eventStream = EventTestData.GetEventStream();
    var events = EventTestData.GetAllEvents();

    _mockHttpMessageHandler
      .WhenCreateStreamMessageRequest()
      .Respond(
        HttpStatusCode.OK,
        "text/event-stream",
        eventStream
      );

    var request = new StreamMessageRequest(
      model: AnthropicModels.Claude35Sonnet,
      messages: [
        new(MessageRole.User, [new TextContent("Hello!")]),
      ]
    );

    var result = Client.CreateMessageAsync(request);

    var actualEvents = await result.ToListAsync();

    actualEvents.Should().BeEquivalentTo(events);
  }

  [Fact]
  public async Task CreateMessageAsync_WhenCalledMessageIsStreamedAndToolProvide_ItShouldReturnToolCall()
  {
    var eventStream = EventTestData.GetEventStream();

    _mockHttpMessageHandler
      .WhenCreateStreamMessageRequest()
      .Respond(
        HttpStatusCode.OK,
        "text/event-stream",
        eventStream
      );

    var getWeather = (string location, string unit) => $"The weather in {location} is 72°{unit}";

    var request = new StreamMessageRequest(
      model: AnthropicModels.Claude35Sonnet,
      messages: [
        new(MessageRole.User, [new TextContent("Hello!")]),
      ],
      tools: [
        Tool.CreateFromFunction("get_weather", "Gets the weather for a location", getWeather)
      ]
    );

    var result = Client.CreateMessageAsync(request);
    var msgCompleteEvent = await result
      .Where(e => e.Type is EventType.MessageComplete)
      .FirstAsync();

    msgCompleteEvent.Data.Should().BeOfType<MessageCompleteEventData>();

    var toolCall = msgCompleteEvent.Data.As<MessageCompleteEventData>().Message.ToolCall;
    var toolCallResult = await toolCall!.InvokeAsync<string>();

    toolCallResult.IsSuccess.Should().BeTrue();
    toolCallResult.Value.Should().Be(getWeather("San Francisco, CA", "fahrenheit"));
  }

  [Fact]
  public async Task CreateMessageAsync_WhenCalledMessageIsStreamAndRequestFails_ItShouldReturnErrorEvent()
  {
    _mockHttpMessageHandler
      .WhenCreateStreamMessageRequest()
      .Respond(
        HttpStatusCode.BadRequest,
        "application/json",
        @"{
            ""type"": ""error"",
            ""error"": {
              ""type"": ""invalid_request_error"",
              ""message"": ""messages: roles must alternate between user and assistant, but found multiple user roles in a row""
            }
          }"
      );

    var request = new StreamMessageRequest(
      model: AnthropicModels.Claude35Sonnet,
      messages: [
        new(MessageRole.User, [new TextContent("Hello!")]),
        new(MessageRole.User, [new TextContent("Hello!")])
      ]
    );

    var result = Client.CreateMessageAsync(request);
    var events = await result.ToListAsync();

    events.Should().HaveCount(1);
    events[0].Type.Should().Be(EventType.Error);
    events[0].Data.Should().BeOfType<ErrorEventData>();
    events[0].Data.Should().BeEquivalentTo(new ErrorEventData(
      new InvalidRequestError(
        "messages: roles must alternate between user and assistant, but found multiple user roles in a row"
      )
    ));
  }

  [Fact]
  public async Task CreateMessageAsync_WhenCalledAndMessageCreatedWithDocumentContent_ItShouldReturnMessage()
  {
    _mockHttpMessageHandler
      .WhenCreateMessageRequest()
      .Respond(
        HttpStatusCode.OK,
        "application/json",
        @"{
            ""content"": [
              {
                ""text"": ""It is a PDF"",
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

    var request = new MessageRequest(
      model: AnthropicModels.Claude35Sonnet,
      messages: [
        new(MessageRole.User, [new TextContent("What is this?")]),
        new(MessageRole.User, [new DocumentContent("application/pdf", "data")])
      ]
    );

    var result = await Client.CreateMessageAsync(request);

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().BeOfType<MessageResponse>();

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
    textContent.As<TextContent>().Text.Should().Be("It is a PDF");
    textContent.As<TextContent>().Type.Should().Be("text");
  }

  [Fact]
  public async Task CountMessageTokensAsync_WhenCalled_ItShouldReturnCountTokensResponse()
  {
    _mockHttpMessageHandler
      .WhenCountMessageTokensRequest()
      .Respond(
        HttpStatusCode.OK,
        "application/json",
        @"{
            ""input_tokens"": 10
          }"
      );

    var request = new CountMessageTokensRequest(
      model: AnthropicModels.Claude35Sonnet,
      messages: [
        new(MessageRole.User, [new TextContent("Hello!")]),
      ]
    );

    var result = await Client.CountMessageTokensAsync(request);

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().BeOfType<TokenCountResponse>();
    result.Value.InputTokens.Should().Be(10);
  }

  [Fact]
  public async Task CountMessageTokensAsync_WhenCalledAndErrorReturned_ItShouldHandleError()
  {
    _mockHttpMessageHandler
      .WhenCountMessageTokensRequest()
      .Respond(
        HttpStatusCode.BadRequest,
        "application/json",
        @"{
            ""type"": ""error"",
            ""error"": {
              ""type"": ""invalid_request_error"",
              ""message"": ""messages: roles must alternate between user and assistant, but found multiple user roles in a row""
            }
          }"
      );

    var request = new CountMessageTokensRequest(
      model: AnthropicModels.Claude35Sonnet,
      messages: [
        new(MessageRole.User, [new TextContent("Hello!")]),
        new(MessageRole.User, [new TextContent("Hello!")])
      ]
    );

    var result = await Client.CountMessageTokensAsync(request);

    result.IsSuccess.Should().BeFalse();
    result.Error.Should().BeOfType<AnthropicError>();
    result.Error.Error.Should().BeOfType<InvalidRequestError>();
  }

  [Fact]
  public async Task CountMessageTokensAsync_WhenCalledRequestFailsAndCanNotDeserializeError_ItShouldReturnUnknownError()
  {
    _mockHttpMessageHandler
      .WhenCountMessageTokensRequest()
      .Respond(
        HttpStatusCode.BadRequest,
        "application/json",
        @"{}"
      );

    var request = new CountMessageTokensRequest(
      model: AnthropicModels.Claude35Sonnet,
      messages: [
        new(MessageRole.User, [new TextContent("Hello!")]),
        new(MessageRole.User, [new TextContent("Hello!")])
      ]
    );

    var result = await Client.CountMessageTokensAsync(request);

    result.IsSuccess.Should().BeFalse();
    result.Error.Should().BeOfType<AnthropicError>();
    result.Error.Error.Should().BeOfType<ApiError>();
  }

  [Fact]
  public async Task ListModelsAsync_WhenCalledWithPagingRequestUsingDefaultValues_ItShouldReturnListOfModels()
  {
    _mockHttpMessageHandler
      .WhenListModelsRequest()
      .Respond(
        HttpStatusCode.OK,
        "application/json",
        @"{
            ""data"": [
              {
                ""type"": ""model"",
                ""id"": ""claude-3-5-sonnet-20241022"",
                ""display_name"": ""Claude 3.5 Sonnet (New)"",
                ""created_at"": ""2024-10-22T00:00:00Z""
              }
            ],
            ""has_more"": true,
            ""first_id"": ""first_id"",
            ""last_id"": ""last_id""
          }"
      );

    var result = await Client.ListModelsAsync();

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().BeOfType<Page<AnthropicModel>>();
    result.Value.HasMore.Should().BeTrue();
    result.Value.FirstId.Should().Be("first_id");
    result.Value.LastId.Should().Be("last_id");
    result.Value.Data.Should().BeEquivalentTo(new AnthropicModel[]
    {
      new()
      {
        Type = "model",
        Id = "claude-3-5-sonnet-20241022",
        DisplayName = "Claude 3.5 Sonnet (New)",
        CreatedAt = DateTimeOffset.Parse("2024-10-22T00:00:00Z")
      }
    });
  }

  [Fact]
  public async Task ListModelsAsync_WhenCalledWithPagingRequestUsingCustomValues_ItShouldReturnListOfModels()
  {
    var pagingRequest = new PagingRequest(afterId: "next_id", limit: 10);

    _mockHttpMessageHandler
      .WhenListModelsRequest()
      .WithQueryString(new Dictionary<string, string>
      {
        { "after_id", pagingRequest.AfterId },
        { "limit", pagingRequest.Limit.ToString() },
      })
      .Respond(
        HttpStatusCode.OK,
        "application/json",
        @"{
            ""data"": [
              {
                ""type"": ""model"",
                ""id"": ""claude-3-5-sonnet-20241022"",
                ""display_name"": ""Claude 3.5 Sonnet (New)"",
                ""created_at"": ""2024-10-22T00:00:00Z""
              }
            ],
            ""has_more"": true,
            ""first_id"": ""first_id"",
            ""last_id"": ""last_id""
          }"
      );

    var result = await Client.ListModelsAsync(pagingRequest);

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().BeOfType<Page<AnthropicModel>>();
    result.Value.HasMore.Should().BeTrue();
    result.Value.FirstId.Should().Be("first_id");
    result.Value.LastId.Should().Be("last_id");
    result.Value.Data.Should().BeEquivalentTo(new AnthropicModel[]
    {
      new()
      {
        Type = "model",
        Id = "claude-3-5-sonnet-20241022",
        DisplayName = "Claude 3.5 Sonnet (New)",
        CreatedAt = DateTimeOffset.Parse("2024-10-22T00:00:00Z")
      }
    });
  }

  [Fact]
  public async Task ListModelsAsync_WhenCalledAndNoModelsReturned_ItShouldReturnEmptyList()
  {
    _mockHttpMessageHandler
      .WhenListModelsRequest()
      .Respond(
        HttpStatusCode.OK,
        "application/json",
        @"{
            ""data"": [],
            ""has_more"": false,
            ""first_id"": null,
            ""last_id"": null
          }"
      );

    var result = await Client.ListModelsAsync();

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().BeOfType<Page<AnthropicModel>>();
    result.Value.HasMore.Should().BeFalse();
    result.Value.FirstId.Should().BeNull();
    result.Value.LastId.Should().BeNull();
    result.Value.Data.Should().BeEmpty();
  }

  [Fact]
  public async Task ListModelsAsync_WhenCalledAndErrorReturned_ItShouldHandleError()
  {
    _mockHttpMessageHandler
      .WhenListModelsRequest()
      .Respond(
        HttpStatusCode.BadRequest,
        "application/json",
        @"{
            ""type"": ""error"",
            ""error"": {
              ""type"": ""invalid_request_error"",
              ""message"": ""messages: roles must alternate between user and assistant, but found multiple user roles in a row""
            }
          }"
      );

    var result = await Client.ListModelsAsync();

    result.IsSuccess.Should().BeFalse();
    result.Error.Should().BeOfType<AnthropicError>();
    result.Error.Error.Should().BeOfType<InvalidRequestError>();
  }

  [Fact]
  public async Task ListModelsAsync_WhenCalledRequestFailsAndCanNotDeserializeError_ItShouldReturnUnknownError()
  {
    _mockHttpMessageHandler
      .WhenListModelsRequest()
      .Respond(
        HttpStatusCode.BadRequest,
        "application/json",
        @"{}"
      );

    var result = await Client.ListModelsAsync();

    result.IsSuccess.Should().BeFalse();
    result.Error.Should().BeOfType<AnthropicError>();
    result.Error.Error.Should().BeOfType<ApiError>();
  }

  [Fact]
  public async Task ListAllModelsAsync_WhenCalled_ItShouldReturnAllModels()
  {
    _mockHttpMessageHandler
      .WhenListModelsRequest()
      .WithExactQueryString(new Dictionary<string, string>()
      {
        { "limit", "20" },
      })
      .Respond(
        HttpStatusCode.OK,
        "application/json",
        @"{
            ""data"": [
              {
                ""type"": ""model"",
                ""id"": ""claude-3-5-sonnet-20241022"",
                ""display_name"": ""Claude 3.5 Sonnet (New)"",
                ""created_at"": ""2024-10-22T00:00:00Z""
              }
            ],
            ""has_more"": true,
            ""first_id"": ""1"",
            ""last_id"": ""1""
          }"
      );

    _mockHttpMessageHandler
      .WhenListModelsRequest()
      .WithExactQueryString(new Dictionary<string, string>()
      {
        { "after_id", "1" },
        { "limit", "20" },
      })
      .Respond(
        HttpStatusCode.OK,
        "application/json",
        @"{
            ""data"": [
              {
                ""type"": ""model"",
                ""id"": ""claude-3-5-sonnet-20241023"",
                ""display_name"": ""Claude 3.5 Sonnet (New)"",
                ""created_at"": ""2024-10-23T00:00:00Z""
              }
            ],
            ""has_more"": false,
            ""first_id"": ""2"",
            ""last_id"": ""2""
          }"
      );

    var pageResponses = Client.ListAllModelsAsync();
    var collectedPages = new List<Page<AnthropicModel>>();

    await foreach (var response in pageResponses)
    {
      response.IsSuccess.Should().BeTrue();
      response.Value.Should().BeOfType<Page<AnthropicModel>>();
      collectedPages.Add(response.Value);
    }

    collectedPages.Should().HaveCount(2);
    collectedPages.Should().BeEquivalentTo(new Page<AnthropicModel>[]
    {
      new()
      {
        HasMore = true,
        FirstId = "1",
        LastId = "1",
        Data = [
          new()
          {
            Type = "model",
            Id = "claude-3-5-sonnet-20241022",
            DisplayName = "Claude 3.5 Sonnet (New)",
            CreatedAt = DateTimeOffset.Parse("2024-10-22T00:00:00Z")
          }
        ]
      },
      new()
      {
        HasMore = false,
        FirstId = "2",
        LastId = "2",
        Data = [
          new()
          {
            Type = "model",
            Id = "claude-3-5-sonnet-20241023",
            DisplayName = "Claude 3.5 Sonnet (New)",
            CreatedAt = DateTimeOffset.Parse("2024-10-23T00:00:00Z")
          }
        ]
      }
    });
  }

  [Fact]
  public async Task ListAllModelsAsync_WhenCalledAndErrorReturned_ItShouldHandleError()
  {
    _mockHttpMessageHandler
      .WhenListModelsRequest()
      .Respond(
        HttpStatusCode.BadRequest,
        "application/json",
        @"{
            ""type"": ""error"",
            ""error"": {
              ""type"": ""invalid_request_error"",
              ""message"": ""messages: roles must alternate between user and assistant, but found multiple user roles in a row""
            }
          }"
      );

    var responses = Client.ListAllModelsAsync();
    var count = 0;

    await foreach (var page in responses)
    {
      count++;
      page.IsSuccess.Should().BeFalse();
      page.Error.Should().BeOfType<AnthropicError>();
      page.Error.Error.Should().BeOfType<InvalidRequestError>();
    }

    count.Should().Be(1);
  }

  [Fact]
  public async Task ListAllModelsAsync_WhenCalledRequestFailsAndCanNotDeserializeError_ItShouldReturnUnknownError()
  {
    _mockHttpMessageHandler
      .WhenListModelsRequest()
      .Respond(
        HttpStatusCode.BadRequest,
        "application/json",
        @"{}"
      );

    var responses = Client.ListAllModelsAsync();
    var count = 0;

    await foreach (var page in responses)
    {
      count++;
      page.IsSuccess.Should().BeFalse();
      page.Error.Should().BeOfType<AnthropicError>();
      page.Error.Error.Should().BeOfType<ApiError>();
    }

    count.Should().Be(1);
  }

  [Fact]
  public async Task ListAllModelsAsync_WhenFirstPageSucceedsAndSecondPageFails_ItShouldReturnFirstPageAndError()
  {
    _mockHttpMessageHandler
      .WhenListModelsRequest()
      .WithExactQueryString(new Dictionary<string, string>
      {
        { "limit", "20" },
      })
      .Respond(
        HttpStatusCode.OK,
        "application/json",
        @"{
            ""data"": [
              {
                ""type"": ""model"",
                ""id"": ""claude-3-5-sonnet-20241022"",
                ""display_name"": ""Claude 3.5 Sonnet (New)"",
                ""created_at"": ""2024-10-22T00:00:00Z""
              }
            ],
            ""has_more"": true,
            ""first_id"": ""1"",
            ""last_id"": ""1""
          }"
      );

    _mockHttpMessageHandler
      .WhenListModelsRequest()
      .WithExactQueryString(new Dictionary<string, string>
      {
        { "after_id", "1" },
        { "limit", "20" },
      })
      .Respond(
        HttpStatusCode.BadRequest,
        "application/json",
        @"{
            ""type"": ""error"",
            ""error"": {
              ""type"": ""invalid_request_error"",
              ""message"": ""messages: roles must alternate between user and assistant, but found multiple user roles in a row""
            }
          }"
      );

    var responses = Client.ListAllModelsAsync();
    var count = 0;

    await foreach (var page in responses)
    {
      count++;

      if (count == 1)
      {
        page.IsSuccess.Should().BeTrue();
        page.Value.Should().BeOfType<Page<AnthropicModel>>();
      }
      else
      {
        page.IsSuccess.Should().BeFalse();
        page.Error.Should().BeOfType<AnthropicError>();
        page.Error.Error.Should().BeOfType<InvalidRequestError>();
      }
    }

    count.Should().Be(2);
  }

  [Fact]
  public async Task GetModelAsync_WhenCalled_ItShouldReturnModel()
  {
    var modelId = "claude-3-5-sonnet-20241022";

    _mockHttpMessageHandler
      .WhenGetModelRequest(modelId)
      .Respond(
        HttpStatusCode.OK,
        "application/json",
        @"{
            ""type"": ""model"",
            ""id"": ""claude-3-5-sonnet-20241022"",
            ""display_name"": ""Claude 3.5 Sonnet (New)"",
            ""created_at"": ""2024-10-22T00:00:00Z""
          }"
      );

    var result = await Client.GetModelAsync(modelId);

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().BeOfType<AnthropicModel>();
    result.Value.Type.Should().Be("model");
    result.Value.Id.Should().Be("claude-3-5-sonnet-20241022");
    result.Value.DisplayName.Should().Be("Claude 3.5 Sonnet (New)");
    result.Value.CreatedAt.Should().Be(DateTimeOffset.Parse("2024-10-22T00:00:00Z"));
  }

  [Fact]
  public async Task GetModelAsync_WhenCalledAndErrorReturned_ItShouldHandleError()
  {
    var modelId = "claude-3-5-sonnet-20241022";

    _mockHttpMessageHandler
      .WhenGetModelRequest(modelId)
      .Respond(
        HttpStatusCode.BadRequest,
        "application/json",
        @"{
            ""type"": ""error"",
            ""error"": {
              ""type"": ""invalid_request_error"",
              ""message"": ""messages: roles must alternate between user and assistant, but found multiple user roles in a row""
            }
          }"
      );

    var result = await Client.GetModelAsync(modelId);

    result.IsSuccess.Should().BeFalse();
    result.Error.Should().BeOfType<AnthropicError>();
    result.Error.Error.Should().BeOfType<InvalidRequestError>();
  }

  [Fact]
  public async Task GetModelAsync_WhenCalledRequestFailsAndCanNotDeserializeError_ItShouldReturnUnknownError()
  {
    var modelId = "claude-3-5-sonnet-20241022";

    _mockHttpMessageHandler
      .WhenGetModelRequest(modelId)
      .Respond(
        HttpStatusCode.BadRequest,
        "application/json",
        @"{}"
      );

    var result = await Client.GetModelAsync(modelId);

    result.IsSuccess.Should().BeFalse();
    result.Error.Should().BeOfType<AnthropicError>();
    result.Error.Error.Should().BeOfType<ApiError>();
  }

  [Fact]
  public async Task GetModelAsync_WhenCalledAndCanNotDeserializeModel_ItShouldReturnEmptyModel()
  {
    var modelId = "claude-3-5-sonnet-20241022";

    _mockHttpMessageHandler
      .WhenGetModelRequest(modelId)
      .Respond(
        HttpStatusCode.OK,
        "application/json",
        @"{}"
      );

    var result = await Client.GetModelAsync(modelId);

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().BeOfType<AnthropicModel>();
  }
}