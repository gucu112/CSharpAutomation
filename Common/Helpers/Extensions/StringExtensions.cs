using Gucu112.CSharp.Automation.Helpers.Stores;

namespace Gucu112.CSharp.Automation.Helpers.Extensions;

public static class StringExtensions
{
    public static byte[] GetBytes(this string text, Encoding? encoding = null)
    {
        ArgumentNullException.ThrowIfNull(text, nameof(text));
        encoding ??= Encoding.UTF8;
        return encoding.GetBytes(text);
    }

    public static string NormalizeSpace(this string text)
    {
        ArgumentNullException.ThrowIfNull(text, nameof(text));
        return RegexStore.NormalizeSpaceRegex().Replace(text, " ").Trim();
    }

    public static string RemoveSpace(this string text)
    {
        ArgumentNullException.ThrowIfNull(text, nameof(text));
        return RegexStore.AnyWhitespaceRegex().Replace(text, string.Empty);
    }
}
