# AnthropicClient

This library for the Anthropic API is meant to simplify development in C# for Anthropic users.

> [!NOTE]
> This is an unofficial SDK for the Anthropic API. It was not built in consultation with Anthropic or any member of their organization.

This SDK was developed independently using existing libraries and the Anthropic API documentation as the starting point with the intention of making development of integrations done in C# with Anthropic quicker and more convenient.

> [!NOTE]  
> This client library is heavily inspired by the [Anthropic.SDK](https://github.com/tghamm/Anthropic.SDK) library. I chose to create a new library because I wanted to handle streaming and tool calling differently as well as have control over the client library as I plan to use it to build a connector for [SemanticKernel](https://github.com/microsoft/semantic-kernel). However if you are looking for a client library the Anthropic.SDK is a great place to start.

## 🛠️ Dependencies

### [Microsoft.Bcl.AsyncInterfaces](https://www.nuget.org/packages/Microsoft.Bcl.AsyncInterfaces/)

![Microsoft.Bcl.AsyncInterfaces NuGet Version](https://img.shields.io/nuget/v/Microsoft.Bcl.AsyncInterfaces)

Used to support async interfaces when streaming messages

### [System.Text.Json](https://www.nuget.org/packages/System.Text.Json/)

![NuGet Version](https://img.shields.io/nuget/v/System.Text.Json)

Used for JSON serialization and deserialization

## 💾 Installation

Install the package from [NuGet](https://www.nuget.org) using the following command:

```bash
dotnet add package AnthropicClient
```

## 🔑 API Key

In order to use the Anthropic API you will need an API key. You can get one by signing up at [Anthropic](https://www.anthropic.com/api). Please keep your API key secure and do not share it with others. Be mindful of where you store your API key and do not commit it to a public repository.

## 👨🏻‍💻 Start Coding

### `AnthropicApiClient`

The most common way to use the SDK is to create an `AnthropicApiClient` instance and call its methods. Its constructor requires two parameters:

- `apiKey` - your Anthropic API key
- `httpClient` - an `HttpClient` instance. You can configure and customize the `HttpClient` instance as needed. This library however will perform the necessary configuration to work with the Anthropic API. Such as setting the base address and adding the proper headers.

> [!NOTE]
> This library does not manage the lifecycle of the `HttpClient` instance. You should create and manage the lifecycle of the `HttpClient` instance in your application.

It is best practice to read the API key from a secure location such as a configuration file or environment variable. For example using the `appsettings.json` file:

```json
{
  "AnthropicApiKey": "YOUR_API"
}
```

Example constructing an `AnthropicApiClient` instance:

```csharp
using AnthropicClient;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
  .AddJsonFile("appsettings.json")
  .Build();

var apiKey = configuration["AnthropicApiKey"];

var client = new AnthropicApiClient(apiKey, new HttpClient());
```

### `IAnthropicApiClient`

The library does expose an interface `IAnthropicApiClient` that can be used for dependency injection and testing. The interface is implemented by the `AnthropicApiClient` class.

### Full API Documentation

This library was developed to make using the Anthropic API easier within a .NET application. If you are looking for the full API documentation you can find it at [Anthropic API Documentation](https://docs.anthropic.com/).

## Usage

The primary use case for working with the Anthropic API is to create a message in response to a request that includes one or more other messages. The created message can then be received either as a complete response or a stream of events. This can be used to create a conversation between the caller and the Anthropic's AI models and/or to use Anthropic's AI models to perform a task.

> [!NOTE]
> The following examples assume that you have already created an instance of the `AnthropicApiClient` class named `client`.

### Create a message

The `AnthropicApiClient` exposes a single method named `CreateMessageAsync` that can be used to create a message. The method requires a `MessageRequest` or a `StreamMessageRequest` instance as a parameter. The `MessageRequest` class is used to create a message whose response is not streamed and the `StreamMessageRequest` class is used to create a message whose response is streamed. The `MessageRequest` instance's properties can be set to configure how the message is created.

#### Non-Streaming

```csharp
using AnthropicClient.Models;

var response = await client.CreateMessageAsync(new MessageRequest(
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
  Console.WriteLine($"Failed to create message");
  Console.WriteLine($"Error Type: {0}", response.Error.Error.Type);
  Console.WriteLine($"Error Message: {0}", response.Error.Error.Message);
}

foreach (var content in response.Value.Content)
{
  switch (content)
  {
    case TextContent textContent:
      Console.WriteLine(textContent.Text);
      break;
  }
}
```

#### Streaming

Anthropic uses Server-Sent Events (SSE) to stream messages. The possible events and the format of those events are documented in the [Anthropic API Documentation](https://docs.anthropic.com/en/api/messages-streaming). This library provides a way to consume them deserialized into strongly-typed C# objects that are returned in an `IAsyncEnumerable` collection.

This allows you to consume the events as they are received and process them in the way that best fits your use case. The following example demonstrates how to consume the streamed events and build up the complete text response from the model.

```csharp
using AnthropicClient.Models;

var events = client.CreateMessageAsync(new StreamMessageRequest(
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

Console.WriteLine(msgBuilder.ToString());
```

##### Message Complete Event

This library also provides a custom `message_complete` event that is yielded when all the message's events have been received. This event is not part of Anthropic's SSE events but is provided to allow for easier consumption of the entire message response if desired and make it easier to implement built in tool calling.

```csharp
using AnthropicClient.Models;

var events = client.CreateMessageAsync(new StreamMessageRequest(
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

Console.WriteLine(textContent);
```

### Tool Calling



#### Create a tool

#### Call a tool

#### Call a tool in streamed message
