using System.Text.Json.Nodes;

namespace AnthropicClient.Tests.Unit.Utils;

public class JsonSchemaGeneratorTests
{
  [Fact]
  public void GenerateInputSchema_GivenFunctionWithNoParameters_ReturnsSchemaWithTypeObject()
  {
    var expectedSchema = new JsonObject
    {
      ["type"] = "object"
    };
    var testMethod = () => true;
    var function = new AnthropicFunction(testMethod.Method);

    var schema = JsonSchemaGenerator.GenerateInputSchema(function);

    JsonAssert.Equal(expectedSchema, schema);
  }

  [Fact]
  public void GenerateInputSchema_GivenFunctionWithParameterThatHasDefaultValue_ItShouldReturnSchemaWithPropertyNameDescriptionAndNoRequiredProperties()
  {
    var expectedSchema = new JsonObject()
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
      ["required"] = new JsonArray(),
    };

    var testMethod = (int age = 0) => age;
    var function = new AnthropicFunction(testMethod.Method);

    var schema = JsonSchemaGenerator.GenerateInputSchema(function);

    JsonAssert.Equal(expectedSchema, schema);
  }

  [Fact]
  public void GenerateInputSchema_GivenFunctionWithParameterThatDoesNotHaveDefaultValue_ItShouldReturnSchemaWithPropertyNameDescriptionAndRequiredProperties()
  {
    var expectedSchema = new JsonObject()
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
    };

    var testMethod = (string name) => name;
    var function = new AnthropicFunction(testMethod.Method);

    var schema = JsonSchemaGenerator.GenerateInputSchema(function);

    JsonAssert.Equal(expectedSchema, schema);
  }

  [Fact]
  public void GenerateInputSchema_GivenFunctionWithParameterThatHasAttribute_ItShouldReturnSchemaWithCorrectPropertyNameDescriptionAndRequiredProperty()
  {
    var expectedSchema = new JsonObject()
    {
      ["type"] = "object",
      ["properties"] = new JsonObject()
      {
        ["Person's Age"] = new JsonObject()
        {
          ["type"] = "integer",
          ["description"] = "The age of the person."
        }
      },
      ["required"] = new JsonArray()
      {
        "Person's Age"
      },
    };

    var testMethod = ([FunctionParameter("The age of the person.", "Person's Age", true)] int name) => name;
    var function = new AnthropicFunction(testMethod.Method);

    var schema = JsonSchemaGenerator.GenerateInputSchema(function);

    JsonAssert.Equal(expectedSchema, schema);
  }

  [Fact]
  public void GenerateInputSchema_GivenFunctionWithParametersThatIncludesCancellationToken_ItShouldReturnSchemaWithoutCancellationToken()
  {
    var expectedSchema = new JsonObject()
    {
      ["type"] = "object",
      ["properties"] = new JsonObject(),
      ["required"] = new JsonArray(),
    };

    var testMethod = (CancellationToken token) => true;
    var function = new AnthropicFunction(testMethod.Method);

    var schema = JsonSchemaGenerator.GenerateInputSchema(function);

    JsonAssert.Equal(expectedSchema, schema);
  }

  [Theory]
  [ClassData(typeof(JsonSchemaGeneratorTestData))]
  public void GenerateInputSchema_GivenFunction_ItShouldReturnExpectedSchema(Tool tool, JsonObject expectedSchema)
  {
    var schema = JsonSchemaGenerator.GenerateInputSchema(tool.Function);
    JsonAssert.Equal(expectedSchema, schema);
  }
}