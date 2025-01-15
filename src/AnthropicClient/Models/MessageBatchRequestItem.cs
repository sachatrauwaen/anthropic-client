using System.Text.Json.Serialization;

using AnthropicClient.Utils;

namespace AnthropicClient.Models;

/// <summary>
/// Represents an item in a batch of messages.
/// </summary>
public class MessageBatchRequestItem
{
  /// <summary>
  /// Gets the custom identifier for the message.
  /// </summary>
  [JsonPropertyName("custom_id")]
  public string CustomId { get; init; }

  /// <summary>
  /// Gets the message request parameters.
  /// </summary>
  public MessageRequest Params { get; init; }

  /// <summary>
  /// Initializes a new instance of the <see cref="MessageBatchRequestItem"/> class.
  /// </summary>
  /// <param name="customId">The custom identifier for the message.</param>
  /// <param name="messageRequest">The message request parameters.</param>
  /// <exception cref="ArgumentException">Thrown when <paramref name="customId"/> is null or whitespace.</exception>
  /// <exception cref="ArgumentException">Thrown when <paramref name="messageRequest"/> is null.</exception>
  /// <returns>An instance of the <see cref="MessageBatchRequestItem"/> class.</returns>
  public MessageBatchRequestItem(string customId, MessageRequest messageRequest)
  {
    ArgumentValidator.ThrowIfNullOrWhitespace(customId, nameof(customId));
    ArgumentValidator.ThrowIfNull(messageRequest, nameof(messageRequest));

    CustomId = customId;
    Params = messageRequest;
  }
}