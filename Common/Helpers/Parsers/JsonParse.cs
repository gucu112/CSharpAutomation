using Gucu112.CSharp.Automation.Helpers.Models;
using Gucu112.CSharp.Automation.Helpers.Models.Interface;

namespace Gucu112.CSharp.Automation.Helpers.Parsers;

/// <summary>
/// Provides methods for parsing JSON data.
/// </summary>
public static partial class Parse
{
    private static IFileSystem FileSystem => ParseSettings.FileSystem;

    /// <summary>
    /// Deserializes the JSON content into an object of type T.
    /// </summary>
    /// <typeparam name="T">The type of the object to deserialize.</typeparam>
    /// <param name="content">The JSON content to deserialize.</param>
    /// <param name="settings">The JSON settings to use for deserialization.</param>
    /// <returns>The deserialized object of type T.</returns>
    public static T? FromJson<T>(string content, JsonSettings? settings = null)
    {
        return FromJson<T>(new StringReader(content), settings);
    }

    /// <summary>
    /// Deserializes the JSON content from a TextReader into an object of type T.
    /// </summary>
    /// <typeparam name="T">The type of the object to deserialize.</typeparam>
    /// <param name="textReader">The TextReader containing the JSON content to deserialize.</param>
    /// <param name="settings">The JSON settings to use for deserialization.</param>
    /// <returns>The deserialized object of type T.</returns>
    public static T? FromJson<T>(TextReader textReader, JsonSettings? settings = null)
    {
        settings ??= ParseSettings.Json;
        using var jsonReader = JsonSettings.CreateReader(textReader);
        var serializer = JsonSerializer.Create(settings);
        return serializer.Deserialize<T>(jsonReader);
    }

    /// <summary>
    /// Deserializes the JSON content from a Stream into an object of type T.
    /// </summary>
    /// <typeparam name="T">The type of the object to deserialize.</typeparam>
    /// <param name="stream">The Stream containing the JSON content to deserialize.</param>
    /// <param name="settings">The JSON settings to use for deserialization.</param>
    /// <returns>The deserialized object of type T.</returns>
    public static T? FromJson<T>(Stream stream, JsonSettings? settings = null)
    {
        return FromJson<T>(new StreamReader(stream), settings);
    }

    /// <summary>
    /// Deserializes the JSON content from a file at the specified path into an object of type T.
    /// </summary>
    /// <typeparam name="T">The type of the object to deserialize.</typeparam>
    /// <param name="path">The path to the JSON file.</param>
    /// <param name="settings">The JSON settings to use for deserialization.</param>
    /// <returns>The deserialized object of type T.</returns>
    public static T? FromJsonFile<T>(string path, JsonSettings? settings = null)
    {
        using var fileStream = FileSystem.ReadStream(path);
        return FromJson<T>(fileStream, settings);
    }

    /// <summary>
    /// Serializes an object into a JSON string.
    /// </summary>
    /// <param name="value">The object to serialize.</param>
    /// <param name="settings">The JSON settings to use for serialization.</param>
    /// <returns>The JSON string representation of the object.</returns>
    public static string ToJsonString(object? value, JsonSettings? settings = null)
    {
        ArgumentNullException.ThrowIfNull(value, nameof(value));
        return ToJsonWriter<StringWriter>(value, settings).GetStringBuilder().ToString();
    }

    /// <summary>
    /// Serializes an object into a TextWriter using JSON format.
    /// </summary>
    /// <typeparam name="T">The type of the TextWriter to use.</typeparam>
    /// <param name="value">The object to serialize.</param>
    /// <param name="settings">The JSON settings to use for serialization.</param>
    /// <param name="textWriter">The TextWriter to write the JSON content to.</param>
    /// <returns>The TextWriter containing the serialized JSON content.</returns>
    public static T ToJsonWriter<T>(object? value, JsonSettings? settings = null, T? textWriter = null)
        where T : TextWriter
    {
        settings ??= ParseSettings.Json;
        ArgumentNullException.ThrowIfNull(value, nameof(value));

        textWriter ??= (T)(TextWriter)new StringWriter(settings.Encoding);
        using (var jsonWriter = settings.CreateWriter(textWriter))
        {
            var serializer = JsonSerializer.Create(settings);
            serializer.Serialize(jsonWriter, value);
        }

        return textWriter;
    }

    /// <summary>
    /// Serializes an object into a Stream using JSON format.
    /// </summary>
    /// <typeparam name="T">The type of the Stream to use.</typeparam>
    /// <param name="value">The object to serialize.</param>
    /// <param name="settings">The JSON settings to use for serialization.</param>
    /// <param name="stream">The Stream to write the JSON content to.</param>
    /// <returns>The Stream containing the serialized JSON content.</returns>
    public static T ToJsonStream<T>(object? value, JsonSettings? settings = null, T? stream = null)
        where T : Stream
    {
        stream ??= (T)(Stream)new MemoryStream();
        var streamWriter = new StreamWriter(stream, settings?.Encoding, leaveOpen: true);
        return (T)ToJsonWriter(value, settings, streamWriter).BaseStream;
    }

    /// <summary>
    /// Serializes an object into a file at the specified path using JSON format.
    /// </summary>
    /// <param name="value">The object to serialize.</param>
    /// <param name="path">The path to the JSON file.</param>
    /// <param name="settings">The JSON settings to use for serialization.</param>
    public static void ToJsonFile(object? value, string path, JsonSettings? settings = null)
    {
        using var fileStream = FileSystem.WriteStream(path);
        using var memoryStream = ToJsonStream(value, settings, new MemoryStream());
        memoryStream.WriteTo(fileStream);
    }
}
