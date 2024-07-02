using System.Text.Json.Nodes;

namespace AnthropicClient.Tests.Unit.Models;

public class ToolCallTests : SerializationTest
{
  [Fact]
  public async Task InvokeAsync_WhenCalledToolHasNoParamsIsNotAwaitableAndToolCallIsSuccessful_ItShouldReturnSuccessResult()
  {
    var func = () => "Hello, World!";
    var anthropicFunction = new AnthropicFunction(func.Method, func.Target);
    var tool = new Tool("tool", "description", anthropicFunction);
    var toolCall = new ToolCall(tool, new ToolUseContent());

    var result = await toolCall.InvokeAsync();

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().Be("Hello, World!");
    result.Error.Should().BeNull();
  }

  [Fact]
  public async Task InvokeAsync_WhenCalledToolHasNoParamsIsNotAwaitableAndToolCallFails_ItShouldReturnFailureResult()
  {
    var func = new Func<string>(() => throw new Exception("Error"));
    var anthropicFunction = new AnthropicFunction(func.Method, func.Target);
    var tool = new Tool("tool", "description", anthropicFunction);
    var toolCall = new ToolCall(tool, new ToolUseContent());

    var result = await toolCall.InvokeAsync();

    result.IsSuccess.Should().BeFalse();
    result.Value.Should().BeNull();
    result.Error.Should().NotBeNull();
  }

  [Fact]
  public async Task InvokeAsync_WhenCalledToolHasNoParamsIsAwaitableAndToolCallIsSuccessful_ItShouldReturnSuccessResult()
  {
    var func = async () => await Task.FromResult("Hello, World!");
    var anthropicFunction = new AnthropicFunction(func.Method, func.Target);
    var tool = new Tool("tool", "description", anthropicFunction);
    var toolCall = new ToolCall(tool, new ToolUseContent());

    var result = await toolCall.InvokeAsync();

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().Be("Hello, World!");
    result.Error.Should().BeNull();
  }

  [Fact]
  public async Task InvokeAsync_WhenCalledToolHasNoParamsIsAwaitableAndToolCallFails_ItShouldReturnFailureResult()
  {
    var func = new Func<Task<string>>(() => throw new Exception("Error"));
    var anthropicFunction = new AnthropicFunction(func.Method, func.Target);
    var tool = new Tool("tool", "description", anthropicFunction);
    var toolCall = new ToolCall(tool, new ToolUseContent());

    var result = await toolCall.InvokeAsync();

    result.IsSuccess.Should().BeFalse();
    result.Value.Should().BeNull();
    result.Error.Should().NotBeNull();
  }

  [Fact]
  public async Task InvokeAsync_WhenCalledToolHasParamsIsNotAwaitableAndToolCallIsSuccessful_ItShouldReturnSuccessResult()
  {
    var func = (int i) => i.ToString();
    var anthropicFunction = new AnthropicFunction(func.Method, func.Target);
    var tool = new Tool("tool", "description", anthropicFunction);

    var input = new Dictionary<string, object?> { { "i", 42 } };
    var toolCall = new ToolCall(tool, new ToolUseContent { Input = input });

    var result = await toolCall.InvokeAsync();

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().Be("42");
    result.Error.Should().BeNull();
  }

  [Fact]
  public async Task InvokeAsync_WhenCalledToolHasParamsIsNotAwaitableAndToolCallFails_ItShouldReturnFailureResult()
  {
    var func = new Func<int, string>(i => throw new Exception("Error"));
    var anthropicFunction = new AnthropicFunction(func.Method, func.Target);
    var tool = new Tool("tool", "description", anthropicFunction);

    var input = new Dictionary<string, object?> { { "i", 42 } };
    var toolCall = new ToolCall(tool, new ToolUseContent { Input = input });

    var result = await toolCall.InvokeAsync();

    result.IsSuccess.Should().BeFalse();
    result.Value.Should().BeNull();
    result.Error.Should().NotBeNull();
  }

  [Fact]
  public async Task InvokeAsync_WhenCalledToolHasParamsIsAwaitableAndToolCallIsSuccessful_ItShouldReturnSuccessResult()
  {
    var func = async (int i) => await Task.FromResult(i.ToString());
    var anthropicFunction = new AnthropicFunction(func.Method, func.Target);
    var tool = new Tool("tool", "description", anthropicFunction);

    var input = new Dictionary<string, object?> { { "i", 42 } };
    var toolCall = new ToolCall(tool, new ToolUseContent { Input = input });

    var result = await toolCall.InvokeAsync();

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().Be("42");
    result.Error.Should().BeNull();
  }

