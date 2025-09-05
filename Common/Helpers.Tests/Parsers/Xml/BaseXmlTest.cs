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
    /// <typeparam name="T">The type to parse into.</typeparam>
    /// <param name="input">The input XML.</param>
    /// <param name="settings">The XML reader settings.</param>
    /// <returns>The parsed object.</returns>
    protected static T? ParseFromXml<T>(dynamic? input, XmlReaderSettings? settings = null)
        where T : class
    {
        return Parse.FromXml<T>(input, settings);
    }

    /// <summary>
    /// Deserializes the specified object to XML string output.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <param name="value">The object to convert.</param>
    /// <returns>The XML representation of the object.</returns>
    protected static string ParseToXml<T>(object? value)
    {
        if (typeof(T) == typeof(Stream))
        {
            using var stream = Parse.ToXmlStream<MemoryStream>(value);
            return Encoding.UTF8.GetString(stream.GetBuffer(), 0, (int)stream.Length);
        }

        if (typeof(T) == typeof(TextWriter))
        {
            using var writer = Parse.ToXmlWriter<StringWriter>(value);
            return writer.GetStringBuilder().ToString();
        }

        return Parse.ToXmlString(value);
    }
}
