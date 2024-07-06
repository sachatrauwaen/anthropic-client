using System.Reflection;
using System.Text.Json.Nodes;

namespace AnthropicClient.Tests.Unit.Models;

public class ToolTests : SerializationTest
{
  [Theory]
  [InlineData(null)]
  [InlineData("")]
  [InlineData(" ")]
  public void Constructor_WhenGivenInvalidNameValue_ItShouldThrowArgumentException(string name)
  {
    var method = () => true;
    var function = new AnthropicFunction(method.Method);
    var action = () => new Tool(name, "description", function);

    action.Should().Throw<ArgumentException>();
  }

  [Theory]
  [InlineData(null)]
  [InlineData("")]
  [InlineData(" ")]
  public void Constructor_WhenGivenInvalidDescriptionValue_ItShouldThrowArgumentException(string description)
  {
    var method = () => true;
    var function = new AnthropicFunction(method.Method);
    var action = () => new Tool("name", description, function);

    action.Should().Throw<ArgumentException>();
  }

  [Fact]
  public void Constructor_WhenGivenNullFunctionValue_ItShouldThrowArgumentNullException()
  {
    var action = () => new Tool("name", "description", null!);

    action.Should().Throw<ArgumentNullException>();
  }

  [Fact]
  public void Constructor_WhenCalled_ItShouldInitializeProperties()
  {
    var method = () => true;
    var function = new AnthropicFunction(method.Method);
    var tool = new Tool("test name", "description", function);

    var expectedSchema = new JsonObject()
    {
      ["type"] = "object",
    };

    tool.Name.Should().Be("test_name");
    tool.DisplayName.Should().Be("test name");
    tool.Description.Should().Be("description");
    tool.Function.Should().Be(function);
    tool.InputSchema.Should().BeEquivalentTo(
      expectedSchema,
      t => t.IgnoringCyclicReferences()
    );
  }

  [Fact]
  public void Constructor_WhenCalledAndGivenNameIsMoreThanMaxLength_ItShouldTruncateName()
  {
    var method = () => true;
    var function = new AnthropicFunction(method.Method);
    var tool = new Tool(new string('a', 65), "description", function);

    tool.Name.Should().HaveLength(64);
  }

  [Theory]
  [InlineData(typeof(TestClass), null)]
  [InlineData(typeof(TestClass), "")]
  [InlineData(typeof(TestClass), " ")]
  [InlineData(typeof(TestClass), "MadeUpMethod")]
  public void CreateFromStaticMethod_WhenGivenInvalidMethodName_ItShouldThrowArgumentException(Type type, string methodName)
  {
    var action = () => Tool.CreateFromStaticMethod("name", "description", type, methodName);

    action.Should().Throw<ArgumentException>();
  }

  [Fact]
  public void CreateFromStaticMethod_WhenGivenTypeIsNull_ItShouldThrowArgumentNullException()
  {
    var action = () => Tool.CreateFromStaticMethod("name", "description", null!, "TestStaticMethod");

    action.Should().Throw<ArgumentNullException>();
  }

  [Fact]
  public void CreateFromStaticMethod_WhenGivenTypeThatDoesNotContainMethod_ItShouldThrowArgumentException()
  {
    var action = () => Tool.CreateFromStaticMethod("name", "description", typeof(ToolTests), nameof(TestClass.TestStaticMethod));

    action.Should().Throw<ArgumentException>();
  }

  [Fact]
  public void CreateFromStaticMethod_WhenCalled_ItShouldReturnTool()
  {
    var tool = Tool.CreateFromStaticMethod("test name", "description", typeof(TestClass), nameof(TestClass.TestStaticMethod));

    var expectedSchema = new JsonObject()
    {
      ["type"] = "object",
    };

    tool.Name.Should().Be("test_name");
    tool.DisplayName.Should().Be("test name");
    tool.Description.Should().Be("description");
    tool.Function.Method.Name.Should().Be(nameof(TestClass.TestStaticMethod));
    tool.Function.Instance.Should().BeNull();
    tool.InputSchema.Should().BeEquivalentTo(
      expectedSchema,
      t => t.IgnoringCyclicReferences()
    );
  }

