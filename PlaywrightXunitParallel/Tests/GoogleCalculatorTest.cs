using FluentAssertions;
using Gucu112.PlaywrightXunitParallel.Fixtures;
using Gucu112.PlaywrightXunitParallel.Models.Attribute;
using Gucu112.PlaywrightXunitParallel.Models.Enum;
using Gucu112.PlaywrightXunitParallel.Pages;

namespace Gucu112.PlaywrightXunitParallel.Tests;

[Category(TestCategory.Browser)]
[Category(TestCategory.Google)]
public class GoogleCalculatorTest(PlaywrightFixture playwright) : IClassFixture<PlaywrightFixture>, IDisposable
{
    private readonly GoogleCalculatorPage page = new(playwright);

    // https://github.com/xunit/xunit/pull/2967#issuecomment-2218987854
    public static IEnumerable<object[]> TestData =>
    [
        [2, 3], [-10, 20], [112, 50]
    ];

    [Fact]
    public async Task VerifyThatGoogleCalculatorIsProperlyShown()
    {
        await page.CalculatorBoxLocator.IsVisibleAsync();
        await page.BasicSectionBoxLocator.IsVisibleAsync();
        await page.ResultLocator.IsVisibleAsync();

        var numbers = Enumerable.Range(0, 9).Select(i => i.ToString()).ToList();
        var buttons = numbers.Concat(["+", "-", "*", "/", "="]).ToList();

        var buttonsVisibility = await page.GetBasicButtonsVisibility(buttons);
        buttonsVisibility.Should().AllSatisfy(x => x.Value.Should().BeTrue($"{x.Key} should be visible"));
    }

    [Theory]
    [MemberData(nameof(TestData))]
    [InlineData(112, -50)]
    public async Task VerifyThatGoogleCalculatorAddsTwoIntegers(int a, int b)
    {
        await page.Calculate($"{a}+{b}");

        var result = Convert.ToInt32(await page.ResultLocator.TextContentAsync());

        result.Should().Be(a + b);
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public async Task VerifyThatGoogleCalculatorSubstractsTwoIntegers(int a, int b)
    {
        await page.Calculate($"{a}-{b}");

        var result = Convert.ToInt32(await page.ResultLocator.TextContentAsync());

        result.Should().Be(a - b);
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public async Task VerifyThatGoogleCalculatorCanMultiplyTwoIntegers(int a, int b)
    {
        await page.Calculate($"{a}*{b}");

        var result = Convert.ToInt32(await page.ResultLocator.TextContentAsync());

        result.Should().Be(a * b);
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public async Task VerifyThatGoogleCalculatorCanDivideTwoIntegers(int a, int b)
    {
        await page.Calculate($"{a}/{b}");

        var result = Convert.ToSingle(await page.ResultLocator.TextContentAsync());

        result.Should().Be(a / (float)b);
    }

    [Fact]
    public async Task VerifyThatGoogleCalculatorHandlesDivisionByZeroCorrectly()
    {
        await page.Calculate($"2/0");

        var result = await page.ResultLocator.TextContentAsync();

        result.Should().Be("Infinity");
    }

    public void Dispose()
    {
        page.Dispose();
        GC.SuppressFinalize(this);
    }
}
