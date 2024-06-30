using System.Collections;
using System.Text.Json.Nodes;

namespace AnthropicClient.Tests.Unit.Utils;

public class JsonSchemaGeneratorTestData : IEnumerable<object[]>
{
  private const string TestToolName = "Test tool name";
  private const string TestToolDescription = "Test tool description";

  public IEnumerator<object[]> GetEnumerator()
  {
    // string parameter type
    yield return new object[]
    {
      Tool.CreateFromFunction(TestToolName,TestToolDescription,(string name) => name),
      new JsonObject()
      {
        ["type"] = "object",
        ["properties"] = new JsonObject()
        {
          ["name"] = new JsonObject()
          {
            ["type"] = "string",
            ["description"] = string.Empty
          }
        },
        ["required"] = new JsonArray()
        {
          "name"
        },
      }
    };

    // char parameter type
    yield return new object[]
    {
      Tool.CreateFromFunction(TestToolName,TestToolDescription,(char name) => name),
      new JsonObject()
      {
        ["type"] = "object",
        ["properties"] = new JsonObject()
        {
          ["name"] = new JsonObject()
          {
            ["type"] = "string",
            ["description"] = string.Empty
          }
        },
        ["required"] = new JsonArray()
        {
          "name"
        },
      }
    };

    // int parameter type
    yield return new object[]
    {
      Tool.CreateFromFunction(TestToolName,TestToolDescription,(int age) => age),
      new JsonObject()
      {
        ["type"] = "object",
        ["properties"] = new JsonObject()
        {
          ["age"] = new JsonObject()
          {
            ["type"] = "integer",
            ["description"] = string.Empty
          }
        },
        ["required"] = new JsonArray()
        {
          "age"
        },
      }
    };

    // uint parameter type
    yield return new object[]
    {
      Tool.CreateFromFunction(TestToolName,TestToolDescription,(uint age) => age),
      new JsonObject()
      {
        ["type"] = "object",
        ["properties"] = new JsonObject()
        {
          ["age"] = new JsonObject()
          {
            ["type"] = "integer",
            ["description"] = string.Empty
          }
        },
        ["required"] = new JsonArray()
        {
          "age"
        },
      }
    };

    // long parameter type
    yield return new object[]
    {
      Tool.CreateFromFunction(TestToolName,TestToolDescription,(long age) => age),
      new JsonObject()
      {
        ["type"] = "object",
        ["properties"] = new JsonObject()
        {
          ["age"] = new JsonObject()
          {
            ["type"] = "integer",
            ["description"] = string.Empty
          }
        },
        ["required"] = new JsonArray()
        {
          "age"
        },
      }
    };

    // ulong parameter type
    yield return new object[]
    {
      Tool.CreateFromFunction(TestToolName,TestToolDescription,(ulong age) => age),
      new JsonObject()
      {
        ["type"] = "object",
        ["properties"] = new JsonObject()
        {
          ["age"] = new JsonObject()
          {
            ["type"] = "integer",
            ["description"] = string.Empty
          }
        },
        ["required"] = new JsonArray()
        {
          "age"
        },
      }
    };

    // short parameter type
    yield return new object[]
    {
      Tool.CreateFromFunction(TestToolName,TestToolDescription,(short age) => age),
      new JsonObject()
      {
        ["type"] = "object",
        ["properties"] = new JsonObject()
        {
          ["age"] = new JsonObject()
          {
            ["type"] = "integer",
            ["description"] = string.Empty
          }
        },
        ["required"] = new JsonArray()
        {
          "age"
        },
      }
    };

    // ushort parameter type
    yield return new object[]
    {
      Tool.CreateFromFunction(TestToolName,TestToolDescription,(ushort age) => age),
      new JsonObject()
      {
        ["type"] = "object",
        ["properties"] = new JsonObject()
        {
          ["age"] = new JsonObject()
          {
            ["type"] = "integer",
            ["description"] = string.Empty
          }
        },
        ["required"] = new JsonArray()
        {
          "age"
        },
      }
    };

    // byte parameter type
    yield return new object[]
    {
      Tool.CreateFromFunction(TestToolName,TestToolDescription,(byte age) => age),
      new JsonObject()
      {
        ["type"] = "object",
        ["properties"] = new JsonObject()
        {
          ["age"] = new JsonObject()
          {
            ["type"] = "integer",
            ["description"] = string.Empty
          }
        },
        ["required"] = new JsonArray()
        {
          "age"
        },
      }
    };

    // sbyte parameter type
    yield return new object[]
    {
      Tool.CreateFromFunction(TestToolName,TestToolDescription,(sbyte age) => age),
      new JsonObject()
      {
        ["type"] = "object",
        ["properties"] = new JsonObject()
        {
          ["age"] = new JsonObject()
          {
            ["type"] = "integer",
            ["description"] = string.Empty
          }
        },
        ["required"] = new JsonArray()
        {
          "age"
        },
      }
    };

    // bool parameter type
    yield return new object[]
    {
      Tool.CreateFromFunction(TestToolName,TestToolDescription,(bool isAdult) => isAdult),
      new JsonObject()
      {
        ["type"] = "object",
        ["properties"] = new JsonObject()
        {
          ["isAdult"] = new JsonObject()
          {
            ["type"] = "boolean",
            ["description"] = string.Empty
          }
        },
        ["required"] = new JsonArray()
        {
          "isAdult"
        },
      }
    };

    // double parameter type
    yield return new object[]
    {
      Tool.CreateFromFunction(TestToolName,TestToolDescription,(double age) => age),
      new JsonObject()
      {
        ["type"] = "object",
        ["properties"] = new JsonObject()
        {
          ["age"] = new JsonObject()
          {
            ["type"] = "number",
            ["description"] = string.Empty
          }
        },
        ["required"] = new JsonArray()
        {
          "age"
        },
      }
    };

    // float parameter type
    yield return new object[]
    {
      Tool.CreateFromFunction(TestToolName,TestToolDescription,(float age) => age),
      new JsonObject()
      {
        ["type"] = "object",
        ["properties"] = new JsonObject()
        {
          ["age"] = new JsonObject()
          {
            ["type"] = "number",
            ["description"] = string.Empty
          }
        },
        ["required"] = new JsonArray()
        {
          "age"
        },
      }
    };

    // decimal parameter type
    yield return new object[]
    {
      Tool.CreateFromFunction(TestToolName,TestToolDescription,(decimal age) => age),
      new JsonObject()
      {
        ["type"] = "object",
        ["properties"] = new JsonObject()
        {
          ["age"] = new JsonObject()
          {
            ["type"] = "number",
            ["description"] = string.Empty
          }
        },
        ["required"] = new JsonArray()
        {
          "age"
        },
      }
    };

    // datetime parameter type
    yield return new object[]
    {
      Tool.CreateFromFunction(TestToolName,TestToolDescription,(DateTime date) => date),
      new JsonObject()
      {
        ["type"] = "object",
        ["properties"] = new JsonObject()
        {
          ["date"] = new JsonObject()
          {
            ["type"] = "string",
            ["format"] = "date-time",
            ["description"] = string.Empty
          }
        },
        ["required"] = new JsonArray()
        {
          "date"
        },
      }
    };

    // datetimeoffset parameter type
    yield return new object[]
    {
      Tool.CreateFromFunction(TestToolName,TestToolDescription,(DateTimeOffset date) => date),
      new JsonObject()
      {
        ["type"] = "object",
        ["properties"] = new JsonObject()
        {
          ["date"] = new JsonObject()
          {
            ["type"] = "string",
            ["format"] = "date-time",
            ["description"] = string.Empty
          }
        },
        ["required"] = new JsonArray()
        {
          "date"
        },
      }
    };

    // enum parameter type
    yield return new object[]
    {
      Tool.CreateFromFunction(TestToolName,TestToolDescription,(DayOfWeek day) => day),
      new JsonObject()
      {
        ["type"] = "object",
        ["properties"] = new JsonObject()
        {
          ["day"] = new JsonObject()
          {
            ["type"] = "string",
            ["description"] = string.Empty,
            ["enum"] = new JsonArray()
            {
              "Sunday",
              "Monday",
              "Tuesday",
              "Wednesday",
              "Thursday",
              "Friday",
              "Saturday"
            },
          }
        },
        ["required"] = new JsonArray()
        {
          "day"
        },
      }
    };

    // array parameter type
    yield return new object[]
    {
      Tool.CreateFromFunction(TestToolName,TestToolDescription,(int[] numbers) => numbers),
      new JsonObject()
      {
        ["type"] = "object",
        ["properties"] = new JsonObject()
        {
          ["numbers"] = new JsonObject()
          {
            ["type"] = "array",
            ["items"] = new JsonObject()
            {
              ["type"] = "integer"
            },
            ["description"] = string.Empty
          }
        },
        ["required"] = new JsonArray()
        {
          "numbers"
        },
      }
    };

    // list parameter type
    yield return new object[]
    {
      Tool.CreateFromFunction(TestToolName,TestToolDescription,(List<int> numbers) => numbers),
      new JsonObject()
      {
        ["type"] = "object",
        ["properties"] = new JsonObject()
        {
          ["numbers"] = new JsonObject()
          {
            ["type"] = "array",
            ["items"] = new JsonObject()
            {
              ["type"] = "integer"
            },
            ["description"] = string.Empty
          }
        },
        ["required"] = new JsonArray()
        {
          "numbers"
        },
      }
    };
  }

  IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}