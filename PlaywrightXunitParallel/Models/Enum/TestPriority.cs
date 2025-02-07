namespace Gucu112.CSharp.Automation.PlaywrightXunitParallel.Models.Enum;

/// <summary>
/// Represents the test priorities.
/// </summary>
public enum TestPriority
{
    /// <summary>
    /// Covers core, critical functionality of the application.
    /// </summary>
    Critical,

    /// <summary>
    /// Covers important functionality of the application.
    /// </summary>
    High,

    /// <summary>
    /// Covers standard functionality of the application.
    /// </summary>
    Medium,

    /// <summary>
    /// Covers non-essential functionality or enhancements of the application.
    /// </summary>
    Low,

    /// <summary>
    /// Covers insignificant and cosmetic functionality of the application.
    /// </summary>
    Trivial,
}
