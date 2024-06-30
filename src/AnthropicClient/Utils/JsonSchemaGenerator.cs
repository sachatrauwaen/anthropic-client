using System.Reflection;
using System.Text.Json.Nodes;

using AnthropicClient.Models;

namespace AnthropicClient.Utils;

static class JsonSchemaGenerator
{
  private const string TypeKey = "type";
  private const string PropertiesKey = "properties";
  private const string RequiredPropertiesKey = "required";
  private const string DescriptionKey = "description";
  private const string Object = "object";

  public static JsonNode GenerateInputSchema(AnthropicFunction function)
  {
    var parameters = function.Method.GetParameters();
    var schema = new JsonObject()
    {
      [TypeKey] = Object
    };

    if (parameters.Length is 0)
    {
      return schema;
    }

    var properties = new JsonObject();
    var requiredProperties = new JsonArray();

    foreach (var parameter in parameters)
    {
      if (parameter.ParameterType == typeof(CancellationToken))
      {
        continue;
      }

      var paramName = parameter.Name;
      var paramDescription = string.Empty;
      var paramRequired = parameter.HasDefaultValue;

      var paramObject = new JsonObject();
      paramObject[DescriptionKey] = paramDescription;
      
      properties[paramName] = paramObject;

      if (paramRequired)
      {
        requiredProperties.Add(paramName);
      }
    }

    schema[PropertiesKey] = properties;
    schema[RequiredPropertiesKey] = requiredProperties;
    return schema;
  }
}