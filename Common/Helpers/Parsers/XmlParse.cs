using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using XmlReaderSettings = Gucu112.CSharp.Automation.Helpers.Models.XmlReaderSettings;

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
    /// <param name="settings">The XML reader settings to use for deserialization.</param>
    /// <returns>The deserialized object of type <typeparamref name="TOutput"/>.</returns>
    public static TOutput? FromXml<TOutput>(string content, XmlReaderSettings? settings = null)
        where TOutput : class
    {
        using var stringReader = new StringReader(content);
        return FromXml<TOutput>(stringReader, settings);
    }

    /// <summary>
    /// Deserializes the XML content from a <see cref="TextReader"/> into an object of specific type.
    /// </summary>
    /// <typeparam name="TOutput">The type of object to deserialize into.</typeparam>
    /// <param name="textReader">The <see cref="TextReader"/> containing the XML content to deserialize.</param>
    /// <param name="settings">The XML reader settings to use for deserialization.</param>
    /// <returns>The deserialized object of type <typeparamref name="TOutput"/>.</returns>
    public static TOutput? FromXml<TOutput>(TextReader textReader, XmlReaderSettings? settings = null)
        where TOutput : class
    {
        ArgumentNullException.ThrowIfNull(textReader, nameof(textReader));
        settings ??= ParseSettings.XmlRead;

        if (typeof(TOutput) == typeof(XDocument))
        {
            return XDocument.Load(textReader) as TOutput;
        }

        using var xmlReader = settings.CreateReader(textReader);
        var serializer = new XmlSerializer(typeof(TOutput));
        return (TOutput?)serializer.Deserialize(xmlReader);
    }

    /// <summary>
    /// Deserializes the XML content from a <see cref="Stream"/> into an object of specific type.
    /// </summary>
    /// <typeparam name="TOutput">The type of object to deserialize into.</typeparam>
    /// <param name="stream">The <see cref="Stream"/> containing the XML content to deserialize.</param>
    /// <param name="settings">The XML reader settings to use for deserialization.</param>
    /// <returns>The deserialized object of type <typeparamref name="TOutput"/>.</returns>
    public static TOutput? FromXml<TOutput>(Stream stream, XmlReaderSettings? settings = null)
        where TOutput : class
    {
        return FromXml<TOutput>(new StreamReader(stream, settings?.Encoding ?? ParseSettings.DefaultEncoding), settings);
    }

    /// <summary>
    /// Deserializes the XML content from a file at the specified path into an object of specific type.
    /// </summary>
    /// <typeparam name="TOutput">The type of object to deserialize into.</typeparam>
    /// <param name="path">The path to the XML file.</param>
    /// <param name="settings">The XML reader settings to use for deserialization.</param>
    /// <returns>The deserialized object of type <typeparamref name="TOutput"/>.</returns>
    public static TOutput? FromXmlFile<TOutput>(string path, XmlReaderSettings? settings = null)
        where TOutput : class
    {
        using var fileStream = FileSystem.ReadStream(path);
        return FromXml<TOutput>(fileStream, settings);
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
    /// <param name="textWriter">The <typeparamref name="TWriter"/> to write the JSON content to.
    /// If null, <see cref="StringWriter"/> will be used.</param>
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
    /// <param name="stream">The <typeparamref name="TStream"/> to write the XML content to.
    /// If null, <see cref="MemoryStream"/> will be used.</param>
    /// <returns>The <typeparamref name="TStream"/> containing the serialized XML data.</returns>
    public static TStream ToXmlStream<TStream>(object? value, TStream? stream = null)
        where TStream : Stream
    {
        stream ??= (TStream)(Stream)new MemoryStream();
        var streamWriter = new StreamWriter(stream, leaveOpen: true);
        return (TStream)ToXmlWriter(value, streamWriter).BaseStream;
    }

    /// <summary>
    /// Serializes an object into a file at the specified path using XML format.
    /// </summary>
    /// <param name="value">The object to serialize.</param>
    /// <param name="path">The path to the XML file.</param>
    internal static void ToXmlFile(object value, string path)
    {
        using var fileStream = FileSystem.WriteStream(path);
        using var memoryStream = ToXmlStream(value, new MemoryStream());
        memoryStream.WriteTo(fileStream);
    }
}
