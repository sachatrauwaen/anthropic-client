using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using AnthropicClient.Models;

namespace AnthropicClient.Json;

class ToolChoiceConverter : JsonConverter
{
  public override bool CanConvert(Type objectType)
  {
    return objectType == typeof(ToolChoice);
  }

  public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
  {
    JObject jObject = JObject.Load(reader);
    var type = jObject["type"]?.ToString();

    return type switch
    {
      ToolChoiceType.Auto => jObject.ToObject<AutoToolChoice>(serializer),
      ToolChoiceType.Any => jObject.ToObject<AnyToolChoice>(serializer),
      ToolChoiceType.Tool => jObject.ToObject<SpecificToolChoice>(serializer),
      _ => throw new JsonSerializationException($"Unknown tool choice type: {type}")
    };
  }

  public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
  {
    serializer.Serialize(writer, value);
  }
}


