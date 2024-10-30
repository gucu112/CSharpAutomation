using System.Text.RegularExpressions;
using Gucu112.CSharp.Automation.Helpers.Stores;
using Gucu112.CSharp.Automation.Helpers.Tests.Data;

namespace Gucu112.CSharp.Automation.Helpers.Tests.Stores;

[TestFixture]
public class RegexStoreTest
{
    #region AnyWhitespaceRegex

    private static readonly Regex anyWhitespaceRegex = RegexStore.AnyWhitespaceRegex();

    [Test]
    public void AnyWhitespaceRegex_IsInstanceOfRegex()
    {
        Assert.That(anyWhitespaceRegex, Is.InstanceOf<Regex>());
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
        Assert.That(input, Does.Match(anyWhitespaceRegex));
    }

    [TestCase(StringData.EmptyString)]
    [TestCase(StringData.HelloString)]
    [TestCaseSource(typeof(StringData), nameof(StringData.ShortWordsWithoutSpaces))]
    public void AnyWhitespaceRegex_DoesNotMatch(string input)
    {
        Assert.That(input, Does.Not.Match(anyWhitespaceRegex));
    }

    #endregion

    #region NormalizeSpaceRegex

    private static readonly Regex normalizeSpaceRegex = RegexStore.NormalizeSpaceRegex();

    [Test]
    public void NormalizeSpaceRegex_IsInstanceOfRegex()
    {
        Assert.That(normalizeSpaceRegex, Is.InstanceOf<Regex>());
    }

    [TestCase(WhitespaceData.MultipleRegularSpaces)]
    [TestCaseSource(typeof(WhitespaceData), nameof(WhitespaceData.ExceptSingleRegularSpace))]
    [TestCaseSource(typeof(WhitespaceData), nameof(WhitespaceData.InBetweenSingleRegularSpaces))]
    [TestCaseSource(typeof(StringData), nameof(StringData.ShortWordsWithMultipleSpaces))]
    [TestCaseSource(typeof(StringData), nameof(StringData.ShortWordsWithMultipleWhitespaces))]
    public void NormalizeSpaceRegex_DoesMatch(string input)
    {
        Assert.That(input, Does.Match(normalizeSpaceRegex));
    }

    [TestCase(StringData.EmptyString)]
    [TestCase(StringData.HelloString)]
    [TestCaseSource(typeof(WhitespaceData), nameof(WhitespaceData.SingleRegularSpace))]
    [TestCaseSource(typeof(StringData), nameof(StringData.ShortWordsWithSingleRegularSpaces))]
    [TestCaseSource(typeof(StringData), nameof(StringData.ShortWordsWithoutSpaces))]
    public void NormalizeSpaceRegex_DoesNotMatch(string input)
    {
        Assert.That(input, Does.Not.Match(normalizeSpaceRegex));
    }

    #endregion
}
