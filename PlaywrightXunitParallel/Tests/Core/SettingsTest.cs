using Gucu112.PlaywrightXunitParallel.Fixtures;
using Gucu112.PlaywrightXunitParallel.Models;
using Gucu112.PlaywrightXunitParallel.Models.Enum;

namespace Gucu112.PlaywrightXunitParallel.Tests.Core;

[Collection(nameof(SettingsFixture))]
[Trait(TestTrait.Category, nameof(TestCategory.Settings))]
[Trait(TestTrait.Priority, nameof(TestPriority.Medium))]
public class SettingsTest
{
    private readonly SettingsFixture settings;

    public SettingsTest(SettingsFixture fixture)
    {
        settings = fixture;
    }

    [Fact]
    public void VerifyThatEnvironmentIsLoadedCorrectly()
    {
        Assert.EndsWith("Debug", settings.Environment);
    }

    [Fact]
    public void VerifyThatRootPathIsLoadedCorrectly()
    {
        Assert.Contains(AppDomain.CurrentDomain.BaseDirectory, settings.RootPath);
    }

    [Fact]
    public void VerifyThatEntryPointsAreLoadedCorrectly()
    {
        Assert.True(3 <= settings.EntryPoints.Count,
            "EntryPoints list does not have at least 3 elements");
    }

    [Fact]
    public void VerifyThatPlaywrightBrowserNameIsLoadedCorrectly()
    {
        Assert.Equal("chromium", settings.BrowserName);
    }

    [Fact]
    public void VerifyThatPlaywrightExpectTimeoutIsLoadedCorrectly()
    {
        Assert.Equal(5000, settings.ExpectTimeout);
    }

    [Fact]
    public void VerifyThatPlaywrightBrowserOptionsAreLoadedCorrectly()
    {
        var options = settings.BrowserOptions;

        Assert.Equal("pl-PL", options.Locale);
        Assert.Equal("Europe/Warsaw", options.TimezoneId);
        Assert.Equal(52.2316742f, options.Geolocation?.Latitude);
        Assert.Equal(21.0059872f, options.Geolocation?.Longitude);
        Assert.False(options.Offline);
        Assert.Null(options.Proxy);
    }

    [Fact]
    public void VerifyThatPlaywrightLaunchOptionsAreLoadedCorrectly()
    {
        var options = settings.LaunchOptions;

        Assert.Equal("msedge", options.Channel);
        Assert.False(options.Headless, "Headless mode is enabled");
        Assert.Equal(500, options.SlowMo);
        Assert.Equal(15000, options.Timeout);
        Assert.Null(options.Proxy);
    }

    [Fact]
    public void VerifyThatScreenshotDirectoryPathIsLoadedCorrectly()
    {
        Assert.EndsWith("screenshots\\", settings.ScreenshotDir,
            StringComparison.InvariantCultureIgnoreCase);
    }

    [Fact]
    public void VerifyThatRecordVideoDirectoryPathIsLoadedCorrectly()
    {
        Assert.EndsWith("videos\\", settings.RecordVideoDir,
            StringComparison.InvariantCultureIgnoreCase);
    }
}
