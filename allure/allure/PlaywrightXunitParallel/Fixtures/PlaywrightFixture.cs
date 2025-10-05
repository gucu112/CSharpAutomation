namespace Gucu112.CSharp.Automation.PlaywrightXunitParallel.Fixtures;

/// <summary>
/// Represents a controller for working with Playwright.
/// </summary>
public class PlaywrightFixture : IAsyncLifetime
{
    /// <summary>
    /// Gets the browser instance.
    /// </summary>
    public IBrowser Browser { get; private set; } = null!;

    /// <summary>
    /// Gets the launch options for the browser.
    /// </summary>
    public BrowserTypeLaunchOptions LaunchOptions { get; private set; } = null!;

    /// <summary>
    /// Gets the browser context instance.
    /// </summary>
    public IBrowserContext BrowserContext { get; private set; } = null!;

    /// <summary>
    /// Gets the context options for the browser.
    /// </summary>
    public BrowserNewContextOptions BrowserOptions { get; private set; } = null!;

    /// <summary>
    /// Gets or sets the Playwright instance.
    /// </summary>
    private IPlaywright Instance { get; set; } = null!;

    /// <summary>
    /// Gets the settings controller.
    /// </summary>
    private SettingsFixture Settings { get; } = new();

    /// <summary>
    /// Initializes the Playwright fixture asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task InitializeAsync()
    {
        LaunchOptions = Settings.LaunchOptions;
        Instance = await Playwright.CreateAsync();
        Browser = await Instance[Settings.BrowserName].LaunchAsync(LaunchOptions);
        BrowserOptions = Settings.BrowserOptions;
        BrowserContext = await Browser.NewContextAsync(BrowserOptions);
        BrowserContext.SetDefaultTimeout(Settings.ExpectTimeout);
    }

    /// <summary>
    /// Disposes the Playwright fixture asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task DisposeAsync()
    {
        await BrowserContext.DisposeAsync();
        await Browser.DisposeAsync();
        Instance.Dispose();
    }
}
