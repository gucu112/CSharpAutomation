namespace Gucu112.CSharp.Automation.PlaywrightXunitParallel.Models.Interface;

/// <summary>
/// Represents the configuration settings.
/// </summary>
public interface ISettings
{
    /// <summary>
    /// Gets the current environment name.
    /// </summary>
    string Environment { get; }

    /// <summary>
    /// Gets the current root directory path.
    /// </summary>
    string RootPath { get; }

    /// <summary>
    /// Gets the list of entry points.
    /// </summary>
    IList<EntryPoint> EntryPoints { get; }

    /// <summary>
    /// Gets the current browser name.
    /// </summary>
    string BrowserName { get; }

    /// <summary>
    /// Gets the expect timeout in milliseconds.
    /// </summary>
    float ExpectTimeout { get; }

    /// <summary>
    /// Gets the browser options.
    /// </summary>
    BrowserNewContextOptions BrowserOptions { get; }

    /// <summary>
    /// Gets the launch options.
    /// </summary>
    BrowserTypeLaunchOptions LaunchOptions { get; }

    /// <summary>
    /// Gets a value indicating whether video recording is enabled.
    /// </summary>
    bool IsVideoEnabled { get; }

    /// <summary>
    /// Gets the video recording directory path.
    /// </summary>
    string? RecordVideoDir { get; }
}
