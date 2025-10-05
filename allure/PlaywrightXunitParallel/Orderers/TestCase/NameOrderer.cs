namespace Gucu112.CSharp.Automation.PlaywrightXunitParallel.Orderers.TestCase;

/// <summary>
/// Represents an orderer that orders test cases by their display name.
/// </summary>
public class NameOrderer : ITestCaseOrderer
{
    /// <summary>
    /// Represents the assembly name in which the orderer is defined.
    /// </summary>
    public const string AssemblyName = "PlaywrightXunitParallel";

    /// <summary>
    /// Represents the full type name of the orderer.
    /// </summary>
    public const string FullTypeName = $"Gucu112.CSharp.Automation.{AssemblyName}.Orderers.TestCase.{nameof(NameOrderer)}";

    /// <summary>
    /// Orders the test cases by their display name.
    /// </summary>
    /// <typeparam name="TTestCase">The type of the test case.</typeparam>
    /// <param name="testCases">The test cases to order.</param>
    /// <returns>The ordered test cases.</returns>
    public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases)
        where TTestCase : ITestCase
    {
        return testCases.OrderBy(testCase => testCase.DisplayName);
    }
}
