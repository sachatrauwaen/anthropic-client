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
  public string Name { get; init; } = string.Empty;

  /// <summary>
  /// Gets the description of the tool.
  /// </summary>
  public string Description { get; init; } = string.Empty;

  /// <summary>
  /// Gets the input schema of the tool.
  /// </summary>
  [JsonPropertyName("input_schema")]
  public InputSchema InputSchema { get; init; } = new();

  [JsonConstructor]
  internal Tool()
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="Tool"/> class.
  /// </summary>
  /// <param name="name">The name of the tool.</param>
  /// <param name="description">The description of the tool.</param>
  /// <param name="inputSchema">The input schema of the tool.</param>
  /// <exception cref="ArgumentNullException">Thrown when the name, description, or input schema is null.</exception>
  /// <returns>A new instance of the <see cref="Tool"/> class.</returns>
  public Tool(string name, string description, InputSchema inputSchema)
  {
    ArgumentValidator.ThrowIfNull(name, nameof(name));
    ArgumentValidator.ThrowIfNull(description, nameof(description));
    ArgumentValidator.ThrowIfNull(inputSchema, nameof(inputSchema));

    Name = name;
    Description = description;
    InputSchema = inputSchema;
  }
}