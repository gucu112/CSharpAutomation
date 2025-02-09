using System.Xml;
using System.Xml.Linq;
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

        if (typeof(TOutput) == typeof(XDocument))
        {
            return XDocument.Load(textReader) as TOutput;
        }

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

    /// <summary>
    /// Deserializes the XML content from a file at the specified path into an object of specific type.
    /// </summary>
    /// <typeparam name="TOutput">The type of object to deserialize into.</typeparam>
    /// <param name="path">The path to the XML file.</param>
    /// <returns>The deserialized object of type <typeparamref name="TOutput"/>.</returns>
    public static TOutput? FromXmlFile<TOutput>(string path)
        where TOutput : class
    {
        using var fileStream = FileSystem.ReadStream(path);
        return FromXml<TOutput>(fileStream);
    }

    /// <summary>
    /// Serializes an object into a XML string.
    /// </summary>
    /// <param name="value">The object to serialize.</param>
    /// <returns>The XML string representation of the object.</returns>
    public static string ToXmlString(object? value)
    {
        return ToXmlWriter<StringWriter>(value).GetStringBuilder().ToString();
    }

    /// <summary>
    /// Serializes an object into a <see cref="TextWriter"/> using XML format.
    /// </summary>
    /// <typeparam name="TWriter">The type of the <see cref="TextWriter"/> to use.</typeparam>
    /// <param name="value">The object to serialize.</param>
    /// <param name="textWriter">The <typeparamref name="TWriter"/> to write the JSON content to.</param>
    /// <returns>The <typeparamref name="TWriter"/> containing the serialized XML string.</returns>
    public static TWriter ToXmlWriter<TWriter>(object? value, TWriter? textWriter = null)
        where TWriter : TextWriter
    {
        ArgumentNullException.ThrowIfNull(value, nameof(value));

        textWriter ??= (TWriter)(TextWriter)new StringWriter();
        using (var xmlWriter = XmlWriter.Create(textWriter))
        {
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);
            var serializer = new XmlSerializer(value.GetType());
            serializer.Serialize(xmlWriter, value, namespaces);
        }

        return textWriter;
    }

    /// <summary>
    /// Serializes an object into a <see cref="Stream"/> using XML format.
    /// </summary>
    /// <typeparam name="TStream">The type of the <see cref="Stream"/> to use.</typeparam>
    /// <param name="value">The object to serialize.</param>
    /// <returns>The <typeparamref name="TStream"/> containing the serialized XML data.</returns>
    public static TStream ToXmlStream<TStream>(object? value)
        where TStream : Stream
    {
        var stream = new MemoryStream();
        var streamWriter = new StreamWriter(stream, leaveOpen: true);
        return (TStream)ToXmlWriter(value, streamWriter).BaseStream;
    }
}
