using System.Reflection;
using Gucu112.PlaywrightXunit.Fixtures;
using Gucu112.PlaywrightXunit.Pages;

namespace Gucu112.PlaywrightXunit.Tests;

[Trait("Category", "Playwright")]
[Collection(nameof(PlaywrightFixture))]
public class TestPlaywright(PlaywrightFixture playwright)
{
    [Fact]
    public void VerifyThatPlaywrightInstanceIsInitilized()
    {
        // Arrange
        var property = playwright.GetType().GetProperty("Instance",
            BindingFlags.Instance | BindingFlags.NonPublic);

        // Act
        var instance = property?.GetGetMethod(nonPublic: true)?.Invoke(playwright, null);

        // Assert
        Assert.IsAssignableFrom<IPlaywright>(instance);
    }

    [Fact]
    public void VerifyThatBrowserIsInitilizedAndConnected()
    {
        // Act
        var browser = playwright.Browser;

        // Assert
        Assert.IsAssignableFrom<IBrowser>(browser);
        Assert.True(browser.IsConnected);
    }

    [Fact]
    public void VerifyThatCorrectBrowserTypeIsOpened()
    {
        // Arrange
        var settings = new Settings();

        // Act
        var browser = playwright.Browser;

        // Assert
        Assert.Equal(settings.GetBrowserName(), browser.BrowserType.Name);
    }

    [Fact]
    public async Task VerifyThatBrowserContextIsInitialized()
    {
        // Arrange
        var page = new PageBlank(playwright);
        await page.InitializeAsync();

        // Act
        var browserContext = page.BrowserContext;
        await page.DisposeAsync();

        // Assert
        Assert.IsAssignableFrom<IBrowserContext>(browserContext);
    }

    [Fact]
    public async Task VerifyThatPageContextIsInitialized()
    {
        // Arrange
        var page = new PageBlank(playwright);
        await page.InitializeAsync();

        // Act
        var pageContext = page.Context;
        await page.DisposeAsync();

        // Assert
        Assert.IsAssignableFrom<IPage>(pageContext);
    }

    [Fact]
    public async Task VerifyThatPageTimeoutWorksCorrect()
    {
        // Arrange
        var settings = new Settings();
        var page = new PageBlank(playwright);
        await page.InitializeAsync();

        // Act
        await page.Context.GotoAsync(settings.GetTestParameter("PlaywrightBaseURL"));
        var action = async () =>
        {
            await page.Context.GetByTestId("unknow").ClickAsync();
            await page.DisposeAsync();
        };

        // Assert
        var exception = await Assert.ThrowsAsync<TimeoutException>(action);
        Assert.Contains($"Timeout {settings.GetExpectTimeout()}ms exceeded", exception.Message);
    }

    private class PageBlank(PlaywrightFixture playwright) : PageBase(playwright)
    {
    }
}
