#pragma warning disable xUnit1004

using Xunit.Abstractions;

namespace AnthropicClient.Tests.Examples;

public class Examples(ConfigurationFixture config, ITestOutputHelper console) : IClassFixture<ConfigurationFixture>
{
  private readonly ITestOutputHelper _console = console;
  private readonly AnthropicApiClient _client = new(config.AnthropicApiKey, new());

  [Example]
  public async Task CreateMessage()
  {
    var response = await _client.CreateMessageAsync(new MessageRequest(
      AnthropicModels.Claude3Haiku,
      [
        new(
          MessageRole.User, 
          [new TextContent("Please write a haiku about the ocean.")]
        )
      ]
    ));

    if (response.IsSuccess is false)
    {
      _console.WriteLine($"Failed to create message");
      _console.WriteLine($"Error Type: {0}", response.Error.Error.Type);
      _console.WriteLine($"Error Message: {0}", response.Error.Error.Message);
    }

    foreach (var content in response.Value.Content)
    {
      switch (content)
      {
        case TextContent textContent:
          _console.WriteLine(textContent.Text);
          break;
      }
    }
  }
  
  [Example]
  public async Task CreateAndStreamMessage()
  {
    var events = _client.CreateMessageAsync(new StreamMessageRequest(
      AnthropicModels.Claude3Haiku,
      [
        new(
          MessageRole.User, 
          [new TextContent("Please write a haiku about the ocean.")]
        )
      ]
    ));

    var msgBuilder = new StringBuilder();

    await foreach (var e in events)
    {
      switch (e.Data)
      {
        case var data when data is ContentDeltaEventData contentData:
          switch (contentData.Delta)
          {
            case var delta when delta is TextDelta textDelta:
              msgBuilder.Append(textDelta.Text);
              break;
          }
          break;
      }
    }

    _console.WriteLine(msgBuilder.ToString());
  }

  [Example]
  public async Task CreateStreamMessageAndGetCompleteMessageResponse()
  {
    var events = _client.CreateMessageAsync(new StreamMessageRequest(
      AnthropicModels.Claude3Haiku,
      [
        new(
          MessageRole.User, 
          [new TextContent("Please write a haiku about the ocean.")]
        )
      ]
    ));

    MessageResponse? response = null;

    await foreach (var e in events)
    {
      switch (e.Data)
      {
        case var data when data is MessageCompleteEventData msgData:
          response = msgData.Message;
          break;
      }
    }

    var textContent = response?.Content
      .OfType<TextContent>()
      .Aggregate(new StringBuilder(), (sb, c) => sb.Append(c.Text))
      .ToString();

    _console.WriteLine(textContent);
  }
}