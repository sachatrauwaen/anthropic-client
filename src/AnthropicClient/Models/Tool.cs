using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

using AnthropicClient.Utils;

namespace AnthropicClient.Models;

/// <summary>
/// Interface that a class can implement to be used to create a tool.
/// </summary>
public interface ITool
{
  /// <summary>
  /// Gets the name of the tool. Should not be null or empty.
  /// </summary>
  public string Name { get; }

  /// <summary>
  /// Gets the description of the tool. Should not be null or empty.
  /// </summary>
  public string Description { get; }

  /// <summary>
  /// Gets the input schema of the tool. Should not be null.
  /// </summary>
  public MethodInfo Function { get; }
}

/// <summary>
/// Represents a tool that can be used.
/// </summary>
public class Tool
{
  /// <summary>
  /// Gets the name of the tool. This name will conform to the Anthropic tool naming rules.
  /// </summary>
  public string Name { get; }

  /// <summary>
  /// Gets the description of the tool.
  /// </summary>
  public string Description { get; }

  /// <summary>
  /// Gets the input schema of the tool.
  /// </summary>
  [JsonProperty("input_schema")]
  public JObject InputSchema { get; }

  /// <summary>
  /// Gets the function of the tool.
  /// </summary>
  [JsonIgnore]
  public AnthropicFunction Function { get; }

  /// <summary>
  /// Gets or sets the cache control to be used for the tool.
  /// </summary>
  [JsonProperty("cache_control")]
  public CacheControl? CacheControl { get; set; }

  /// <summary>
  /// Gets the display name of the tool.
  /// </summary>
  [JsonIgnore]
  public string DisplayName { get; }

  [JsonConstructor]
  internal Tool()
  {
    var func = () => { };

    Name = string.Empty;
    Description = string.Empty;
    InputSchema = [];
    Function = new AnthropicFunction(func.Method);
    DisplayName = string.Empty;
  }

  internal Tool(string name, string description, AnthropicFunction function, CacheControl? cacheControl = null)
  {
    ArgumentValidator.ThrowIfNullOrWhitespace(name, nameof(name));
    ArgumentValidator.ThrowIfNullOrWhitespace(description, nameof(description));
    ArgumentValidator.ThrowIfNull(function, nameof(function));

    // Anthropic imposes a limit on tool names. Tool names must be...
    // - between 1 and 64 characters long
    // - contain only letters, numbers, underscores, and hyphens
    var sanitizedName = SanitizeName(name);

    Name = sanitizedName;
    DisplayName = name;
    Description = description;
    Function = function;
    InputSchema = JsonSchemaGenerator.GenerateInputSchema(function);
    CacheControl = cacheControl;
  }

  /// <summary>
  /// Creates a tool from a type that implements <see cref="ITool"/>.
  /// </summary>
  /// <typeparam name="T">The type that implements <see cref="ITool"/>.</typeparam>
  /// <exception cref="ArgumentException">Thrown when the name or description of the tool is null or empty.</exception>
  /// <exception cref="ArgumentNullException">Thrown when the function of the tool is null.</exception>
  /// <returns>The created tool as instance of <see cref="Tool"/>.</returns>
  /// <remarks>The implementation of <see cref="ITool"/> must have a parameterless constructor.</remarks>
  public static Tool CreateFromClass<T>(CacheControl? cacheControl = null) where T : ITool, new()
  {
    var tool = new T();

    ArgumentValidator.ThrowIfNullOrWhitespace(tool.Name, nameof(tool.Name));
    ArgumentValidator.ThrowIfNullOrWhitespace(tool.Description, nameof(tool.Description));
    ArgumentValidator.ThrowIfNull(tool.Function, nameof(tool.Function));

    return new Tool(tool.Name, tool.Description, new AnthropicFunction(tool.Function, tool), cacheControl);
  }

  /// <summary>
  /// Creates a tool from a static method.
  /// </summary>
  /// <param name="name">The name of the tool.</param>
  /// <param name="description">The description of the tool.</param>
  /// <param name="type">The type that contains the method.</param>
  /// <param name="methodName">The name of the method.</param>
  /// <param name="cacheControl">The cache control to be used for the tool.</param>
  /// <exception cref="ArgumentException">Thrown when <paramref name="methodName"/> is null or empty.</exception>
  /// <exception cref="ArgumentNullException">Thrown when <paramref name="type"/> is null.</exception>
  /// <exception cref="ArgumentException">Thrown when the method is not found in the type.</exception>
  /// <returns>The created tool as instance of <see cref="Tool"/>.</returns>
  /// <remarks>The name of the tool will be sanitized to conform to the Anthropic tool naming rules.</remarks>
  public static Tool CreateFromStaticMethod(
    string name,
    string description,
    Type type,
    string methodName,
    CacheControl? cacheControl = null
  )
  {
    ArgumentValidator.ThrowIfNullOrWhitespace(methodName, nameof(methodName));
    ArgumentValidator.ThrowIfNull(type, nameof(type));

    var method = type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);

    if (method is null)
    {
      throw new ArgumentException($"Method '{methodName}' not found in type '{type.FullName}'.", nameof(methodName));
    }

