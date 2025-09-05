using Gucu112.CSharp.Automation.Helpers.Models;

namespace Gucu112.CSharp.Automation.Helpers.Parsers;

/// <summary>
/// Represents a class that provides XML reader parsing settings.
/// </summary>
public static partial class ParseSettings
{
    private static Func<XmlReaderSettings> xmlReaderSettingsResolver = new(() => new());

    /// <summary>
    /// Gets or sets the XML reader parsing settings.
    /// </summary>
    public static XmlReaderSettings XmlRead
    {
        get => xmlReaderSettingsResolver();
        set => xmlReaderSettingsResolver = () => value;
    }
}
