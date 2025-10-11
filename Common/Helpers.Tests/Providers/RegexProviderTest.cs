using System.Text.RegularExpressions;
using Gucu112.CSharp.Automation.Helpers.Providers;
using Gucu112.CSharp.Automation.Helpers.Tests.Data;

namespace Gucu112.CSharp.Automation.Helpers.Tests.Providers;

[TestFixture]
public class RegexProviderTest : BaseTest
{
    private static readonly Regex AnyWhitespaceRegex = RegexProvider.AnyWhitespaceRegex();

    private static readonly Regex NormalizeSpaceRegex = RegexProvider.NormalizeSpaceRegex();

    [Test]
    public void AnyWhitespaceRegex_IsInstanceOfRegex()
    {
        Assert.That(AnyWhitespaceRegex, Is.InstanceOf<Regex>());
    }

    [TestCase(WhitespaceData.MultipleRegularSpaces)]
    [TestCaseSource(typeof(WhitespaceData), nameof(WhitespaceData.SingleRegularSpace))]
    [TestCaseSource(typeof(WhitespaceData), nameof(WhitespaceData.ExceptSingleRegularSpace))]
    [TestCaseSource(typeof(WhitespaceData), nameof(WhitespaceData.InBetweenSingleRegularSpaces))]
    [TestCaseSource(typeof(StringData), nameof(StringData.ShortWordsWithSingleRegularSpaces))]
    [TestCaseSource(typeof(StringData), nameof(StringData.ShortWordsWithMultipleSpaces))]
    [TestCaseSource(typeof(StringData), nameof(StringData.ShortWordsWithMultipleWhitespaces))]
    public void AnyWhitespaceRegex_DoesMatch(string input)
    {
        Assert.That(input, Does.Match(AnyWhitespaceRegex));
    }

    [TestCase(StringData.EmptyString)]
    [TestCase(StringData.HelloString)]
    [TestCaseSource(typeof(StringData), nameof(StringData.ShortWordsWithoutSpaces))]
    public void AnyWhitespaceRegex_DoesNotMatch(string input)
    {
        Assert.That(input, Does.Not.Match(AnyWhitespaceRegex));
    }

    [Test]
    public void NormalizeSpaceRegex_IsInstanceOfRegex()
    {
        Assert.That(NormalizeSpaceRegex, Is.InstanceOf<Regex>());
    }

    [TestCase(WhitespaceData.MultipleRegularSpaces)]
    [TestCaseSource(typeof(WhitespaceData), nameof(WhitespaceData.ExceptSingleRegularSpace))]
    [TestCaseSource(typeof(WhitespaceData), nameof(WhitespaceData.InBetweenSingleRegularSpaces))]
    [TestCaseSource(typeof(StringData), nameof(StringData.ShortWordsWithMultipleSpaces))]
    [TestCaseSource(typeof(StringData), nameof(StringData.ShortWordsWithMultipleWhitespaces))]
    public void NormalizeSpaceRegex_DoesMatch(string input)
    {
        Assert.That(input, Does.Match(NormalizeSpaceRegex));
    }

    [TestCase(StringData.EmptyString)]
    [TestCase(StringData.HelloString)]
    [TestCaseSource(typeof(WhitespaceData), nameof(WhitespaceData.SingleRegularSpace))]
    [TestCaseSource(typeof(StringData), nameof(StringData.ShortWordsWithSingleRegularSpaces))]
    [TestCaseSource(typeof(StringData), nameof(StringData.ShortWordsWithoutSpaces))]
    public void NormalizeSpaceRegex_DoesNotMatch(string input)
    {
        Assert.That(input, Does.Not.Match(NormalizeSpaceRegex));
    }
}
