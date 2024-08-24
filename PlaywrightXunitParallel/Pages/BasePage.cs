using Gucu112.PlaywrightXunitParallel.Fixtures;

namespace Gucu112.PlaywrightXunitParallel.Pages;

public abstract class BasePage : BasePageAsync, IDisposable
{
    public BasePage(PlaywrightFixture playwright) : base(playwright)
    {
        InitializeAsync().Wait();
    }

    public void Dispose()
    {
        Task.Run(DisposeAsync);
        GC.SuppressFinalize(this);
    }
}
