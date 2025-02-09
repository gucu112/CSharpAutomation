using Gucu112.CSharp.Automation.PlaywrightXunitParallel.Fixtures;

namespace Gucu112.CSharp.Automation.PlaywrightXunitParallel.Pages;

/// <summary>
/// Represents an asynchronous page.
/// </summary>
/// <param name="playwright">The Playwright controller.</param>
public abstract class BasePageAsync(PlaywrightFixture playwright)
    : IAsyncLifetime
{
    /// <summary>
    /// Gets the browser context.
    /// </summary>
    public IBrowserContext BrowserContext => playwright.BrowserContext;

    /// <summary>
    /// Gets the page context.
    /// </summary>
    public IPage Context { get; private set; } = null!;

    /// <summary>
    /// Gets the base URL address.
    /// </summary>
    public abstract string BaseUrl { get; }

    /// <summary>
    /// Gets the settings controller.
    /// </summary>
    protected static SettingsFixture Settings => new();

    /// <summary>
    /// Initializes the page asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task InitializeAsync()
    {
        Context = await BrowserContext.NewPageAsync();
        await BeforeGoToBaseUrl();
        await Context.GotoAsync(BaseUrl);
    }

    /// <summary>
    /// Disposes the page asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task DisposeAsync()
    {
        await Context.CloseAsync();
    }

    /// <summary>
    /// Executes actions before navigating to the base URL.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public virtual Task BeforeGoToBaseUrl()
    {
        return Task.CompletedTask;
    }
}
