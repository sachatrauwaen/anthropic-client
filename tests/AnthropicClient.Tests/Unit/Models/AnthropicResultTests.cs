namespace AnthropicClient.Tests.Unit.Models;

public class AnthropicResultTests 
{
  [Fact]
  public void Success_WhenCalled_ItShouldReturnSuccessResult()
  {
    var value = "success";
    var requestId = Guid.NewGuid().ToString();

    var actual = AnthropicResult<string>.Success(value, requestId);

    actual.IsSuccess.Should().BeTrue();
    actual.Value.Should().Be(value);
    actual.RequestId.Should().Be(requestId);
  }

  [Fact]
  public void Failure_WhenCalled_ItShouldReturnFailureResult()
  {
    var error = new AnthropicError(new AuthenticationError("message"));
    var requestId = Guid.NewGuid().ToString();

    var actual = AnthropicResult<string>.Failure(error, requestId);

    actual.IsSuccess.Should().BeFalse();
    actual.Error.Should().Be(error);
    actual.RequestId.Should().Be(requestId);
  }
}