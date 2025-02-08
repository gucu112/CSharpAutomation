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
        where TOutput : class
    {
        using var stringReader = new StringReader(content);
        return FromXml<TOutput>(stringReader);
    }

    /// <summary>
    /// Deserializes the XML content from a <see cref="TextReader"/> into an object of specific type.
    /// </summary>
    /// <typeparam name="TOutput">The type of object to deserialize into.</typeparam>
    /// <param name="textReader">The <see cref="TextReader"/> containing the XML content to deserialize.</param>
    /// <returns>The deserialized object of type <typeparamref name="TOutput"/>.</returns>
    public static TOutput? FromXml<TOutput>(TextReader textReader)
        where TOutput : class
    {
        ArgumentNullException.ThrowIfNull(textReader, nameof(textReader));

        using var xmlReader = new XmlTextReader(textReader);
        var serializer = new XmlSerializer(typeof(TOutput));
        return (TOutput?)serializer.Deserialize(xmlReader);
    }

    /// <summary>
    /// Deserializes the XML content from a <see cref="Stream"/> into an object of specific type.
    /// </summary>
    /// <typeparam name="TOutput">The type of object to deserialize into.</typeparam>
    /// <param name="stream">The <see cref="Stream"/> containing the XML content to deserialize.</param>
    /// <returns>The deserialized object of type <typeparamref name="TOutput"/>.</returns>
    public static TOutput? FromXml<TOutput>(Stream stream)
        where TOutput : class
    {
        return FromXml<TOutput>(new StreamReader(stream));
    }
}
