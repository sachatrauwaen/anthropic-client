namespace AnthropicClient.Tests.Unit.Models;

public class StreamMessageRequestTests : SerializationTest
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
    ""max_tokens"": 512,
    ""metadata"": { ""test"":""test"" },
    ""stop_sequences"": [],
    ""temperature"": 0.5,
    ""topK"": 10,
    ""topP"": 0.5,
    ""tool_choice"": { ""type"":""auto"" },
    ""tools"": [],
    ""stream"": true
  }";

  [Fact]
  public void Constructor_WhenCalled_ItShouldInitializeProperties()
  {
    var model = AnthropicModels.Claude3Sonnet;
    var messages = new List<Message> { new() };
    var maxTokens = 512;
    var system = "test-system";
    var metadata = new Dictionary<string, object> { ["test"] = "test" };
    var temperature = 0.5m;
    var topK = 10;
    var topP = 0.5m;
    var toolChoice = new AutoToolChoice();
    var tools = new List<Tool>();

    var messageRequest = new StreamMessageRequest(
      model: model,
      messages: messages,
      maxTokens: maxTokens,
      system: system,
      metadata: metadata,
      temperature: temperature,
      topK: topK,
      topP: topP,
      toolChoice: toolChoice,
      tools: tools
    );

    messageRequest.Model.Should().Be(model);
    messageRequest.Messages.Should().BeSameAs(messages);
    messageRequest.MaxTokens.Should().Be(maxTokens);
    messageRequest.System.Should().Be(system);
    messageRequest.Metadata.Should().BeSameAs(metadata);
    messageRequest.Temperature.Should().Be(temperature);
    messageRequest.TopK.Should().Be(topK);
    messageRequest.TopP.Should().Be(topP);
    messageRequest.ToolChoice.Should().Be(toolChoice);
    messageRequest.Tools.Should().BeSameAs(tools);
    messageRequest.Stream.Should().BeTrue();
  }

  [Fact]
  public void Constructor_WhenCalledAndModelIsNull_ItShouldThrowArgumentNullException()
  {
    var action = () => new StreamMessageRequest(
      model: null!,
      messages: [new()]
    );

    action.Should().Throw<ArgumentNullException>();
  }

  [Fact]
  public void Constructor_WhenCalledAndMessagesIsNull_ItShouldThrowArgumentNullException()
  {
    var action = () => new StreamMessageRequest(
      model: AnthropicModels.Claude3Sonnet,
      messages: null!
    );

    action.Should().Throw<ArgumentNullException>();
  }

  [Fact]
  public void Constructor_WhenCalledAndModelIsInvalid_ItShouldThrowArgumentException()
  {
    var action = () => new StreamMessageRequest(
      model: "invalid-model",
      messages: [new()]
    );

    action.Should().Throw<ArgumentException>();
  }

  [Fact]
  public void Constructor_WhenCalledAndMessagesIsEmpty_ItShouldThrowArgumentException()
  {
    var action = () => new StreamMessageRequest(
      model: AnthropicModels.Claude3Sonnet,
      messages: []
    );

    action.Should().Throw<ArgumentException>();
  }

  [Fact]
  public void Constructor_WhenCalledAndMaxTokensIsInvalid_ItShouldThrowArgumentException()
  {
    var action = () => new StreamMessageRequest(
      model: AnthropicModels.Claude3Sonnet,
      messages: [new()],
      maxTokens: 0
    );

    action.Should().Throw<ArgumentException>();
  }

  [Theory]
  [InlineData(-1)]
  [InlineData(2)]
  public void Constructor_WhenCalledAndTemperatureIsInvalid_ItShouldThrowArgumentException(decimal temperature)
  {
    var action = () => new StreamMessageRequest(
      model: AnthropicModels.Claude3Sonnet,
      messages: [new()],
      temperature: temperature
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
    var maxTokens = 512;
    var system = "test-system";
    var metadata = new Dictionary<string, object>
    {
      ["test"] = "test"
    };
    var temperature = 0.5m;
    var topK = 10;
    var topP = 0.5m;
    var toolChoice = new AutoToolChoice();
    var tools = new List<Tool>();

    var messageRequest = new StreamMessageRequest(
      model: model,
      messages: messages,
      maxTokens: maxTokens,
      system: system,
      metadata: metadata,
      temperature: temperature,
      topK: topK,
      topP: topP,
      toolChoice: toolChoice,
      tools: tools
    );

    var actual = Serialize(messageRequest);

    JsonAssert.Equal(_testJson, actual);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldHaveExpectedShape()
  {
    var messageRequest = Deserialize<StreamMessageRequest>(_testJson);

    messageRequest!.Model.Should().Be(AnthropicModels.Claude3Sonnet);
    messageRequest.System.Should().BeNull();
    messageRequest.Messages.Should().HaveCount(1);
    messageRequest.MaxTokens.Should().Be(512);
    messageRequest.Metadata.Should().HaveCount(1);

    var testValue = messageRequest.Metadata!.GetValueOrDefault("test")!.ToString();
    testValue.Should().Be("test");

    messageRequest.Temperature.Should().Be(0.5m);
    messageRequest.TopK.Should().Be(10);
    messageRequest.TopP.Should().Be(0.5m);
    messageRequest.ToolChoice.Should().BeOfType<AutoToolChoice>();
    messageRequest.ToolChoice!.Type.Should().Be("auto");
    messageRequest.Tools.Should().HaveCount(0);
    messageRequest.Stream.Should().BeTrue();
  }
}