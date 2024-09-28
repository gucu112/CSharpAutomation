namespace Gucu112.CSharp.Automation.PlaywrightXunitParallel.Fixtures;

public class PlaywrightFixture : IAsyncLifetime
{
    private SettingsFixture Settings { get; } = new();
    private IPlaywright Instance { get; set; } = null!;
    public IBrowser Browser { get; private set; } = null!;
    public BrowserTypeLaunchOptions LaunchOptions { get; private set; } = null!;
    public IBrowserContext BrowserContext { get; private set; } = null!;
    public BrowserNewContextOptions BrowserOptions { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        LaunchOptions = Settings.LaunchOptions;
        Instance = await Playwright.CreateAsync();
        Browser = await Instance[Settings.BrowserName].LaunchAsync(LaunchOptions);
        BrowserOptions = Settings.BrowserOptions;
        BrowserContext = await Browser.NewContextAsync(BrowserOptions);
        BrowserContext.SetDefaultTimeout(Settings.ExpectTimeout);
    }

    public async Task DisposeAsync()
    {
        await BrowserContext.DisposeAsync();
        await Browser.DisposeAsync();
        Instance.Dispose();
    }
}
