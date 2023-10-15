using System.Reflection;
using Gucu112.PlaywrightXunit.Fixtures;
using Gucu112.PlaywrightXunit.Pages;

namespace Gucu112.PlaywrightXunit.Tests;

[Collection(nameof(PlaywrightFixture))]
public class TestPlaywright
{
    private readonly PlaywrightFixture fixture;

    public TestPlaywright(PlaywrightFixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact]
    public void VerifyIfPlaywrightInstanceInitilized()
    {
        // Arrange
        var property = fixture.GetType().GetProperty("Instance",
            BindingFlags.Instance | BindingFlags.NonPublic);

        // Act
        var instance = property?.GetGetMethod(nonPublic: true)?.Invoke(fixture, null);

        // Assert
        Assert.IsAssignableFrom<IPlaywright>(instance);
    }

    [Fact]
    public void VerifyIfBrowserInitilizedAndConnected()
    {
        // Act
        var browser = fixture.Browser;

        // Assert
        Assert.IsAssignableFrom<IBrowser>(browser);
        Assert.True(browser.IsConnected);
    }

    [Fact]
    public void VerifyIfCorrectBrowserTypeOpened()
    {
        // Arrange
        var settings = new Settings();

        // Act
        var browser = fixture.Browser;

        // Assert
        Assert.Equal(settings.GetBrowserName(), browser.BrowserType.Name);
    }

    [Fact]
    public async Task VerifyIfBrowserContextInitialized()
    {
        // Arrange
        var page = new PageBlank(fixture);
        await page.InitializeAsync();

        // Act
        var browserContext = page.BrowserContext;
        await page.DisposeAsync();

        // Assert
        Assert.IsAssignableFrom<IBrowserContext>(browserContext);
    }

    [Fact]
    public async Task VerifyIfPageContextInitialized()
    {
        // Arrange
        var page = new PageBlank(fixture);
        await page.InitializeAsync();

        // Act
        var pageContext = page.Context;
        await page.DisposeAsync();

        // Assert
        Assert.IsAssignableFrom<IPage>(pageContext);
    }

    [Fact]
    public async Task VerifyIfPageTimeoutIsCorrect()
    {
        // Arrange
        var settings = new Settings();
        var page = new PageBlank(fixture);
        await page.InitializeAsync();

        // Act
        await page.Context.GotoAsync(settings.GetTestParameter("PlaywrightBaseURL"));
        var action = async () =>
        {
            await page.Context.GetByTestId("unknow").ClickAsync();
            await page.DisposeAsync();
        };

        // Assert
        Exception exception = await Assert.ThrowsAsync<TimeoutException>(action);
        Assert.Contains($"Timeout {settings.GetExpectTimeout()}ms exceeded", exception.Message);
    }

    private class PageBlank : PageBase
    {
        public PageBlank(PlaywrightFixture fixture) : base(fixture) { }
    }
}
