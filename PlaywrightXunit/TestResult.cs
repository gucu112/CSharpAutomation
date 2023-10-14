namespace Gucu112.PlaywrightXunit;

public class TestResult
{
    [Fact]
    public void TestPasses()
    {
        Assert.True(true);
    }

    [Fact]
    public void TestFails()
    {
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
