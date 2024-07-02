namespace AnthropicClient.Models;

/// <summary>
/// Represents a tool call result.
/// </summary>
public class ToolCallResult<T>
{
  /// <summary>
  /// The value of the tool call result. Can be null if the call failed, the call was successful but the return type is void or Task, or the call was successful but the return value is null
  /// </summary>
  public T? Value { get; }

  /// <summary>
  /// The error of the tool call result.
  /// </summary>
  public Exception Error { get; }

  /// <summary>
  /// Indicates whether the tool call was successful.
  /// </summary>
  public bool IsSuccess { get; }

  /// <summary>
  /// Initializes a new instance of the <see cref="ToolCallResult{T}"/> class.
  /// </summary>
  /// <param name="value">The value of the tool call result.</param>
  /// <param name="error">The error of the tool call result.</param>
  /// <param name="isSuccess">Indicates whether the tool call was successful.</param>
  /// <returns>A new instance of the <see cref="ToolCallResult{T}"/> class.</returns>
  protected ToolCallResult(T? value, Exception error, bool isSuccess)
  {
    Value = value;
    Error = error;
    IsSuccess = isSuccess;
  }

  /// <summary>
  /// Creates a successful tool call result.
  /// </summary>
  /// <param name="value">The value of the tool call result.</param>
  /// <returns>A new instance of the <see cref="ToolCallResult{T}"/> class.</returns>
  public static ToolCallResult<T> Success(T? value)
  {
    return new ToolCallResult<T>(value, null!, true);
  }

  /// <summary>
  /// Creates a failed ool call result.
  /// </summary>
  /// <param name="error">The error of the tool call result.</param>
  /// <returns>A new instance of the <see cref="ToolCallResult{T}"/> class.</returns>
  public static ToolCallResult<T> Failure(Exception error)
  {
    return new ToolCallResult<T>(default!, error, false);
  }
}