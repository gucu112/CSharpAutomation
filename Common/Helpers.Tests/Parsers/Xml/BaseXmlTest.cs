using Gucu112.CSharp.Automation.Helpers.Parsers;
using XmlReaderSettings = Gucu112.CSharp.Automation.Helpers.Models.XmlReaderSettings;

namespace Gucu112.CSharp.Automation.Helpers.Tests.Parsers.Xml;

/// <summary>
/// Base class for XML parsing tests.
/// </summary>
public class BaseXmlTest : BaseTest
{
    /// <summary>
    /// Parses a value of any type to XML input.
    /// </summary>
    /// <typeparam name="TOutput">The type to parse into.</typeparam>
    /// <param name="input">The input XML.</param>
    /// <param name="settings">The XML reader settings.</param>
    /// <returns>The parsed object.</returns>
    protected static TOutput? ParseFromXml<TOutput>(dynamic? input, XmlReaderSettings? settings = null)
    {
        return Parse.FromXml<TOutput>(input, settings);
    }

    /// <summary>
    /// Deserializes the specified object to XML string output.
    /// </summary>
    /// <typeparam name="TInput">The type of the object.</typeparam>
    /// <param name="value">The object to convert.</param>
    /// <returns>The XML representation of the object.</returns>
    protected static string ParseToXml<TInput>(object? value)
    {
        if (typeof(TInput) == typeof(Stream))
        {
            using var stream = Parse.ToXmlStream<MemoryStream>(value);
            return Encoding.UTF8.GetString(stream.GetBuffer(), 0, (int)stream.Length);
        }

        if (typeof(TInput) == typeof(TextWriter))
        {
            using var writer = Parse.ToXmlWriter<StringWriter>(value);
            return writer.GetStringBuilder().ToString();
        }

        return Parse.ToXmlString(value);
    }
}
