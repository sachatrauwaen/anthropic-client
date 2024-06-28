using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

public class JsonDelta : ContentDelta
{
  [JsonPropertyName("partial_json")]
  public string PartialJson { get; init; } = string.Empty;

  [JsonConstructor]
  internal JsonDelta() : base(ContentDeltaType.JsonDelta)
  {
  }

  public JsonDelta(string partialJson) : base(ContentDeltaType.JsonDelta)
  {
    PartialJson = partialJson;
  }
}