using System.Text.RegularExpressions;

namespace Gucu112.CSharp.Automation.Helpers.Stores;

public static partial class RegexStore
{
    [GeneratedRegex(@"\s+")]
    public static partial Regex AnyWhitespaceRegex();

    [GeneratedRegex(@"\s{2,}|[^\S ]+")]
    public static partial Regex NormalizeSpaceRegex();
}
