namespace Gucu112.CSharp.Automation.Helpers.Extensions;

public static class StringExtensions
{
    public static string NormalizeSpace(this string text)
    {
        ArgumentNullException.ThrowIfNull(text, nameof(text));
        return RegexStore.NormalizeSpaceRegex().Replace(text, " ").Trim();
    }
}
