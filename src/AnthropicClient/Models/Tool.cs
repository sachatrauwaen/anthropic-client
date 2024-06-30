using System.Reflection;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

using AnthropicClient.Utils;

namespace AnthropicClient.Models;

/// <summary>
/// Represents a tool that can be used in the chat.
/// </summary>
public class Tool
{
  /// <summary>
  /// Gets the name of the tool.
  /// </summary>
  public string Name { get; }

  /// <summary>
  /// Gets the description of the tool.
  /// </summary>
  public string Description { get; }

  /// <summary>
  /// Gets the input schema of the tool.
  /// </summary>
  [JsonPropertyName("input_schema")]
  public JsonObject InputSchema { get; }

  /// <summary>
  /// Gets the function of the tool.
  /// </summary>
  [JsonIgnore]
  public AnthropicFunction Function { get; }
  
  internal Tool(string name, string description, AnthropicFunction function)
  {
    ArgumentValidator.ThrowIfNull(name, nameof(name));
    ArgumentValidator.ThrowIfNull(description, nameof(description));
    ArgumentValidator.ThrowIfNull(function, nameof(function));

    Name = name;
    Description = description;
    Function = function;
    InputSchema = JsonSchemaGenerator.GenerateInputSchema(function);
  }

  /// <summary>
  /// Creates a tool from a static method.
  /// </summary>
  /// <param name="name">The name of the tool.</param>
  /// <param name="description">The description of the tool.</param>
  /// <param name="type">The type that contains the method.</param>
  /// <param name="methodName">The name of the method.</param>
  /// <exception cref="ArgumentNullException">Thrown when <paramref name="name"/>, <paramref name="description"/>, <paramref name="type"/>, or <paramref name="methodName"/> is null.</exception>
  /// <exception cref="ArgumentException">Thrown when the method is not found in the type.</exception>
  /// <returns>The created tool as instance of <see cref="Tool"/>.</returns>
  public static Tool CreateFromStaticMethod(string name, string description, Type type, string methodName)
  {
    ArgumentValidator.ThrowIfNull(name, nameof(name));
    ArgumentValidator.ThrowIfNull(description, nameof(description));
    ArgumentValidator.ThrowIfNull(type, nameof(type));
    ArgumentValidator.ThrowIfNull(methodName, nameof(methodName));

    var method = type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);
    
    if (method is null)
    {
      throw new ArgumentException($"Method '{methodName}' not found in type '{type.FullName}'.", nameof(methodName));
    }

    return new Tool(name, description, new AnthropicFunction(method));
  }

  /// <summary>
  /// Creates a tool from an instance method.
  /// </summary>
  /// <param name="name">The name of the tool.</param>
  /// <param name="description">The description of the tool.</param>
  /// <param name="instance">The instance that contains the method.</param>
  /// <param name="methodName">The name of the method.</param>
  /// <exception cref="ArgumentNullException">Thrown when <paramref name="name"/>, <paramref name="description"/>, <paramref name="instance"/>, or <paramref name="methodName"/> is null.</exception>
  /// <exception cref="ArgumentException">Thrown when the method is not found in the type.</exception>
  /// <returns>The created tool as instance of <see cref="Tool"/>.</returns>
  public static Tool CreateFromInstanceMethod(string name, string description, object instance, string methodName)
  {
    ArgumentValidator.ThrowIfNull(name, nameof(name));
    ArgumentValidator.ThrowIfNull(description, nameof(description));
    ArgumentValidator.ThrowIfNull(instance, nameof(instance));
    ArgumentValidator.ThrowIfNull(methodName, nameof(methodName));

    var method = instance.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);
    
    if (method is null)
    {
      throw new ArgumentException($"Method '{methodName}' not found in type '{instance.GetType().FullName}'.", nameof(methodName));
    }

    return new Tool(name, description, new AnthropicFunction(method, instance));
  }

  /// <summary>
  /// Creates a tool from a function.
  /// </summary>
  /// <typeparam name="TResult">The type of the result.</typeparam>
  /// <param name="name">The name of the tool.</param>
  /// <param name="description">The description of the tool.</param>
  /// <param name="func">The function.</param>
  /// <exception cref="ArgumentNullException">Thrown when <paramref name="name"/>, <paramref name="description"/>, or <paramref name="func"/> is null.</exception>
  /// <returns>The created tool as instance of <see cref="Tool"/>.</returns>
  public static Tool CreateFromFunction<TResult>(string name, string description, Func<TResult> func)
  {
    ArgumentValidator.ThrowIfNull(name, nameof(name));
    ArgumentValidator.ThrowIfNull(description, nameof(description));
    ArgumentValidator.ThrowIfNull(func, nameof(func));

    return new Tool(name, description, new AnthropicFunction(func.Method, func.Target));
  }

  public static Tool CreateFromFunction<T1, TResult>(string name, string description, Func<T1, TResult> func)
  {
    ArgumentValidator.ThrowIfNull(name, nameof(name));
    ArgumentValidator.ThrowIfNull(description, nameof(description));
    ArgumentValidator.ThrowIfNull(func, nameof(func));

    return new Tool(name, description, new AnthropicFunction(func.Method, func.Target));
  }
}