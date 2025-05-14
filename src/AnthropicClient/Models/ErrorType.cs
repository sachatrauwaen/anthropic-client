namespace AnthropicClient.Models;

/// <summary>
/// Represents the error type.
/// </summary>
public class ErrorType
{
  /// <summary>
  /// Represents the invalid_request error type.
  /// </summary>
  public const string InvalidRequestError = "invalid_request_error";

  /// <summary>
  /// Represents the authentication_error type.
  /// </summary>
  public const string AuthenticationError = "authentication_error";

  /// <summary>
  /// Represents the permission_error type.
  /// </summary>
  public const string PermissionError = "permission_error";

  /// <summary>
  /// Represents the not_found_error type.
  /// </summary>
  public const string NotFoundError = "not_found_error";

  /// <summary>
  /// Represents the rate_limit_error type.
  /// </summary>
  public const string RateLimitError = "rate_limit_error";

  /// <summary>
  /// Represents the api_error type.
  /// </summary>
  public const string ApiError = "api_error";

  /// <summary>
  /// Represents the overloaded_error type.
  /// </summary>
  public const string OverloadedError = "overloaded_error";
}


