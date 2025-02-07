using Gucu112.CSharp.Automation.PlaywrightXunit.Fixtures;

namespace Gucu112.CSharp.Automation.PlaywrightXunit.Tests;

[Trait("Category", "Pages")]
[Collection(nameof(PlaywrightFixture))]
public class TestBing(PageBing page)
    : IClassFixture<PageBing>
{
    [Fact]
    public async Task VerifyThatSearchInputIsVisible()
    {
        // Act
        var isVisible = await page.SearchInput.IsVisibleAsync();

        // Assert
        Assert.True(isVisible);
    }

    [Fact]
    public async Task VerifyThatSearchResultsContainSearchPhrase()
    {
        // Arrange
        var phrase = "catwalk";
        await page.SearchInput.FillAsync(phrase);
        await page.Context.Keyboard.PressAsync("Enter");

        // Act
        var results = await page.SearchResult.AllTextContentsAsync() as IEnumerable<string>;

        // Assert
        Assert.All(results, result => Assert.Contains(phrase, result, StringComparison.InvariantCultureIgnoreCase));
    }
}
