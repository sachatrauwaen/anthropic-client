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

    // class parameter type with no attributes
    yield return new object[]
    {
      Tool.CreateFromFunction(TestToolName,TestToolDescription,(Person person) => person),
      new JsonObject()
      {
        ["type"] = "object",
        ["definitions"] = new JsonObject()
        {
          [$"{typeof(Person).FullName}"] = new JsonObject()
          {
            ["type"] = "object",
            ["properties"] = new JsonObject()
            {
              ["Name"] = new JsonObject()
              {
                ["type"] = "string",
                ["description"] = string.Empty
              },
              ["Age"] = new JsonObject()
              {
                ["type"] = "integer",
                ["description"] = string.Empty
              }
            },
            ["required"] = new JsonArray()
            {
              "Name",
              "Age"
            }
          }
        },
        ["properties"] = new JsonObject()
        {
          ["person"] = new JsonObject()
          {
            ["$ref"] = $"#/definitions/{typeof(Person).FullName}",
            ["description"] = string.Empty
          }
        },
        ["required"] = new JsonArray()
        {
          "person",
        },
      }
    };

    // nested class parameter type with no attributes
    yield return new object[]
    {
      Tool.CreateFromFunction(TestToolName,TestToolDescription,(Family family) => family),
      new JsonObject()
      {
        ["type"] = "object",
        ["definitions"] = new JsonObject()
        {
          [$"{typeof(Person).FullName}"] = new JsonObject()
          {
            ["type"] = "object",
            ["properties"] = new JsonObject()
            {
              ["Name"] = new JsonObject()
              {
                ["type"] = "string",
                ["description"] = string.Empty
              },
              ["Age"] = new JsonObject()
              {
                ["type"] = "integer",
                ["description"] = string.Empty
              }
            },
            ["required"] = new JsonArray()
            {
              "Name",
              "Age"
            }
          },
          [$"{typeof(Family).FullName}"] = new JsonObject()
          {
            ["type"] = "object",
            ["properties"] = new JsonObject()
            {
              ["Members"] = new JsonObject()
              {
                ["type"] = "array",
                ["items"] = new JsonObject()
                {
                  ["$ref"] = $"#/definitions/{typeof(Person).FullName}"
                },
                ["description"] = string.Empty
              }
            },
            ["required"] = new JsonArray()
            {
              "Members"
            }
          }
        },
        ["properties"] = new JsonObject()
        {
          ["family"] = new JsonObject()
          {
            ["$ref"] = $"#/definitions/{typeof(Family).FullName}",
            ["description"] = string.Empty
          }
        },
        ["required"] = new JsonArray()
        {
          "family",
        },
      }
    };

    var family =new Family();

    // parameters from instance method
    yield return new object[]
    {
      Tool.CreateFromInstanceMethod(TestToolName,TestToolDescription, family, nameof(family.AddMember)),
      new JsonObject()
      {
        ["type"] = "object",
        ["definitions"] = new JsonObject()
        {
          [$"{typeof(Person).FullName}"] = new JsonObject()
          {
            ["type"] = "object",
            ["properties"] = new JsonObject()
            {
              ["Name"] = new JsonObject()
              {
                ["type"] = "string",
                ["description"] = string.Empty
              },
              ["Age"] = new JsonObject()
              {
                ["type"] = "integer",
                ["description"] = string.Empty
              }
            },
            ["required"] = new JsonArray()
            {
              "Name",
              "Age"
            }
          },
        },
        ["properties"] = new JsonObject()
        {
          ["member"] = new JsonObject()
          {
            ["$ref"] = $"#/definitions/{typeof(Person).FullName}",
            ["description"] = string.Empty
          }
        },
        ["required"] = new JsonArray()
        {
          "member",
        },
      }
    };

    // parameters from static method
    yield return new object[]
    {
      Tool.CreateFromStaticMethod(TestToolName,TestToolDescription, typeof(Person), nameof(Person.Create)),
      new JsonObject()
      {
        ["type"] = "object",
        ["properties"] = new JsonObject()
        {
          ["name"] = new JsonObject()
          {
            ["type"] = "string",
            ["description"] = string.Empty
          },
          ["age"] = new JsonObject()
          {
            ["type"] = "integer",
            ["description"] = string.Empty
          }
        },
        ["required"] = new JsonArray()
        {
          "name",
          "age",
        },
      }
    };
  }

  IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

class Family 
{
  public List<Person> Members { get; } = [];

  public bool AddMember(Person member)
  {
    Members.Add(member);
    return true;
  }
}

class Person(string name, int age)
{
  public string Name { get; } = name;
  public int Age { get; } = age;

  public static Person Create(string name, int age) => new Person(name, age);
}