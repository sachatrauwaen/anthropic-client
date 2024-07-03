namespace AnthropicClient.Tests.Data;

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

    // GUID parameter type
    yield return new object[]
    {
      Tool.CreateFromFunction(TestToolName,TestToolDescription,(Guid id) => id),
      new JsonObject()
      {
        ["type"] = "object",
        ["properties"] = new JsonObject()
        {
          ["id"] = new JsonObject()
          {
            ["type"] = "string",
            ["format"] = "uuid",
            ["description"] = string.Empty
          }
        },
        ["required"] = new JsonArray()
        {
          "id"
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

    // date time parameter type
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

    // date time offset parameter type
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

    var family = new Family();

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

    // multiple parameters of same complex type
    yield return new object[]
    {
      Tool.CreateFromFunction(TestToolName,TestToolDescription,(Person person1, Person person2) => person1),
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
          ["person1"] = new JsonObject()
          {
            ["$ref"] = $"#/definitions/{typeof(Person).FullName}",
            ["description"] = string.Empty
          },
          ["person2"] = new JsonObject()
          {
            ["$ref"] = $"#/definitions/{typeof(Person).FullName}",
            ["description"] = string.Empty
          }
        },
        ["required"] = new JsonArray()
        {
          "person1",
          "person2"
        },
      }
    };

    // complex type with field
    yield return new object[]
    {
      Tool.CreateFromFunction(TestToolName,TestToolDescription,(Dad dad) => dad),
      new JsonObject()
      {
        ["type"] = "object",
        ["definitions"] = new JsonObject()
        {
          [$"{typeof(Dad).FullName}"] = new JsonObject()
          {
            ["type"] = "object",
            ["properties"] = new JsonObject()
            {
              ["Role"] = new JsonObject()
              {
                ["type"] = "string",
                ["description"] = string.Empty
              }
            },
            ["required"] = new JsonArray()
            {
              "Role"
            }
          }
        },
        ["properties"] = new JsonObject()
        {
          ["dad"] = new JsonObject()
          {
            ["$ref"] = $"#/definitions/{typeof(Dad).FullName}",
            ["description"] = string.Empty
          }
        },
        ["required"] = new JsonArray()
        {
          "dad",
        },
      }
    };

    // complex type with members of same type
    yield return new object[]
    {
      Tool.CreateFromFunction(TestToolName,TestToolDescription,(NuclearFamily family) => family),
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
          [$"{typeof(NuclearFamily).FullName}"] = new JsonObject()
          {
            ["type"] = "object",
            ["properties"] = new JsonObject()
            {
              ["Father"] = new JsonObject()
              {
                ["$ref"] = $"#/definitions/{typeof(Person).FullName}",
                ["description"] = string.Empty
              },
              ["Mother"] = new JsonObject()
              {
                ["$ref"] = $"#/definitions/{typeof(Person).FullName}",
                ["description"] = string.Empty
              }
            },
            ["required"] = new JsonArray()
            {
              "Father",
              "Mother"
            }
          }
        },
        ["properties"] = new JsonObject()
        {
          ["family"] = new JsonObject()
          {
            ["$ref"] = $"#/definitions/{typeof(NuclearFamily).FullName}",
            ["description"] = string.Empty
          }
        },
        ["required"] = new JsonArray()
        {
          "family",
        },
      }
    };

    // string parameter type with custom name
    yield return new object[]
    {
      Tool.CreateFromFunction(TestToolName,TestToolDescription,([FunctionParameter("Person's Name", "Name", true)]string name) => name),
      new JsonObject()
      {
        ["type"] = "object",
        ["properties"] = new JsonObject()
        {
          ["Name"] = new JsonObject()
          {
            ["type"] = "string",
            ["description"] = "Person's Name"
          }
        },
        ["required"] = new JsonArray()
        {
          "Name"
        },
      }
    };

    // string parameter with custom description
    yield return new object[]
    {
      Tool.CreateFromFunction(TestToolName,TestToolDescription,([FunctionParameter("The name of the person.", required: true)]string name) => name),
      new JsonObject()
      {
        ["type"] = "object",
        ["properties"] = new JsonObject()
        {
          ["name"] = new JsonObject()
          {
            ["type"] = "string",
            ["description"] = "The name of the person."
          }
        },
        ["required"] = new JsonArray()
        {
          "name"
        },
      }
    };

    // complex type with attributes
    yield return new object[]
    {
      Tool.CreateFromFunction(TestToolName,TestToolDescription,(Rule rule) => rule),
      new JsonObject()
      {
        ["type"] = "object",
        ["definitions"] = new JsonObject()
        {
          [$"{typeof(Rule).FullName}"] = new JsonObject()
          {
            ["type"] = "object",
            ["properties"] = new JsonObject()
            {
              ["Status"] = new JsonObject()
              {
                ["type"] = "string",
                ["description"] = "Indicates the current status of the rule.",
                ["default"] = "Active",
                ["enum"] = new JsonArray()
                {
                  "Inactive",
                  "Active",
                }
              },
              ["Type"] = new JsonObject()
              {
                ["type"] = "string",
                ["description"] = "The type of the rule.",
                ["default"] = "Type A",
                ["enum"] = new JsonArray()
                {
                  "Type B",
                  "Type A",
                }
              }
            },
            ["required"] = new JsonArray()
            {
              "Status"
            }
          }
        },
        ["properties"] = new JsonObject()
        {
          ["rule"] = new JsonObject()
          {
            ["$ref"] = $"#/definitions/{typeof(Rule).FullName}",
            ["description"] = string.Empty
          }
        },
        ["required"] = new JsonArray()
        {
          "rule",
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

class NuclearFamily
{
  public Person Father { get; } = new Person("Father", 40);
  public Person Mother { get; } = new Person("Mother", 38);
}

class Person(string name, int age)
{
  public string Name { get; } = name;
  public int Age { get; } = age;

  public static Person Create(string name, int age) => new Person(name, age);
}

class Dad
{
  public string Role = "Father";
}

class Rule
{
  [FunctionProperty(
    "Indicates the current status of the rule.",
    true,
    "Active",
    new object[] { "Active", "Inactive" }
  )]
  public string Status { get; } = "Active";

  [FunctionProperty(
    "The type of the rule.",
    false,
    "Type A",
    new object[] { "Type B" }
  )]
  public string Type { get; } = "Type A";
}