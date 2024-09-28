using Gucu112.CSharp.Automation.PlaywrightXunitParallel.Fixtures;

namespace Gucu112.CSharp.Automation.PlaywrightXunitParallel.Pages;

public class GoogleSearchPage(PlaywrightFixture playwright) : BasePage(playwright)
{
    public static string MainUrl => Settings.EntryPoints.Single(ep => ep.Name == "Google Search").Url;
    public override string BaseUrl => MainUrl;

    public ILocator SearchInputLocator => Context.GetByRole(AriaRole.Combobox);
    public ILocator SearchButtonLocator => Context.GetByRole(AriaRole.Button, new() { Name = "Google Search" }).First;
    public ILocator SearchResultLinkLocator => Context.GetByRole(AriaRole.Link).Filter(new() { Has = Context.Locator("h3") }).First;

    public override async Task BeforeGoToBaseUrl()
    {
        await BrowserContext.AddCookiesAsync(
        [
            new Cookie()
            {
                Name = "SOCS",
                Value = "CAISHA",
                Domain = ".google.com",
                Path = "/"
            }
        ]);
    }

    public async Task<IDictionary<string, bool>> GetElementsVisibility()
    {
        return new Dictionary<string, bool>
        {
            { "SearchInput", await SearchInputLocator.IsVisibleAsync() },
            { "SearchButton", await SearchButtonLocator.IsVisibleAsync() },
        };
    }
}
