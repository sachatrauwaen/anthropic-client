using System.Text.Json.Serialization;

namespace AnthropicClient.Models;

public abstract class EventData
{
  public string Type { get; init; }

  protected EventData(string type)
  {
    Type = type;
  }
}