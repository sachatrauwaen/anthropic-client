namespace AnthropicClient.Models;

/// <summary>
/// Represents a tool call result.
/// </summary>
public class ToolCallResult<T>
{
  private T? _value = default!;

  /// <summary>
  /// The value of the tool call result. Can be null if the call failed, the call was successful but the return type is void or Task, or the call was successful but the return value is null
  /// </summary>
  /// <exception cref="InvalidOperationException">Thrown when the result is not successful.</exception>
  public T? Value 
  { 
    get
    {
      return IsSuccess ? _value : throw new InvalidOperationException("The result is not successful. Check the error property for more information.");
    }

    private set
    {
      _value = value;
    }
  }

  private Exception _error = default!;

  /// <summary>
  /// The error of the tool call result.
  /// </summary>
  /// <exception cref="InvalidOperationException">Thrown when the result is successful.</exception>
  public Exception Error 
  { 
    get
    {
      return IsSuccess ? throw new InvalidOperationException("The result is successful. Check the value property for more information.") : _error;
    }

    private set
    {
      _error = value;
    } 
  }

  /// <summary>
  /// Indicates whether the tool call was successful.
  /// </summary>
  public bool IsSuccess { get; }

  /// <summary>
  /// Indicates whether the tool call failed.
  /// </summary>
  public bool IsFailure => !IsSuccess;

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