using System.Reflection;
using System.Text.RegularExpressions;
using Gucu112.PlaywrightXunitParallel.Models;
using Gucu112.PlaywrightXunitParallel.Models.Interface;
using Microsoft.Extensions.Configuration;

namespace Gucu112.PlaywrightXunitParallel.Fixtures;

public class SettingsFixture : ISettings
{
    private const string DefaultConfiguration = "Debug";

    private readonly IConfigurationRoot? appConfig;

    public SettingsFixture()
    {
        appConfig = new ConfigurationBuilder()
            .AddJsonFile("appConfig.json", optional: false)
            .Build();
    }

    public string Environment => ExpandEnvironment(appConfig?.GetValue<string>("Environment"));
    public string RootPath => ExpandRootPath(appConfig?.GetValue<string>("RootPath"));
    public IList<EntryPoint> EntryPoints => appConfig?.GetSection("EntryPoints").Get<List<EntryPoint>>() ?? [];
    public string BrowserName => appConfig?.GetValue<string>("BrowserName") ?? GetCurrentBrowserName();
    public int ExpectTimeout => appConfig?.GetValue<int>("ExpectTimeout") ?? 0;
    public BrowserNewContextOptions BrowserOptions => appConfig?.GetSection("BrowserOptions").Get<BrowserNewContextOptions>() ?? new();
    public BrowserTypeLaunchOptions LaunchOptions => appConfig?.GetSection("LaunchOptions").Get<BrowserTypeLaunchOptions>() ?? new();
    public string ScreenshotDir => ExpandRootPath(appConfig?.GetValue<string>("ScreenshotDir"));
    public string RecordVideoDir => ExpandRootPath(appConfig?.GetValue<string>("RecordVideoDir"));

    private static string GetCurrentConfiguration()
    {
        return typeof(SettingsFixture).Assembly
            .GetCustomAttribute<AssemblyConfigurationAttribute>()?
            .Configuration ?? DefaultConfiguration;
    }

    private static string GetCurrentBrowserName()
    {
        var pattern = "^(Chromium|Firefox|Webkit)(Debug|Release)$";
        return Regex.Replace(GetCurrentConfiguration(), pattern,
            match => match.Groups[1].Value).ToLowerInvariant();
    }

    private static string ExpandEnvironment(string? name)
    {
        return name?.Replace("#{Environment}", GetCurrentConfiguration()) ?? DefaultConfiguration;
    }

    private static string ExpandRootPath(string? path)
    {
        return path?.Replace("#{RootPath}", AppDomain.CurrentDomain.BaseDirectory) ?? @".\\";
    }
}

[CollectionDefinition(nameof(SettingsFixture))]
public class SettingsCollection : ICollectionFixture<SettingsFixture>
{
}
