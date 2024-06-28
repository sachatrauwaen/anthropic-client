namespace AnthropicClient.Models;

public abstract class ContentDelta
{
  public string Type { get; init; }

  protected ContentDelta(string type)
  {
    Type = type;
  }
}