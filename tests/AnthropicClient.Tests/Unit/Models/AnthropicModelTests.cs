namespace AnthropicClient.Tests.Unit.Models;

public class AnthropicModelTests : SerializationTest
{
  private const string SampleJson = @"{
    ""type"": ""model"",
    ""id"": ""claude-3-opus-20240229"",
    ""display_name"": ""Claude 3 Opus"",
    ""created_at"": ""2024-02-29T00:00:00Z""
  }";

  [Fact]
  public void Constructor_WhenCalled_ItShouldReturnAnInstanceWithPropertiesSet()
  {
    var model = new AnthropicModel();

    model.Type.Should().BeEmpty();
    model.Id.Should().BeEmpty();
    model.DisplayName.Should().BeEmpty();
    model.CreatedAt.Should().Be(default);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldHaveExpectedValues()
  {
    var result = Deserialize<AnthropicModel>(SampleJson);

    result.Should().NotBeNull();
    result!.Type.Should().Be("model");
    result.Id.Should().Be("claude-3-opus-20240229");
    result.DisplayName.Should().Be("Claude 3 Opus");
    result.CreatedAt.Should().Be(new DateTimeOffset(2024, 2, 29, 0, 0, 0, TimeSpan.Zero));
  }

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldReturnExpectedShape()
  {
    var model = new AnthropicModel
    {
      Type = "model",
      Id = "claude-3-opus-20240229",
      DisplayName = "Claude 3 Opus",
      CreatedAt = new DateTimeOffset(2024, 2, 29, 0, 0, 0, TimeSpan.Zero)
    };

    var result = Serialize(model);

    var expectedJson = @"{
      ""type"": ""model"",
      ""id"": ""claude-3-opus-20240229"",
      ""display_name"": ""Claude 3 Opus"",
      ""created_at"": ""2024-02-29T00:00:00+00:00""
    }";

    JsonAssert.Equal(expectedJson, result);
  }
}