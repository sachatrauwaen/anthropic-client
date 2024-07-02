namespace AnthropicClient.Tests.Integration;

public class ErrorTestData : IEnumerable<object[]>
{
  public IEnumerator<object[]> GetEnumerator()
  {
    yield return new object[]
    {
      HttpStatusCode.BadRequest,
      @"{
        ""type"": ""error"",
        ""error"": {
          ""type"": ""invalid_request_error"",
          ""message"": ""Invalid request.""
        }
      }",
      typeof(InvalidRequestError)
    };

    yield return new object[]
    {
      HttpStatusCode.Unauthorized,
      @"{
        ""type"": ""error"",
        ""error"": {
          ""type"": ""authentication_error"",
          ""message"": ""Authentication error.""
        }
      }",
      typeof(AuthenticationError)
    };

    yield return new object[]
    {
      HttpStatusCode.Forbidden,
      @"{
        ""type"": ""error"",
        ""error"": {
          ""type"": ""permission_error"",
          ""message"": ""Permission error.""
        }
      }",
      typeof(PermissionError)
    };

    yield return new object[]
    {
      HttpStatusCode.NotFound,
      @"{
        ""type"": ""error"",
        ""error"": {
          ""type"": ""not_found_error"",
          ""message"": ""Not found error.""
        }
      }",
      typeof(NotFoundError)
    };

    yield return new object[]
    {
      HttpStatusCode.TooManyRequests,
      @"{
        ""type"": ""error"",
        ""error"": {
          ""type"": ""rate_limit_error"",
          ""message"": ""Rate limit error.""
        }
      }",
      typeof(RateLimitError)
    };

    yield return new object[]
    {
      HttpStatusCode.InternalServerError,
      @"{
        ""type"": ""error"",
        ""error"": {
          ""type"": ""api_error"",
          ""message"": ""Internal server error.""
        }
      }",
      typeof(ApiError)
    };

    yield return new object[]
    {
      (HttpStatusCode)529,
      @"{
        ""type"": ""error"",
        ""error"": {
          ""type"": ""overloaded_error"",
          ""message"": ""Overloaded error.""
        }
      }",
      typeof(OverloadedError)
    };
  }

  IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}