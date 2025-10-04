using Gucu112.CSharp.Automation.Helpers.Models;
using Gucu112.CSharp.Automation.Helpers.Providers;

namespace Gucu112.CSharp.Automation.Helpers.Parsers;

/// <summary>
/// Represents a class that provides parsing settings.
/// </summary>
public static partial class ParseSettings
{
    /// <inheritdoc cref="JsonParseSettings.Json"/>
    public static JsonSettings Json
    {
        get => JsonParseSettings.Json;
        set => JsonParseSettings.Json = value;
    }

    /// <summary>
    /// Gets or sets the file system implementation used for parsing.
    /// </summary>
    internal static IFileSystem FileSystem { get; set; } = new FileSystemProvider();
}
