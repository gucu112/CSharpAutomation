using Gucu112.CSharp.Automation.PlaywrightXunitParallel.Models.Attribute;
using Gucu112.CSharp.Automation.PlaywrightXunitParallel.Models.Enum;

namespace Gucu112.CSharp.Automation.PlaywrightXunitParallel.Orderers.TestCase;

/// <summary>
/// Represents an orderer that orders test cases by their priority.
/// </summary>
public class PriorityOrderer : ITestCaseOrderer
{
    /// <summary>
    /// Represents the assembly name in which the orderer is defined.
    /// </summary>
    public const string AssemblyName = "PlaywrightXunitParallel";

    /// <summary>
    /// Represents the full type name of the orderer.
    /// </summary>
    public const string FullTypeName = $"Gucu112.CSharp.Automation.{AssemblyName}.Orderers.TestCase.{nameof(PriorityOrderer)}";

    /// <summary>
    /// Orders the test cases by their priority.
    /// </summary>
    /// <typeparam name="TTestCase">The type of the test case.</typeparam>
    /// <param name="testCases">The test cases to order.</param>
    /// <returns>The ordered test cases.</returns>
    public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases)
        where TTestCase : ITestCase
    {
        var priorityDictionary = new Dictionary<TestPriority, IList<TTestCase>>();

        foreach (var testCase in testCases)
        {
            var attribute = testCase.TestMethod.Method.GetCustomAttributes(typeof(PriorityAttribute)).SingleOrDefault()
                ?? testCase.TestMethod.TestClass.Class.GetCustomAttributes(typeof(PriorityAttribute)).SingleOrDefault();

            var priority = attribute?.GetNamedArgument<TestPriority>("Priority") ?? TestPriority.Trivial;

            if (priorityDictionary.TryGetValue(priority, out var testCasesList))
            {
                testCasesList.Add(testCase);
                continue;
            }

            priorityDictionary.Add(priority, [testCase]);
        }

        return priorityDictionary.OrderBy(pair => pair.Key).SelectMany(pair => pair.Value).ToList();
    }
}
