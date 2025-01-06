namespace AnthropicClient.Tests.Unit.Models;

public class PagingRequestTests : SerializationTest
{
  [Fact]
  public void Constructor_WhenLimitIsLessThanMinimum_ItShouldThrowArgumentOutOfRangeException()
  {
    var act = () => new PagingRequest(limit: 0);

    act.Should().Throw<ArgumentOutOfRangeException>().WithMessage("limit must be between 1 and 1000. (Parameter 'limit')");
  }

  [Fact]
  public void Constructor_WhenLimitIsGreaterThanMaximum_ItShouldThrowArgumentOutOfRangeException()
  {
    var act = () => new PagingRequest(limit: 1001);

    act.Should().Throw<ArgumentOutOfRangeException>().WithMessage("limit must be between 1 and 1000. (Parameter 'limit')");
  }

  [Fact]
  public void ToQueryParameters_WhenNoPropertiesSet_ItShouldReturnEmptyString()
  {
    var pagingRequest = new PagingRequest();

    var result = pagingRequest.ToQueryParameters();

    result.Should().Be("limit=20");
  }

  [Fact]
  public void ToQueryParameters_WhenBeforeIdIsSet_ItShouldReturnBeforeId()
  {
    var pagingRequest = new PagingRequest(beforeId: "before-id");

    var result = pagingRequest.ToQueryParameters();

    result.Should().Be("before_id=before-id&limit=20");
  }

  [Fact]
  public void ToQueryParameters_WhenAfterIdIsSet_ItShouldReturnAfterId()
  {
    var pagingRequest = new PagingRequest(afterId: "after-id");

    var result = pagingRequest.ToQueryParameters();

    result.Should().Be("after_id=after-id&limit=20");
  }

  [Fact]
  public void ToQueryParameters_WhenLimitIsSetToNonDefaultValue_ItShouldReturnLimit()
  {
    var pagingRequest = new PagingRequest(limit: 10);

    var result = pagingRequest.ToQueryParameters();

    result.Should().Be("limit=10");
  }

  [Fact]
  public void ToQueryParameters_WhenAllPropertiesAreSet_ItShouldReturnAllProperties()
  {
    var pagingRequest = new PagingRequest(beforeId: "before-id", afterId: "after-id", limit: 10);

    var result = pagingRequest.ToQueryParameters();

    result.Should().Be("before_id=before-id&after_id=after-id&limit=10");
  }
}