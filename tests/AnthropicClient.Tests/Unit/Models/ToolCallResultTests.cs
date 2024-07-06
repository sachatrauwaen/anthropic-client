namespace AnthropicClient.Tests.Unit.Models;

public class ToolCallResultTests
{
  [Fact]
  public void Success_WhenCalled_ItShouldReturnSuccessResult()
  {
    var value = "success";

    var actual = ToolCallResult<string>.Success(value);

    actual.IsSuccess.Should().BeTrue();
    actual.IsFailure.Should().BeFalse();
    actual.Value.Should().Be(value);

    var action = () => { var _ = actual.Error; };
    action.Should().Throw<InvalidOperationException>();
  }

  [Fact]
  public void Failure_WhenCalled_ItShouldReturnFailureResult()
  {
    var error = new Exception();

    var actual = ToolCallResult<string>.Failure(error);

    actual.IsSuccess.Should().BeFalse();
    actual.IsFailure.Should().BeTrue();
    actual.Error.Should().Be(error);
    
    var action = () => { var _ = actual.Value; };
    action.Should().Throw<InvalidOperationException>();
  }
}