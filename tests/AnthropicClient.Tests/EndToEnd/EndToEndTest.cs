namespace AnthropicClient.Tests.EndToEnd;

public class EndToEndTest(
  HttpClientFixture httpClientFixture,
  ConfigurationFixture configFixture
) : IClassFixture<HttpClientFixture>, IClassFixture<ConfigurationFixture>
{
  protected readonly AnthropicApiClient _client = new(configFixture.AnthropicApiKey, httpClientFixture.HttpClient);
}