﻿using FluentAssertions;
using Gucu112.PlaywrightXunitParallel.Fixtures;
using Gucu112.PlaywrightXunitParallel.Models.Attribute;
using Gucu112.PlaywrightXunitParallel.Models.Enum;
using Gucu112.PlaywrightXunitParallel.Orderers.TestCase;

namespace Gucu112.PlaywrightXunitParallel.Tests.Core;

[Collection(nameof(SettingsFixture))]
[TestCaseOrderer(NameOrderer.FullTypeName, NameOrderer.AssemblyName)]

[Category(TestCategory.Settings)]
[Priority(TestPriority.Medium)]
public class SettingsTest(SettingsFixture settings)
{
    [Fact]
    public void VerifyThatEnvironmentIsLoadedCorrectly()
    {
        settings.Environment.Should().EndWith("Debug");
    }

    [Fact]
    public void VerifyThatRootPathIsLoadedCorrectly()
    {
        settings.RootPath.Should().Contain(AppDomain.CurrentDomain.BaseDirectory);
    }

    [Fact]
    public void VerifyThatEntryPointsAreLoadedCorrectly()
    {
        settings.EntryPoints.Should().HaveCountGreaterThanOrEqualTo(3);
    }

    [Fact]
    public void VerifyThatPlaywrightBrowserNameIsLoadedCorrectly()
    {
        settings.BrowserName.Should().Be("chromium");
    }

    [Fact]
    public void VerifyThatPlaywrightExpectTimeoutIsLoadedCorrectly()
    {
        settings.ExpectTimeout.Should().Be(5000);
    }

    [Fact]
    public void VerifyThatPlaywrightBrowserOptionsAreLoadedCorrectly()
    {
        var options = settings.BrowserOptions;

        options.Locale.Should().Be("pl-PL");
        options.TimezoneId.Should().Be("Europe/Warsaw");
        options.Geolocation.Should().BeEquivalentTo(new Geolocation()
        {
            Latitude = 52.2316742f,
            Longitude = 21.0059872f
        });
        options.Offline.Should().BeFalse();
        options.Proxy.Should().BeNull();
    }

    [Fact]
    public void VerifyThatPlaywrightLaunchOptionsAreLoadedCorrectly()
    {
        var options = settings.LaunchOptions;

        options.Channel.Should().Be("msedge");
        options.Headless.Should().BeFalse();
        options.SlowMo.Should().BeLessThanOrEqualTo(1000);
        options.Timeout.Should().BeGreaterThanOrEqualTo(10000);
        options.Proxy.Should().BeNull();
    }

    [Fact]
    public void VerifyThatScreenshotDirectoryPathIsLoadedCorrectly()
    {
        settings.ScreenshotDir.Should().EndWithEquivalentOf("screenshots\\");
    }

    [Fact]
    public void VerifyThatRecordVideoDirectoryPathIsLoadedCorrectly()
    {
        settings.RecordVideoDir.Should().EndWithEquivalentOf("videos\\");
    }
}
