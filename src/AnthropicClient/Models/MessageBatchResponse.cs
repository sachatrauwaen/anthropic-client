using Newtonsoft.Json;

namespace AnthropicClient.Models;

/// <summary>
/// Represents a response to a batch of messages.
/// </summary>
public class MessageBatchResponse
{
  /// <summary>
  /// Gets the identifier of the batch.
  /// </summary>
  public string Id { get; init; } = string.Empty;

  /// <summary>
  /// Gets the type of the batch.
  /// </summary>
  public string Type { get; init; } = string.Empty;

  /// <summary>
  /// Gets the processing status of the batch.
  /// </summary>
  [JsonProperty("processing_status")]
  public string ProcessingStatus { get; init; } = string.Empty;

  /// <summary>
  /// Gets the counts of requests in the batch.
  /// </summary>
  [JsonProperty("request_counts")]
  public MessageBatchRequestCounts RequestCounts { get; init; } = new();

  /// <summary>
  /// Gets the date and time when the batch ended.
  /// </summary>
  [JsonProperty("ended_at")]
  public DateTimeOffset? EndedAt { get; init; } = DateTimeOffset.MinValue;

  /// <summary>
  /// Gets the date and time when the batch was created.
  /// </summary>
  [JsonProperty("created_at")]
  public DateTimeOffset CreatedAt { get; init; }

  /// <summary>
  /// Gets the date and time when the batch expires.
  /// </summary>
  [JsonProperty("expires_at")]
  public DateTimeOffset ExpiresAt { get; init; }

  /// <summary>
  /// Gets the date and time when the batch was archived.
  /// </summary>
  [JsonProperty("archived_at")]
  public DateTimeOffset? ArchivedAt { get; init; } = DateTimeOffset.MinValue;

  /// <summary>
  /// Gets the date and time when the batch cancellation was initiated.
  /// </summary>
  [JsonProperty("cancel_initiated_at")]
  public DateTimeOffset? CancelInitiatedAt { get; init; } = DateTimeOffset.MinValue;

  /// <summary>
  /// Gets the URL to the results of the batch.
  /// </summary>
  [JsonProperty("results_url")]
  public string? ResultsUrl { get; init; } = string.Empty;
}

/// <summary>
/// Represents the counts of requests in a batch of messages.
/// </summary>
public class MessageBatchRequestCounts
{
  /// <summary>
  /// Gets the number of requests in the batch that are processing.
  /// </summary>
  public int Processing { get; init; }

  /// <summary>
  /// Gets the number of requests in the batch that succeeded.
  /// </summary>
  public int Succeeded { get; init; }

  /// <summary>
  /// Gets the number of requests in the batch that errored.
  /// </summary>
  public int Errored { get; init; }

  /// <summary>
  /// Gets the number of requests in the batch that were cancelled.
  /// </summary>
  public int Canceled { get; init; }

  /// <summary>
  /// Gets the number of requests in the batch that expired.
  /// </summary>
  public int Expired { get; init; }
}


