using Gucu112.CSharp.Automation.PlaywrightXunit.Fixtures;

namespace Gucu112.CSharp.Automation.PlaywrightXunit.Pages;

public class PageGoogle : PageBase, IAsyncLifetime
{
    public PageGoogle(PlaywrightFixture playwright)
        : base(playwright)
    {
        BaseUrl = Settings.GetTestParameter("GoogleBaseURL");
    }

    public string BaseUrl { get; private set; }

    public ILocator SearchInput => Context.Locator("textarea[name=q]");

    public ILocator SearchResult => Context.Locator("h3[class^=LC20lb]");

    public new async Task InitializeAsync()
    {
        await base.InitializeAsync();
        await BrowserContext.AddCookiesAsync(
        [
            new Cookie()
            {
                Name = "SOCS",
                Value = "CAISHA",
                Domain = ".google.com",
                Path = "/",
            },
        ]);
        await Context.GotoAsync(BaseUrl);
    }
}
