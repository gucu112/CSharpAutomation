using Gucu112.CSharp.Automation.Helpers.Models.Interface;

namespace Gucu112.CSharp.Automation.Helpers.Parsers;

public static partial class Parse
{
    private static IFileSystem FileSystem => ParseSettings.FileSystem;

    public static T? FromJson<T>(string content)
    {
        return JsonConvert.DeserializeObject<T>(content);
    }

    public static T? FromJson<T>(TextReader reader)
    {
        using var jsonReader = new JsonTextReader(reader);
        var serializer = new JsonSerializer();
        return serializer.Deserialize<T>(jsonReader);
    }

    public static T? FromJson<T>(Stream stream)
    {
        return FromJson<T>(new StreamReader(stream));
    }

    public static T? FromJsonFile<T>(string path)
    {
        using var fileStream = FileSystem.ReadStream(path);
        return FromJson<T>(fileStream);
    }

    public static string ToJsonString(object? value)
    {
        ArgumentNullException.ThrowIfNull(value, nameof(value));
        return JsonConvert.SerializeObject(value);
    }

    public static T ToJsonWriter<T>(object? value) where T : TextWriter, new()
    {
        ArgumentNullException.ThrowIfNull(value, nameof(value));

        var writer = new T();
        using (var jsonWriter = new JsonTextWriter(writer))
        {
            var serializer = new JsonSerializer();
            serializer.Serialize(jsonWriter, value);
        }

        return writer;
    }

    public static T ToJsonStream<T>(object? value) where T : Stream, new()
    {
        ArgumentNullException.ThrowIfNull(value, nameof(value));

        var stream = new T();
        using (var writer = new StreamWriter(stream, leaveOpen: true))
        using (var jsonWriter = new JsonTextWriter(writer))
        {
            var serializer = new JsonSerializer();
            serializer.Serialize(jsonWriter, value);
        }

        return stream;
    }
}
