using Gucu112.PlaywrightXunit.Fixtures;

namespace Gucu112.PlaywrightXunit.Tests;

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

    [Theory]
    [InlineData("c")]
    [InlineData("d")]
    [InlineData("e")]
    public async Task VerifyThatSearchResultsContainSearchPhrase(string phrase)
    {
        // Arrange
        await page.SearchInput.FillAsync(phrase);
        await page.Context.Keyboard.PressAsync("Enter");

        // Act
        var results = await page.SearchResult.AllTextContentsAsync() as IEnumerable<string>;

        // Assert
        Assert.All(results, result => Assert.Contains(phrase, result, StringComparison.InvariantCultureIgnoreCase));
    }
}
