using Gucu112.PlaywrightXunit.Fixtures;

namespace Gucu112.PlaywrightXunit.Pages;

public abstract class PageBase : IAsyncLifetime
{
    private readonly Settings settings;

    protected IBrowser Browser { get; set; } = null!;
    public IBrowserContext BrowserContext { get; private set; } = null!;
    public IPage Context { get; private set; } = null!;

    public PageBase(PlaywrightFixture fixture)
    {
        settings = new Settings();
        Browser = fixture.Browser;
    }

    public async Task InitializeAsync()
    {
        BrowserContext = await Browser.NewContextAsync();
        BrowserContext.SetDefaultTimeout(settings.GetExpectTimeout());
        Context = await BrowserContext.NewPageAsync();
    }

    public async Task DisposeAsync()
    {
        await BrowserContext.DisposeAsync();
    }
}
