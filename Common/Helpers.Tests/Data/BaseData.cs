namespace Gucu112.CSharp.Automation.Helpers.Tests.Data;

/// <summary>
/// Base class for test case data creation.
/// </summary>
public class BaseData
{
    /// <summary>
    /// Creates a test case data with the specified input.
    /// </summary>
    /// <typeparam name="TInput">The type of the input.</typeparam>
    /// <param name="input">The actual input value.</param>
    /// <returns>The created test case data.</returns>
    protected static TestCaseData Create<TInput>(object? input)
    {
        return new TestCaseData(input) { TypeArgs = [typeof(TInput)] };
    }

    /// <summary>
    /// Creates a test case data with the specified input and output.
    /// </summary>
    /// <typeparam name="TInput">The type of the input.</typeparam>
    /// <param name="input">The actual input value.</param>
    /// <param name="output">The expected output value.</param>
    /// <returns>The created test case data.</returns>
    protected static TestCaseData Create<TInput>(object? input, object? output)
    {
        return new TestCaseData(input) { TypeArgs = [typeof(TInput)] }.Returns(output);
    }
}
