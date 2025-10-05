using Gucu112.CSharp.Automation.PlaywrightXunit.Fixtures;

namespace Gucu112.CSharp.Automation.PlaywrightXunit.Pages;

public abstract class PageBase(PlaywrightFixture playwright)
    : IAsyncLifetime
{
    public IPage Context { get; private set; } = null!;

    public IBrowserContext BrowserContext { get; private set; } = null!;

    protected IBrowser Browser { get; init; } = playwright.Browser;

    protected Settings Settings { get; init; } = new Settings();

    public async Task InitializeAsync()
    {
        BrowserContext = await Browser.NewContextAsync();
        BrowserContext.SetDefaultTimeout(Settings.GetExpectTimeout());
        Context = await BrowserContext.NewPageAsync();
    }

    public async Task DisposeAsync()
    {
        await BrowserContext.DisposeAsync();
    }
}
