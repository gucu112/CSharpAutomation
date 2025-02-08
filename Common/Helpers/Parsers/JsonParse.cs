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
    /// Deserializes the JSON content into an object of specific type.
    /// </summary>
    /// <typeparam name="TOutput">The type of object to deserialize into.</typeparam>
    /// <param name="content">The JSON content to deserialize.</param>
    /// <param name="settings">The JSON settings to use for deserialization.</param>
    /// <returns>The deserialized object of type <typeparamref name="TOutput"/>.</returns>
    public static TOutput? FromJson<TOutput>(string content, JsonSettings? settings = null)
        where TOutput : class
    {
        return FromJson<TOutput>(new StringReader(content), settings);
    }

    /// <summary>
    /// Deserializes the JSON content from a <see cref="TextReader"/> into an object of specific type.
    /// </summary>
    /// <typeparam name="TOutput">The type of object to deserialize into.</typeparam>
    /// <param name="textReader">The <see cref="TextReader"/> containing the JSON content to deserialize.</param>
    /// <param name="settings">The JSON settings to use for deserialization.</param>
    /// <returns>The deserialized object of type <typeparamref name="TOutput"/>.</returns>
    public static TOutput? FromJson<TOutput>(TextReader textReader, JsonSettings? settings = null)
        where TOutput : class
    {
        settings ??= ParseSettings.Json;
        using var jsonReader = JsonSettings.CreateReader(textReader);
        var serializer = JsonSerializer.Create(settings);
        return serializer.Deserialize<TOutput>(jsonReader);
    }

    /// <summary>
    /// Deserializes the JSON content from a <see cref="Stream"/> into an object of specific type.
    /// </summary>
    /// <typeparam name="TOutput">The type of object to deserialize into.</typeparam>
    /// <param name="stream">The <see cref="Stream"/> containing the JSON content to deserialize.</param>
    /// <param name="settings">The JSON settings to use for deserialization.</param>
    /// <returns>The deserialized object of type <typeparamref name="TOutput"/>.</returns>
    public static TOutput? FromJson<TOutput>(Stream stream, JsonSettings? settings = null)
        where TOutput : class
    {
        return FromJson<TOutput>(new StreamReader(stream), settings);
    }

    /// <summary>
    /// Deserializes the JSON content from a file at the specified path into an object of specific type.
    /// </summary>
    /// <typeparam name="TOutput">The type of object to deserialize into.</typeparam>
    /// <param name="path">The path to the JSON file.</param>
    /// <param name="settings">The JSON settings to use for deserialization.</param>
    /// <returns>The deserialized object of type <typeparamref name="TOutput"/>.</returns>
    public static TOutput? FromJsonFile<TOutput>(string path, JsonSettings? settings = null)
        where TOutput : class
    {
        using var fileStream = FileSystem.ReadStream(path);
        return FromJson<TOutput>(fileStream, settings);
    }

    /// <summary>
    /// Serializes an object into a JSON string.
    /// </summary>
    /// <param name="value">The object to serialize.</param>
    /// <param name="settings">The JSON settings to use for serialization.</param>
    /// <returns>The JSON string representation of the object.</returns>
    public static string ToJsonString(object? value, JsonSettings? settings = null)
    {
        return ToJsonWriter<StringWriter>(value, settings).GetStringBuilder().ToString();
    }

    /// <summary>
    /// Serializes an object into a <see cref="TextWriter"/> using JSON format.
    /// </summary>
    /// <typeparam name="TWritter">The type of the <see cref="TextWriter"/> to use.</typeparam>
    /// <param name="value">The object to serialize.</param>
    /// <param name="settings">The JSON settings to use for serialization.</param>
    /// <param name="textWriter">The <see cref="TextWriter"/> to write the JSON content to.</param>
    /// <returns>The <see cref="TextWriter"/> containing the serialized JSON content.</returns>
    public static TWritter ToJsonWriter<TWritter>(object? value, JsonSettings? settings = null, TWritter? textWriter = null)
        where TWritter : TextWriter
    {
        settings ??= ParseSettings.Json;
        ArgumentNullException.ThrowIfNull(value, nameof(value));

        textWriter ??= (TWritter)(TextWriter)new StringWriter(settings.Encoding);
        using (var jsonWriter = settings.CreateWriter(textWriter))
        {
            var serializer = JsonSerializer.Create(settings);
            serializer.Serialize(jsonWriter, value);
        }

        return textWriter;
    }

    /// <summary>
    /// Serializes an object into a <see cref="Stream"/> using JSON format.
    /// </summary>
    /// <typeparam name="TStream">The type of the <see cref="Stream"/> to use.</typeparam>
    /// <param name="value">The object to serialize.</param>
    /// <param name="settings">The JSON settings to use for serialization.</param>
    /// <param name="stream">The <see cref="Stream"/> to write the JSON content to.</param>
    /// <returns>The <see cref="Stream"/> containing the serialized JSON content.</returns>
    public static TStream ToJsonStream<TStream>(object? value, JsonSettings? settings = null, TStream? stream = null)
        where TStream : Stream
    {
        stream ??= (TStream)(Stream)new MemoryStream();
        var streamWriter = new StreamWriter(stream, settings?.Encoding, leaveOpen: true);
        return (TStream)ToJsonWriter(value, settings, streamWriter).BaseStream;
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
