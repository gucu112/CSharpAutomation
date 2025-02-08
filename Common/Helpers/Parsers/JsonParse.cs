using Gucu112.CSharp.Automation.Helpers.Models;

namespace Gucu112.CSharp.Automation.Helpers.Parsers;

/// <inheritdoc/>
public class JsonParse : IJsonParse
{
    private static IFileSystem FileSystem => ParseSettings.FileSystem;

    /// <inheritdoc/>
    public TOutput? FromJson<TOutput>(string content, JsonSettings? settings = null)
    {
        return FromJson<TOutput>(new StringReader(content), settings);
    }

    /// <inheritdoc/>
    public TOutput? FromJson<TOutput>(TextReader textReader, JsonSettings? settings = null)
    {
        settings ??= ParseSettings.Json;
        using var jsonReader = JsonSettings.CreateReader(textReader);
        var serializer = JsonSerializer.Create(settings);
        return serializer.Deserialize<TOutput>(jsonReader);
    }

    /// <inheritdoc/>
    public TOutput? FromJson<TOutput>(Stream stream, JsonSettings? settings = null)
    {
        return FromJson<TOutput>(new StreamReader(stream), settings);
    }

    /// <inheritdoc/>
    public TOutput? FromJsonFile<TOutput>(string path, JsonSettings? settings = null)
    {
        using var fileStream = FileSystem.ReadStream(path);
        return FromJson<TOutput>(fileStream, settings);
    }

    /// <inheritdoc/>
    public string ToJsonString(object? value, JsonSettings? settings = null)
    {
        return ToJsonWriter<StringWriter>(value, settings).GetStringBuilder().ToString();
    }

    /// <inheritdoc/>
    public TWritter ToJsonWriter<TWritter>(object? value, JsonSettings? settings = null, TWritter? textWriter = null)
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

    /// <inheritdoc/>
    public TStream ToJsonStream<TStream>(object? value, JsonSettings? settings = null, TStream? stream = null)
        where TStream : Stream
    {
        stream ??= (TStream)(Stream)new MemoryStream();
        var streamWriter = new StreamWriter(stream, settings?.Encoding, leaveOpen: true);
        return (TStream)ToJsonWriter(value, settings, streamWriter).BaseStream;
    }

    /// <inheritdoc/>
    public void ToJsonFile(object? value, string path, JsonSettings? settings = null)
    {
        using var fileStream = FileSystem.WriteStream(path);
        using var memoryStream = ToJsonStream(value, settings, new MemoryStream());
        memoryStream.WriteTo(fileStream);
    }
}
