using FluentAssertions;
using Gucu112.CSharp.Automation.PlaywrightXunitParallel.Fixtures;
using Gucu112.CSharp.Automation.PlaywrightXunitParallel.Pages;

namespace Gucu112.CSharp.Automation.PlaywrightXunitParallel.Tests;

public class GoogleUnitConverterTest(PlaywrightFixture playwright) : IClassFixture<PlaywrightFixture>, IDisposable
{
    private readonly GoogleUnitConverterPage page = new(playwright);

    [Fact]
    public async Task VerifyThatGoogleUnitConverterIsProperlyShown()
    {
        var elementsVisiblity = await page.GetElementsVisibility();
        elementsVisiblity.Should().AllSatisfy(x => x.Value.Should().BeTrue($"'{x.Key}' element should be visible"));

        var typeOptions = await page.ConverterTypeSelectLocator.Locator("option").AllInnerTextsAsync();
        typeOptions.Should().Contain(["Mass", "Temperature", "Time"]);
    }

    [Fact]
    public async Task VerifyThatGoogleMassUnitConverterIsProperlyShown()
    {
        IReadOnlyList<string> actualUnits, expectedUnits;

        await page.ConverterTypeSelectLocator.SelectOptionAsync("Mass");
        expectedUnits = ["Kilogram", "Pound"];
        actualUnits = await page.ConverterLeftUnitSelectLocator.Locator("option").AllInnerTextsAsync();
        actualUnits.Should().Contain(expectedUnits);
        actualUnits = await page.ConverterRightUnitSelectLocator.Locator("option").AllInnerTextsAsync();
        actualUnits.Should().Contain(expectedUnits);
    }

    [Fact]
    public async Task VerifyThatGoogleTemperatureUnitConverterIsProperlyShown()
    {
        IReadOnlyList<string> actualUnits;

        await page.ConverterTypeSelectLocator.SelectOptionAsync("Temperature");
        actualUnits = await page.ConverterLeftUnitSelectLocator.Locator("option").AllInnerTextsAsync();
        actualUnits.Should().ContainMatch("* Celsius");
        actualUnits.Should().Contain("Fahrenheit");
        actualUnits = await page.ConverterRightUnitSelectLocator.Locator("option").AllInnerTextsAsync();
        actualUnits.Should().ContainMatch("* Celsius");
        actualUnits.Should().Contain("Fahrenheit");
    }

    [Fact]
    public async Task VerifyThatGoogleTimeUnitConverterIsProperlyShown()
    {
        IReadOnlyList<string> actualUnits, expectedUnits;

        await page.ConverterTypeSelectLocator.SelectOptionAsync("Time");
        expectedUnits = ["Second", "Calendar year"];
        actualUnits = await page.ConverterLeftUnitSelectLocator.Locator("option").AllInnerTextsAsync();
        actualUnits.Should().Contain(expectedUnits);
        actualUnits = await page.ConverterRightUnitSelectLocator.Locator("option").AllInnerTextsAsync();
        actualUnits.Should().Contain(expectedUnits);
    }

    [Theory]
    [InlineData("kilograms how many pounds", 50, 110)]
    [InlineData("pounds how many kilograms", 150, 68)]
    public async Task VerifyThatGoogleMassUnitConverterWorksThroughGoogleSearch(string phrase, int a, int b)
    {
        await page.Context.GotoAsync(GoogleSearchPage.MainUrl);

        await page.SearchInputLocator.FillAsync($"{a} {phrase}");
        await page.SearchButtonLocator.ClickAsync();

        var actualConverterType = await page.ConverterTypeSelectLocator.InputValueAsync();
        actualConverterType.Should().Be("Mass");

        var actualFromUnit = await page.ConverterLeftUnitSelectLocator.InputValueAsync();
        var expectedFromUnit = phrase.Split().First();
        expectedFromUnit = expectedFromUnit.Remove(expectedFromUnit.Length - 1);
        actualFromUnit.Should().ContainEquivalentOf(expectedFromUnit);

        var actualToUnit = await page.ConverterRightUnitSelectLocator.InputValueAsync();
        var expectedToUnit = phrase.Split().Last();
        expectedToUnit = expectedToUnit.Remove(expectedToUnit.Length - 1);
        actualToUnit.Should().ContainEquivalentOf(expectedToUnit);

        var actualFromValue = Convert.ToInt32(await page.ConverterLeftUnitInputLocator.InputValueAsync());
        var actualToValue = (int)Math.Round(Convert.ToSingle(await page.ConverterRightUnitInputLocator.InputValueAsync()));
        actualFromValue.Should().Be(a);
        actualToValue.Should().Be(b);
    }

