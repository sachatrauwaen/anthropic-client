#pragma warning disable xUnit1004

using System.Reflection;

using Xunit.Abstractions;

namespace AnthropicClient.Tests.Examples;

public class Examples(ConfigurationFixture config, ITestOutputHelper console) : IClassFixture<ConfigurationFixture>
{
  private readonly ITestOutputHelper _console = console;
  private readonly AnthropicApiClient _client = new(config.AnthropicApiKey, new());

  [Example]
  public async Task CreateMessage()
  {
    var response = await _client.CreateMessageAsync(new MessageRequest(
      AnthropicModels.Claude3Haiku,
      [
        new(
          MessageRole.User, 
          [new TextContent("Please write a haiku about the ocean.")]
        )
      ]
    ));

    if (response.IsSuccess is false)
    {
      _console.WriteLine($"Failed to create message");
      _console.WriteLine($"Error Type: {0}", response.Error.Error.Type);
      _console.WriteLine($"Error Message: {0}", response.Error.Error.Message);
      return;
    }

    foreach (var content in response.Value.Content)
    {
      switch (content)
      {
        case TextContent textContent:
          _console.WriteLine(textContent.Text);
          break;
      }
    }
  }
  
  [Example]
  public async Task CreateAndStreamMessage()
  {
    var events = _client.CreateMessageAsync(new StreamMessageRequest(
      AnthropicModels.Claude3Haiku,
      [
        new(
          MessageRole.User, 
          [new TextContent("Please write a haiku about the ocean.")]
        )
      ]
    ));

    var msgBuilder = new StringBuilder();

    await foreach (var e in events)
    {
      switch (e.Data)
      {
        case var data when data is ContentDeltaEventData contentData:
          switch (contentData.Delta)
          {
            case var delta when delta is TextDelta textDelta:
              msgBuilder.Append(textDelta.Text);
              break;
          }
          break;
      }
    }

    _console.WriteLine(msgBuilder.ToString());
  }

  [Example]
  public async Task CreateStreamMessageAndGetCompleteMessageResponse()
  {
    var events = _client.CreateMessageAsync(new StreamMessageRequest(
      AnthropicModels.Claude3Haiku,
      [
        new(
          MessageRole.User, 
          [new TextContent("Please write a haiku about the ocean.")]
        )
      ]
    ));

    MessageResponse? response = null;

    await foreach (var e in events)
    {
      switch (e.Data)
      {
        case var data when data is MessageCompleteEventData msgData:
          response = msgData.Message;
          break;
      }
    }

    var textContent = response?.Content
      .OfType<TextContent>()
      .Aggregate(new StringBuilder(), (sb, c) => sb.Append(c.Text))
      .ToString();

    _console.WriteLine(textContent);
  }

  [Example]
  public async Task CreateAToolFromAClass()
  {
    var getWeatherTool = Tool.CreateFromClass<GetWeatherTool>();

    var response = await _client.CreateMessageAsync(new MessageRequest(
      AnthropicModels.Claude3Haiku,
      [
        new(
          MessageRole.User, 
          [new TextContent("What is the weather in New York?")]
        )
      ],
      tools: [getWeatherTool]
    ));

    if (response.IsSuccess is false)
    {
      _console.WriteLine($"Failed to create message");
      _console.WriteLine($"Error Type: {0}", response.Error.Error.Type);
      _console.WriteLine($"Error Message: {0}", response.Error.Error.Message);
      return;
    }

    foreach (var content in response.Value.Content)
    {
      switch (content)
      {
        case TextContent textContent:
          _console.WriteLine(textContent.Text);
          break;
        case ToolUseContent toolUseContent:
          _console.WriteLine(toolUseContent.Name);
          break;
      }
    }
  }

  [Example]
  public async Task CreateAToolFromAStaticMethod()
  {
    var getWeatherTool = Tool.CreateFromStaticMethod(
      "Get Weather", 
      "Get the weather for a location in the specified units", 
      typeof(GetWeatherTool), 
      nameof(GetWeatherTool.GetWeather)
    );

    var response = await _client.CreateMessageAsync(new MessageRequest(
      AnthropicModels.Claude3Haiku,
      [
        new(
          MessageRole.User, 
          [new TextContent("What is the weather in New York?")]
        )
      ],
      tools: [getWeatherTool]
    ));

    if (response.IsSuccess is false)
    {
      _console.WriteLine($"Failed to create message");
      _console.WriteLine($"Error Type: {0}", response.Error.Error.Type);
      _console.WriteLine($"Error Message: {0}", response.Error.Error.Message);
      return;
    }

    foreach (var content in response.Value.Content)
    {
      switch (content)
      {
        case TextContent textContent:
          _console.WriteLine(textContent.Text);
          break;
        case ToolUseContent toolUseContent:
          _console.WriteLine(toolUseContent.Name);
          break;
      }
    }
  }

