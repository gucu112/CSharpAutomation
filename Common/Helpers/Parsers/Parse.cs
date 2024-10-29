namespace Gucu112.CSharp.Automation.Helpers.Parsers;

public static class Parse
{
    #region Json

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

    #endregion
}
