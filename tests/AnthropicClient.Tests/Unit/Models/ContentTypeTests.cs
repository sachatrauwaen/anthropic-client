namespace AnthropicClient.Tests.Unit.Models;

public class ContentTypeTests
{
  [Fact]
  public void Text_WhenCalled_ItShouldReturnText()
  {
    ContentType.Text.Should().Be("text");
  }

  [Fact]
  public void Image_WhenCalled_ItShouldReturnImage()
  {
    ContentType.Image.Should().Be("image");
  }

  [Fact]
  public void ToolUse_WhenCalled_ItShouldReturnToolUse()
  {
    ContentType.ToolUse.Should().Be("tool_use");
  }

  [Fact]
  public void ToolResult_WhenCalled_ItShouldReturnToolResult()
  {
    ContentType.ToolResult.Should().Be("tool_result");
  }

  [Fact]
  public void Document_WhenCalled_ItShouldReturnDocument()
  {
    ContentType.Document.Should().Be("document");
  }
}