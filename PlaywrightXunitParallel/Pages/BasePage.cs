using Gucu112.CSharp.Automation.PlaywrightXunitParallel.Fixtures;

namespace Gucu112.CSharp.Automation.PlaywrightXunitParallel.Pages;

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
