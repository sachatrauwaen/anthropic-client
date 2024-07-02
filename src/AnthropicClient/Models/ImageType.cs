namespace AnthropicClient.Models;

/// <summary>
/// Represents the image type.
/// </summary>
public static class ImageType
{
  /// <summary>
  /// Represents the JPEG image type.
  /// </summary>
  public const string Jpg = "image/jpeg";

  /// <summary>
  /// Represents the PNG image type.
  /// </summary>
  public const string Png = "image/png";

  /// <summary>
  /// Represents the GIF image type.
  /// </summary>
  public const string Gif = "image/gif";

  /// <summary>
  /// Represents the WebP image type.
  /// </summary>
  public const string Webp = "image/webp";

  internal static bool IsValidImageType(string imageType) => imageType is Jpg or Png or Gif or Webp;
}