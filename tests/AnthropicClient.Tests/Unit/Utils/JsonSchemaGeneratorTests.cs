using System.Text.Json.Nodes;

namespace AnthropicClient.Tests.Unit.Models;

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
          ["description"] = string.Empty
        }
      },
      ["required"] = new JsonArray(),
    };

    var testMethod = (int age) => age;
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
}