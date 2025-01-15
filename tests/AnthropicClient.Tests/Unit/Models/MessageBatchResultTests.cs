namespace AnthropicClient.Tests.Unit.Models;

public class MessageBatchResultTests : SerializationTest
{
  [Fact]
  public void JsonDeserialization_WhenHasUnknownType_ItShouldThrowException()
  {
    var json = @"{""type"":""unknown""}";

    var action = () => Deserialize<MessageBatchResult>(json);

    action.Should().Throw<JsonException>();
  }

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldHaveExpectedShape()
  {
    var expectedJson = @"{""type"":""expired""}";
    var messageBatchResult = new ExpiredMessageBatchResult();

    var json = Serialize<MessageBatchResult>(messageBatchResult);

    JsonAssert.Equal(expectedJson, json);
  }
}