  [Fact]
  public async Task InvokeAsync_WhenCalledToolHasParamsIsAwaitableAndToolCallFails_ItShouldReturnFailureResult()
  {
    var func = new Func<int, Task<string>>(i => throw new Exception("Error"));
    var anthropicFunction = new AnthropicFunction(func.Method, func.Target);
    var tool = new Tool("tool", "description", anthropicFunction);

    var input = new Dictionary<string, object?> { { "i", 42 } };
    var toolCall = new ToolCall(tool, new ToolUseContent { Input = input });

    var result = await toolCall.InvokeAsync();

    result.IsSuccess.Should().BeFalse();
    result.Value.Should().BeNull();
    result.Error.Should().NotBeNull();
  }

  [Fact]
  public async Task InvokeAsync_WhenCalledAndToolHasCustomParameterName_ItShouldReturnSuccessResult()
  {
    var func = ([FunctionParameter("Person's Age", "Age")] int i) => i.ToString();
    var anthropicFunction = new AnthropicFunction(func.Method, func.Target);
    var tool = new Tool("tool", "description", anthropicFunction);

    var input = new Dictionary<string, object?> { { "Age", 42 } };
    var toolCall = new ToolCall(tool, new ToolUseContent { Input = input });

    var result = await toolCall.InvokeAsync();

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().Be("42");
    result.Error.Should().BeNull();
  }

  [Fact]
  public async Task InvokeAsync_WhenCalledToolHasDefaultValueAndToolUseHasNotInput_ItShouldReturnSuccessResult()
  {
    var func = (int i = 42) => i.ToString();
    var anthropicFunction = new AnthropicFunction(func.Method, func.Target);
    var tool = new Tool("tool", "description", anthropicFunction);

    var toolCall = new ToolCall(tool, new ToolUseContent());

    var result = await toolCall.InvokeAsync();

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().Be("42");
    result.Error.Should().BeNull();
  }

  [Fact]
  public async Task InvokeAsync_WhenCalledToolHasNoDefaultValueAndNoInput_ItShouldReturnFailureResult()
  {
    var func = (int i) => i.ToString();
    var anthropicFunction = new AnthropicFunction(func.Method, func.Target);
    var tool = new Tool("tool", "description", anthropicFunction);

    var toolCall = new ToolCall(tool, new ToolUseContent());

    var result = await toolCall.InvokeAsync();

    result.IsSuccess.Should().BeFalse();
    result.Value.Should().BeNull();
    result.Error.Should().NotBeNull();
  }

  [Fact]
  public async Task InvokeAsync_WhenCalledAndToolHasEnumParameter_ItShouldReturnSuccessResult()
  {
    var func = (DayOfWeek day) => day.ToString();
    var anthropicFunction = new AnthropicFunction(func.Method, func.Target);
    var tool = new Tool("tool", "description", anthropicFunction);

    var input = new Dictionary<string, object?> { { "day", DayOfWeek.Monday } };
    var toolCall = new ToolCall(tool, new ToolUseContent { Input = input });

    var result = await toolCall.InvokeAsync();

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().Be("Monday");
    result.Error.Should().BeNull();
  }

  [Fact]
  public async Task InvokeAsync_WhenCalledAndToolHasComplexParameterType_ItShouldReturnSuccessResult()
  {
    var func = (Person person) => person.Name;
    var anthropicFunction = new AnthropicFunction(func.Method, func.Target);
    var tool = new Tool("tool", "description", anthropicFunction);

    var personJson = @"{
      ""person"": {
        ""Name"": ""John""
      }
    }";

    var input = Deserialize<Dictionary<string, object?>>(personJson);

    var toolCall = new ToolCall(tool, new ToolUseContent { Input = input! });

    var result = await toolCall.InvokeAsync();

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().Be("John");
    result.Error.Should().BeNull();
  }

  [Fact]
  public async Task InvokeAsync_WhenCalledAndToolReturnsVoid_ItShouldReturnSuccessResult()
  {
    var func = (int i) => { };
    var anthropicFunction = new AnthropicFunction(func.Method, func.Target);
    var tool = new Tool("tool", "description", anthropicFunction);

    var input = new Dictionary<string, object?> { { "i", 42 } };
    var toolCall = new ToolCall(tool, new ToolUseContent { Input = input });

    var result = await toolCall.InvokeAsync();

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().BeNull();
    result.Error.Should().BeNull();
  }

  [Fact]
  public async Task InvokeAsync_WhenCalledAndToolReturnsTask_ItShouldReturnSuccessResult()
  {
    var func = async (int i) => await Task.CompletedTask;
    var anthropicFunction = new AnthropicFunction(func.Method, func.Target);
    var tool = new Tool("tool", "description", anthropicFunction);

    var input = new Dictionary<string, object?> { { "i", 42 } };
    var toolCall = new ToolCall(tool, new ToolUseContent { Input = input });

    var result = await toolCall.InvokeAsync();

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().BeNull();
    result.Error.Should().BeNull();
  }
}

class Person
{
  public string Name { get; set; } = string.Empty;
}