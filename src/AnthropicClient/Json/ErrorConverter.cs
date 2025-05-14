using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using AnthropicClient.Models;

namespace AnthropicClient.Json;

class ErrorConverter : JsonConverter
{
  public override bool CanConvert(Type objectType)
  {
    return objectType == typeof(Error);
  }

  public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
  {
    JObject jObject = JObject.Load(reader);
    var type = jObject["type"]?.ToString();

    return type switch
    {
      ErrorType.InvalidRequestError => jObject.ToObject<InvalidRequestError>(serializer),
      ErrorType.AuthenticationError => jObject.ToObject<AuthenticationError>(serializer),
      ErrorType.PermissionError => jObject.ToObject<PermissionError>(serializer),
      ErrorType.NotFoundError => jObject.ToObject<NotFoundError>(serializer),
      ErrorType.RateLimitError => jObject.ToObject<RateLimitError>(serializer),
      ErrorType.ApiError => jObject.ToObject<ApiError>(serializer),
      ErrorType.OverloadedError => jObject.ToObject<OverloadedError>(serializer),
      _ => throw new JsonSerializationException($"Unknown error type: {type}")
    };
  }

  public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
  {
    serializer.Serialize(writer, value);
  }
}


