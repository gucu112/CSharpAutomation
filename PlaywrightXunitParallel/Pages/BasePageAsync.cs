using Gucu112.CSharp.Automation.PlaywrightXunitParallel.Fixtures;

namespace Gucu112.CSharp.Automation.PlaywrightXunitParallel.Pages;

public abstract class BasePageAsync(PlaywrightFixture playwright) : IAsyncLifetime
{
    protected static SettingsFixture Settings => new();
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
