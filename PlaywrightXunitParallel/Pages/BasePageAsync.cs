using Gucu112.PlaywrightXunitParallel.Fixtures;

namespace Gucu112.PlaywrightXunitParallel.Pages;

public abstract class BasePageAsync(PlaywrightFixture playwright) : IAsyncLifetime
{
    public IBrowserContext BrowserContext => playwright.BrowserContext;
    public IPage Context { get; private set; } = null!;
    public abstract string BaseUrl { get; }

    public async Task InitializeAsync()
    {
        Context = await BrowserContext.NewPageAsync();
        await BeforeGoToBaseUrl();
        await Context.GotoAsync(BaseUrl);
    }

    public async Task DisposeAsync()
    {
        await Context.CloseAsync();
    }

    public virtual Task BeforeGoToBaseUrl()
    {
        return Task.CompletedTask;
    }
}
