namespace AnthropicClient.Tests.Examples;

public class ExampleAttribute : FactAttribute
{
  public ExampleAttribute()
  {
    var skipExamples = Environment.GetEnvironmentVariable("SKIP_EXAMPLES");

    if (skipExamples == "true")
    {
      Skip = "Example";
    }
  }
}