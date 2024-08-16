using System.Text.Json;
using System.Text.Json.Serialization;

using AnthropicClient.Models;

namespace AnthropicClient.Json;

class MessageRequestConverter : JsonConverter<MessageRequest>
{
  public override MessageRequest Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    return JsonSerializer.Deserialize<MessageRequest>(ref reader, options)!;
  }

  public override void Write(Utf8JsonWriter writer, MessageRequest value, JsonSerializerOptions options)
  {
    throw new NotImplementedException();
  }
}