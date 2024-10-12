using System.Text.RegularExpressions;
using Gucu112.CSharp.Automation.Helpers.Stores;
using Gucu112.CSharp.Automation.Helpers.Tests.Data;

namespace Gucu112.CSharp.Automation.Helpers.Tests.Stores;

[TestFixture]
public class RegexStoreTest
{
    private static readonly Regex normalizeSpaceRegex = RegexStore.NormalizeSpaceRegex();

    [Test]
    public void IsInstanceOfRegex()
    {
        Assert.That(normalizeSpaceRegex, Is.InstanceOf<Regex>());
    }

    [TestCaseSource(typeof(WhitespaceData), nameof(WhitespaceData.ExceptSingleRegularSpace))]
    [TestCaseSource(typeof(WhitespaceData), nameof(WhitespaceData.InBetweenSingleRegularSpaces))]
    [TestCaseSource(typeof(StringData), nameof(StringData.ShortWordsWithMultipleSpaces))]
    [TestCaseSource(typeof(StringData), nameof(StringData.ShortWordsWithMultipleWhitespaces))]
    public void DoesMatch(string input)
    {
        Assert.That(input, Does.Match(normalizeSpaceRegex));
    }

    [TestCase(StringData.EmptyString)]
    [TestCaseSource(typeof(WhitespaceData), nameof(WhitespaceData.SingleRegularSpace))]
    [TestCaseSource(typeof(StringData), nameof(StringData.ShortWordsWithSingleRegularSpaces))]
    [TestCaseSource(typeof(StringData), nameof(StringData.ShortWordsWithoutSpaces))]
    public void DoesNotMatch(string input)
    {
        Assert.That(input, Does.Not.Match(normalizeSpaceRegex));
    }
}
