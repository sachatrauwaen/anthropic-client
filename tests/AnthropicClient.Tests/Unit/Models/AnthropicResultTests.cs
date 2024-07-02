namespace AnthropicClient.Tests.Unit.Models;

public class AnthropicResultTests
{
  [Fact]
  public void Success_WhenCalled_ItShouldReturnSuccessResult()
  {
    var value = "success";
    var headers = new AnthropicHeaders();

    var actual = AnthropicResult<string>.Success(value, headers);

    actual.IsSuccess.Should().BeTrue();
    actual.Value.Should().Be(value);
    actual.Headers.Should().Be(headers);
  }

  [Fact]
  public void Failure_WhenCalled_ItShouldReturnFailureResult()
  {
    var error = new AnthropicError(new AuthenticationError("message"));
    var headers = new AnthropicHeaders();

    var actual = AnthropicResult<string>.Failure(error, headers);

    actual.IsSuccess.Should().BeFalse();
    actual.Error.Should().Be(error);
    actual.Headers.Should().Be(headers);
  }
}