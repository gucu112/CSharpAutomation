using Gucu112.PlaywrightXunitParallel.Fixtures;

namespace Gucu112.PlaywrightXunitParallel.Pages;

public abstract class BasePage : IAsyncLifetime
{
    protected SettingsFixture Settings { get; set; }
    protected IBrowser Browser { get; set; } = null!;
    public IBrowserContext BrowserContext { get; private set; } = null!;
    public IPage Context { get; private set; } = null!;
    public BrowserNewContextOptions BrowserOptions { get; private set; } = null!;

    public BasePage(
        SettingsFixture settingsFixture,
        PlaywrightFixture playwrightFixture
    )
    {
        Settings = settingsFixture;
        BrowserOptions = settingsFixture.BrowserOptions;
        Browser = playwrightFixture.Browser;
    }

    public async Task InitializeAsync()
    {
        BrowserContext = await Browser.NewContextAsync(BrowserOptions);
        BrowserContext.SetDefaultTimeout(Settings.ExpectTimeout);
        Context = await BrowserContext.NewPageAsync();
    }

    public async Task DisposeAsync()
    {
        await Context.CloseAsync();
        await BrowserContext.DisposeAsync();
    }
}
