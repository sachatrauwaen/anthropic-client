using System.Reflection;
using System.Text.Json;

using AnthropicClient.Json;

namespace AnthropicClient.Models;

/// <summary>
/// Represents a tool call.
/// </summary>
public class ToolCall
{
  /// <summary>
  /// Gets the tool of the tool call.
  /// </summary>
  public Tool Tool { get; }

  /// <summary>
  /// Gets the tool use of the tool call.
  /// </summary>
  public ToolUseContent ToolUse { get; }

  /// <summary>
  /// Initializes a new instance of the <see cref="ToolCall"/> class.
  /// </summary>
  /// <param name="tool">The tool of the tool call.</param>
  /// <param name="toolUse">The tool use of the tool call.</param>
  /// <returns>A new instance of the <see cref="ToolCall"/> class.</returns>
  public ToolCall(Tool tool, ToolUseContent toolUse)
  {
    Tool = tool;
    ToolUse = toolUse;
  }

  /// <summary>
  /// Invokes the tool call.
  /// </summary>
  /// <returns>The result of the tool call as an instance of <see cref="ToolCallResult{T}"/>.</returns>
  public async Task<ToolCallResult<object>> InvokeAsync()
  {
    return await InvokeAsync<object>();
  }

  /// <summary>
  /// Invokes the tool call.
  /// </summary>
  /// <typeparam name="T">The type of the result value.</typeparam>
  /// <returns>The result of the tool call as an instance of <see cref="ToolCallResult{T}"/>.</returns>
  /// <exception cref="ArgumentException">Thrown when a parameter name is not found.</exception>
  /// <exception cref="ArgumentException">Thrown when an argument is missing.</exception>
  public async Task<ToolCallResult<T>> InvokeAsync<T>()
  {
    try
    {
      T? result = default;

      var arguments = GetArguments();
      var isAwaitable = Tool.Function.Method.ReturnType.GetMethod(nameof(Task.GetAwaiter)) is not null;

      if (isAwaitable)
      {
        var task = (Task)Tool.Function.Method.Invoke(Tool.Function.Instance, arguments);

        await task;

        const string resultPropertyName = "Result";
        var resultProperty = task.GetType().GetProperty(resultPropertyName);
        var isVoidTaskResult = resultProperty.PropertyType.FullName.Contains("VoidTaskResult");

        result = resultProperty is not null && isVoidTaskResult is false ? (T)resultProperty.GetValue(task) : default;
      }
      else
      {
        result = (T)Tool.Function.Method.Invoke(Tool.Function.Instance, arguments);
      }

      return ToolCallResult<T>.Success(result);
    }
    catch (Exception e)
    {
      return ToolCallResult<T>.Failure(e);
    }
  }

  private object?[] GetArguments()
  {
    var parameters = Tool.Function.Method.GetParameters();
    var arguments = new object?[parameters.Length];

    for (var i = 0; i < parameters.Length; i++)
    {
      var parameter = parameters[i];
      var attribute = parameter.GetCustomAttribute<FunctionParameterAttribute>();
      var parameterName = attribute is not null
        ? string.IsNullOrWhiteSpace(attribute.Name)
          ? parameter.Name
          : attribute.Name
        : parameter.Name;

      if (parameterName == null)
      {
        throw new ArgumentException($"Failed to find a valid parameter name for {Tool.Function.Method.DeclaringType}.{Tool.Function.Method.Name}()");
      }

      if (ToolUse.Input.TryGetValue(parameterName, out var value))
      {
        arguments[i] = value is string s && parameter.ParameterType.IsEnum
          ? Enum.Parse(parameter.ParameterType, s, true)
          : value is JsonElement element
            ? JsonSerializer.Deserialize(element.GetRawText(), parameter.ParameterType, JsonSerializationOptions.DefaultOptions)
            : value;
      }
      else
      {
        arguments[i] = parameter.HasDefaultValue
          ? parameter.DefaultValue
          : throw new ArgumentException($"Missing argument for parameter '{parameter.Name}'");
      }
    }

    return arguments;
  }
}