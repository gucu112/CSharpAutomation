using Gucu112.CSharp.Automation.PlaywrightXunitParallel.Fixtures;

namespace Gucu112.CSharp.Automation.PlaywrightXunitParallel.Pages;

public class GoogleCalculatorPage(PlaywrightFixture playwright) : GoogleSearchPage(playwright)
{
    public override string BaseUrl => Settings.EntryPoints.Single(ep => ep.Name == "Google Calculator").Url;

    public ILocator CalculatorBoxLocator => Context.Locator("[jscontroller='qxNryb']");
    public ILocator BasicSectionBoxLocator => CalculatorBoxLocator.Locator("table.ElumCf");
    public ILocator ResultLocator => CalculatorBoxLocator.Locator("#cwos");

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

    public new async Task<IDictionary<string, bool>> GetElementsVisibility()
    {
        return new Dictionary<string, bool>
        {
            { "CalculatorBox", await CalculatorBoxLocator.IsVisibleAsync() },
            { "BasicSectionBox", await BasicSectionBoxLocator.IsVisibleAsync() },
            { "Result", await ResultLocator.IsVisibleAsync() },
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
