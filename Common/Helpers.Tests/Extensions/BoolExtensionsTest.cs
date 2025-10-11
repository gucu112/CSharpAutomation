using Gucu112.CSharp.Automation.Helpers.Extensions;

namespace Gucu112.CSharp.Automation.Helpers.Tests.Extensions;

[TestFixture]
public class BoolExtensionsTest : BaseTest
{
    [TestCase(true, ExpectedResult = "Yes")]
    [TestCase(false, ExpectedResult = "No")]
    public string ToLocalizedString_DoesReturnRegular(bool value)
    {
        return BoolExtensions.ToLocalizedString(value);
    }

    [TestCase(null, ExpectedResult = null)]
    [TestCase(true, ExpectedResult = "Yes")]
    [TestCase(false, ExpectedResult = "No")]
    public string? ToLocalizedString_DoesReturnNullable(bool? value)
    {
        return BoolExtensions.ToLocalizedString(value);
    }
}
