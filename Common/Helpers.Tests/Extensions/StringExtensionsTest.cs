using Gucu112.CSharp.Automation.Helpers.Extensions;
using Gucu112.CSharp.Automation.Helpers.Tests.Data;

namespace Gucu112.CSharp.Automation.Helpers.Tests.Extensions;

[TestFixture]
public class StringExtensionsTest
{
    #region GetBytes

    [Test]
    public void GetBytes_ThrowsOnNull()
    {
        Assert.Throws<ArgumentNullException>(() => StringExtensions.GetBytes(null!));
    }

    [TestCase(StringData.EmptyString)]
    public void GetBytes_DoesReturnEmpty(string input)
    {
        Assert.That(StringExtensions.GetBytes(input), Is.Empty);
    }

    [TestCase(WhitespaceData.MultipleRegularSpaces)]
    [TestCaseSource(typeof(WhitespaceData), nameof(WhitespaceData.SingleRegularSpace))]
    public void GetBytes_DoesHaveAllCorrectBytes(string input)
    {
        Assert.That(StringExtensions.GetBytes(input), Has.All.EqualTo(32));
    }

    [TestCase(StringData.HelloString, ExpectedResult = new byte[] { 104, 101, 108, 108, 111 })]
    public byte[] GetBytes_WithDefaultEncoding_DoesReturnCorrectBytes(string input)
    {
        return StringExtensions.GetBytes(input);
    }

    [TestCase(StringData.HelloString, ExpectedResult = new byte[] { 104, 101, 108, 108, 111 })]
    public byte[] GetBytes_WithUnicodeEncoding_DoesReturnCorrectBytes(string input)
    {
        var groupedBytes = StringExtensions.GetBytes(input, Encoding.Unicode)
            .Select((code, index) => (Byte: code, Index: index))
            .GroupBy(t => t.Index / 2).Select(g => g.Select(t => t.Byte));

        Assert.That(groupedBytes.Select(b => b.Last()), Has.All.Zero);
        return groupedBytes.Select(b => b.First()).ToArray();
    }

    #endregion

    #region NormalizeSpace

    [Test]
    public void NormalizeSpace_ThrowsOnNull()
    {
        Assert.Throws<ArgumentNullException>(() => StringExtensions.NormalizeSpace(null!));
    }

    [TestCase(StringData.EmptyString)]
    [TestCase(WhitespaceData.MultipleRegularSpaces)]
    [TestCaseSource(typeof(WhitespaceData), nameof(WhitespaceData.SingleRegularSpace))]
    [TestCaseSource(typeof(WhitespaceData), nameof(WhitespaceData.ExceptSingleRegularSpace))]
    [TestCaseSource(typeof(WhitespaceData), nameof(WhitespaceData.InBetweenSingleRegularSpaces))]
    public void NormalizeSpace_DoesReturnEmptyString(string input)
    {
        Assert.That(StringExtensions.NormalizeSpace(input), Is.Empty);
    }

    [TestCaseSource(typeof(StringData), nameof(StringData.ShortWordsWithoutSpaces))]
    [TestCaseSource(typeof(StringData), nameof(StringData.ShortWordsWithSingleRegularSpaces))]
    [TestCaseSource(typeof(StringData), nameof(StringData.ShortWordsWithMultipleWhitespaces))]
    public void NormalizeSpace_DoesNotStartOrEndWithWhitespace(string input)
    {
        Assert.That(StringExtensions.NormalizeSpace(input), Does.Not.Match(@"^\s"));
        Assert.That(StringExtensions.NormalizeSpace(input), Does.Not.Match(@"\s$"));
    }

    [TestCaseSource(typeof(StringData), nameof(StringData.ShortWordsWithSingleRegularSpaces))]
    [TestCaseSource(typeof(StringData), nameof(StringData.ShortWordsWithMultipleSpaces))]
    [TestCaseSource(typeof(StringData), nameof(StringData.ShortWordsWithMultipleWhitespaces))]
    public void NormalizeSpace_DoesNotContainMultipleConsecutiveWhitespaces(string input)
    {
        Assert.That(StringExtensions.NormalizeSpace(input), Does.Not.Match(@"\s{2,}"));
    }

    #endregion

    #region RemoveSpace

    [Test]
    public void RemoveSpace_ThrowsOnNull()
    {
        Assert.Throws<ArgumentNullException>(() => StringExtensions.RemoveSpace(null!));
    }

    [TestCase(StringData.EmptyString)]
    [TestCase(WhitespaceData.MultipleRegularSpaces)]
    [TestCaseSource(typeof(WhitespaceData), nameof(WhitespaceData.SingleRegularSpace))]
    [TestCaseSource(typeof(WhitespaceData), nameof(WhitespaceData.ExceptSingleRegularSpace))]
    [TestCaseSource(typeof(WhitespaceData), nameof(WhitespaceData.InBetweenSingleRegularSpaces))]
    public void RemoveSpace_DoesReturnEmptyString(string input)
    {
        Assert.That(StringExtensions.RemoveSpace(input), Is.Empty);
    }

    [TestCaseSource(typeof(StringData), nameof(StringData.ShortWordsWithoutSpaces))]
    [TestCaseSource(typeof(StringData), nameof(StringData.ShortWordsWithSingleRegularSpaces))]
    [TestCaseSource(typeof(StringData), nameof(StringData.ShortWordsWithMultipleSpaces))]
    [TestCaseSource(typeof(StringData), nameof(StringData.ShortWordsWithMultipleWhitespaces))]
    public void RemoveSpace_DoesNotContainSpaces(string input)
    {
        Assert.That(StringExtensions.RemoveSpace(input), Does.Not.Contain(@" "));
    }

    #endregion
}
