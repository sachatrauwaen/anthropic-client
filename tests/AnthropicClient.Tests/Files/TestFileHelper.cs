namespace AnthropicClient.Tests.Files;

static class TestFileHelper
{
  public static string GetTestFilePath(string fileName) =>
    Path.Combine(Directory.GetCurrentDirectory(), "Files", fileName);
}