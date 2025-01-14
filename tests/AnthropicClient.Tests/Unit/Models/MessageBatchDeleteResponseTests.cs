namespace AnthropicClient.Tests.Unit.Models;

public class MessageBatchDeleteResponseTests : SerializationTest
{
  private const string SampleJson = @"{
    ""id"": ""test-id"",
    ""type"": ""test-type""
  }";

  [Fact]
  public void Constructor_WhenCalled_ItShouldReturnAnInstanceWithPropertiesSet()
  {
    var result = new MessageBatchDeleteResponse();

    result.Id.Should().BeEmpty();
    result.Type.Should().BeEmpty();
  }

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldHaveExpectedShape()
  {
    var result = new MessageBatchDeleteResponse
    {
      Id = "test-id",
      Type = "test-type"
    };

    var json = Serialize(result);

    JsonAssert.Equal(SampleJson, json);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldHaveExpectedValues()
  {
    var result = Deserialize<MessageBatchDeleteResponse>(SampleJson);

    result!.Id.Should().Be("test-id");
    result.Type.Should().Be("test-type");
  }
}