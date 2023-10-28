namespace Gucu112.PlaywrightXunitParallel.Fixtures;

public class PlaywrightFixture : IAsyncLifetime
{
    private readonly SettingsFixture settings;

    public PlaywrightFixture(SettingsFixture fixture)
    {
        settings = fixture;
    }

    private IPlaywright Instance { get; set; } = null!;
    public IBrowser Browser { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        Instance = await Playwright.CreateAsync();
        Browser = await Instance[settings.BrowserName]
            .LaunchAsync(settings.LaunchOptions);
    }

    public async Task DisposeAsync()
    {
        await Browser.DisposeAsync();
        Instance.Dispose();
    }
}
