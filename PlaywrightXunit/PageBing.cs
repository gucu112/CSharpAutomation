using Gucu112.CSharp.Automation.PlaywrightXunit.Fixtures;

namespace Gucu112.CSharp.Automation.PlaywrightXunit.Pages;

public class PageBing : PageBase, IAsyncLifetime
{
    public PageBing(PlaywrightFixture fixture)
        : base(fixture)
    {
        BaseUrl = Settings.GetTestParameter("BingBaseURL");
    }

    public string BaseUrl { get; private set; }

    public ILocator AcceptCookiesButton => Context.Locator("button[id=bnp_btn_accept]");

    public ILocator SearchInput => Context.Locator("textarea[id=sb_form_q]");

    public ILocator SearchResult => Context.Locator("li[data-tag] h2>a[href]");

    public new async Task InitializeAsync()
    {
        await base.InitializeAsync();
        await Context.GotoAsync(BaseUrl);
        await AcceptCookiesButton.ClickAsync();
    }
}
