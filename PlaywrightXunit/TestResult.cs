namespace Gucu112.PlaywrightXunit;

public class TestResult
{
    private readonly ITestOutputHelper output;

    public TestResult(ITestOutputHelper output)
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
        Assert.True(false);
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
