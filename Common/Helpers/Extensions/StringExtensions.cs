using Gucu112.CSharp.Automation.Helpers.Providers;

namespace Gucu112.CSharp.Automation.Helpers.Extensions;

/// <summary>
/// Provides extension methods for string manipulation.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Converts the string to a byte array using the specified encoding.
    /// If no encoding is provided, <see cref="Encoding.UTF8"/> is used by default.
    /// </summary>
    /// <param name="text">The string to convert.</param>
    /// <param name="encoding">The encoding to use (optional).</param>
    /// <returns>The byte array representation of the string.</returns>
    public static byte[] GetBytes(this string text, Encoding? encoding = null)
    {
        ArgumentNullException.ThrowIfNull(text, nameof(text));
        encoding ??= Encoding.UTF8;
        return encoding.GetBytes(text);
    }

    /// <summary>
    /// Normalizes the whitespaces in the string by replacing consecutive whitespace characters with a single space.
    /// </summary>
    /// <param name="text">The string to normalize whitespaces.</param>
    /// <returns>The normalized string.</returns>
    public static string NormalizeSpace(this string text)
    {
        ArgumentNullException.ThrowIfNull(text, nameof(text));
        return RegexProvider.NormalizeSpaceRegex().Replace(text, " ").Trim();
    }

    /// <summary>
    /// Removes all whitespace characters from the string.
    /// </summary>
    /// <param name="text">The string to remove whitespaces from.</param>
    /// <returns>The string without whitespaces.</returns>
    public static string RemoveSpace(this string text)
    {
        ArgumentNullException.ThrowIfNull(text, nameof(text));
        return RegexProvider.AnyWhitespaceRegex().Replace(text, string.Empty);
    }
}
