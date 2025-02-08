using System.Xml;
using System.Xml.Serialization;

namespace Gucu112.CSharp.Automation.Helpers.Parsers;

/// <summary>
/// Provides methods for parsing XML data.
/// </summary>
public static partial class Parse
{
    /// <summary>
    /// Deserializes the XML content into an object of specific type.
    /// </summary>
    /// <typeparam name="TOutput">The type of object to deserialize into.</typeparam>
    /// <param name="content">The XML content to deserialize.</param>
    /// <returns>The deserialized object of type <typeparamref name="TOutput"/>.</returns>
    public static TOutput? FromXml<TOutput>(string content)
    {
        using var strReader = new StringReader(content);
        using var xmlReader = new XmlTextReader(strReader);

        var serializer = new XmlSerializer(typeof(TOutput));
        return (TOutput?)serializer.Deserialize(xmlReader);
    }
}
