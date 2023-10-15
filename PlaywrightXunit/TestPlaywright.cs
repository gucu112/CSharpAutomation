using System.Reflection;
using Gucu112.PlaywrightXunit.Fixtures;

namespace Gucu112.PlaywrightXunit.Tests;

[Collection(nameof(PlaywrightFixture))]
public class TestPlaywright
{
    private readonly PlaywrightFixture fixture;

    public TestPlaywright(PlaywrightFixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact]
    public void VerifyIfPlaywrightInstanceInitilized()
    {
        // Arrange
        var property = fixture.GetType().GetProperty("Instance",
            BindingFlags.Instance | BindingFlags.NonPublic);

        // Act
        var instance = property?.GetGetMethod(nonPublic: true)?.Invoke(fixture, null);

        // Assert
        Assert.IsAssignableFrom<IPlaywright>(instance);
    }

    [Fact]
    public void VerifyIfBrowserInitilizedAndConnected()
    {
        // Arrange
        var browser = fixture.Browser;

        // Assert
        Assert.IsAssignableFrom<IBrowser>(browser);
        Assert.True(browser.IsConnected);
    }
}
