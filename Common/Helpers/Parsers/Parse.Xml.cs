using Gucu112.CSharp.Automation.Helpers.Models;

namespace Gucu112.CSharp.Automation.Helpers.Parsers;

/// <inheritdoc path="Common/Helpers/Parsers/Parse.cs"/>
[System.Diagnostics.CodeAnalysis.SuppressMessage(
    "StyleCop.CSharp.DocumentationRules",
    "SA1648:inheritdoc should be used with inheriting class",
    Justification = "partial class")]
public static partial class Parse
{
    private static readonly XmlParse XmlParse = new();

    /// <inheritdoc cref="IXmlParse.FromXml{TOutput}(string, XmlReaderSettings?)"/>
    public static TOutput? FromXml<TOutput>(string content, XmlReaderSettings? settings = null)
    {
        return XmlParse.FromXml<TOutput>(content, settings);
    }

    /// <inheritdoc cref="IXmlParse.FromXml{TOutput}(TextReader, XmlReaderSettings?)"/>
    public static TOutput? FromXml<TOutput>(TextReader textReader, XmlReaderSettings? settings = null)
    {
        return XmlParse.FromXml<TOutput>(textReader, settings);
    }

    /// <inheritdoc cref="IXmlParse.FromXml{TOutput}(Stream, XmlReaderSettings?)"/>
    public static TOutput? FromXml<TOutput>(Stream stream, XmlReaderSettings? settings = null)
    {
        return XmlParse.FromXml<TOutput>(stream, settings);
    }

    /// <inheritdoc cref="IXmlParse.FromXmlFile{TOutput}(string, XmlReaderSettings?)"/>
    public static TOutput? FromXmlFile<TOutput>(string path, XmlReaderSettings? settings = null)
    {
        return XmlParse.FromXmlFile<TOutput>(path, settings);
    }

    /// <inheritdoc cref="IXmlParse.ToXmlString(object?)"/>
    public static string ToXmlString(object? value)
    {
        return XmlParse.ToXmlString(value);
    }

    /// <inheritdoc cref="IXmlParse.ToXmlWriter{TWriter}(object?, TWriter?)"/>
    public static TWriter ToXmlWriter<TWriter>(object? value, TWriter? textWriter = null)
        where TWriter : TextWriter
    {
        return XmlParse.ToXmlWriter(value, textWriter);
    }

    /// <inheritdoc cref="IXmlParse.ToXmlStream{TStream}(object?, TStream?)"/>
    public static TStream ToXmlStream<TStream>(object? value, TStream? stream = null)
        where TStream : Stream
    {
        return XmlParse.ToXmlStream(value, stream);
    }

    /// <inheritdoc cref="IXmlParse.ToXmlFile(object?, string)"/>
    public static void ToXmlFile(object? value, string path)
    {
        XmlParse.ToXmlFile(value, path);
    }
}
