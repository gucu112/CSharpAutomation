using Gucu112.CSharp.Automation.Helpers.Models;

namespace Gucu112.CSharp.Automation.Helpers.Parsers;

public static partial class ParseSettings
{
    private static Func<JsonSettings> jsonSettings = new(() => new());

    public static JsonSettings Json
    {
        get => jsonSettings();
        set
        {
            jsonSettings = () => value;
            JsonConvert.DefaultSettings = jsonSettings;
        }
    }
}
