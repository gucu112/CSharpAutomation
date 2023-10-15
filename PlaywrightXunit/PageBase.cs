using Gucu112.PlaywrightXunit.Fixtures;

namespace Gucu112.PlaywrightXunit.Pages;

public abstract class PageBase : IAsyncLifetime
{
    protected Settings Settings { get; set; }
    protected IBrowser Browser { get; set; } = null!;
    public IBrowserContext BrowserContext { get; private set; } = null!;
    public IPage Context { get; private set; } = null!;

    public PageBase(PlaywrightFixture fixture)
    {
        Settings = new Settings();
        Browser = fixture.Browser;
    }

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