  [Theory]
  [InlineData(null)]
  [InlineData("")]
  [InlineData(" ")]
  [InlineData("MadeUpMethod")]
  public void CreateFromInstanceMethod_WhenGivenInvalidMethodName_ItShouldThrowArgumentException(string methodName)
  {
    var action = () => Tool.CreateFromInstanceMethod("name", "description", new TestClass(), methodName);

    action.Should().Throw<ArgumentException>();
  }

  [Fact]
  public void CreateFromInstanceMethod_WhenGivenInstanceIsNull_ItShouldThrowArgumentNullException()
  {
    var action = () => Tool.CreateFromInstanceMethod("name", "description", null!, "TestMethod");

    action.Should().Throw<ArgumentNullException>();
  }

  [Fact]
  public void CreateFromInstanceMethod_WhenGivenInstanceThatDoesNotContainMethod_ItShouldThrowArgumentException()
  {
    var action = () => Tool.CreateFromInstanceMethod("name", "description", new ToolTests(), nameof(TestClass.TestStaticMethod));

    action.Should().Throw<ArgumentException>();
  }

  [Fact]
  public void CreateFromInstanceMethod_WhenMethodIsNotAnInstanceMethod_ItShouldThrowArgumentException()
  {
    var action = () => Tool.CreateFromInstanceMethod("name", "description", new TestClass(), nameof(TestClass.TestStaticMethod));

    action.Should().Throw<ArgumentException>();
  }

  [Fact]
  public void CreateFromInstanceMethod_WhenCalled_ItShouldReturnTool()
  {
    var instance = new TestClass();
    var tool = Tool.CreateFromInstanceMethod("test name", "description", instance, nameof(instance.TestInstanceMethod));

    var expectedSchema = new JsonObject()
    {
      ["type"] = "object",
    };

    tool.Name.Should().Be("test_name");
    tool.DisplayName.Should().Be("test name");
    tool.Description.Should().Be("description");
    tool.Function.Method.Name.Should().Be(nameof(TestClass.TestInstanceMethod));
    tool.Function.Instance.Should().Be(instance);
    tool.InputSchema.Should().BeEquivalentTo(
      expectedSchema,
      t => t.IgnoringCyclicReferences()
    );
  }

  [Fact]
  public void CreateFromParameterlessFunction_WhenGivenNullFunction_ItShouldThrowArgumentNullException()
  {
    var action = () => Tool.CreateFromFunction<string>("name", "description", null!);

    action.Should().Throw<ArgumentNullException>();
  }

  [Fact]
  public void CreateFromParameterlessFunction_WhenCalled_ItShouldReturnTool()
  {
    var func = () => true;
    var tool = Tool.CreateFromFunction("test name", "description", func);

    var expectedSchema = new JsonObject()
    {
      ["type"] = "object",
    };

    tool.Name.Should().Be("test_name");
    tool.DisplayName.Should().Be("test name");
    tool.Description.Should().Be("description");
    tool.Function.Method.Should().BeSameAs(func.Method);
    tool.InputSchema.Should().BeEquivalentTo(
      expectedSchema,
      t => t.IgnoringCyclicReferences()
    );
  }

  [Fact]
  public void CreateFromFunctionWithParameter_WhenGivenNullFunction_ItShouldThrowArgumentNullException()
  {
    var action = () => Tool.CreateFromFunction<string, string>("name", "description", null!);

    action.Should().Throw<ArgumentNullException>();
  }

  [Fact]
  public void CreateFromFunctionWithParameter_WhenCalled_ItShouldReturnTool()
  {
    var func = (string s) => true;
    var tool = Tool.CreateFromFunction("test name", "description", func);

    var expectedSchema = new JsonObject()
    {
      ["type"] = "object",
      ["properties"] = new JsonObject()
      {
        ["s"] = new JsonObject()
        {
          ["type"] = "string",
        },
      },
      ["required"] = new JsonArray()
      {
        "s",
      },
    };

    tool.Name.Should().Be("test_name");
    tool.DisplayName.Should().Be("test name");
    tool.Description.Should().Be("description");
    tool.Function.Method.Should().BeSameAs(func.Method);
    tool.InputSchema.Should().BeEquivalentTo(
      expectedSchema,
      t => t.IgnoringCyclicReferences()
    );
  }

