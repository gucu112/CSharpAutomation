using Gucu112.PlaywrightXunit.Fixtures;

namespace Gucu112.PlaywrightXunit.Tests;

[Trait("Category", "Pages")]
[Collection(nameof(PlaywrightFixture))]
public class TestGoogle : IClassFixture<PageGoogle>
{
    private readonly PageGoogle page;

    public TestGoogle(PageGoogle page)
    {
        this.page = page;
    }

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
        var phrase = "castle";
        await page.SearchInput.FillAsync(phrase);
        await page.Context.Keyboard.PressAsync("Enter");

        // Act
        var results = await page.SearchResult.AllTextContentsAsync() as IEnumerable<string>;

        // Assert
        Assert.All(results, result => Assert.Contains(phrase, result, StringComparison.InvariantCultureIgnoreCase));
    }
}
