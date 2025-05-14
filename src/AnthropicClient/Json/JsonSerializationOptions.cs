using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace AnthropicClient.Json;

static class JsonSerializationOptions
{
  public static JsonSerializerSettings DefaultOptions => new()
  {
    NullValueHandling = NullValueHandling.Ignore,
    ContractResolver = new CamelCasePropertyNamesContractResolver(),
    Converters =
    {
      new ContentConverter(),
      new ToolChoiceConverter(),
      new ErrorConverter(),
      new EventDataConverter(),
      new ContentDeltaConverter(),
      new StringEnumConverter(),
      new MessageBatchResultConverter(),
    }
  };
}


