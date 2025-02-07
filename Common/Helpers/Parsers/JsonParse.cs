using Gucu112.CSharp.Automation.Helpers.Models;
using Gucu112.CSharp.Automation.Helpers.Models.Interface;

namespace Gucu112.CSharp.Automation.Helpers.Parsers;

public static partial class Parse
{
    private static IFileSystem FileSystem => ParseSettings.FileSystem;

    public static T? FromJson<T>(string content, JsonSettings? settings = null)
    {
        return FromJson<T>(new StringReader(content), settings);
    }

    public static T? FromJson<T>(TextReader textReader, JsonSettings? settings = null)
    {
        settings ??= ParseSettings.Json;
        using var jsonReader = JsonSettings.CreateReader(textReader);
        var serializer = JsonSerializer.Create(settings);
        return serializer.Deserialize<T>(jsonReader);
    }

    public static T? FromJson<T>(Stream stream, JsonSettings? settings = null)
    {
        return FromJson<T>(new StreamReader(stream), settings);
    }

    public static T? FromJsonFile<T>(string path, JsonSettings? settings = null)
    {
        using var fileStream = FileSystem.ReadStream(path);
        return FromJson<T>(fileStream, settings);
    }

    public static string ToJsonString(object? value, JsonSettings? settings = null)
    {
        ArgumentNullException.ThrowIfNull(value, nameof(value));
        return ToJsonWriter<StringWriter>(value, settings).GetStringBuilder().ToString();
    }

    public static T ToJsonWriter<T>(object? value, JsonSettings? settings = null, T? textWriter = null) where T : TextWriter
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

    public static T ToJsonStream<T>(object? value, JsonSettings? settings = null, T? stream = null) where T : Stream
    {
        stream ??= (T)(Stream)new MemoryStream();
        var streamWriter = new StreamWriter(stream, settings?.Encoding, leaveOpen: true);
        return (T)ToJsonWriter(value, settings, streamWriter).BaseStream;
    }

    public static void ToJsonFile(object? value, string path, JsonSettings? settings = null)
    {
        using var fileStream = FileSystem.WriteStream(path);
        using var memoryStream = ToJsonStream(value, settings, new MemoryStream());
        memoryStream.WriteTo(fileStream);
    }
}
