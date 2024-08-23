using Gucu112.PlaywrightXunitParallel.Fixtures;
using Gucu112.PlaywrightXunitParallel.Models.Attribute;
using Gucu112.PlaywrightXunitParallel.Models.Enum;
using Gucu112.PlaywrightXunitParallel.Orderers.TestCase;
using Gucu112.PlaywrightXunitParallel.Pages;

namespace Gucu112.PlaywrightXunitParallel.Tests.Core;

[Collection(nameof(SettingsFixture))]
[TestCaseOrderer(PriorityOrderer.FullTypeName, PriorityOrderer.AssemblyName)]

[Category(TestCategory.Browser)]
public class PlaywrightTest(
    SettingsFixture settings,
    PlaywrightFixture playwright
) : IClassFixture<PlaywrightFixture>
{
    [Fact]
    [Priority(TestPriority.Critical)]
    public void VerifyThatBrowserDoesExist()
    {
        Assert.NotNull(playwright.Browser);
        Assert.True(playwright.Browser.IsConnected, "Browser is not connected");
        Assert.Equal("chromium", playwright.Browser.BrowserType.Name);
    }

    [Fact]
    [Category(TestCategory.Settings)]
    [Priority(TestPriority.Medium)]
    public void VerifyThatLaunchOptionsAreInitalized()
    {
        Assert.NotNull(playwright.LaunchOptions);
        Assert.Equal("msedge", playwright.LaunchOptions.Channel);
    }

    [Fact]
    [Priority(TestPriority.High)]
    public void VerifyThatBrowserContextDoesExist()
    {
        using var page = new BlankPage(settings, playwright);

        Assert.NotNull(page.BrowserContext);
        Assert.Equal("chromium", page.BrowserContext.Browser?.BrowserType.Name);
    }

    [Fact]
    [Category(TestCategory.Settings)]
    [Priority(TestPriority.Low)]
    public void VerifyThatBrowserOptionsAreInitialized()
    {
        using var page = new BlankPage(settings, playwright);

        Assert.NotNull(page.BrowserOptions);
        Assert.False(page.BrowserOptions.Offline, "Browser is in offline mode");
    }

    [Fact]
    [Priority(TestPriority.High)]
    public void VerifyThatPageDoesExist()
    {
        using var page = new BlankPage(settings, playwright);

        Assert.NotNull(page.Context);
        Assert.Equal("about:blank", page.Context.Url);
    }

    [Fact]
    [Category(TestCategory.Settings)]
    [Priority(TestPriority.High)]
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
            Task.Run(InitializeAsync).Wait();
        }

        public void Dispose()
        {
            Task.Run(DisposeAsync).Wait();
        }
    }
}
