using Gucu112.CSharp.Automation.Helpers.Models.Interface;

namespace Gucu112.CSharp.Automation.Helpers.Parsers;

public static partial class Parse
{
    private static IFileSystem FileSystem => ParseSettings.FileSystem;

    public static T? FromJson<T>(string content)
    {
        return FromJson<T>(new StringReader(content));
    }

    public static T? FromJson<T>(TextReader reader)
    {
        var serializer = new JsonSerializer();
        using var jsonReader = new JsonTextReader(reader);
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
}
