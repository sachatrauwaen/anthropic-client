using AnthropicClient.Models;

/// <summary>
/// Represents the result of an Anthropic API operation.
/// </summary>
/// <typeparam name="T">The type of the result value.</typeparam>
public class AnthropicResult<T>
{
  /// <summary>
  /// The value of the result.
  /// </summary>
  public T Value { get; }

  /// <summary>
  /// The error of the result.
  /// </summary>
  public AnthropicError Error { get; }

  /// <summary>
  /// Indicates whether the operation was successful.
  /// </summary>
  public bool IsSuccess { get; }

  /// <summary>
  /// The request ID of the operation.
  /// </summary>
  public string RequestId { get; }

  /// <summary>
  /// Initializes a new instance of the <see cref="AnthropicResult{T}"/> class.
  /// </summary>
  /// <param name="value">The value of the result.</param>
  /// <param name="error">The error of the result.</param>
  /// <param name="isSuccess">Indicates whether the operation was successful.</param>
  /// <param name="requestId">The request ID of the operation.</param>
  /// <returns>A new instance of the <see cref="AnthropicResult{T}"/> class.</returns>
  protected AnthropicResult(T value, AnthropicError error, bool isSuccess, string requestId)
  {
    Value = value;
    Error = error;
    IsSuccess = isSuccess;
    RequestId = requestId;
  }

  /// <summary>
  /// Creates a successful result.
  /// </summary>
  /// <param name="value">The value of the result.</param>
  /// <param name="requestId">The request ID of the operation.</param>
  /// <returns>A new instance of the <see cref="AnthropicResult{T}"/> class.</returns>
  public static AnthropicResult<T> Success(T value, string requestId)
  {
    return new AnthropicResult<T>(value, null!, true, requestId);
  }

  /// <summary>
  /// Creates a failed result.
  /// </summary>
  /// <param name="error">The error of the result.</param>
  /// <param name="requestId">The request ID of the operation.</param>
  /// <returns>A new instance of the <see cref="AnthropicResult{T}"/> class.</returns>
  public static AnthropicResult<T> Failure(AnthropicError error, string requestId)
  {
    return new AnthropicResult<T>(default!, error, false, requestId);
  }
}
