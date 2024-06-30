using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;

using AnthropicClient.Json;
using AnthropicClient.Models;

namespace AnthropicClient.Utils;

static class JsonSchemaGenerator
{
  private const string TypeKey = "type";
  private const string PropertiesKey = "properties";
  private const string RequiredPropertiesKey = "required";
  private const string DescriptionKey = "description";
  private const string DefinitionsKey = "definitions";
  private const string RefKey = "$ref";
  private const string ItemsKey = "items";
  private const string EnumKey = "enum";
  private const string FormatKey = "format";
  private const string ObjectType = "object";
  private const string StringType = "string";
  private const string ArrayType = "array";

  internal static JsonObject GenerateInputSchema(AnthropicFunction function)
  {
    var parameters = function.Method.GetParameters();
    var inputSchema = new JsonObject()
    {
      [TypeKey] = ObjectType
    };

    if (parameters.Length is 0)
    {
      return inputSchema;
    }

    var properties = new JsonObject();
    var requiredProperties = new JsonArray();

    foreach (var parameter in parameters)
    {
      if (parameter.ParameterType == typeof(CancellationToken))
      {
        continue;
      }

      var attribute = parameter.GetCustomAttribute<FunctionParameterAttribute>();
      var paramName = attribute?.Name ?? parameter.Name;
      var paramDescription = attribute?.Description ?? string.Empty;
      var paramRequired = attribute?.Required ?? !parameter.HasDefaultValue;

      var paramSchema = GenerateParameterTypeSchema(parameter.ParameterType, inputSchema);
      paramSchema[DescriptionKey] = paramDescription;
      
      properties[paramName] = paramSchema;

      if (paramRequired)
      {
        requiredProperties.Add(paramName);
      }
    }

    inputSchema[PropertiesKey] = properties;
    inputSchema[RequiredPropertiesKey] = requiredProperties;
    return inputSchema;
  }

  private static JsonObject GenerateParameterTypeSchema(Type type, JsonObject inputSchema)
  {
    var definitions = inputSchema[DefinitionsKey];

    // Check if a definition for the type already exists
    // If it does, return a reference to the definition
    // no need to evaluate the type further
    if (definitions is not null && definitions.AsObject().ContainsKey(type.FullName))
    {
      return new JsonObject()
      {
        [RefKey] = GetDefinitionPath(type)
      };
    }

    var paramSchema = type switch
    {
      var t when t == typeof(string) || t == typeof(char) => new JsonObject() 
      { 
        [TypeKey] = StringType 
      },
      var t when 
        t == typeof(int) ||  
        t == typeof(uint) ||
        t == typeof(long) ||
        t == typeof(ulong) || 
        t == typeof(short) ||
        t == typeof(ushort) || 
        t == typeof(byte) || 
        t == typeof(sbyte) => new JsonObject() 
      { 
        [TypeKey] = "integer" 
      },
      var t when t == typeof(bool) => new JsonObject() 
      { 
        [TypeKey] = "boolean" 
      },
      var t when 
        t == typeof(double) || 
        t == typeof(float) || 
        t == typeof(decimal) => new JsonObject() 
      { 
        [TypeKey] = "number" 
      },
      var t when t == typeof(DateTime) || t == typeof(DateTimeOffset) => new JsonObject() 
      { 
        [TypeKey] = StringType, 
        [FormatKey] = "date-time" 
      },
      var t when t == typeof(Guid) => new JsonObject() 
      { 
        [TypeKey] = StringType, 
        [FormatKey] = "uuid" 
      },
      var t when t.IsEnum => new JsonObject() 
      { 
        [TypeKey] = StringType, 
        [EnumKey] = Enum.GetNames(t).Aggregate(
          new JsonArray(), (acc, name) => 
          {
            acc.Add(name); 
            return acc; 
          }
        )
      },
      var t when t.IsArray => new JsonObject() 
      { 
        [TypeKey] = ArrayType, 
        [ItemsKey] = GenerateParameterTypeSchema(t.GetElementType()!, inputSchema)
      },
      var t when t.IsGenericType && t.GetGenericTypeDefinition() == typeof(List<>) => new JsonObject() 
      { 
        [TypeKey] = ArrayType, 
        [ItemsKey] = GenerateParameterTypeSchema(t.GetGenericArguments()[0], inputSchema)
      },
      _ => GenerateTypeDefinitionSchema(type, inputSchema)
    };

    return paramSchema;
  }

  private static JsonObject GenerateTypeDefinitionSchema(Type type, JsonObject inputSchema)
  {
    var definitions = inputSchema[DefinitionsKey] ?? new JsonObject();
    var typeSchema = new JsonObject()
    {
      [TypeKey] = ObjectType
    };

    List<MemberInfo> members = [];
    var bindingFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;
    members.AddRange(type.GetProperties(bindingFlags));
    members.AddRange(type.GetFields(bindingFlags));

    var memberProperties = new JsonObject();
    var memberRequiredProperties = new JsonArray();

    foreach (var member in members)
    {
      JsonObject memberProperty;

      var memberType = member switch
      {
        PropertyInfo property => property.PropertyType,
        FieldInfo field => field.FieldType,
        _ => throw new InvalidOperationException("Member is not a property or field")
      };

      // TODO: Provide attribute to allow specifying...
      // 1. Name
      // 2. Description
      // 3. Required
      // 4. Default values
      // 5. Possible values
      var memberPropertyName = memberType.Name;
      var memberDescription = string.Empty;
      var memberRequired = Nullable.GetUnderlyingType(memberType) is null;

      memberProperty = definitions.AsObject().ContainsKey(memberType.FullName)
        ? new JsonObject()
          {
            [RefKey] = GetDefinitionPath(memberType)
          }
        : GenerateParameterTypeSchema(memberType, inputSchema);

      if (memberRequired)
      {
        memberRequiredProperties.Add(memberPropertyName);
      }

      memberProperty[DescriptionKey] = memberDescription;
      memberProperties[memberPropertyName] = memberProperty;
    }

    typeSchema[PropertiesKey] = memberProperties;
    typeSchema[RequiredPropertiesKey] = memberRequiredProperties;
    definitions[type.FullName] = typeSchema;
    inputSchema[DefinitionsKey] = definitions;
    return new JsonObject()
    {
      [RefKey] = GetDefinitionPath(type)
    };
  }

  private static string GetDefinitionPath(Type type) => $"#/definitions/{type.FullName}";
}