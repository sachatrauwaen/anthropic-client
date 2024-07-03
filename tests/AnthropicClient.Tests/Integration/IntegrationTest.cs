namespace AnthropicClient.Tests.Integration;

public class IntegrationTest
{
  protected readonly MockHttpMessageHandler _mockHttpMessageHandler = new();
  protected AnthropicApiClient Client => CreateClient();

  private AnthropicApiClient CreateClient()
  {
    return new AnthropicApiClient("test-key", _mockHttpMessageHandler.ToHttpClient());
  }
}

public static class MockHttpMessageHandlerExtensions
{
  private static MockedRequest SetupBaseRequest(this MockHttpMessageHandler mockHttpMessageHandler)
  {
    return mockHttpMessageHandler
      .When(HttpMethod.Post, "https://api.anthropic.com/v1/messages")
      .WithHeaders(new Dictionary<string, string>
      {
        { "anthropic-version", "2023-06-01" },
        { "x-api-key", "test-key" }
      });
  }

  public static MockedRequest WhenCreateMessageRequest(this MockHttpMessageHandler mockHttpMessageHandler)
  {
    return mockHttpMessageHandler
      .SetupBaseRequest()
      .WithJsonContent<ChatMessageRequest>(r => r.Stream == false, JsonSerializationOptions.DefaultOptions);
  }

  public static MockedRequest WhenCreateStreamMessageRequest(this MockHttpMessageHandler mockHttpMessageHandler)
  {
    return mockHttpMessageHandler
      .SetupBaseRequest()
      .WithJsonContent<StreamChatMessageRequest>(r => r.Stream == true, JsonSerializationOptions.DefaultOptions);
  }
}