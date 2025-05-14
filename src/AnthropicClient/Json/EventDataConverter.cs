using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using AnthropicClient.Models;

namespace AnthropicClient.Json;

class EventDataConverter : JsonConverter
{
  public override bool CanConvert(Type objectType)
  {
    return objectType == typeof(EventData);
  }

  public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
  {
    JObject jObject = JObject.Load(reader);
    var type = jObject["type"]?.ToString();

    return type switch
    {
      EventType.Ping => jObject.ToObject<PingEventData>(serializer),
      EventType.Error => jObject.ToObject<ErrorEventData>(serializer),
      EventType.MessageStart => jObject.ToObject<MessageStartEventData>(serializer),
      EventType.MessageDelta => jObject.ToObject<MessageDeltaEventData>(serializer),
      EventType.MessageStop => jObject.ToObject<MessageStopEventData>(serializer),
      EventType.ContentBlockStart => jObject.ToObject<ContentStartEventData>(serializer),
      EventType.ContentBlockDelta => jObject.ToObject<ContentDeltaEventData>(serializer),
      EventType.ContentBlockStop => jObject.ToObject<ContentStopEventData>(serializer),
      _ => throw new JsonSerializationException($"Unknown content type: {type}")
    };
  }

  public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
  {
    serializer.Serialize(writer, value);
  }
}


