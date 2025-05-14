namespace AnthropicClient.Models;

/// <summary>
/// Represents a message batch delete response.
/// </summary>
public class MessageBatchDeleteResponse
{
  /// <summary>
  /// Gets the ID of the message batch that was deleted.
  /// </summary>
  public string Id { get; init; } = string.Empty;

  /// <summary>
  /// Gets the type of the message batch response.
  /// </summary>
  public string Type { get; init; } = string.Empty;
}


