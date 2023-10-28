using Gucu112.PlaywrightXunitParallel.Fixtures;
using Gucu112.PlaywrightXunitParallel.Pages;

namespace Gucu112.PlaywrightXunitParallel.Tests.Core;

[Collection(nameof(SettingsFixture))]
public class PlaywrightTest : IClassFixture<PlaywrightFixture>
{
    private readonly SettingsFixture settings;
    private readonly PlaywrightFixture playwright;

    public PlaywrightTest(
        SettingsFixture settingsFixture,
        PlaywrightFixture playwrightFixture)
    {
        settings = settingsFixture;
        playwright = playwrightFixture;
    }

    [Fact]
    public void VerifyThatBrowserDoesExist()
    {
        Assert.NotNull(playwright.Browser);
        Assert.True(playwright.Browser.IsConnected, "Browser is not connected");
        Assert.Equal("chromium", playwright.Browser.BrowserType.Name);
    }

    [Fact]
    public void VerifyThatLaunchOptionsAreInitalized()
    {
        Assert.NotNull(playwright.LaunchOptions);
        Assert.Equal("msedge", playwright.LaunchOptions.Channel);
    }

    [Fact]
    public void VerifyThatBrowserContextDoesExist()
    {
        using var page = new BlankPage(settings, playwright);

        Assert.NotNull(page.BrowserContext);
        Assert.Equal("chromium", page.BrowserContext.Browser?.BrowserType.Name);
    }

    [Fact]
    public void VerifyThatBrowserOptionsAreInitialized()
    {
        using var page = new BlankPage(settings, playwright);

        Assert.NotNull(page.BrowserOptions);
        Assert.False(page.BrowserOptions.Offline, "Browser is in offline mode");
    }

    [Fact]
    public void VerifyThatPageDoesExist()
    {
        using var page = new BlankPage(settings, playwright);

        Assert.NotNull(page.Context);
        Assert.Equal("about:blank", page.Context.Url);
    }

    [Fact]
    public async Task VerifyThatPageTimeoutWorksCorrect()
    {
        using var page = new BlankPage(settings, playwright);
        var action = async () => await page.Context.GetByTestId("unknow").ClickAsync();

        await page.Context.GotoAsync("chrome://about");

        Exception exception = await Assert.ThrowsAsync<TimeoutException>(action);
        Assert.Contains($"Timeout {settings.ExpectTimeout}ms exceeded", exception.Message);
    }

    private class BlankPage : BasePage, IDisposable
    {
        public BlankPage(
            SettingsFixture settings,
            PlaywrightFixture playwright
        ) : base(settings, playwright)
        {
            Task.Run(async () => await InitializeAsync()).Wait();
        }

        public void Dispose()
        {
            Task.Run(async () => await DisposeAsync()).Wait();
        }
    }
}
