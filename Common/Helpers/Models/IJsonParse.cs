namespace Gucu112.CSharp.Automation.Helpers.Models;

/// <summary>
/// Provides methods for parsing JSON data.
/// </summary>
public interface IJsonParse
{
    /// <summary>
    /// Deserializes the JSON content into an object of type T.
    /// </summary>
    /// <typeparam name="T">The type of the object to deserialize.</typeparam>
    /// <param name="content">The JSON content to deserialize.</param>
    /// <param name="settings">The JSON settings to use for deserialization.</param>
    /// <returns>The deserialized object of type T.</returns>
    T? FromJson<T>(string content, JsonSettings? settings = null);

    /// <summary>
    /// Deserializes the JSON content from a TextReader into an object of type T.
    /// </summary>
    /// <typeparam name="T">The type of the object to deserialize.</typeparam>
    /// <param name="textReader">The TextReader containing the JSON content to deserialize.</param>
    /// <param name="settings">The JSON settings to use for deserialization.</param>
    /// <returns>The deserialized object of type T.</returns>
    T? FromJson<T>(TextReader textReader, JsonSettings? settings = null);

    /// <summary>
    /// Deserializes the JSON content from a Stream into an object of type T.
    /// </summary>
    /// <typeparam name="T">The type of the object to deserialize.</typeparam>
    /// <param name="stream">The Stream containing the JSON content to deserialize.</param>
    /// <param name="settings">The JSON settings to use for deserialization.</param>
    /// <returns>The deserialized object of type T.</returns>
    T? FromJson<T>(Stream stream, JsonSettings? settings = null);

    /// <summary>
    /// Deserializes the JSON content from a file at the specified path into an object of type T.
    /// </summary>
    /// <typeparam name="T">The type of the object to deserialize.</typeparam>
    /// <param name="path">The path to the JSON file.</param>
    /// <param name="settings">The JSON settings to use for deserialization.</param>
    /// <returns>The deserialized object of type T.</returns>
    T? FromJsonFile<T>(string path, JsonSettings? settings = null);

    /// <summary>
    /// Serializes an object into a JSON string.
    /// </summary>
    /// <param name="value">The object to serialize.</param>
    /// <param name="settings">The JSON settings to use for serialization.</param>
    /// <returns>The JSON string representation of the object.</returns>
    string ToJsonString(object? value, JsonSettings? settings = null);

    /// <summary>
    /// Serializes an object into a TextWriter using JSON format.
    /// </summary>
    /// <typeparam name="T">The type of the TextWriter to use.</typeparam>
    /// <param name="value">The object to serialize.</param>
    /// <param name="settings">The JSON settings to use for serialization.</param>
    /// <param name="textWriter">The TextWriter to write the JSON content to.</param>
    /// <returns>The TextWriter containing the serialized JSON content.</returns>
    T ToJsonWriter<T>(object? value, JsonSettings? settings = null, T? textWriter = null)
        where T : TextWriter;

    /// <summary>
    /// Serializes an object into a Stream using JSON format.
    /// </summary>
    /// <typeparam name="T">The type of the Stream to use.</typeparam>
    /// <param name="value">The object to serialize.</param>
    /// <param name="settings">The JSON settings to use for serialization.</param>
    /// <param name="stream">The Stream to write the JSON content to.</param>
    /// <returns>The Stream containing the serialized JSON content.</returns>
    T ToJsonStream<T>(object? value, JsonSettings? settings = null, T? stream = null)
        where T : Stream;

    /// <summary>
    /// Serializes an object into a file at the specified path using JSON format.
    /// </summary>
    /// <param name="value">The object to serialize.</param>
    /// <param name="path">The path to the JSON file.</param>
    /// <param name="settings">The JSON settings to use for serialization.</param>
    void ToJsonFile(object? value, string path, JsonSettings? settings = null);
}
