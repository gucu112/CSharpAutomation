using System.Xml.Linq;
using Gucu112.CSharp.Automation.PlaywrightXunit.Fixtures;

namespace Gucu112.CSharp.Automation.PlaywrightXunit.Tests;

[Trait("Category", "Settings")]
public class TestSettings
{
    [Fact]
    public void VerifyThatXunitRunsWithoutParallelExecution()
    {
        // Arrange
        var rootElement = XDocument.Load(".runsettings").Root ?? new XElement("RunSettings");

        // Act
        var maxCpuCountElement = rootElement.Elements()
            .First(element => element.Name == "RunConfiguration").Elements()
            .First(element => element.Name == "MaxCpuCount");
        var parallelExecutionElement = rootElement.Elements()
            .First(element => element.Name == "xUnit").Elements()
            .First(element => element.Name == "ParallelizeTestCollections");

        // Assert
        var runConfigurationCpuCount = Convert.ToInt32(maxCpuCountElement.Value);
        var isParallelExecutionEnabled = Convert.ToBoolean(parallelExecutionElement.Value);
        Assert.Equal(1, runConfigurationCpuCount);
        Assert.False(isParallelExecutionEnabled, "Xunit runs with parallel execution enabled");
    }

    [Fact]
    public void VerifyThatEnvironmentVariablesAreLoadedCorrectly()
    {
        // Arrange
        ISet<string> expectedVariables = new HashSet<string>
        {
            "Path",
            "ProgramFiles",
            "ProgramData",
            "SystemDrive",
            "SystemRoot",
        };

        // Act
        var variables = Settings.GetEnvironmentVariables();

        // Assert
        var actualVariables = variables.Keys.ToHashSet() as ISet<string>;
        Assert.True(expectedVariables.Count <= actualVariables.Count, "Not all system variables were loaded");
        Assert.ProperSuperset(expectedVariables, actualVariables);
    }

    [Fact]
    public void VerifyThatEnvironmentVariableIsLoadedCorrectly()
    {
        // Arrange
        var variableName = "HEADED";

        // Act
        var variable = Settings.GetEnvironmentVariable(variableName);

        // Assert
        Assert.Equal("1", variable);
    }

    [Fact]
    public void VerifyThatPlaywrightBrowserNameIsLoadedCorrectly()
    {
        // Arrange
        var settings = new Settings();

        // Act
        var browserName = settings.GetBrowserName();

        // Assert
        Assert.Equal("firefox", browserName);
    }

    [Fact]
    public void VerifyThatPlaywrightExpectedTimeoutIsLoadedCorrectly()
    {
        // Arrange
        var settings = new Settings();

        // Act
        var timeout = settings.GetExpectTimeout();

        // Assert
        Assert.Equal(5000, timeout);
    }

    [Fact]
    public void VerifyThatPlaywrightBrowserOptionsAreLoadedCorrectly()
    {
        // Arrange
        var settings = new Settings();

        // Act
        var options = settings.GetBrowserOptions();

        // Assert
        Assert.Equal("en-GB", options.Locale);
        Assert.Equal("Europe/London", options.TimezoneId);
        Assert.Equal(51.5007325f, options.Geolocation?.Latitude);
        Assert.Equal(-0.1272003f, options.Geolocation?.Longitude);
        Assert.False(options.Offline);
        Assert.Equal("localhost:3128", options.Proxy?.Server);
    }

    [Fact]
    public void VerifyThatPlaywrightLaunchOptionsAreLoadedCorrectly()
    {
        // Arrange
        var settings = new Settings();

        // Act
        var options = settings.GetLaunchOptions();

        // Assert
        Assert.False(options.Headless, "Headless mode is enabled");
        Assert.Equal(500, options.SlowMo);
        Assert.Equal(15000, options.Timeout);
        Assert.Equal("per-context", options.Proxy?.Server);
    }

    [Fact]
    public void VerifyThatTestParametersAreLoadedCorrectly()
    {
        // Arrange
        var settings = new Settings();

        // Act
        var parameters = settings.GetTestParameters();

        // Assert
        Assert.Equal(3, parameters.Count);
    }

    [Fact]
    public void VerifyThatUrlTestParameterIsLoadedCorrectly()
    {
        // Arrange
        var settings = new Settings();

        // Act
        var baseUrl = settings.GetTestParameter("PlaywrightBaseURL");

        // Assert
        Assert.Contains("playwright.dev", baseUrl);
    }
}
