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
    actual.IsFailure.Should().BeFalse();
    actual.Value.Should().Be(value);

    var action = () => { var _ = actual.Error; };
    action.Should().Throw<InvalidOperationException>();

    actual.Headers.Should().Be(headers);
  }

  [Fact]
  public void Failure_WhenCalled_ItShouldReturnFailureResult()
  {
    var error = new AnthropicError(new AuthenticationError("message"));
    var headers = new AnthropicHeaders();

    var actual = AnthropicResult<string>.Failure(error, headers);

    actual.IsSuccess.Should().BeFalse();
    actual.IsFailure.Should().BeTrue();
    actual.Error.Should().Be(error);

    var action = () => { var _ = actual.Value; };
    action.Should().Throw<InvalidOperationException>();

    actual.Headers.Should().Be(headers);
  }
}