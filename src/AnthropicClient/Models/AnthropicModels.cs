namespace AnthropicClient.Models;

/// <summary>
/// Provides constants for the Anthropic models.
/// </summary>
public static class AnthropicModels
{
  /// <summary>
  /// The Claude 3 Opus model.
  /// </summary>
  public const string Claude3Opus = "claude-3-opus-20240229";

  /// <summary>
  /// The Claude 3 Opus model.
  /// </summary>
  public const string Claude3Opus20241022 = "claude-3-opus-20240229";

  /// <summary>
  /// The Claude 3 Opus model.
  /// </summary>
  public const string Claude3OpusLatest = "claude-3-opus-latest";

  /// <summary>
  /// The Claude 3 Sonnet model.
  /// </summary>
  public const string Claude3Sonnet = "claude-3-sonnet-20240229";

  /// <summary>
  /// The Claude 3 Sonnet model.
  /// </summary>
  public const string Claude3Sonnet20240229 = "claude-3-sonnet-20240229";

  /// <summary>
  /// The Claude 3.5 Sonnet model.
  /// </summary>
  public const string Claude35Sonnet = "claude-3-5-sonnet-20240620";

  /// <summary>
  /// The Claude 3.5 Sonnet model.
  /// </summary>
  public const string Claude35Sonnet20240620 = "claude-3-5-sonnet-20240620";

  /// <summary>
  /// The Claude 3.5 Sonnet model.
  /// </summary>
  public const string Claude35Sonnet20241022 = "claude-3-5-sonnet-20241022";

  /// <summary>
  /// The Claude 3.5 Sonnet model.
  /// </summary>
  public const string Claude35SonnetLatest = "claude-3-5-sonnet-latest";

  /// <summary>
  /// The Claude 3 Haiku model.
  /// </summary>
  public const string Claude3Haiku = "claude-3-haiku-20240307";

  /// <summary>
  /// The Claude 3 Haiku model.
  /// </summary>
  public const string Claude3Haiku20240307 = "claude-3-haiku-20240307";

  /// <summary>
  /// The Claude 3.5 Haiku model.
  /// </summary>
  public const string Claude35Haiku20241022 = "claude-3-5-haiku-20241022";

  /// <summary>
  /// The Claude 3.5 Haiku model.
  /// </summary>
  public const string Claude35HaikuLatest = "claude-3-5-haiku-latest";

  internal static bool IsValidModel(string modelId) => modelId is 
    Claude3Opus or 
    Claude3Opus20241022 or
    Claude3OpusLatest or
    
    Claude3Sonnet or 
    Claude3Sonnet20240229 or
    Claude35Sonnet or 
    Claude35Sonnet20240620 or
    Claude35Sonnet20241022 or
    Claude35SonnetLatest or
    
    Claude3Haiku or
    Claude3Haiku20240307 or
    Claude35Haiku20241022 or
    Claude35HaikuLatest;
}