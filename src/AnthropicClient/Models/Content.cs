using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

/// <summary>
/// Represents part of the content of a message.
/// </summary>
public abstract class Content
{
  /// <summary>
  /// Gets the type of the content.
  /// </summary>
  public string Type { get; init; } = string.Empty;

  [JsonConstructor]
  internal Content()
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="Content"/> class.
  /// </summary>
  /// <param name="type">The type of the content.</param>
  /// <returns>A new instance of the <see cref="Content"/> class.</returns>
  protected Content(string type)
  {
    Type = type;
  }
}