using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using AnthropicClient.Models;

namespace AnthropicClient.Json;

class ContentConverter : JsonConverter
{
  public override bool CanConvert(Type objectType)
  {
    return objectType == typeof(Content);
  }

  public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
  {
    JObject jObject = JObject.Load(reader);
    var type = jObject["type"]?.ToString();

    return type switch
    {
      ContentType.Text => jObject.ToObject<TextContent>(serializer),
      ContentType.Image => jObject.ToObject<ImageContent>(serializer),
      ContentType.Document => jObject.ToObject<DocumentContent>(serializer),
      ContentType.ToolUse => jObject.ToObject<ToolUseContent>(serializer),
      ContentType.ToolResult => jObject.ToObject<ToolResultContent>(serializer),
      _ => throw new JsonSerializationException($"Unknown content type: {type}")
    };
  }

  public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
  {
    serializer.Serialize(writer, value);
  }
}


