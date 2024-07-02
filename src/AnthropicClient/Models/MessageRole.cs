namespace AnthropicClient.Models;

/// <summary>
/// Represents the message role.
/// </summary>
public static class MessageRole
{
  /// <summary>
  /// Represents the user role.
  /// </summary>
  public const string User = "user";

  /// <summary>
  /// Represents the assistant role.
  /// </summary>
  public const string Assistant = "assistant";

  internal static bool IsValidRole(string role) => role is User or Assistant;
}