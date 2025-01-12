namespace AnthropicClient.Tests.Unit.Models;

public class CanceledMessageBatchResultTests : SerializationTest
{
  private const string SampleJson = @"{
    ""type"": ""canceled""
  }";

  [Fact]
  public void Constructor_WhenCalled_ItShouldReturnAnInstanceWithPropertiesSet()
  {
    var result = new CanceledMessageBatchResult();

    result.Type.Should().Be(MessageBatchResultType.Canceled);
  }

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldHaveExpectedShape()
  {
    var result = new CanceledMessageBatchResult();

    var json = Serialize(result);

    JsonAssert.Equal(SampleJson, json);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldHaveExpectedProperties()
  {
    var result = Deserialize<CanceledMessageBatchResult>(SampleJson);

    result!.Type.Should().Be(MessageBatchResultType.Canceled);
  }
}