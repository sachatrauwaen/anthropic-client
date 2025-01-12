namespace AnthropicClient.Tests.Unit.Models;

public class ExpiredMessageBatchResultTests : SerializationTest
{
  private const string SampleJson = @"{
    ""type"": ""expired""
  }";

  [Fact]
  public void Constructor_WhenCalled_ItShouldReturnAnInstanceWithPropertiesSet()
  {
    var result = new ExpiredMessageBatchResult();

    result.Type.Should().Be(MessageBatchResultType.Expired);
  }

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldHaveExpectedShape()
  {
    var result = new ExpiredMessageBatchResult();

    var json = Serialize(result);

    JsonAssert.Equal(SampleJson, json);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldHaveExpectedProperties()
  {
    var result = Deserialize<ExpiredMessageBatchResult>(SampleJson);

    result!.Type.Should().Be(MessageBatchResultType.Expired);
  }
}