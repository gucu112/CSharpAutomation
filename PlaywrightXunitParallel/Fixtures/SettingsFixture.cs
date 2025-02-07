using System.Reflection;
using System.Text.RegularExpressions;
using Gucu112.CSharp.Automation.PlaywrightXunitParallel.Models;
using Gucu112.CSharp.Automation.PlaywrightXunitParallel.Models.Interface;
using Microsoft.Extensions.Configuration;

namespace Gucu112.CSharp.Automation.PlaywrightXunitParallel.Fixtures;

/// <summary>
/// Represents a controller that provides configuration settings.
/// </summary>
public class SettingsFixture : ISettings
{
    private const string ConfigurationPattern = "^(Chromium|Firefox|Webkit)?(Debug|Release)$";

    private const string DefaultConfiguration = "Debug";

    /// <summary>
    /// Represents the configuration object.
    /// </summary>
    private readonly IConfigurationRoot? appConfig;

    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsFixture"/> class.
    /// Reads and builds the configuration object from the appConfig.json file.
    /// </summary>
    public SettingsFixture()
    {
        appConfig = new ConfigurationBuilder()
            .AddJsonFile("appConfig.json", optional: true)
            .Build();
    }

    /// <inheritdoc/>
    public string Environment => ExpandEnvironment(appConfig?.GetValue<string>("Environment"));

    /// <inheritdoc/>
    public string RootPath => ExpandRootPath(appConfig?.GetValue<string>("RootPath"));

    /// <inheritdoc/>
    public IList<EntryPoint> EntryPoints => appConfig?.GetSection("EntryPoints").Get<List<EntryPoint>>() ?? [];

    /// <inheritdoc/>
    public string BrowserName => appConfig?.GetValue<string>("BrowserName") ?? GetCurrentBrowserName();

    /// <inheritdoc/>
    public float ExpectTimeout => appConfig?.GetValue<float>("ExpectTimeout") ?? 0.0f;

    /// <inheritdoc/>
    public BrowserNewContextOptions BrowserOptions => appConfig?.GetSection("BrowserOptions").Get<BrowserNewContextOptions>() ?? new();

    /// <inheritdoc/>
    public BrowserTypeLaunchOptions LaunchOptions => appConfig?.GetSection("LaunchOptions").Get<BrowserTypeLaunchOptions>() ?? new();

    /// <inheritdoc/>
    public bool IsVideoEnabled => RecordVideoDir is not null;

    /// <inheritdoc/>
    public string? RecordVideoDir => BrowserOptions.RecordVideoDir;

    private static string GetCurrentConfiguration()
    {
        return typeof(SettingsFixture).Assembly
            .GetCustomAttribute<AssemblyConfigurationAttribute>()?
            .Configuration ?? DefaultConfiguration;
    }

    private static string GetCurrentBrowserName()
    {
        return Regex.Replace(
            GetCurrentConfiguration(),
            ConfigurationPattern,
            match => match.Groups[1].Value).ToLower();
    }

    private static string ExpandEnvironment(string? name)
    {
        return name?.Replace("{{Environment}}", GetCurrentConfiguration()) ?? DefaultConfiguration;
    }

    private static string ExpandRootPath(string? path)
    {
        return path?.Replace("{{RootPath}}", AppDomain.CurrentDomain.BaseDirectory) ?? ".";
    }
}

/// <summary>
/// Represents a collection definition for the SettingsFixture.
/// </summary>
[CollectionDefinition(nameof(SettingsFixture))]
public class SettingsCollection : ICollectionFixture<SettingsFixture>
{
}
