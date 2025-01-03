namespace AnthropicClient.Tests.Unit.Models;

public class TokenCountResponseTests : SerializationTest
{
  [Fact]
  public void Constructor_WhenCalled_ShouldInitializeProperties()
  {
    var expectedTokenCount = 1;

    var response = new TokenCountResponse
    {
      InputTokens = expectedTokenCount
    };

    response.InputTokens.Should().Be(expectedTokenCount);
  }

  [Fact]
  public void JsonSerialization_WhenCalled_ItShouldSerializeCorrectly()
  {
    var expectedJson = @"{
      ""input_tokens"": 1
    }";

    var response = new TokenCountResponse
    {
      InputTokens = 1
    };

    var actual = Serialize(response);

    JsonAssert.Equal(expectedJson, actual);
  }

  [Fact]
  public void JsonDeserialization_WhenCalled_ItShouldDeserializeCorrectly()
  {
    var json = @"{
      ""input_tokens"": 1
    }";

    var response = Deserialize<TokenCountResponse>(json);

    response!.InputTokens.Should().Be(1);
  }
}