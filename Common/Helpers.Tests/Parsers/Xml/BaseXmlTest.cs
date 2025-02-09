using Gucu112.CSharp.Automation.Helpers.Parsers;

namespace Gucu112.CSharp.Automation.Helpers.Tests.Parsers.Xml;

/// <summary>
/// Base class for XML parsing tests.
/// </summary>
public class BaseXmlTest
{
    /// <summary>
    /// Parses a value of any type to XML input.
    /// </summary>
    /// <typeparam name="T">The type to parse into.</typeparam>
    /// <param name="input">The input XML.</param>
    /// <returns>The parsed object.</returns>
    protected static T? ParseFromXml<T>(dynamic? input)
        where T : class
    {
        return Parse.FromXml<T>(input);
    }
}
