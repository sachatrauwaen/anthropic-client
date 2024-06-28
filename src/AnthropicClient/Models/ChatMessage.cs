using System.Text.Json.Serialization;

using AnthropicClient.Utils;

namespace AnthropicClient.Models;

/// <summary>
/// Represents a chat message.
/// </summary>
public class ChatMessage
{
  /// <summary>
  /// Gets the role of the message.
  /// </summary>
  public string Role { get; init; } = string.Empty;

  /// <summary>
  /// Gets the contents of the message.
  /// </summary>
  public List<Content> Content { get; init; } = [];

  [JsonConstructor]
  internal ChatMessage()
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="ChatMessage"/> class.
  /// </summary>
  /// <param name="role">The role of the message.</param>
  /// <param name="content">The contents of the message.</param>
  /// <exception cref="ArgumentException">Thrown when the role is invalid.</exception>
  /// <exception cref="ArgumentNullException">Thrown when the role or content is null.</exception>
  /// <returns>A new instance of the <see cref="ChatMessage"/> class.</returns>
  public ChatMessage(string role, List<Content> content)
  {
    ArgumentValidator.ThrowIfNull(role, nameof(role));
    ArgumentValidator.ThrowIfNull(content, nameof(content));

    if (MessageRole.IsValidRole(role) is false)
    {
      throw new ArgumentException($"Invalid role: {role}");
    }

    Role = role;
    Content = content;
  }
}