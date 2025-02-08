using Gucu112.CSharp.Automation.Helpers.Parsers;

namespace Gucu112.CSharp.Automation.Helpers.Tests.Parsers.Xml;

public class BaseXmlTest
{
    protected static T? ParseFromXml<T>(dynamic? input)
        where T : class
    {
        return Parse.FromXml<T>(input);
    }
}
