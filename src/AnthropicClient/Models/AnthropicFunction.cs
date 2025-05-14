using System.Reflection;

namespace AnthropicClient.Models;

/// <summary>
/// Represents a function that can be provided as a tool.
/// </summary>
public class AnthropicFunction
{
  /// <summary>
  /// Gets the method of the function.
  /// </summary>
  public MethodInfo Method { get; }

  /// <summary>
  /// Gets the instance on which the method is invoked.
  /// </summary>
  public object? Instance { get; }

  internal AnthropicFunction(MethodInfo method)
  {
    Method = method;
  }

  internal AnthropicFunction(MethodInfo method, object? instance)
  {
    Method = method;
    Instance = instance;
  }
}


