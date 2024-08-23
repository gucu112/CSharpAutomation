namespace Gucu112.PlaywrightXunitParallel.Fixtures;

public class PlaywrightFixture(SettingsFixture settings) : IAsyncLifetime
{
    private IPlaywright Instance { get; set; } = null!;
    public IBrowser Browser { get; private set; } = null!;
    public BrowserTypeLaunchOptions LaunchOptions { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        LaunchOptions = settings.LaunchOptions;
        Instance = await Playwright.CreateAsync();
        Browser = await Instance[settings.BrowserName].LaunchAsync(LaunchOptions);
    }

    public async Task DisposeAsync()
    {
        await Browser.DisposeAsync();
        Instance.Dispose();
    }
}