    [Theory]
    [InlineData("celsius to fahrenheit", 30, 86)]
    [InlineData("fahrenheit to celsius", 125, 51.67)]
    public async Task VerifyThatGoogleTemperatureUnitConverterWorksThroughGoogleSearch(string phrase, int a, float b)
    {
        await page.Context.GotoAsync(GoogleSearchPage.MainUrl);

        await page.SearchInputLocator.FillAsync($"{a} {phrase}");
        await page.SearchButtonLocator.ClickAsync();

        var actualConverterType = await page.ConverterTypeSelectLocator.InputValueAsync();
        actualConverterType.Should().Be("Temperature");

        var actualFromUnit = await page.ConverterLeftUnitSelectLocator.InputValueAsync();
        var expectedFromUnit = phrase.Split().First();
        actualFromUnit.Should().ContainEquivalentOf(expectedFromUnit);

        var actualToUnit = await page.ConverterRightUnitSelectLocator.InputValueAsync();
        var expectedToUnit = phrase.Split().Last();
        actualToUnit.Should().ContainEquivalentOf(expectedToUnit);

        var actualFromValue = Convert.ToInt32(await page.ConverterLeftUnitInputLocator.InputValueAsync());
        var actualToValue = Convert.ToSingle(await page.ConverterRightUnitInputLocator.InputValueAsync());
        actualFromValue.Should().Be(a);
        actualToValue.Should().BeApproximately(b, 0.01f);
    }

    [Fact]
    public async Task VerifyThatGoogleTimeUnitConverterWorksThroughGoogleSearch()
    {
        await page.Context.GotoAsync(GoogleSearchPage.MainUrl);

        await page.SearchInputLocator.FillAsync($"100000000 sec to year");
        await page.SearchButtonLocator.ClickAsync();

        var result = Convert.ToDouble(await page.ConverterRightUnitInputLocator.InputValueAsync());
        result.Should().BeApproximately(3.17, 0.01);
    }

    [Fact]
    public async Task VerifyThatGoogleMassUnitConverterWorksWhenUsingKilogramsAndPounds()
    {
        await page.ConverterTypeSelectLocator.SelectOptionAsync("Mass");

        await page.ConverterLeftUnitInputLocator.FillAsync("100");
        await page.ConverterLeftUnitSelectLocator.SelectOptionAsync("Kilogram");
        await page.ConverterRightUnitSelectLocator.SelectOptionAsync("Pound");
        var poundResult = await page.ConverterRightUnitInputLocator.InputValueAsync();
        poundResult.Should().Be("220.462");

        await page.ConverterLeftUnitInputLocator.FillAsync("300");
        await page.ConverterLeftUnitSelectLocator.SelectOptionAsync("Pound");
        await page.ConverterRightUnitSelectLocator.SelectOptionAsync("Kilogram");
        var kilogramResult = await page.ConverterRightUnitInputLocator.InputValueAsync();
        kilogramResult.Should().Be("136.078");
    }

    [Theory]
    [ClassData(typeof(CelsiusToFahrenheitTestData))]
    public async Task VerifyThatGoogleTemperatureUnitConverterWorksWhenUsingCelsiusToFahrenheit(int celsius, int fahrenheit)
    {
        await page.ConvertTemperatureFromCelsius(celsius);
        await page.ConverterRightUnitSelectLocator.SelectOptionAsync("Fahrenheit");
        var fahrenheitResult = Convert.ToInt32(await page.ConverterRightUnitInputLocator.InputValueAsync());
        fahrenheitResult.Should().Be(fahrenheit);
    }

    [Theory]
    [ClassData(typeof(FahrenheitToCelsiusTestData))]
    public async Task VerifyThatGoogleTemperatureUnitConverterWorksWhenUsingFahrenheitToCelsius(float fahrenheit, double celsius)
    {
        await page.ConvertTemperatureFromFahrenheit(fahrenheit);
        await page.ConverterRightUnitSelectLocator.SelectOptionAsync("Degree Celsius");
        var fahrenheitResult = Convert.ToDouble(await page.ConverterRightUnitInputLocator.InputValueAsync());
        fahrenheitResult.Should().BeApproximately(celsius, 0.01);
    }

    [Fact]
    public async Task VerifyThatGoogleTimeUnitConverterWorksWhenUsingSecondsAndYear()
    {
        await page.ConverterTypeSelectLocator.SelectOptionAsync("Time");

        await page.ConverterRightUnitSelectLocator.SelectOptionAsync("Calendar year");
        await page.ConverterRightUnitInputLocator.FillAsync("1");
        await page.Context.Keyboard.PressAsync("Enter");
        var result = await page.ConverterLeftUnitInputLocator.InputValueAsync();
        result.Should().Be("3.154e+7");
    }

    public void Dispose()
    {
        page.Dispose();
        GC.SuppressFinalize(this);
    }

    internal class CelsiusToFahrenheitTestData : TheoryData<int, int>
    {
        public CelsiusToFahrenheitTestData()
        {
            Add(-50, -58);
            Add(0, 32);
            Add(100, 212);
        }
    }

    internal class FahrenheitToCelsiusTestData : TheoryData<int, double>
    {
        public FahrenheitToCelsiusTestData()
        {
            Add(-50, -45.56);
            Add(0, -17.78);
            Add(200, 93.33);
        }
    }
}
