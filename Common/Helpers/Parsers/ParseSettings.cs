using Gucu112.CSharp.Automation.Helpers.Models.Interface;
using Gucu112.CSharp.Automation.Helpers.Wrappers;

namespace Gucu112.CSharp.Automation.Helpers.Parsers;

/// <summary>
/// Represents a class that provides parsing settings.
/// </summary>
public static partial class ParseSettings
{
    /// <summary>
    /// Gets or sets the file system implementation used for parsing.
    /// </summary>
    internal static IFileSystem FileSystem { get; set; } = new FileSystemWrapper();
}
