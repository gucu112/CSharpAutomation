namespace Gucu112.PlaywrightXunit.Tests;

public class TestResults
{
    private readonly ITestOutputHelper output;

    public TestResults(ITestOutputHelper output)
    {
        this.output = output;
    }

    [Fact]
    public void TestPasses()
    {
        Assert.True(true);
        output.WriteLine("Test have passed");
    }

    [Fact]
    public void TestFails()
    {
        output.WriteLine("Test is about to fail");
        Assert.Fail();
    }

    [Fact(Skip = "Test is skipped using Fact")]
    public void TestIsSkipped()
    {
    }

    [SkippableFact]
    public void TestIsSkippable()
    {
        Skip.If(true, "Test is skipped using SkippableFact");
    }
}
