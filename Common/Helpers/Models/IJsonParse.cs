namespace Gucu112.CSharp.Automation.Helpers.Models;

/// <summary>
/// Provides methods for parsing JSON data.
/// </summary>
public interface IJsonParse
{
    /// <summary>
    /// Deserializes the JSON content into an object of specific type.
    /// </summary>
    /// <typeparam name="TOutput">The type of object to deserialize into.</typeparam>
    /// <param name="content">The JSON content to deserialize.</param>
    /// <param name="settings">The JSON settings to use for deserialization.</param>
    /// <returns>The deserialized object of type <typeparamref name="TOutput"/>.</returns>
    TOutput? FromJson<TOutput>(string content, JsonSettings? settings = null);

    /// <summary>
    /// Deserializes the JSON content from a <see cref="TextReader"/> into an object of specific type.
    /// </summary>
    /// <typeparam name="TOutput">The type of object to deserialize into.</typeparam>
    /// <param name="textReader">The <see cref="TextReader"/> containing the JSON content to deserialize.</param>
    /// <param name="settings">The JSON settings to use for deserialization.</param>
    /// <returns>The deserialized object of type <typeparamref name="TOutput"/>.</returns>
    TOutput? FromJson<TOutput>(TextReader textReader, JsonSettings? settings = null);

    /// <summary>
    /// Deserializes the JSON content from a <see cref="Stream"/> into an object of specific type.
    /// </summary>
    /// <typeparam name="TOutput">The type of object to deserialize into.</typeparam>
    /// <param name="stream">The <see cref="Stream"/> containing the JSON content to deserialize.</param>
    /// <param name="settings">The JSON settings to use for deserialization.</param>
    /// <returns>The deserialized object of type <typeparamref name="TOutput"/>.</returns>
    TOutput? FromJson<TOutput>(Stream stream, JsonSettings? settings = null);

    /// <summary>
    /// Deserializes the JSON content from a file at the specified path into an object of specific type.
    /// </summary>
    /// <typeparam name="TOutput">The type of object to deserialize into.</typeparam>
    /// <param name="path">The path to the JSON file.</param>
    /// <param name="settings">The JSON settings to use for deserialization.</param>
    /// <returns>The deserialized object of type <typeparamref name="TOutput"/>.</returns>
    TOutput? FromJsonFile<TOutput>(string path, JsonSettings? settings = null);

    /// <summary>
    /// Serializes an object into a JSON string.
    /// </summary>
    /// <param name="value">The object to serialize.</param>
    /// <param name="settings">The JSON settings to use for serialization.</param>
    /// <returns>The JSON string representation of the object.</returns>
    string ToJsonString(object? value, JsonSettings? settings = null);

    /// <summary>
    /// Serializes an object into a <see cref="TextWriter"/> using JSON format.
    /// </summary>
    /// <typeparam name="TWriter">The type of the <see cref="TextWriter"/> to use.</typeparam>
    /// <param name="value">The object to serialize.</param>
    /// <param name="settings">The JSON settings to use for serialization.</param>
    /// <param name="textWriter">The <typeparamref name="TWriter"/> to write the JSON content to.
    /// If null, <see cref="StringWriter"/> will be used.</param>
    /// <returns>The <typeparamref name="TWriter"/> containing the serialized JSON string.</returns>
    TWriter ToJsonWriter<TWriter>(object? value, JsonSettings? settings = null, TWriter? textWriter = null)
        where TWriter : TextWriter;

    /// <summary>
    /// Serializes an object into a <see cref="Stream"/> using JSON format.
    /// </summary>
    /// <typeparam name="TStream">The type of the <see cref="Stream"/> to use.</typeparam>
    /// <param name="value">The object to serialize.</param>
    /// <param name="settings">The JSON settings to use for serialization.</param>
    /// <param name="stream">The <typeparamref name="TStream"/> to write the JSON content to.
    /// If null, <see cref="MemoryStream"/> will be used.</param>
    /// <returns>The <typeparamref name="TStream"/> containing the serialized JSON content.</returns>
    TStream ToJsonStream<TStream>(object? value, JsonSettings? settings = null, TStream? stream = null)
        where TStream : Stream;

    /// <summary>
    /// Serializes an object into a file at the specified path using JSON format.
    /// </summary>
    /// <param name="value">The object to serialize.</param>
    /// <param name="path">The path to the JSON file.</param>
    /// <param name="settings">The JSON settings to use for serialization.</param>
    void ToJsonFile(object? value, string path, JsonSettings? settings = null);
}