  [Example]
  public async Task CreateAToolFromInstanceMethod()
  {
    var toolInstance = new GetWeatherTool();

    var getWeatherTool = Tool.CreateFromInstanceMethod(
      "Get Weather", 
      "Get the weather for a location in the specified units", 
      toolInstance,
      nameof(toolInstance.GetWeather)
    );

    var response = await _client.CreateMessageAsync(new MessageRequest(
      AnthropicModels.Claude3Haiku,
      [
        new(
          MessageRole.User, 
          [new TextContent("What is the weather in New York?")]
        )
      ],
      tools: [getWeatherTool]
    ));

    if (response.IsSuccess is false)
    {
      _console.WriteLine($"Failed to create message");
      _console.WriteLine($"Error Type: {0}", response.Error.Error.Type);
      _console.WriteLine($"Error Message: {0}", response.Error.Error.Message);
      return;
    }

    foreach (var content in response.Value.Content)
    {
      switch (content)
      {
        case TextContent textContent:
          _console.WriteLine(textContent.Text);
          break;
        case ToolUseContent toolUseContent:
          _console.WriteLine(toolUseContent.Name);
          break;
      }
    }
  }
    
  [Example]
  public async Task CreateAToolFromDelegate()
  {
    var tool = (string location, string units) => $"The weather in {location} is 72 degrees {units}";

    var getWeatherTool = Tool.CreateFromFunction(
      "Get Weather", 
      "Get the weather for a location in the specified units", 
      tool
    );

    var response = await _client.CreateMessageAsync(new MessageRequest(
      AnthropicModels.Claude3Haiku,
      [
        new(
          MessageRole.User, 
          [new TextContent("What is the weather in New York?")]
        )
      ],
      tools: [getWeatherTool]
    ));

    if (response.IsSuccess is false)
    {
      _console.WriteLine($"Failed to create message");
      _console.WriteLine($"Error Type: {0}", response.Error.Error.Type);
      _console.WriteLine($"Error Message: {0}", response.Error.Error.Message);
      return;
    }

    foreach (var content in response.Value.Content)
    {
      switch (content)
      {
        case TextContent textContent:
          _console.WriteLine(textContent.Text);
          break;
        case ToolUseContent toolUseContent:
          _console.WriteLine(toolUseContent.Name);
          break;
      }
    }
  }

  [Example]
  public async Task CreateAToolFromDelegateWithParameterAttributes()
  {
    var tool = (
      [FunctionParameter(description: "The location of the weather being got", name: "Location", required: true)] 
      string location, 
      string units
    ) => $"The weather in {location} is 72 degrees {units}";

    var getWeatherTool = Tool.CreateFromFunction(
      "Get Weather", 
      "Get the weather for a location in the specified units", 
      tool
    );

    var response = await _client.CreateMessageAsync(new MessageRequest(
      AnthropicModels.Claude3Haiku,
      [
        new(
          MessageRole.User, 
          [new TextContent("What is the weather in New York?")]
        )
      ],
      tools: [getWeatherTool]
    ));

    if (response.IsSuccess is false)
    {
      _console.WriteLine($"Failed to create message");
      _console.WriteLine($"Error Type: {0}", response.Error.Error.Type);
      _console.WriteLine($"Error Message: {0}", response.Error.Error.Message);
      return;
    }

    foreach (var content in response.Value.Content)
    {
      switch (content)
      {
        case TextContent textContent:
          _console.WriteLine(textContent.Text);
          break;
        case ToolUseContent toolUseContent:
          _console.WriteLine(toolUseContent.Name);
          break;
      }
    }
  }

  [Example]
  public async Task CreateAToolFromDelegateWithInputClass()
  {
    var tool = (GetWeatherInput input) => $"The weather in {input.Location} is 72 degrees {input.Units}";

    var getWeatherTool = Tool.CreateFromFunction(
      "Get Weather", 
      "Get the weather for a location in the specified units", 
      tool
    );

    var response = await _client.CreateMessageAsync(new MessageRequest(
      AnthropicModels.Claude3Haiku,
      [
        new(
          MessageRole.User, 
          [new TextContent("What is the weather in New York?")]
        )
      ],
      tools: [getWeatherTool]
    ));

    if (response.IsSuccess is false)
    {
      _console.WriteLine($"Failed to create message");
      _console.WriteLine($"Error Type: {0}", response.Error.Error.Type);
      _console.WriteLine($"Error Message: {0}", response.Error.Error.Message);
      return;
    }

    foreach (var content in response.Value.Content)
    {
      switch (content)
      {
        case TextContent textContent:
          _console.WriteLine(textContent.Text);
          break;
        case ToolUseContent toolUseContent:
          _console.WriteLine(toolUseContent.Name);
          break;
      }
    }
  }

