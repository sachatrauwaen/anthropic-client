namespace AnthropicClient.Tests.EndToEnd;

public class EndToEndTest(ConfigurationFixture configFixture) : IClassFixture<ConfigurationFixture>
{
  protected readonly AnthropicApiClient _client = new(configFixture.AnthropicApiKey, new());
}