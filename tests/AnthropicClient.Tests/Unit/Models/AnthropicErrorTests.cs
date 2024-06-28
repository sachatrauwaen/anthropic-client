namespace AnthropicClient.Tests.Unit.Models;

public class AnthropicErrorTests : SerializationTest
{
  private readonly string _testApiErrorJson = @"{
    ""type"": ""error"",
    ""error"": {
      ""type"": ""api_error"",
      ""message"": ""message""
    }
  }";

  private readonly string _testAuthenticationErrorJson = @"{
    ""type"": ""error"",
    ""error"": {
      ""type"": ""authentication_error"",
      ""message"": ""message""
    }
  }";

  private readonly string _testRateLimitErrorJson = @"{
    ""type"": ""error"",
    ""error"": {
      ""type"": ""rate_limit_error"",
      ""message"": ""message""
    }
  }";

  private readonly string _testPermissionErrorJson = @"{
    ""type"": ""error"",
    ""error"": {
      ""type"": ""permission_error"",
      ""message"": ""message""
    }
  }";

  private readonly string _testNotFoundErrorJson = @"{
    ""type"": ""error"",
    ""error"": {
      ""type"": ""not_found_error"",
      ""message"": ""message""
    }
  }";

  private readonly string _testOverloadedErrorJson = @"{
    ""type"": ""error"",
    ""error"": {
      ""type"": ""overloaded_error"",
      ""message"": ""message""
    }
  }";

  private readonly string _testInvalidRequestErrorJson = @"{
    ""type"": ""error"",
    ""error"": {
      ""type"": ""invalid_request_error"",
      ""message"": ""message""
    }
  }";

  [Fact]
  public void Constructor_WhenCalled_ItShouldInitializeProperties()
  {
    var apiError = new ApiError("message");

    var error = new AnthropicError(apiError);

    error.Type.Should().Be("error");
    error.Error.Should().BeSameAs(apiError);
  }

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldBeExpectedShape()
  {
    var apiError = new ApiError("message");
    var error = new AnthropicError(apiError);

    var json = Serialize(error);

    JsonAssert.Equal(_testApiErrorJson, json);
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldBeExpectedModel()
  {
    var error = Deserialize<AnthropicError>(_testApiErrorJson);

    error!.Type.Should().Be("error");
    error.Error.Should().BeOfType<ApiError>();
    error.Error!.Message.Should().Be("message");
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldBeExpectedModelForAuthenticationError()
  {
    var error = Deserialize<AnthropicError>(_testAuthenticationErrorJson);

    error!.Type.Should().Be("error");
    error.Error.Should().BeOfType<AuthenticationError>();
    error.Error!.Message.Should().Be("message");
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldBeExpectedModelForRateLimitError()
  {
    var error = Deserialize<AnthropicError>(_testRateLimitErrorJson);

    error!.Type.Should().Be("error");
    error.Error.Should().BeOfType<RateLimitError>();
    error.Error!.Message.Should().Be("message");
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldBeExpectedModelForPermissionError()
  {
    var error = Deserialize<AnthropicError>(_testPermissionErrorJson);

    error!.Type.Should().Be("error");
    error.Error.Should().BeOfType<PermissionError>();
    error.Error!.Message.Should().Be("message");
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldBeExpectedModelForNotFoundError()
  {
    var error = Deserialize<AnthropicError>(_testNotFoundErrorJson);

    error!.Type.Should().Be("error");
    error.Error.Should().BeOfType<NotFoundError>();
    error.Error!.Message.Should().Be("message");
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldBeExpectedModelForOverloadedError()
  {
    var error = Deserialize<AnthropicError>(_testOverloadedErrorJson);

    error!.Type.Should().Be("error");
    error.Error.Should().BeOfType<OverloadedError>();
    error.Error!.Message.Should().Be("message");
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldBeExpectedModelForInvalidRequestError()
  {
    var error = Deserialize<AnthropicError>(_testInvalidRequestErrorJson);

    error!.Type.Should().Be("error");
    error.Error.Should().BeOfType<InvalidRequestError>();
    error.Error!.Message.Should().Be("message");
  }

  [Fact]
  public void JsonDeserialization_WhenDeserialized_ItShouldBeExpectedModelForUnknownError()
  {
    var json = @"{
      ""type"": ""error"",
      ""error"": {
        ""type"": ""unknown_error"",
        ""message"": ""message""
      }
    }";

    var action = () => Deserialize<AnthropicError>(json);

    action.Should().Throw<JsonException>();
  }
}