    return new Tool(name, description, new AnthropicFunction(method), cacheControl);
  }

  /// <summary>
  /// Creates a tool from an instance method.
  /// </summary>
  /// <param name="name">The name of the tool.</param>
  /// <param name="description">The description of the tool.</param>
  /// <param name="instance">The instance that contains the method.</param>
  /// <param name="methodName">The name of the method.</param>
  /// <param name="cacheControl">The cache control to be used for the tool.</param>
  /// <exception cref="ArgumentException">Thrown when <paramref name="methodName"/> is null or empty.</exception> 
  /// <exception cref="ArgumentNullException">Thrown when <paramref name="instance"/> is null.</exception> 
  /// <exception cref="ArgumentException">Thrown when <paramref name="methodName"/> is not found in the type of <paramref name="instance"/>.</exception> 
  /// <returns>The created tool as instance of <see cref="Tool"/>.</returns>
  /// <remarks>The name of the tool will be sanitized to conform to the Anthropic tool naming rules.</remarks>
  public static Tool CreateFromInstanceMethod(
    string name,
    string description,
    object instance,
    string methodName,
    CacheControl? cacheControl = null
  )
  {
    ArgumentValidator.ThrowIfNullOrWhitespace(methodName, nameof(methodName));
    ArgumentValidator.ThrowIfNull(instance, nameof(instance));

    var method = instance.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);

    if (method is null)
    {
      throw new ArgumentException($"Method '{methodName}' not found in type '{instance.GetType().FullName}'.", nameof(methodName));
    }

    return new Tool(name, description, new AnthropicFunction(method, instance), cacheControl);
  }

  /// <summary>
  /// Creates a tool from a function.
  /// </summary>
  /// <typeparam name="TResult">The type of the result for the delegate.</typeparam>
  /// <param name="name">The name of the tool.</param>
  /// <param name="description">The description of the tool.</param>
  /// <param name="func">The function.</param>
  /// <param name="cacheControl">The cache control to be used for the tool.</param>
  /// <exception cref="ArgumentNullException">Thrown when <paramref name="func"/> is null.</exception>
  /// <returns>The created tool as instance of <see cref="Tool"/>.</returns>
  /// <remarks>The name of the tool will be sanitized to conform to the Anthropic tool naming rules.</remarks>
  public static Tool CreateFromFunction<TResult>(
    string name,
    string description,
    Func<TResult> func,
    CacheControl? cacheControl = null
  )
  {
    ArgumentValidator.ThrowIfNull(func, nameof(func));

    return new Tool(name, description, new AnthropicFunction(func.Method, func.Target), cacheControl);
  }

  /// <summary>
  /// Creates a tool from a function.
  /// </summary>
  /// <typeparam name="T1">The type of the first parameter for the delegate.</typeparam>
  /// <typeparam name="TResult">The type of the result for the delegate.</typeparam>
  /// <param name="name">The name of the tool.</param>
  /// <param name="description">The description of the tool.</param>
  /// <param name="func">The function.</param>
  /// <param name="cacheControl">The cache control to be used for the tool.</param>
  /// <exception cref="ArgumentNullException">Thrown when <paramref name="func"/> is null.</exception>
  /// <returns>The created tool as instance of <see cref="Tool"/>.</returns>
  /// <remarks>The name of the tool will be sanitized to conform to the Anthropic tool naming rules.</remarks>
  public static Tool CreateFromFunction<T1, TResult>(
    string name,
    string description,
    Func<T1, TResult> func,
    CacheControl? cacheControl = null
  )
  {
    ArgumentValidator.ThrowIfNull(func, nameof(func));

    return new Tool(name, description, new AnthropicFunction(func.Method, func.Target), cacheControl);
  }

  /// <summary>
  /// Creates a tool from a function.
  /// </summary>
  /// <typeparam name="T1">The type of the first parameter for the delegate.</typeparam>
  /// <typeparam name="T2">The type of the second parameter for the delegate.</typeparam>
  /// <typeparam name="TResult">The type of the result for the delegate.</typeparam>
  /// <param name="name">The name of the tool.</param>
  /// <param name="description">The description of the tool.</param>
  /// <param name="func">The function.</param>
  /// <param name="cacheControl">The cache control to be used for the tool.</param>
  /// <exception cref="ArgumentNullException">Thrown when <paramref name="func"/> is null.</exception>
  /// <returns>The created tool as instance of <see cref="Tool"/>.</returns>
  /// <remarks>The name of the tool will be sanitized to conform to the Anthropic tool naming rules.</remarks>
  public static Tool CreateFromFunction<T1, T2, TResult>(
    string name,
    string description,
    Func<T1, T2, TResult> func,
    CacheControl? cacheControl = null
  )
  {
    ArgumentValidator.ThrowIfNull(func, nameof(func));

    return new Tool(name, description, new AnthropicFunction(func.Method, func.Target), cacheControl);
  }


  private static string SanitizeName(string name)
  {
    var sanitizedName = name.Trim();

    if (sanitizedName.Length > 64)
    {
      sanitizedName = sanitizedName.Substring(0, 64);
    }

    sanitizedName = new Regex("[^a-zA-Z0-9_-]").Replace(sanitizedName, "_");

    return sanitizedName;
  }
}


