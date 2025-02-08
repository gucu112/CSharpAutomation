using Gucu112.CSharp.Automation.Helpers.Models;

namespace Gucu112.CSharp.Automation.Helpers.Parsers;

/// <summary>
/// Represents a class that provides JSON parsing settings.
/// </summary>
public static class JsonParseSettings
{
    private static Func<JsonSettings> jsonSettingsResolver = new(() => new());

    /// <summary>
    /// Gets or sets the JSON parsing settings.
    /// </summary>
    public static JsonSettings Json
    {
        get => jsonSettingsResolver();
        set
        {
            jsonSettingsResolver = () => value;
            JsonConvert.DefaultSettings = jsonSettingsResolver;
        }
    }
}
