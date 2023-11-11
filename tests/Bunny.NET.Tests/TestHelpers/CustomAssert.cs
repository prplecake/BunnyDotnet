using System.Reflection;

namespace Bunny.NET.Tests.TestHelpers;

public class CustomAssert
{
    public static void PropertiesAreEqual<T>(T expected, T actual)
    {
        var failures = new List<string>();
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (var property in properties)
        {
            var v1 = property.GetValue(expected);
            var v2 = property.GetValue(actual);
            if (v1 is null && v2 is null) continue;
            if (!v1.Equals(v2)) failures.Add($"{property.Name}: Expected:<{v1}> Actual:<{v2}>");
        }
        if (failures.Any())
        {
            Assert.Fail(
                $"CustomAssert.PropertiesAreEqual failed. {Environment.NewLine}{string.Join(Environment.NewLine, failures)}");
        }

    }
}
