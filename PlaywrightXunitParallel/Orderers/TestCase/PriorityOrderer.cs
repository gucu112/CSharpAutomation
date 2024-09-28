using Gucu112.CSharp.Automation.PlaywrightXunitParallel.Models.Attribute;
using Gucu112.CSharp.Automation.PlaywrightXunitParallel.Models.Enum;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Gucu112.CSharp.Automation.PlaywrightXunitParallel.Orderers.TestCase;

public class PriorityOrderer : ITestCaseOrderer
{
    public const string FullTypeName = $"Gucu112.CSharp.Automation.{AssemblyName}.Orderers.TestCase.{nameof(PriorityOrderer)}";
    public const string AssemblyName = "PlaywrightXunitParallel";

    public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
    {
        var priorityDictionary = new Dictionary<TestPriority, IList<TTestCase>>();

        foreach (var testCase in testCases)
        {
            var attribute = testCase.TestMethod.Method.GetCustomAttributes(typeof(PriorityAttribute)).SingleOrDefault()
                ?? testCase.TestMethod.TestClass.Class.GetCustomAttributes(typeof(PriorityAttribute)).SingleOrDefault();

            var priority = attribute?.GetNamedArgument<TestPriority>("Priority") ?? TestPriority.Trivial;

            if (!priorityDictionary.ContainsKey(priority))
            {
                priorityDictionary.Add(priority, [testCase]);
            }
            else
            {
                priorityDictionary.GetValueOrDefault(priority)?.Add(testCase);
            }
        }

        return priorityDictionary.OrderBy(pair => pair.Key).SelectMany(pair => pair.Value).ToList();
    }
}
