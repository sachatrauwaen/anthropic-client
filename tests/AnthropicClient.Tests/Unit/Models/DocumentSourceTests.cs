namespace AnthropicClient.Tests.Unit.Models;

public class DocumentSourceTests
{
  [Fact]
  public void Constructor_WhenCalledWithValidArguments_ItShouldSetProperties()
  {
    var mediaType = "application/pdf";
    var data = "base64data";

    var source = new DocumentSource(mediaType, data);

    source.MediaType.Should().Be(mediaType);
    source.Data.Should().Be(data);
  }

  [Fact]
  public void Constructor_WhenCalledWithNullMediaType_ItShouldThrowArgumentNullException()
  {
    string? mediaType = null;
    var data = "base64data";

    var action = () => new DocumentSource(mediaType!, data);

    action.Should().Throw<ArgumentNullException>();
  }

  [Fact]
  public void Constructor_WhenCalledWithNullData_ItShouldThrowArgumentNullException()
  {
    var mediaType = "application/pdf";
    string? data = null;

    var action = () => new DocumentSource(mediaType, data!);

    action.Should().Throw<ArgumentNullException>();
  }

  [Fact]
  public void Constructor_WhenCalled_ItShouldHaveTypePropertySetToBase64()
  {
    var mediaType = "application/pdf";
    var data = "base64data";

    var source = new DocumentSource(mediaType, data);

    source.Type.Should().Be("base64");
  }
}