using Gucu112.CSharp.Automation.Helpers.Extensions;
using Gucu112.CSharp.Automation.Helpers.Tests.Data;

namespace Gucu112.CSharp.Automation.Helpers.Tests.Extensions;

[TestFixture]
public class StringExtensionsTest
{
    [Test]
    public void ThrowsOnNull()
    {
        Assert.Throws<ArgumentNullException>(() => StringExtensions.NormalizeSpace(null!));
    }

    [TestCase(StringData.EmptyString)]
    [TestCaseSource(typeof(WhitespaceData), nameof(WhitespaceData.SingleRegularSpace))]
    [TestCaseSource(typeof(WhitespaceData), nameof(WhitespaceData.ExceptSingleRegularSpace))]
    [TestCaseSource(typeof(WhitespaceData), nameof(WhitespaceData.InBetweenSingleRegularSpace))]
    public void DoesReturnEmptyString(string input)
    {
        Assert.That(StringExtensions.NormalizeSpace(input), Is.Empty);
    }

    [TestCaseSource(typeof(StringData), nameof(StringData.ShortWordsWithoutSpaces))]
    [TestCaseSource(typeof(StringData), nameof(StringData.ShortWordsWithSingleRegularSpaces))]
    [TestCaseSource(typeof(StringData), nameof(StringData.ShortWordsWithMultipleWhitespaces))]
    public void DoesNotStartOrEndWithWhitespace(string input)
    {
        Assert.That(StringExtensions.NormalizeSpace(input), Does.Not.Match(@"^\s"));
        Assert.That(StringExtensions.NormalizeSpace(input), Does.Not.Match(@"\s$"));
    }

    [TestCaseSource(typeof(StringData), nameof(StringData.ShortWordsWithSingleRegularSpaces))]
    [TestCaseSource(typeof(StringData), nameof(StringData.ShortWordsWithMultipleSpaces))]
    [TestCaseSource(typeof(StringData), nameof(StringData.ShortWordsWithMultipleWhitespaces))]
    public void DoesNotContainMultipleConsecutiveWhitespaces(string input)
    {
        Assert.That(StringExtensions.NormalizeSpace(input), Does.Not.Match(@"\s{2,}"));
    }
}
