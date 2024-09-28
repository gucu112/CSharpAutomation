namespace Gucu112.CSharp.Automation.PlaywrightXunit.Fixtures;

public class PlaywrightFixture : IAsyncLifetime
{
    private readonly Settings settings;

    public PlaywrightFixture()
    {
        settings = new Settings();
    }

    private IPlaywright Instance { get; set; } = null!;
    public IBrowser Browser { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        Instance = await Playwright.CreateAsync();
        Browser = await Instance[settings.GetBrowserName()]
            .LaunchAsync(settings.GetLaunchOptions());
    }

    public async Task DisposeAsync()
    {
        await Browser.DisposeAsync();
        Instance.Dispose();
    }
}

[CollectionDefinition(nameof(PlaywrightFixture))]
public class PlaywrightCollection : ICollectionFixture<PlaywrightFixture>
{
}
