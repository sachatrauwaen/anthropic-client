using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

/// <summary>
/// Represents a JSON delta.
/// </summary>
public class JsonDelta : ContentDelta
{
  /// <summary>
  /// Gets the partial JSON.
  /// </summary>
  [JsonPropertyName("partial_json")]
  public string PartialJson { get; init; } = string.Empty;

  [JsonConstructor]
  internal JsonDelta() : base(ContentDeltaType.JsonDelta)
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="JsonDelta"/> class.
  /// </summary>
  /// <param name="partialJson">The partial JSON.</param>
  /// <returns>A new instance of the <see cref="JsonDelta"/> class.</returns>
  public JsonDelta(string partialJson) : base(ContentDeltaType.JsonDelta)
  {
    PartialJson = partialJson;
  }
}