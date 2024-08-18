namespace AnthropicClient.Tests.EndToEnd;

public class EndToEndTest(ConfigurationFixture configFixture) : IClassFixture<ConfigurationFixture>
{
  protected readonly AnthropicApiClient _client = new(configFixture.AnthropicApiKey, new());
  protected AnthropicApiClient CreateClient(HttpClient httpClient) => new(configFixture.AnthropicApiKey, httpClient);
}