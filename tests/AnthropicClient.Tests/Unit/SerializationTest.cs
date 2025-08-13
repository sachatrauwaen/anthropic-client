using Newtonsoft.Json;

namespace AnthropicClient.Tests.Unit;

public class SerializationTest
{
  private readonly JsonSerializerSettings _jsonSerializerOptions = JsonSerializationOptions.DefaultOptions;

  protected string Serialize<T>(T obj) => JsonConvert.SerializeObject(obj, _jsonSerializerOptions);

  protected T? Deserialize<T>(string json) => JsonConvert.DeserializeObject<T>(json, _jsonSerializerOptions);
}