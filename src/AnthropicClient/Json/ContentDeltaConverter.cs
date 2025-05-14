using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using AnthropicClient.Models;

namespace AnthropicClient.Json;

class ContentDeltaConverter : JsonConverter
{
  public override bool CanConvert(Type objectType)
  {
    return objectType == typeof(ContentDelta);
  }

  public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
  {
    JObject jObject = JObject.Load(reader);
    var type = jObject["type"]?.ToString();

    return type switch
    {
      ContentDeltaType.TextDelta => jObject.ToObject<TextDelta>(serializer),
      ContentDeltaType.JsonDelta => jObject.ToObject<JsonDelta>(serializer),
      _ => throw new JsonSerializationException($"Unknown content type: {type}")
    };
  }

  public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
  {
    serializer.Serialize(writer, value);
  }
}


