using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

/// <summary>
/// Represents a page.
/// </summary>
public class Page
{
  /// <summary>
  /// The id of the first item in the page.
  /// </summary>
  [JsonPropertyName("first_id")]
  public string FirstId { get; init; } = string.Empty;

  /// <summary>
  /// The id of the last item in the page.
  /// </summary>
  [JsonPropertyName("last_id")]
  public string LastId { get; init; } = string.Empty;

  /// <summary>
  /// Indicates whether there is more data to be retrieved.
  /// </summary>
  [JsonPropertyName("has_more")]
  public bool HasMore { get; init; }
}

/// <summary>
/// Represents a page with data.
/// </summary>
public class Page<T> : Page
{
  /// <summary>
  /// The data in the page.
  /// </summary>
  public T[] Data { get; init; } = [];
}