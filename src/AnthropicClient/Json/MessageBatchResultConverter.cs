using System.Text.Json;
using System.Text.Json.Serialization;

using AnthropicClient.Models;

namespace AnthropicClient.Json;

class MessageBatchResultConverter : JsonConverter<MessageBatchResult>
{
  public override MessageBatchResult Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    using var jsonDocument = JsonDocument.ParseValue(ref reader);
    var root = jsonDocument.RootElement;
    var type = root.GetProperty("type").GetString();
    return type switch
    {
      MessageBatchResultType.Succeeded => JsonSerializer.Deserialize<SucceededMessageBatchResult>(root.GetRawText(), options)!,
      MessageBatchResultType.Errored => JsonSerializer.Deserialize<ErroredMessageBatchResult>(root.GetRawText(), options)!,
      MessageBatchResultType.Canceled => JsonSerializer.Deserialize<CanceledMessageBatchResult>(root.GetRawText(), options)!,
      MessageBatchResultType.Expired => JsonSerializer.Deserialize<ExpiredMessageBatchResult>(root.GetRawText(), options)!,
      _ => throw new JsonException($"Unknown message batch result type: {type}")
    };
  }

  public override void Write(Utf8JsonWriter writer, MessageBatchResult value, JsonSerializerOptions options)
  {
    JsonSerializer.Serialize(writer, value, value.GetType(), options);
  }
}