  [Fact]
  public void CreateFromClass_WhenCalledWithToolWhoseNameIsNull_ItShouldThrowException()
  {
    var action = () => Tool.CreateFromClass<ToolWithNullName>();

    action.Should().Throw<ArgumentException>();
  }

  [Fact]
  public void CreateFromClass_WhenCalledWithToolWhoseNameIsEmpty_ItShouldThrowException()
  {
    var action = () => Tool.CreateFromClass<ToolWithEmptyName>();

    action.Should().Throw<ArgumentException>();
  }

  [Fact]
  public void CreateFromClass_WhenCalledWithToolWhoseDescriptionIsNull_ItShouldThrowException()
  {
    var action = () => Tool.CreateFromClass<ToolWithNullDescription>();

    action.Should().Throw<ArgumentException>();
  }

  [Fact]
  public void CreateFromClass_WhenCalledWithToolWhoseDescriptionIsEmpty_ItShouldThrowException()
  {
    var action = () => Tool.CreateFromClass<ToolWithEmptyDescription>();

    action.Should().Throw<ArgumentException>();
  }

  [Fact]
  public void CreateFromClass_WhenCalledWithToolWhoseFunctionIsNull_ItShouldThrowException()
  {
    var action = () => Tool.CreateFromClass<ToolWithNullFunction>();

    action.Should().Throw<ArgumentNullException>();
  }

  [Fact]
  public void CreateFromClass_WhenCalledWithProperTool_ItShouldReturnTool()
  {
    var tool = Tool.CreateFromClass<ProperTool>();

    var expectedSchema = new JsonObject()
    {
      ["type"] = "object",
    };

    tool.Name.Should().Be("Name");
    tool.DisplayName.Should().Be("Name");
    tool.Description.Should().Be("Description");
    tool.Function.Method.Name.Should().Be(nameof(ProperTool.GetWeather));
    tool.Function.Instance.Should().BeNull();
    tool.InputSchema.Should().BeEquivalentTo(
      expectedSchema,
      t => t.IgnoringCyclicReferences()
    );
  }
}

class TestClass
{
  private readonly bool _result = true;

  public static bool TestStaticMethod() => true;
  public bool TestInstanceMethod() => _result;
}

class ProperTool : ITool
{
  public string Name => "Name";

  public string Description { get; } = "Description";

  public MethodInfo Function => typeof(ProperTool).GetMethod(nameof(GetWeather))!;

  public static string GetWeather()
  {
    return "Sunny";
  }
}

class ToolWithNullName : ITool
{
  public string Name => null!;

  public string Description { get; } = "Description";

  public MethodInfo Function => typeof(ToolWithNullName).GetMethod(nameof(Tool))!;

  public static string GetWeather()
  {
    return "Sunny";
  }
}

class ToolWithEmptyName : ITool
{
  public string Name => string.Empty;

  public string Description { get; } = "Description";

  public MethodInfo Function => typeof(ToolWithEmptyName).GetMethod(nameof(Tool))!;

  public static string GetWeather()
  {
    return "Sunny";
  }
}

class ToolWithNullDescription : ITool
{
  public string Name => "Name";

  public string Description => null!;

  public MethodInfo Function => typeof(ToolWithNullDescription).GetMethod(nameof(Tool))!;

  public static string GetWeather()
  {
    return "Sunny";
  }
}

class ToolWithEmptyDescription : ITool
{
  public string Name => "Name";

  public string Description => string.Empty;

  public MethodInfo Function => typeof(ToolWithEmptyDescription).GetMethod(nameof(Tool))!;

  public static string GetWeather()
  {
    return "Sunny";
  }
}

class ToolWithNullFunction : ITool
{
  public string Name => "Name";

  public string Description => "Description";

  public MethodInfo Function => null!;
}