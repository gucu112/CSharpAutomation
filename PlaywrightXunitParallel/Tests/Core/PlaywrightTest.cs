﻿using FluentAssertions;
using Gucu112.PlaywrightXunitParallel.Fixtures;
using Gucu112.PlaywrightXunitParallel.Models.Attribute;
using Gucu112.PlaywrightXunitParallel.Models.Enum;
using Gucu112.PlaywrightXunitParallel.Orderers.TestCase;
using Gucu112.PlaywrightXunitParallel.Pages;

namespace Gucu112.PlaywrightXunitParallel.Tests.Core;

[Collection(nameof(SettingsFixture))]
[TestCaseOrderer(PriorityOrderer.FullTypeName, PriorityOrderer.AssemblyName)]

[Category(TestCategory.Browser)]
public class PlaywrightTest(
    SettingsFixture settings,
    PlaywrightFixture playwright
) : IClassFixture<PlaywrightFixture>
{
    [Fact]
    [Priority(TestPriority.Critical)]
    public void VerifyThatBrowserDoesExist()
    {
        playwright.Browser.Should().NotBeNull();
        playwright.Browser.IsConnected.Should().BeTrue();
        playwright.Browser.BrowserType.Name.Should().Be("chromium");
    }

    [Fact]
    [Category(TestCategory.Settings)]
    [Priority(TestPriority.Medium)]
    public void VerifyThatLaunchOptionsAreInitalized()
    {
        playwright.LaunchOptions.Should().NotBeNull();
        playwright.LaunchOptions.Channel.Should().Be("msedge");
    }

    [Fact]
    [Priority(TestPriority.High)]
    public void VerifyThatBrowserContextDoesExist()
    {
        using var page = new BlankPage(settings, playwright);

        page.BrowserContext.Should().NotBeNull();
        page.BrowserContext.Browser?.BrowserType.Name.Should().Be("chromium");
    }

    [Fact]
    [Category(TestCategory.Settings)]
    [Priority(TestPriority.Low)]
    public void VerifyThatBrowserOptionsAreInitialized()
    {
        using var page = new BlankPage(settings, playwright);

        page.BrowserOptions.Should().NotBeNull();
        page.BrowserOptions.Offline.Should().BeFalse();
    }

    [Fact]
    [Priority(TestPriority.High)]
    public void VerifyThatPageDoesExist()
    {
        using var page = new BlankPage(settings, playwright);

        page.Context.Should().NotBeNull();
        page.Context.Url.Should().Be("about:blank");
    }

    [Fact]
    [Category(TestCategory.Settings)]
    [Priority(TestPriority.High)]
    public async Task VerifyThatPageTimeoutWorksCorrect()
    {
        using var page = new BlankPage(settings, playwright);
        var action = async () => await page.Context.GetByTestId("unknow").ClickAsync();

        await page.Context.GotoAsync("chrome://about");

        var exception = await action.Should().ThrowExactlyAsync<TimeoutException>();
        exception.Which.Message.Should().Contain($"Timeout {settings.ExpectTimeout}ms exceeded");
    }

    private class BlankPage : BasePage, IDisposable
    {
        public BlankPage(
            SettingsFixture settings,
            PlaywrightFixture playwright
        ) : base(settings, playwright)
        {
            Task.Run(InitializeAsync).Wait();
        }

        public void Dispose()
        {
            Task.Run(DisposeAsync).Wait();
        }
    }
}
