using Xunit.Abstractions;
using Xunit.Sdk;

namespace Gucu112.CSharp.Automation.PlaywrightXunitParallel.Orderers.TestCase;

public class NameOrderer : ITestCaseOrderer
{
    public const string FullTypeName = $"Gucu112.CSharp.Automation.{AssemblyName}.Orderers.TestCase.{nameof(NameOrderer)}";
    public const string AssemblyName = "PlaywrightXunitParallel";

    public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
    {
        return testCases.OrderBy(testCase => testCase.DisplayName);
    }
}
