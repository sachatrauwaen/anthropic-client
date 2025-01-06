namespace AnthropicClient.Tests.Unit.Models;

public class PageTests : SerializationTest
{
  private const string BasePageJson = @"{
    ""first_id"": ""msg_123"",
    ""last_id"": ""msg_456"",
    ""has_more"": true
  }";

  private const string GenericPageJson = @"{
    ""first_id"": ""msg_123"",
    ""last_id"": ""msg_456"",
    ""has_more"": true,
    ""data"": [""item1"", ""item2""]
  }";

  private const string EmptyPageJson = @"{
    ""first_id"": """",
    ""last_id"": """",
    ""has_more"": false,
    ""data"": []
  }";

  [Fact]
  public void Constructor_WhenCalled_ItShouldReturnAnInstanceWithPropertiesSet()
  {
    var page = new Page();

    page.FirstId.Should().BeEmpty();
    page.LastId.Should().BeEmpty();
    page.HasMore.Should().BeFalse();
  }

  [Fact]
  public void Constructor_WhenCalledWithGeneric_ItShouldReturnAnInstanceWithPropertiesSet()
  {
    var page = new Page<string>();

    page.FirstId.Should().BeEmpty();
    page.LastId.Should().BeEmpty();
    page.HasMore.Should().BeFalse();
    page.Data.Should().BeEmpty();
  }

  [Fact]
  public void JsonDeserialization_WhenCalled_ItShouldHaveCorrectValues()
  {
    var result = Deserialize<Page>(BasePageJson);

    result.Should().NotBeNull();
    result!.FirstId.Should().Be("msg_123");
    result.LastId.Should().Be("msg_456");
    result.HasMore.Should().BeTrue();
  }

  [Fact]
  public void JsonSerialization_WhenCalled_ItShouldHaveExpectedShape()
  {
    var page = new Page
    {
      FirstId = "msg_123",
      LastId = "msg_456",
      HasMore = true
    };

    var result = Serialize(page);

    JsonAssert.Equal(BasePageJson, result);
  }

  [Fact]
  public void JsonDeserialization_WhenCalledWithData_ItShouldHaveCorrectValues()
  {
    var result = Deserialize<Page<string>>(GenericPageJson);

    result.Should().NotBeNull();
    result!.FirstId.Should().Be("msg_123");
    result.LastId.Should().Be("msg_456");
    result.HasMore.Should().BeTrue();
    result.Data.Should().BeEquivalentTo(["item1", "item2"]);
  }

  [Fact]
  public void JsonSerialization_WhenCalledWithData_ItShouldHaveExpectedShape()
  {
    var page = new Page<string>
    {
      FirstId = "msg_123",
      LastId = "msg_456",
      HasMore = true,
      Data = ["item1", "item2"]
    };

    var result = Serialize(page);

    JsonAssert.Equal(GenericPageJson, result);
  }

  [Fact]
  public void JsonDeserialization_WhenCalledWithEmptyData_ItShouldHaveCorrectValues()
  {
    var result = Deserialize<Page<string>>(EmptyPageJson);

    result.Should().NotBeNull();
    result!.FirstId.Should().BeEmpty();
    result.LastId.Should().BeEmpty();
    result.HasMore.Should().BeFalse();
    result.Data.Should().BeEmpty();
  }
}