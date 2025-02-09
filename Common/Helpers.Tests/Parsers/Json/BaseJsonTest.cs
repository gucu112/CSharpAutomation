using Allure.NUnit;
using Gucu112.CSharp.Automation.Helpers.Models;
using Gucu112.CSharp.Automation.Helpers.Parsers;

namespace Gucu112.CSharp.Automation.Helpers.Tests.Parsers.Json;

/// <summary>
/// Base class for JSON parsing tests.
/// </summary>
[AllureNUnit]
public class BaseJsonTest : BaseTest
{
    /// <summary>
    /// Serializes a value of any type to JSON input.
    /// </summary>
    /// <typeparam name="T">The type to parse into.</typeparam>
    /// <param name="input">The input JSON.</param>
    /// <param name="settings">The JSON settings.</param>
    /// <returns>The serialized object.</returns>
    protected static T? ParseFromJson<T>(dynamic? input, JsonSettings? settings = null)
        where T : class
    {
        return Parse.FromJson<T>(input, settings);
    }

    /// <summary>
    /// Deserializes the specified object to JSON string output.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <param name="value">The object to convert.</param>
    /// <param name="settings">The JSON settings.</param>
    /// <returns>The JSON representation of the object.</returns>
    protected static string ParseToJson<T>(object? value, JsonSettings? settings = null)
    {
        if (typeof(T) == typeof(Stream))
        {
            var encoding = settings?.Encoding ?? Encoding.UTF8;
            using var stream = Parse.ToJsonStream<MemoryStream>(value, settings);
            return encoding.GetString(stream.GetBuffer(), 0, (int)stream.Length);
        }

        if (typeof(T) == typeof(TextWriter))
        {
            using var writer = Parse.ToJsonWriter<StringWriter>(value, settings);
            return writer.GetStringBuilder().ToString();
        }

        return Parse.ToJsonString(value, settings);
    }
}
