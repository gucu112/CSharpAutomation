using System.Text.RegularExpressions;

namespace Gucu112.CSharp.Automation.Helpers.Stores;

/// <summary>
/// Provides a collection of regular expressions for common string operations.
/// </summary>
public static partial class RegexStore
{
    /// <summary>
    /// Gets a regular expression that matches one or more whitespace characters.
    /// </summary>
    [GeneratedRegex(@"\s+")]
    public static partial Regex AnyWhitespaceRegex();

    /// <summary>
    /// Gets a regular expression that matches consecutive whitespacespaces except a single space.
    /// </summary>
    [GeneratedRegex(@"\s{2,}|[^\S ]+")]
    public static partial Regex NormalizeSpaceRegex();
}
