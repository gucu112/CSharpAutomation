namespace Gucu112.CSharp.Automation.Helpers.Extensions;

/// <summary>
/// Provides extension methods for boolean values.
/// </summary>
public static class BoolExtensions
{
    /// <summary>
    /// Converts a boolean value to a localized string representation.
    /// </summary>
    /// <param name="value">The boolean value.</param>
    /// <returns>The localized string representation of the boolean value.</returns>
    public static string ToLocalizedString(this bool value)
    {
        return value switch
        {
            true => "Yes",
            false => "No",
        };
    }

    /// <summary>
    /// Converts a nullable boolean value to a localized string representation.
    /// </summary>
    /// <param name="value">The nullable boolean value.</param>
    /// <returns>The localized string representation of the boolean value.</returns>
    public static string? ToLocalizedString(this bool? value)
    {
        return value.HasValue ? ToLocalizedString(value.Value) : null;
    }
}
