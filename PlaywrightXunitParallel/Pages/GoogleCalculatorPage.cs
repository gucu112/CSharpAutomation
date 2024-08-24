using Gucu112.PlaywrightXunitParallel.Fixtures;

namespace Gucu112.PlaywrightXunitParallel.Pages;

public class GoogleCalculatorPage(PlaywrightFixture playwright) : BasePage(playwright)
{
    public override string BaseUrl => Settings.EntryPoints.Single(ep => ep.Name == "Google Calculator").Url;

    public ILocator CalculatorBoxLocator => Context.Locator("[jscontroller='qxNryb']");
    public ILocator BasicSectionBoxLocator => CalculatorBoxLocator.Locator("table.ElumCf");
    public ILocator ResultLocator => CalculatorBoxLocator.Locator("#cwos");

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

    public ILocator GetBasicButtonLocator(string buttonSymbol)
    {
        return buttonSymbol switch
        {
            "+" => BasicSectionBoxLocator.GetByRole(AriaRole.Button, new() { Name = "plus" }),
            "-" => BasicSectionBoxLocator.GetByRole(AriaRole.Button, new() { Name = "minus" }),
            "*" => BasicSectionBoxLocator.GetByRole(AriaRole.Button, new() { Name = "multiply" }),
            "/" => BasicSectionBoxLocator.GetByRole(AriaRole.Button, new() { Name = "divide" }),
            _ => BasicSectionBoxLocator.GetByText(buttonSymbol),
        };
    }

    public async Task<IDictionary<string, bool>> GetBasicButtonsVisibility(IEnumerable<string> buttons)
    {
        var tasks = buttons.Select(async button => new KeyValuePair<string, bool>(button,
            await GetBasicButtonLocator(button).IsVisibleAsync()));

        return (await Task.WhenAll(tasks)).ToDictionary();
    }

    public async Task Calculate(string equasion)
    {
        var buttons = equasion.ToCharArray().Select(c => $"{c}").ToList();

        foreach (var button in buttons)
        {
            await GetBasicButtonLocator(button).ClickAsync();
        }

        if (buttons.Last() != "=")
        {
            await GetBasicButtonLocator("=").ClickAsync();
        }
    }
}
