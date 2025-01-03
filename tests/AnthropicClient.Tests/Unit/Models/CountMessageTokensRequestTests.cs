namespace AnthropicClient.Tests.Unit.Models;

public class CountMessageTokensRequestTests : SerializationTest
{
  private readonly string _testJson = @"{
    ""model"": ""claude-3-sonnet-20240229"",
    ""system"": [{
      ""type"": ""text"",
      ""text"": ""test-system""
    }],
    ""messages"": [
      { ""role"": ""user"", ""content"": [{ ""text"": ""Hello!"", ""type"": ""text"" }] }
    ],
    ""tool_choice"": { ""type"":""auto"" },
    ""tools"": []
  }";

  [Fact]
  public void Constructor_WhenCalled_ItShouldInitializeProperties()
  {
    var model = AnthropicModels.Claude3Sonnet;
    var messages = new List<Message> { new() };
    var systemPrompt = new List<TextContent>() { new("test-system") };
    var toolChoice = new AutoToolChoice();
    var tools = new List<Tool>();

    var request = new CountMessageTokensRequest(
      model: model,
      messages: messages,
      toolChoice: toolChoice,
      tools: tools,
      systemPrompt: systemPrompt
    );

    request.Model.Should().Be(model);
    request.Messages.Should().BeSameAs(messages);
    request.ToolChoice.Should().Be(toolChoice);
    request.Tools.Should().BeSameAs(tools);
    request.SystemPrompt.Should().BeSameAs(systemPrompt);
  }

  [Fact]
  public void Constructor_WhenCalledAndModelIsNull_ItShouldThrowArgumentNullException()
  {
    var action = () => new CountMessageTokensRequest(
      model: null!,
      messages: [new()]
    );

    action.Should().Throw<ArgumentNullException>();
  }

  [Fact]
  public void Constructor_WhenCalledAndMessagesIsNull_ItShouldThrowArgumentNullException()
  {
    var action = () => new CountMessageTokensRequest(
      model: AnthropicModels.Claude3Sonnet,
      messages: null!
    );

    action.Should().Throw<ArgumentNullException>();
  }

  [Fact]
  public void Constructor_WhenCalledAndMessagesIsEmpty_ItShouldThrowArgumentException()
  {
    var action = () => new CountMessageTokensRequest(
      model: AnthropicModels.Claude3Sonnet,
      messages: []
    );

    action.Should().Throw<ArgumentException>();
  }

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldHaveExpectedShape()
  {
    var messages = new List<Message>()
    {
      new()
      {
        Role = MessageRole.User,
        Content = [new TextContent("Hello!")]
      }
    };

    var model = AnthropicModels.Claude3Sonnet;
    var systemPrompt = new List<TextContent>() { new("test-system") };
    var toolChoice = new AutoToolChoice();
    var tools = new List<Tool>();

    var request = new CountMessageTokensRequest(
      model: model,
      messages: messages,
      toolChoice: toolChoice,
      tools: tools,
      systemPrompt: systemPrompt
    );

    var actual = Serialize(request);

    JsonAssert.Equal(_testJson, actual);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldHaveExpectedShape()
  {
    var request = Deserialize<CountMessageTokensRequest>(_testJson);

    request!.Model.Should().Be(AnthropicModels.Claude3Sonnet);
    request.SystemPrompt.Should().BeEquivalentTo(new List<TextContent> { new("test-system") });
    request.Messages.Should().HaveCount(1);
    request.ToolChoice.Should().BeOfType<AutoToolChoice>();
    request.ToolChoice!.Type.Should().Be("auto");
    request.Tools.Should().HaveCount(0);
  }
}