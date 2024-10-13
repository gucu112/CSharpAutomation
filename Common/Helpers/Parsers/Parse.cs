using Newtonsoft.Json;

namespace Gucu112.CSharp.Automation.Helpers.Parsers;

public static class Parse
{
    #region Json

    public static T? FromJson<T>(string content)
    {
        return JsonConvert.DeserializeObject<T>(content);
    }

    public static T? FromJson<T>(TextReader input)
    {
        var serializer = new JsonSerializer();
        using var reader = new JsonTextReader(input);
        return serializer.Deserialize<T>(reader);
    }

    #endregion
}
