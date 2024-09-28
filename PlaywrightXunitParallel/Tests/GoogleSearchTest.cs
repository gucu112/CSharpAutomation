using FluentAssertions;
using Gucu112.CSharp.Automation.PlaywrightXunitParallel.Fixtures;
using Gucu112.CSharp.Automation.PlaywrightXunitParallel.Models.Attribute;
using Gucu112.CSharp.Automation.PlaywrightXunitParallel.Models.Enum;
using Gucu112.CSharp.Automation.PlaywrightXunitParallel.Pages;

namespace Gucu112.CSharp.Automation.PlaywrightXunitParallel.Tests;

[Category(TestCategory.Browser)]
[Category(TestCategory.Google)]
public class GoogleSearchTest(PlaywrightFixture playwright) : IClassFixture<PlaywrightFixture>, IDisposable
{
    private readonly GoogleSearchPage page = new(playwright);

    [Fact]
    public async Task VerifyThatGoogleSearchIsProperlyShown()
    {
        var elementsVisiblity = await page.GetElementsVisibility();
        elementsVisiblity.Should().AllSatisfy(x => x.Value.Should().BeTrue($"'{x.Key}' element should be visible"));
    }

    [Theory]
    [InlineData("cat")]
    [InlineData("dog")]
    public async Task VerifyThatGoogleSearchShowsCorrectFirstResult(string phrase)
    {
        await page.SearchInputLocator.FillAsync(phrase);
        await page.SearchButtonLocator.ClickAsync();

        var linkText = await page.SearchResultLinkLocator.Locator("h3").TextContentAsync();
        var linkHref = await page.SearchResultLinkLocator.GetAttributeAsync("href");

        linkText.Should().ContainEquivalentOf(phrase);
        linkHref.Should().Contain("wikipedia.org");
    }

    public void Dispose()
    {
        page.Dispose();
        GC.SuppressFinalize(this);
    }
}
