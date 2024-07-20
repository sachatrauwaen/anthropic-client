namespace AnthropicClient.Tests.Unit.Models;

public class ImageSourceTests
{
  [Fact]
  public void Constructor_WhenCalledWithValidArguments_ItShouldSetProperties()
  {
    var mediaType = "image/jpeg";
    var data = "base64data";

    var imageSource = new ImageSource(mediaType, data);

    imageSource.MediaType.Should().Be(mediaType);
    imageSource.Data.Should().Be(data);
  }

  [Fact]
  public void Constructor_WhenCalledWithInvalidMediaType_ItShouldThrowArgumentException()
  {
    var mediaType = "invalid";
    var data = "base64data";

    var action = () => new ImageSource(mediaType, data);

    action.Should().Throw<ArgumentException>();
  }

  [Fact]
  public void Constructor_WhenCalledWithNullMediaType_ItShouldThrowArgumentNullException()
  {
    string? mediaType = null;
    var data = "base64data";

    var action = () => new ImageSource(mediaType!, data);

    action.Should().Throw<ArgumentNullException>();
  }

  [Fact]
  public void Constructor_WhenCalledWithNullData_ItShouldThrowArgumentNullException()
  {
    var mediaType = "image/jpeg";
    string? data = null;

    var action = () => new ImageSource(mediaType, data!);

    action.Should().Throw<ArgumentNullException>();
  }

  [Fact]
  public void Constructor_WhenCalled_ItShouldHaveTypePropertySetToBase64()
  {
    var mediaType = "image/jpeg";
    var data = "base64data";

    var imageSource = new ImageSource(mediaType, data);

    imageSource.Type.Should().Be("base64");
  }
}