using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

/// <summary>
/// Represents a request to page through a collection of items.
/// </summary>
public class PagingRequest
{
  private const int LimitMinimum = 1;
  private const int LimitMaximum = 1000;
  private const int DefaultLimit = 20;

  /// <summary>
  /// The ID of the item before which to start the page.
  /// </summary>
  [JsonPropertyName("before_id")]
  public string BeforeId { get; init; }

  /// <summary>
  /// The ID of the item after which to start the page.
  /// </summary>
  [JsonPropertyName("after_id")]
  public string AfterId { get; init; }

  /// <summary>
  /// The maximum number of items to return in the page.
  /// </summary>
  public int Limit { get; init; }

  /// <summary>
  /// Initializes a new instance of the <see cref="PagingRequest"/> class.
  /// </summary>
  /// <param name="beforeId">The ID of the item before which to start the page.</param>
  /// <param name="afterId">The ID of the item after which to start the page.</param>
  /// <param name="limit">The maximum number of items to return in the page.</param>
  /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="limit"/> is less than 1 or greater than 1000.</exception>
  /// <returns>A new instance of the <see cref="PagingRequest"/> class.</returns>
  public PagingRequest(
    string beforeId = "",
    string afterId = "",
    int limit = DefaultLimit
  )
  {
    if (limit is < LimitMinimum or > LimitMaximum)
    {
      throw new ArgumentOutOfRangeException(nameof(limit), $"{nameof(limit)} must be between {LimitMinimum} and {LimitMaximum}.");
    }

    BeforeId = beforeId;
    AfterId = afterId;
    Limit = limit;
  }

  /// <summary>
  /// Converts the <see cref="PagingRequest"/> to a query string.
  /// </summary>
  /// <returns>The query string representation of the <see cref="PagingRequest"/>.</returns>
  public string ToQueryParameters()
  {
    var parameters = new List<string>();

    if (string.IsNullOrEmpty(BeforeId) is false)
    {
      parameters.Add($"before_id={BeforeId}");
    }

    if (string.IsNullOrEmpty(AfterId) is false)
    {
      parameters.Add($"after_id={AfterId}");
    }

    parameters.Add($"limit={Limit}");

    return string.Join("&", parameters);
  }
}