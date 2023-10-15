using Gucu112.PlaywrightXunit.Fixtures;
using Gucu112.PlaywrightXunit.Pages;

namespace Gucu112.PlaywrightXunit;

public class PageGoogle : PageBase, IAsyncLifetime
{
    public string BaseUrl { get; private set; }

    public PageGoogle(PlaywrightFixture fixture) : base(fixture)
    {
        BaseUrl = Settings.GetTestParameter("GoogleBaseURL");
    }

    public new async Task InitializeAsync()
    {
        await base.InitializeAsync();
        await BrowserContext.AddCookiesAsync(new List<Cookie>
        {
            new Cookie()
            {
                Name = "SOCS",
                Value = "CAISHA",
                Domain = ".google.com",
                Path = "/"
            }
        });
        await Context.GotoAsync(BaseUrl);
    }

    public ILocator SearchInput => Context.Locator("textarea[name=q]");
    public ILocator SearchResult => Context.Locator("h3[class^=LC20lb]");
}
