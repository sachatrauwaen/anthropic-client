namespace AnthropicClient.Tests.Unit.Models;

public class MessageTests : SerializationTest
{
  private readonly string _testJson = @"{
    ""role"": ""assistant"",
    ""content"": [
      { ""text"": ""text"", ""type"": ""text"" }
    ]
  }";

  [Fact]
  public void Constructor_WhenCalled_ItShouldInitializeProperties()
  {
    var role = "assistant";
    var content = new List<Content> { new TextContent("text") };

    var message = new Message(role, content);

    message.Role.Should().Be(role);
    message.Content.Should().BeSameAs(content);
  }

  [Fact]
  public void Constructor_WhenCalledAndRoleIsNull_ItShouldThrowArgumentNullException()
  {
    var content = new List<Content> { new TextContent("text") };

    var action = () => new Message(null!, content);

    action.Should().Throw<ArgumentNullException>();
  }

  [Fact]
  public void Constructor_WhenCalledAndContentIsNull_ItShouldThrowArgumentNullException()
  {
    var role = "assistant";

    var action = () => new Message(role, null!);

    action.Should().Throw<ArgumentNullException>();
  }

  [Fact]
  public void Constructor_WhenCalledAndRoleIsInvalid_ItShouldThrowArgumentException()
  {
    var role = "invalid";
    var content = new List<Content> { new TextContent("text") };

    var action = () => new Message(role, content);

    action.Should().Throw<ArgumentException>();
  }

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldReturnJsonString()
  {
    var role = "assistant";
    var content = new List<Content> { new TextContent("text") };
    var message = new Message(role, content);

    var actual = Serialize(message);

    JsonAssert.Equal(_testJson, actual);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldReturnMessage()
  {
    var message = Deserialize<Message>(_testJson);

    message.Should().NotBeNull();
    message!.Role.Should().Be("assistant");
    message.Content.Should().HaveCount(1);
    message.Content[0].Should().BeOfType<TextContent>();
    message.Content[0].As<TextContent>().Text.Should().Be("text");
  }
}