using Gucu112.PlaywrightXunitParallel.Fixtures;

namespace Gucu112.PlaywrightXunitParallel.Tests.Core;

[Collection(nameof(SettingsFixture))]
public class PlaywrightTest : IClassFixture<PlaywrightFixture>
{
    private readonly PlaywrightFixture playwright;

    public PlaywrightTest(PlaywrightFixture fixture)
    {
        playwright = fixture;
    }

    [Fact]
    public void VerifyThatBrowserIsConnected()
    {
        Assert.True(playwright.Browser.IsConnected, "Browser is not connected");
        Assert.Equal("chromium", playwright.Browser.BrowserType.Name);
    }
}
