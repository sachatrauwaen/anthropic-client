using System.Net.Http.Headers;

namespace AnthropicClient.Models;

/// <summary>
/// Represents headers included in Anthropic API responses.
/// </summary>
public class AnthropicHeaders
{
  private const string RequestIdHeaderKey = "request-id";
  private const string RateLimitRequestsLimitHeaderKey = "anthropic-ratelimit-requests-limit";
  private const string RateLimitRequestsRemainingHeaderKey = "anthropic-ratelimit-requests-remaining";
  private const string RateLimitRequestsResetHeaderKey = "anthropic-ratelimit-requests-reset";
  private const string RateLimitTokensLimitHeaderKey = "anthropic-ratelimit-tokens-limit";
  private const string RateLimitTokensRemainingHeaderKey = "anthropic-ratelimit-tokens-remaining";
  private const string RateLimitTokensResetHeaderKey = "anthropic-ratelimit-tokens-reset";
  private const string RetryAfterHeaderKey = "retry-after";

  /// <summary>
  /// Gets the request ID.
  /// </summary>
  public string RequestId { get; init; } = string.Empty;

  /// <summary>
  /// Gets the rate limit requests limit.
  /// </summary>
  public int RateLimitRequestsLimit { get; init; }

  /// <summary>
  /// Gets the rate limit requests remaining.
  /// </summary>
  public int RateLimitRequestsRemaining { get; init; }

  /// <summary>
  /// Gets the time of the rate limit requests reset.
  /// </summary>
  public DateTimeOffset RateLimitRequestsReset { get; init; }

  /// <summary>
  /// Gets the rate limit tokens limit.
  /// </summary>
  public int RateLimitTokensLimit { get; init; }

  /// <summary>
  /// Gets the rate limit tokens remaining.
  /// </summary>
  public int RateLimitTokensRemaining { get; init; }

  /// <summary>
  /// Gets the time of the rate limit tokens reset.
  /// </summary>
  public DateTimeOffset RateLimitTokensReset { get; init; }

  /// <summary>
  /// Gets the retry after value.
  /// </summary>
  public int RetryAfter { get; init; }

  internal AnthropicHeaders()
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="AnthropicHeaders"/> class.
  /// </summary>
  /// <param name="headers">The HTTP response headers.</param>
  /// <returns>A new instance of the <see cref="AnthropicHeaders"/> class.</returns>
  public AnthropicHeaders(HttpResponseHeaders headers)
  {
    RequestId = headers.GetValues(RequestIdHeaderKey).FirstOrDefault() ?? string.Empty;
    RateLimitRequestsLimit = int.Parse(headers.GetValues(RateLimitRequestsLimitHeaderKey).FirstOrDefault() ?? "0");
    RateLimitRequestsRemaining = int.Parse(headers.GetValues(RateLimitRequestsRemainingHeaderKey).FirstOrDefault() ?? "0");
    RateLimitRequestsReset = DateTimeOffset.Parse(headers.GetValues(RateLimitRequestsResetHeaderKey).FirstOrDefault() ?? DateTimeOffset.MinValue.ToString());
    RateLimitTokensLimit = int.Parse(headers.GetValues(RateLimitTokensLimitHeaderKey).FirstOrDefault() ?? "0");
    RateLimitTokensRemaining = int.Parse(headers.GetValues(RateLimitTokensRemainingHeaderKey).FirstOrDefault() ?? "0");
    RateLimitTokensReset = DateTimeOffset.Parse(headers.GetValues(RateLimitTokensResetHeaderKey).FirstOrDefault() ?? DateTimeOffset.MinValue.ToString());
    RetryAfter = int.Parse(headers.GetValues(RetryAfterHeaderKey).FirstOrDefault() ?? "0");
  }
}