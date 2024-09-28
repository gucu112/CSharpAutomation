namespace Gucu112.PlaywrightXunit.Tests;

[Trait("Category", "Results")]
public class TestResults(ITestOutputHelper output)
{
    [Fact]
    public void TestPass()
    {
        Assert.True(true);
        output.WriteLine("Test has passed");
    }

    [Fact]
    public void TestFail()
    {
        output.WriteLine("Test is about to fail");
        Assert.Fail();
    }

    [Fact(Skip = "Test is skipped using Fact")]
    public void TestSkip()
    {
    }

    [SkippableFact]
    public void TestSkippable()
    {
        Skip.If(true, "Test is skipped using SkippableFact");
    }

    [Fact(Timeout = 1000)]
    public async Task TestTimeout()
    {
        output.WriteLine("Test is about to wait 3000 milliseconds");
        await Task.Delay(3000);
    }
}
