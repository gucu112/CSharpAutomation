using Gucu112.CSharp.Automation.PlaywrightXunitParallel.Fixtures;

namespace Gucu112.CSharp.Automation.PlaywrightXunitParallel.Pages;

/// <summary>
/// Represents a synchronous base page.
/// </summary>
public abstract class BasePage : BasePageAsync, IDisposable
{
    /// <summary>
    /// Initializes the page synchronously.
    /// </summary>
    /// <param name="playwright">The Playwright controller.</param>
    public BasePage(PlaywrightFixture playwright)
        : base(playwright)
    {
        InitializeAsync().Wait();
    }

    /// <summary>
    /// Disposes the page synchronously.
    /// </summary>
    public void Dispose()
    {
        Task.Run(DisposeAsync);
        GC.SuppressFinalize(this);
    }
}
