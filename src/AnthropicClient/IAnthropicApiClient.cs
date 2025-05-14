using AnthropicClient.Models;

namespace AnthropicClient;

/// <summary>
/// Represents a client for interacting with the Anthropic API.
/// </summary>
public interface IAnthropicApiClient
{
  /// <summary>
  /// Creates a message asynchronously.
  /// </summary>
  /// <param name="request">The message request to create.</param>
  /// <returns>A task that represents the asynchronous operation. The task result contains the response as an <see cref="AnthropicResult{T}"/>.</returns>
  Task<AnthropicResult<MessageResponse>> CreateMessageAsync(MessageRequest request);

  /// <summary>
  /// Creates a message asynchronously and streams the response.
  /// </summary>
  /// <param name="request">The message request to create.</param>
  /// <returns>An asynchronous enumerable that yields the response event by event.</returns>
  IAsyncEnumerable<AnthropicEvent> CreateMessageAsync(StreamMessageRequest request);

  /// <summary>
  /// Creates a batch of messages asynchronously.
  /// </summary>
  /// <param name="request">The message batch request to create.</param>
  /// <returns>A task that represents the asynchronous operation. The task result contains the response as an <see cref="AnthropicResult{T}"/> where T is <see cref="MessageBatchResponse"/>.</returns>
  Task<AnthropicResult<MessageBatchResponse>> CreateMessageBatchAsync(MessageBatchRequest request);

  /// <summary>
  /// Gets a message batch asynchronously.
  /// </summary>
  /// <param name="batchId">The ID of the message batch to get.</param>
  /// <returns>A task that represents the asynchronous operation. The task result contains the response as an <see cref="AnthropicResult{T}"/> where T is <see cref="MessageBatchResponse"/>.</returns>
  Task<AnthropicResult<MessageBatchResponse>> GetMessageBatchAsync(string batchId);

  /// <summary>
  /// Lists the message batches asynchronously.
  /// </summary>
  /// <param name="request">The paging request to use for listing the message batches.</param>
  /// <returns>A task that represents the asynchronous operation. The task result contains the response as an <see cref="AnthropicResult{T}"/> where T is <see cref="Page{T}"/> where T is <see cref="MessageBatchResponse"/>.</returns>
  Task<AnthropicResult<Page<MessageBatchResponse>>> ListMessageBatchesAsync(PagingRequest? request = null);

  /// <summary>
  /// Lists all message batches asynchronously.
  /// </summary>
  /// <param name="limit">The maximum number of message batches to return in each page.</param>
  /// <returns>An asynchronous enumerable that yields the response as an <see cref="AnthropicResult{T}"/> where T is <see cref="Page{T}"/> where T is <see cref="MessageBatchResponse"/>.</returns>
  IAsyncEnumerable<AnthropicResult<Page<MessageBatchResponse>>> ListAllMessageBatchesAsync(int limit = 20);

  /// <summary>
  /// Cancels a message batch asynchronously.
  /// </summary>
  /// <param name="batchId">The ID of the message batch to cancel.</param>
  /// <returns>A task that represents the asynchronous operation. The task result contains the response as an <see cref="AnthropicResult{T}"/> where T is <see cref="MessageBatchResponse"/>.</returns>
  Task<AnthropicResult<MessageBatchResponse>> CancelMessageBatchAsync(string batchId);

  /// <summary>
  /// Deletes a message batch asynchronously.
  /// </summary>
  /// <param name="batchId">The ID of the message batch to delete.</param>
  /// <returns>A task that represents the asynchronous operation. The task result contains the response as an <see cref="AnthropicResult{T}"/> where T is <see cref="MessageBatchDeleteResponse"/>.</returns>
  Task<AnthropicResult<MessageBatchDeleteResponse>> DeleteMessageBatchAsync(string batchId);

  /// <summary>
  /// Gets the results of a message batch asynchronously.
  /// </summary>
  /// <param name="batchId">The ID of the message batch to get the results for.</param>
  /// <returns>A task that represents the asynchronous operation. The task result contains the response as an <see cref="AnthropicResult{T}"/> where T is <see cref="IAsyncEnumerable{T}"/> where T is <see cref="MessageBatchResultItem"/>.</returns>
  Task<AnthropicResult<IAsyncEnumerable<MessageBatchResultItem>>> GetMessageBatchResultsAsync(string batchId);

  /// <summary>
  /// Counts the tokens in a message asynchronously.
  /// </summary>
  /// <param name="request">The count message tokens request.</param>
  /// <returns>A task that represents the asynchronous operation. The task result contains the response as an <see cref="AnthropicResult{T}"/> where T is <see cref="TokenCountResponse"/>.</returns>
  Task<AnthropicResult<TokenCountResponse>> CountMessageTokensAsync(CountMessageTokensRequest request);

  /// <summary>
  /// Lists the models asynchronously.
  /// </summary>
  /// <param name="request">The paging request to use for listing the models.</param>
  /// <returns>A task that represents the asynchronous operation. The task result contains the response as an <see cref="AnthropicResult{T}"/> where T is <see cref="Page{T}"/> where T is <see cref="AnthropicModel"/>.</returns>
  Task<AnthropicResult<Page<AnthropicModel>>> ListModelsAsync(PagingRequest? request = null);

  /// <summary>
  /// Lists the models asynchronously
  /// </summary>
  /// <param name="limit">The maximum number of models to return in each page.</param>
  /// <returns>An asynchronous enumerable that yields the response as an <see cref="AnthropicResult{T}"/> where T is <see cref="Page{T}"/> where T is <see cref="AnthropicModel"/>.</returns>
  /// 
  IAsyncEnumerable<AnthropicResult<Page<AnthropicModel>>> ListAllModelsAsync(int limit = 20);

  /// <summary>
  /// Gets a model by its ID asynchronously.
  /// </summary>
  /// <param name="modelId">The ID of the model to get.</param>
  /// <returns>A task that represents the asynchronous operation. The task result contains the response as an <see cref="AnthropicResult{T}"/> where T is <see cref="AnthropicModel"/>.</returns>
  Task<AnthropicResult<AnthropicModel>> GetModelAsync(string modelId);
}


