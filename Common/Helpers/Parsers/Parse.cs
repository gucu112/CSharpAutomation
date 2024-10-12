using Newtonsoft.Json;

namespace Gucu112.CSharp.Automation.Helpers.Parsers;

public static class Parse
{
    #region Json

    public static T? FromJson<T>(string content)
    {
        return JsonConvert.DeserializeObject<T>(content);
    }

    #endregion
}
