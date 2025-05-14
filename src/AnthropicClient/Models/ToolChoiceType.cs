namespace AnthropicClient.Models;

/// <summary>
/// Represents the tool choice type.
/// </summary>
public class ToolChoiceType
{
  /// <summary>
  /// Represents the auto tool choice type.
  /// </summary>
  public const string Auto = "auto";

  /// <summary>
  /// Represents the any tool choice type.
  /// </summary>
  public const string Any = "any";

  /// <summary>
  /// Represents the specific tool choice type.
  /// </summary>
  public const string Tool = "tool";

  internal static bool IsValidType(string type) => type is Auto or Any or Tool;
}


