namespace Gucu112.CSharp.Automation.Helpers.Tests.Data;

public static class StringData
{
    public const string EmptyString = "";

    public const string HelloString = "hello";

    public const string LoremIpsumString = "Lorem ipsum dolor sit amet";

    public static IEnumerable ShortWordsWithoutSpaces
    {
        get
        {
            yield return new TestCaseData("abc");
            yield return new TestCaseData("123");
            yield return new TestCaseData("VWXYZ");
            yield return new TestCaseData("98765");
            yield return new TestCaseData("Extraordinary");
        }
    }

    public static IEnumerable ShortWordsWithSingleRegularSpaces
    {
        get
        {
            yield return new TestCaseData(" abc");
            yield return new TestCaseData("abc ");
            yield return new TestCaseData(" 123");
            yield return new TestCaseData("123 ");
            yield return new TestCaseData("VWXYZ 98765");
            yield return new TestCaseData(" Big bang theory ");
        }
    }

    public static IEnumerable ShortWordsWithMultipleSpaces
    {
        get
        {
            yield return new TestCaseData("   abc");
            yield return new TestCaseData("abc   ");
            yield return new TestCaseData("  123");
            yield return new TestCaseData("123  ");
            yield return new TestCaseData("VWXYZ  \u00A0  98765");
            yield return new TestCaseData(" Big  bang \u00A0 theory ");
        }
    }

    public static IEnumerable ShortWordsWithMultipleWhitespaces
    {
        get
        {
            yield return new TestCaseData("\t\tabc\v");
            yield return new TestCaseData("\fabc\r\n");
            yield return new TestCaseData(" \t 123 \v ");
            yield return new TestCaseData(" \f 123 \r\n ");
            yield return new TestCaseData("VWXYZ \t \u0085 \v\f 98765");
            yield return new TestCaseData(" Big \u0020 bang \u00A0 theory ");
            yield return new TestCaseData("hard\u00A0\u00A0spaces\u00A0only");
        }
    }
}
