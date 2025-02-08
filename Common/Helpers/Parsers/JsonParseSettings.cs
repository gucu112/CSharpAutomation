using Gucu112.CSharp.Automation.Helpers.Models;

namespace Gucu112.CSharp.Automation.Helpers.Parsers;

/// <summary>
/// Represents a class that provides JSON parsing settings.
/// </summary>
public static partial class ParseSettings
{
    private static Func<JsonSettings> jsonSettings = new(() => new());

    /// <summary>
    /// Gets or sets the JSON parsing settings.
    /// </summary>
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
