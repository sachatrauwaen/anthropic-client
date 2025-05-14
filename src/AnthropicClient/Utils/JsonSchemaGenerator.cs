using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

  internal static JObject GenerateInputSchema(AnthropicFunction function)
  {
    var parameters = function.Method.GetParameters();
    var inputSchema = new JObject()
    {
      [TypeKey] = ObjectType
    };

    if (parameters.Length is 0)
    {
      return inputSchema;
    }

    var properties = new JObject();
    var requiredProperties = new JArray();

    foreach (var parameter in parameters)
    {
      if (parameter.ParameterType == typeof(CancellationToken))
      {
        continue;
      }

      var attribute = parameter.GetCustomAttribute<FunctionParameterAttribute>();
      var paramName = attribute is not null
        ? string.IsNullOrWhiteSpace(attribute.Name)
          ? parameter.Name
          : attribute.Name
        : parameter.Name;

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

  private static JObject GenerateParameterTypeSchema(Type type, JObject inputSchema)
  {
    var definitions = inputSchema[DefinitionsKey] as JObject ?? new JObject();

    // Check if a definition for the type already exists
    // If it does, return a reference to the definition
    // no need to evaluate the type further
    if (definitions.ContainsKey(type.FullName))
    {
      return new JObject()
      {
        [RefKey] = GetDefinitionPath(type)
      };
    }

    var paramSchema = type switch
    {
      var t when t == typeof(string) || t == typeof(char) => new JObject()
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
        t == typeof(sbyte) => new JObject()
        {
          [TypeKey] = "integer"
        },
      var t when t == typeof(bool) => new JObject()
      {
        [TypeKey] = "boolean"
      },
      var t when
        t == typeof(double) ||
        t == typeof(float) ||
        t == typeof(decimal) => new JObject()
        {
          [TypeKey] = "number"
        },
      var t when t == typeof(DateTime) || t == typeof(DateTimeOffset) => new JObject()
      {
        [TypeKey] = StringType,
        [FormatKey] = "date-time"
      },
      var t when t == typeof(Guid) => new JObject()
      {
        [TypeKey] = StringType,
        [FormatKey] = "uuid"
      },
      var t when t.IsEnum => new JObject()
      {
        [TypeKey] = StringType,
        [EnumKey] = Enum.GetNames(t).Aggregate(
          new JArray(), (acc, name) =>
          {
            acc.Add(name);
            return acc;
          }
        )
      },
      var t when t.IsArray => new JObject()
      {
        [TypeKey] = ArrayType,
        [ItemsKey] = GenerateParameterTypeSchema(t.GetElementType()!, inputSchema)
      },
      var t when t.IsGenericType && t.GetGenericTypeDefinition() == typeof(List<>) => new JObject()
      {
        [TypeKey] = ArrayType,
        [ItemsKey] = GenerateParameterTypeSchema(t.GetGenericArguments()[0], inputSchema)
      },
      _ => GenerateTypeDefinitionSchema(type, inputSchema)
    };

    return paramSchema;
  }

  private static JObject GenerateTypeDefinitionSchema(Type type, JObject inputSchema)
  {
    var definitions = inputSchema[DefinitionsKey] as JObject ?? new JObject();
    inputSchema[DefinitionsKey] = definitions;

    var typeSchema = new JObject()
    {
      [TypeKey] = ObjectType
    };

    List<MemberInfo> members = [];
    var bindingFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;
    members.AddRange(type.GetProperties(bindingFlags));
    members.AddRange(type.GetFields(bindingFlags));

    var memberProperties = new JObject();
    var memberRequiredProperties = new JArray();

    foreach (var member in members)
    {
      JObject memberProperty;

      var memberType = member switch
      {
        PropertyInfo property => property.PropertyType,
        FieldInfo field => field.FieldType,
        _ => throw new InvalidOperationException("Member is not a property or field")
      };

      var attribute = member.GetCustomAttribute<FunctionPropertyAttribute>();

      var memberPropertyName = member.Name;
      var memberDescription = attribute?.Description ?? string.Empty;
      var memberRequired = attribute?.Required ?? Nullable.GetUnderlyingType(memberType) is null;

      memberProperty = definitions.ContainsKey(memberType.FullName)
        ? new JObject()
        {
          [RefKey] = GetDefinitionPath(memberType)
        }
        : GenerateParameterTypeSchema(memberType, inputSchema);

      if (memberRequired)
      {
        memberRequiredProperties.Add(memberPropertyName);
      }

      JToken? defaultValue = null;

      if (attribute?.DefaultValue is not null)
      {
        defaultValue = JToken.Parse(JsonConvert.SerializeObject(attribute.DefaultValue, JsonSerializationOptions.DefaultOptions));
        memberProperty["default"] = defaultValue;
      }

      if (attribute?.PossibleValues is { Length: > 0 })
      {
        var enumValues = new JArray();

        foreach (var value in attribute.PossibleValues)
        {
          var enumValue = JToken.Parse(JsonConvert.SerializeObject(value, JsonSerializationOptions.DefaultOptions));

          if (defaultValue is null || !JToken.DeepEquals(enumValue, defaultValue))
          {
            enumValues.Add(enumValue);
          }
        }

        var containsDefaultValue = enumValues.Any(value => JToken.DeepEquals(value, defaultValue));

        if (defaultValue is not null && containsDefaultValue is false)
        {
          enumValues.Add(JToken.Parse(JsonConvert.SerializeObject(defaultValue, JsonSerializationOptions.DefaultOptions)));
        }

        memberProperty[EnumKey] = enumValues;
      }

      memberProperty[DescriptionKey] = memberDescription;
      memberProperties[memberPropertyName] = memberProperty;
    }

    typeSchema[PropertiesKey] = memberProperties;
    typeSchema[RequiredPropertiesKey] = memberRequiredProperties;
    definitions[type.FullName] = typeSchema;
    inputSchema[DefinitionsKey] = definitions;
    return new JObject()
    {
      [RefKey] = GetDefinitionPath(type)
    };
  }

  private static string GetDefinitionPath(Type type) => $"#/definitions/{type.FullName}";
}


