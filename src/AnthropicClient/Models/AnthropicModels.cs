namespace AnthropicClient.Models;

/// <summary>
/// Provides constants for the Anthropic models.
/// </summary>
public static class AnthropicModels
{
  /// <summary>
  /// The Claude-3 Opus model.
  /// </summary>
  public const string Claude3Opus = "claude-3-opus-20240229";

  /// <summary>
  /// The Claude-3 Sonnet model.
  /// </summary>
  public const string Claude3Sonnet = "claude-3-sonnet-20240229";

  /// <summary>
  /// The Claude-3.5 Sonnet model.
  /// </summary>
  public const string Claude35Sonnet = "claude-3-5-sonnet-20240620";

  /// <summary>
  /// The Claude-3 Haiku model.
  /// </summary>
  public const string Claude3Haiku = "claude-3-haiku-20240307";

  internal static bool IsValidModel(string modelId) => modelId is Claude3Opus or Claude3Sonnet or Claude35Sonnet or Claude3Haiku;
}