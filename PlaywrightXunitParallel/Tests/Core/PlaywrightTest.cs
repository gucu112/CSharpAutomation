using Gucu112.PlaywrightXunitParallel.Fixtures;
using Gucu112.PlaywrightXunitParallel.Models;
using Gucu112.PlaywrightXunitParallel.Models.Enum;
using Gucu112.PlaywrightXunitParallel.Pages;

namespace Gucu112.PlaywrightXunitParallel.Tests.Core;

[Collection(nameof(SettingsFixture))]
[Trait(TestTrait.Category, nameof(TestCategory.Browser))]
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
    [Trait(TestTrait.Priority, nameof(TestPriority.Critical))]
    public void VerifyThatBrowserDoesExist()
    {
        Assert.NotNull(playwright.Browser);
        Assert.True(playwright.Browser.IsConnected, "Browser is not connected");
        Assert.Equal("chromium", playwright.Browser.BrowserType.Name);
    }

    [Fact]
    [Trait(TestTrait.Category, nameof(TestCategory.Settings))]
    [Trait(TestTrait.Priority, nameof(TestPriority.Medium))]
    public void VerifyThatLaunchOptionsAreInitalized()
    {
        Assert.NotNull(playwright.LaunchOptions);
        Assert.Equal("msedge", playwright.LaunchOptions.Channel);
    }

    [Fact]
    [Trait(TestTrait.Priority, nameof(TestPriority.High))]
    public void VerifyThatBrowserContextDoesExist()
    {
        using var page = new BlankPage(settings, playwright);

        Assert.NotNull(page.BrowserContext);
        Assert.Equal("chromium", page.BrowserContext.Browser?.BrowserType.Name);
    }

    [Fact]
    [Trait(TestTrait.Category, nameof(TestCategory.Settings))]
    [Trait(TestTrait.Priority, nameof(TestPriority.Low))]
    public void VerifyThatBrowserOptionsAreInitialized()
    {
        using var page = new BlankPage(settings, playwright);

        Assert.NotNull(page.BrowserOptions);
        Assert.False(page.BrowserOptions.Offline, "Browser is in offline mode");
    }

    [Fact]
    [Trait(TestTrait.Priority, nameof(TestPriority.High))]
    public void VerifyThatPageDoesExist()
    {
        using var page = new BlankPage(settings, playwright);

        Assert.NotNull(page.Context);
        Assert.Equal("about:blank", page.Context.Url);
    }

    [Fact]
    [Trait(TestTrait.Category, nameof(TestCategory.Settings))]
    [Trait(TestTrait.Priority, nameof(TestPriority.High))]
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
