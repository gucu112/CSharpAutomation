using Gucu112.CSharp.Automation.PlaywrightXunitParallel.Fixtures;

namespace Gucu112.CSharp.Automation.PlaywrightXunitParallel.Pages;

public class GoogleUnitConverterPage(PlaywrightFixture playwright)
    : GoogleSearchPage(playwright)
{
    public override string BaseUrl => Settings.EntryPoints.Single(ep => ep.Name == "Google Unit Converter").Url;

    public ILocator ConverterBoxLocator => Context.Locator(".vk_c");

    public ILocator ConverterTypeSelectLocator => ConverterBoxLocator.Locator("select").First;

    public ILocator ConverterLeftUnitInputLocator => ConverterBoxLocator.Locator("#HG5Seb input");

    public ILocator ConverterLeftUnitSelectLocator => ConverterBoxLocator.Locator("#HG5Seb select");

    public ILocator ConverterRightUnitInputLocator => ConverterBoxLocator.Locator("#NotFQb input");

    public ILocator ConverterRightUnitSelectLocator => ConverterBoxLocator.Locator("#NotFQb select");

    public new async Task<IDictionary<string, bool>> GetElementsVisibility()
    {
        return new Dictionary<string, bool>
        {
            { "ConverterBox", await ConverterBoxLocator.IsVisibleAsync() },
            { "ConverterTypeSelect", await ConverterTypeSelectLocator.IsVisibleAsync() },
            { "ConverterLeftUnitInput", await ConverterLeftUnitInputLocator.IsVisibleAsync() },
            { "ConverterLeftUnitSelect", await ConverterLeftUnitSelectLocator.IsVisibleAsync() },
            { "ConverterRightUnitInput", await ConverterRightUnitInputLocator.IsVisibleAsync() },
            { "ConverterRightUnitSelect", await ConverterRightUnitSelectLocator.IsVisibleAsync() },
        };
    }

    public async Task ConvertTemperatureFromCelsius(int celsius)
    {
        await ConverterTypeSelectLocator.SelectOptionAsync("Temperature");
        await ConverterLeftUnitInputLocator.FillAsync(celsius.ToString());
        await ConverterLeftUnitSelectLocator.SelectOptionAsync("Degree Celsius");
    }

    public async Task ConvertTemperatureFromFahrenheit(float fahrenheit)
    {
        await ConverterTypeSelectLocator.SelectOptionAsync("Temperature");
        await ConverterLeftUnitInputLocator.FillAsync(fahrenheit.ToString());
        await ConverterLeftUnitSelectLocator.SelectOptionAsync("Fahrenheit");
    }
}
