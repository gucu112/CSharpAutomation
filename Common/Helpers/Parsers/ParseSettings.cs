using Gucu112.CSharp.Automation.Helpers.Models.Interface;
using Gucu112.CSharp.Automation.Helpers.Wrappers;

namespace Gucu112.CSharp.Automation.Helpers.Parsers;

public static partial class ParseSettings
{
    internal static IFileSystem FileSystem { get; set; } = new FileSystemWrapper();
}
