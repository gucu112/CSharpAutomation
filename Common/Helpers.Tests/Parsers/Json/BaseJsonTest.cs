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
    /// <typeparam name="TOutput">The type to parse into.</typeparam>
    /// <param name="input">The input JSON.</param>
    /// <param name="settings">The JSON settings.</param>
    /// <returns>The serialized object.</returns>
    protected static TOutput? ParseFromJson<TOutput>(dynamic? input, JsonSettings? settings = null)
    {
        return Parse.FromJson<TOutput>(input, settings);
    }

    /// <summary>
    /// Deserializes the specified object to JSON string output.
    /// </summary>
    /// <typeparam name="TInput">The type of the object.</typeparam>
    /// <param name="value">The object to convert.</param>
    /// <param name="settings">The JSON settings.</param>
    /// <returns>The JSON representation of the object.</returns>
    protected static string ParseToJson<TInput>(object? value, JsonSettings? settings = null)
    {
        if (typeof(TInput) == typeof(Stream))
        {
            var encoding = settings?.Encoding ?? ParseSettings.DefaultEncoding;
            using var stream = Parse.ToJsonStream<MemoryStream>(value, settings);
            return encoding.GetString(stream.GetBuffer(), 0, (int)stream.Length);
        }

        if (typeof(TInput) == typeof(TextWriter))
        {
            using var writer = Parse.ToJsonWriter<StringWriter>(value, settings);
            return writer.GetStringBuilder().ToString();
        }

        return Parse.ToJsonString(value, settings);
    }
}
