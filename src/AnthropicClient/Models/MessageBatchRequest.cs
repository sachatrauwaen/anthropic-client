namespace AnthropicClient.Models;

/// <summary>
/// Represents a request to create a batch of messages.
/// </summary>
public class MessageBatchRequest
{
  /// <summary>
  /// Gets the requests to create messages.
  /// </summary>
  public List<MessageBatchRequestItem> Requests { get; init; } = [];

  /// <summary>
  /// Initializes a new instance of the <see cref="MessageBatchRequest"/> class.
  /// </summary>
  /// <param name="requests">The requests to create messages.</param>
  /// <exception cref="ArgumentException">Thrown when <paramref name="requests"/> is empty.</exception>
  /// <returns>An instance of the <see cref="MessageBatchRequest"/> class.</returns>
  public MessageBatchRequest(List<MessageBatchRequestItem> requests)
  {
    if (requests.Count == 0)
    {
      throw new ArgumentException($"{nameof(requests)} must not be empty.", nameof(requests));
    }

    Requests = requests;
  }
}