using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Gucu112.CSharp.Automation.Helpers.Models;
using XmlReaderSettings = Gucu112.CSharp.Automation.Helpers.Models.XmlReaderSettings;

namespace Gucu112.CSharp.Automation.Helpers.Parsers;

/// <inheritdoc/>
public class XmlParse : IXmlParse
{
    private static IFileSystem FileSystem => ParseSettings.FileSystem;

    /// <inheritdoc/>
    public TOutput? FromXml<TOutput>(string content, XmlReaderSettings? settings = null)
    {
        using var stringReader = new StringReader(content);
        return FromXml<TOutput>(stringReader, settings);
    }

    /// <inheritdoc/>
    public TOutput? FromXml<TOutput>(TextReader textReader, XmlReaderSettings? settings = null)
    {
        ArgumentNullException.ThrowIfNull(textReader, nameof(textReader));
        settings ??= ParseSettings.XmlRead;

        if (typeof(TOutput) == typeof(XDocument))
        {
            return (TOutput)(object)XDocument.Load(textReader);
        }

        using var xmlReader = settings.CreateReader(textReader);
        var serializer = new XmlSerializer(typeof(TOutput));
        return (TOutput?)serializer.Deserialize(xmlReader);
    }

    /// <inheritdoc/>
    public TOutput? FromXml<TOutput>(Stream stream, XmlReaderSettings? settings = null)
    {
        return FromXml<TOutput>(new StreamReader(stream, settings?.Encoding ?? ParseSettings.DefaultEncoding), settings);
    }

    /// <inheritdoc/>
    public TOutput? FromXmlFile<TOutput>(string path, XmlReaderSettings? settings = null)
    {
        using var fileStream = FileSystem.ReadStream(path);
        return FromXml<TOutput>(fileStream, settings);
    }

    /// <inheritdoc/>
    public string ToXmlString(object? value)
    {
        return ToXmlWriter<StringWriter>(value).GetStringBuilder().ToString();
    }

    /// <inheritdoc/>
    public TWriter ToXmlWriter<TWriter>(object? value, TWriter? textWriter = null)
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

    /// <inheritdoc/>
    public TStream ToXmlStream<TStream>(object? value, TStream? stream = null)
        where TStream : Stream
    {
        stream ??= (TStream)(Stream)new MemoryStream();
        var streamWriter = new StreamWriter(stream, leaveOpen: true);
        return (TStream)ToXmlWriter(value, streamWriter).BaseStream;
    }

    /// <inheritdoc/>
    public void ToXmlFile(object? value, string path)
    {
        using var fileStream = FileSystem.WriteStream(path);
        using var memoryStream = ToXmlStream(value, new MemoryStream());
        memoryStream.WriteTo(fileStream);
    }
}
