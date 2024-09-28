using Gucu112.CSharp.Automation.PlaywrightXunit.Fixtures;
using Gucu112.CSharp.Automation.PlaywrightXunit.Pages;

namespace Gucu112.CSharp.Automation.PlaywrightXunit;

public class PageBing : PageBase, IAsyncLifetime
{
    public string BaseUrl { get; private set; }

    public PageBing(PlaywrightFixture fixture) : base(fixture)
    {
        BaseUrl = Settings.GetTestParameter("BingBaseURL");
    }

    public new async Task InitializeAsync()
    {
        await base.InitializeAsync();
        await Context.GotoAsync(BaseUrl);
        await AcceptCookiesButton.ClickAsync();
    }

    public ILocator AcceptCookiesButton => Context.Locator("button[id=bnp_btn_accept]");
    public ILocator SearchInput => Context.Locator("textarea[id=sb_form_q]");
    public ILocator SearchResult => Context.Locator("li[data-tag] h2>a[href]");
}
