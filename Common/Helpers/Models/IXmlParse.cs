namespace Gucu112.CSharp.Automation.Helpers.Models;

/// <summary>
/// Provides methods for parsing XML data.
/// </summary>
public interface IXmlParse
{
    /// <summary>
    /// Deserializes the XML content into an object of specific type.
    /// </summary>
    /// <typeparam name="TOutput">The type of object to deserialize into.</typeparam>
    /// <param name="content">The XML content to deserialize.</param>
    /// <param name="settings">The XML reader settings to use for deserialization.</param>
    /// <returns>The deserialized object of type <typeparamref name="TOutput"/>.</returns>
    TOutput? FromXml<TOutput>(string content, XmlReaderSettings? settings = null);

    /// <summary>
    /// Deserializes the XML content from a <see cref="TextReader"/> into an object of specific type.
    /// </summary>
    /// <typeparam name="TOutput">The type of object to deserialize into.</typeparam>
    /// <param name="textReader">The <see cref="TextReader"/> containing the XML content to deserialize.</param>
    /// <param name="settings">The XML reader settings to use for deserialization.</param>
    /// <returns>The deserialized object of type <typeparamref name="TOutput"/>.</returns>
    TOutput? FromXml<TOutput>(TextReader textReader, XmlReaderSettings? settings = null);

    /// <summary>
    /// Deserializes the XML content from a <see cref="Stream"/> into an object of specific type.
    /// </summary>
    /// <typeparam name="TOutput">The type of object to deserialize into.</typeparam>
    /// <param name="stream">The <see cref="Stream"/> containing the XML content to deserialize.</param>
    /// <param name="settings">The XML reader settings to use for deserialization.</param>
    /// <returns>The deserialized object of type <typeparamref name="TOutput"/>.</returns>
    TOutput? FromXml<TOutput>(Stream stream, XmlReaderSettings? settings = null);

    /// <summary>
    /// Deserializes the XML content from a file at the specified path into an object of specific type.
    /// </summary>
    /// <typeparam name="TOutput">The type of object to deserialize into.</typeparam>
    /// <param name="path">The path to the XML file.</param>
    /// <param name="settings">The XML reader settings to use for deserialization.</param>
    /// <returns>The deserialized object of type <typeparamref name="TOutput"/>.</returns>
    TOutput? FromXmlFile<TOutput>(string path, XmlReaderSettings? settings = null);

    /// <summary>
    /// Serializes an object into a XML string.
    /// </summary>
    /// <param name="value">The object to serialize.</param>
    /// <returns>The XML string representation of the object.</returns>
    string ToXmlString(object? value);

    /// <summary>
    /// Serializes an object into a <see cref="TextWriter"/> using XML format.
    /// </summary>
    /// <typeparam name="TWriter">The type of the <see cref="TextWriter"/> to use.</typeparam>
    /// <param name="value">The object to serialize.</param>
    /// <param name="textWriter">The <typeparamref name="TWriter"/> to write the JSON content to.
    /// If null, <see cref="StringWriter"/> will be used.</param>
    /// <returns>The <typeparamref name="TWriter"/> containing the serialized XML string.</returns>
    TWriter ToXmlWriter<TWriter>(object? value, TWriter? textWriter = null)
        where TWriter : TextWriter;

    /// <summary>
    /// Serializes an object into a <see cref="Stream"/> using XML format.
    /// </summary>
    /// <typeparam name="TStream">The type of the <see cref="Stream"/> to use.</typeparam>
    /// <param name="value">The object to serialize.</param>
    /// <param name="stream">The <typeparamref name="TStream"/> to write the XML content to.
    /// If null, <see cref="MemoryStream"/> will be used.</param>
    /// <returns>The <typeparamref name="TStream"/> containing the serialized XML data.</returns>
    TStream ToXmlStream<TStream>(object? value, TStream? stream = null)
        where TStream : Stream;

    /// <summary>
    /// Serializes an object into a file at the specified path using XML format.
    /// </summary>
    /// <param name="value">The object to serialize.</param>
    /// <param name="path">The path to the XML file.</param>
    void ToXmlFile(object? value, string path);
}
