using Gucu112.PlaywrightXunitParallel.Fixtures;

namespace Gucu112.PlaywrightXunitParallel.Pages;

public abstract class BasePage(
    SettingsFixture settings,
    PlaywrightFixture playwright
) : IAsyncLifetime
{
    protected SettingsFixture Settings { get; init; } = settings;
    protected IBrowser Browser { get; init; } = playwright.Browser;
    public BrowserNewContextOptions BrowserOptions { get; init; } = settings.BrowserOptions;
    public IBrowserContext BrowserContext { get; private set; } = null!;
    public IPage Context { get; private set; } = null!;
    public abstract string BaseUrl { get; }

    public async Task InitializeAsync()
    {
        BrowserContext = await Browser.NewContextAsync(BrowserOptions);
        BrowserContext.SetDefaultTimeout(Settings.ExpectTimeout);
        Context = await BrowserContext.NewPageAsync();
        await Context.GotoAsync(BaseUrl);
    }

    public async Task DisposeAsync()
    {
        await Context.CloseAsync();
        await BrowserContext.DisposeAsync();
    }
}