  [Example]
  public async Task CreateAMessageProvideAToolAndCallTool()
  {
    List<Message> messages = [
      new(
        MessageRole.User, 
        [new TextContent("What is the weather in New York?")]
      )
    ];

    List<Tool> tools = [Tool.CreateFromClass<GetWeatherTool>()];

    var response = await _client.CreateMessageAsync(new MessageRequest(
      AnthropicModels.Claude3Haiku,
      messages,
      tools: tools
    ));

    if (response.IsSuccess is false)
    {
      _console.WriteLine($"Failed to create message");
      _console.WriteLine($"Error Type: {0}", response.Error.Error.Type);
      _console.WriteLine($"Error Message: {0}", response.Error.Error.Message);
      return;
    }

    foreach (var content in response.Value.Content)
    {
      messages.Add(new(MessageRole.Assistant, [content]));

      switch (content)
      {
        case TextContent textContent:
          _console.WriteLine(textContent.Text);
          break;
        case ToolUseContent toolUseContent:
          _console.WriteLine(toolUseContent.Name);
          break;
      }
    }

    if (response.Value.ToolCall is not null)
    {
      var toolCallResult = await response.Value.ToolCall.InvokeAsync<string>();
      string toolResultContent;

      if (toolCallResult.IsSuccess && toolCallResult.Value is not null)
      {
        _console.WriteLine(toolCallResult.Value);
        toolResultContent = toolCallResult.Value;
      }
      else
      {
        _console.WriteLine(toolCallResult.Error.Message);
        toolResultContent = toolCallResult.Error.Message;
      }

      messages.Add(
        new(
          MessageRole.User, 
          [
            new ToolResultContent(
              response.Value.ToolCall.ToolUse.Id, 
              toolResultContent
            )
          ]
        )
      );
    }

    var finalResponse = await _client.CreateMessageAsync(new MessageRequest(
      AnthropicModels.Claude3Haiku,
      messages,
      tools: tools
    ));

    if (finalResponse.IsSuccess is false)
    {
      _console.WriteLine($"Failed to create message");
      _console.WriteLine($"Error Type: {0}", finalResponse.Error.Error.Type);
      _console.WriteLine($"Error Message: {0}", finalResponse.Error.Error.Message);
      return;
    }

    foreach (var content in finalResponse.Value.Content)
    {
      switch (content)
      {
        case TextContent textContent:
          _console.WriteLine(textContent.Text);
          break; 
      }
    }
  }

  [Example]
  public async Task CreateAMessageProvideAToolStreamTheResponseAndCallTheTool()
  {
    var tool = (string location, string units) => $"The weather in {location} is 72 degrees {units}";

    List<Message> messages = [
      new(
        MessageRole.User, 
        [new TextContent("What is the weather in New York?")]
      )
    ];

    List<Tool> tools = [Tool.CreateFromFunction(
      "Get Weather", 
      "Get the weather for a location in the specified units", 
      tool
    )];

    var events = _client.CreateMessageAsync(new StreamMessageRequest(
      AnthropicModels.Claude3Haiku,
      messages,
      tools: tools
    ));

    MessageResponse? response = null;

    await foreach (var e in events)
    {
      switch (e.Data)
      {
        case var data when data is MessageCompleteEventData msgData:
          response = msgData.Message;
          break;
      }
    }

    if (response is null)
    {
      _console.WriteLine("Failed to get message response");
      return;
    }

    foreach (var content in response.Content)
    {
      messages.Add(new(MessageRole.Assistant, [content]));
    }

    if (response?.ToolCall is not null)
    {
      var toolCallResult = await response.ToolCall.InvokeAsync<string>();
      string toolResultContent;

      if (toolCallResult.IsSuccess && toolCallResult.Value is not null)
      {
        toolResultContent = toolCallResult.Value;
      }
      else
      {
        toolResultContent = toolCallResult.Error.Message;
      }

      messages.Add(
        new(
          MessageRole.User, 
          [
            new ToolResultContent(
              response.ToolCall.ToolUse.Id, 
              toolResultContent
            )
          ]
        )
      );
    }

    var finalResponse = await _client.CreateMessageAsync(new MessageRequest(
      AnthropicModels.Claude3Haiku,
      messages,
      tools: tools
    ));

    if (finalResponse.IsSuccess is false)
    {
      _console.WriteLine($"Failed to create message");
      _console.WriteLine($"Error Type: {0}", finalResponse.Error.Error.Type);
      _console.WriteLine($"Error Message: {0}", finalResponse.Error.Error.Message);
      return;
    }

    foreach (var content in finalResponse.Value.Content)
    {
      switch (content)
      {
        case TextContent textContent:
          _console.WriteLine(textContent.Text);
          break; 
      }
    }
  }
}

class GetWeatherTool : ITool
{
  public string Name => "Get Weather";

  public string Description => "Get the weather for a location in the specified units";

  public MethodInfo Function => typeof(GetWeatherTool).GetMethod(nameof(GetWeather))!;

  public string GetWeather(string location, string units)
  {
    return $"The weather in {location} is 72 degrees {units}";
  }

  public string GetWeatherByInput(GetWeatherInput input)
  {
    return $"The weather in {input.Location} is 72 degrees {input.Units}";
  }

  public static string GetWeatherStatically(string location)
  {
    return $"The weather in {location} is 72 degrees Fahrenheit";
  }
}

class GetWeatherInput
{
  [FunctionProperty(
    description: "The location of the weather being got",
    required: true
  )]
  public string Location { get; } = string.Empty;

  [FunctionProperty(
    description: "The units to get the weather in",
    required: false,
    defaultValue: "Fahrenheit",
    possibleValues: ["Fahrenheit", "Celsius"]
  )]
  public string Units { get; } = "Fahrenheit";
}