using System.Xml.Linq;

namespace Gucu112.PlaywrightXunit.Tests;

public class TestSettings
{
    [Fact]
    public void VerifyIfXunitRunsWithoutParallelExecution()
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
    public void VerifyIfEnvironmentVariablesLoadedCorrectly()
    {
        // Arrange
        var expectedVariables = new HashSet<string>
        {
            "Path",
            "ProgramFiles",
            "ProgramData",
            "SystemDrive",
            "SystemRoot"
        } as ISet<string>;

        // Act
        var variables = Settings.GetEnvironmentVariables();

        // Assert
        var actualVariables = variables.Keys.ToHashSet() as ISet<string>;
        Assert.True(expectedVariables.Count <= actualVariables.Count, "Not all system variables were loaded");
        Assert.ProperSuperset(expectedVariables, actualVariables);
    }

    [Fact]
    public void VerifyIfEnvironmentVariableLoadedCorrectly()
    {
        // Arrange
        var variableName = "HEADED";

        // Act
        var variable = Settings.GetEnvironmentVariable(variableName);

        // Assert
        Assert.Equal("1", variable);
    }

    [Fact]
    public void VerifyIfPlaywrightBrowserNameLoadedCorrectly()
    {
        // Arrange
        var settings = new Settings();

        // Act
        var browserName = settings.GetBrowserName();

        // Assert
        Assert.Equal("firefox", browserName);
    }

    [Fact]
    public void VerifyIfPlaywrightBrowserOptionsLoadedCorrectly()
    {
        // Arrange
        var settings = new Settings();

        // Act
        var options = settings.GetBrowserOptions();

        // Assert
        Assert.False(options.Offline);
        // TODO: Proxy
    }

    [Fact]
    public void VerifyIfPlaywrightLaunchOptionsLoadedCorrectly()
    {
        // Arrange
        var settings = new Settings();

        // Act
        var options = settings.GetLaunchOptions();

        // Assert
        Assert.False(options.Headless, "Headless mode is enabled");
        Assert.Equal(500, options.SlowMo);
        Assert.Equal(20000, options.Timeout);
        // TODO: Proxy
    }
}
