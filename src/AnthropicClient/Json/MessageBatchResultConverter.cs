using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using AnthropicClient.Models;

namespace AnthropicClient.Json;

class MessageBatchResultConverter : JsonConverter
{
  public override bool CanConvert(Type objectType)
  {
    return objectType == typeof(MessageBatchResult);
  }

  public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
  {
    JObject jObject = JObject.Load(reader);
    var type = jObject["type"]?.ToString();

    return type switch
    {
      MessageBatchResultType.Succeeded => jObject.ToObject<SucceededMessageBatchResult>(serializer),
      MessageBatchResultType.Errored => jObject.ToObject<ErroredMessageBatchResult>(serializer),
      MessageBatchResultType.Canceled => jObject.ToObject<CanceledMessageBatchResult>(serializer),
      MessageBatchResultType.Expired => jObject.ToObject<ExpiredMessageBatchResult>(serializer),
      _ => throw new JsonSerializationException($"Unknown message batch result type: {type}")
    };
  }

  public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
  {
    serializer.Serialize(writer, value);
  }
}


