namespace Gucu112.CSharp.Automation.Helpers.Tests.Data;

public static class WhitespaceData
{
    public static IEnumerable SingleRegularSpace
    {
        get
        {
            yield return new TestCaseData(" ");
            yield return new TestCaseData("\u0020");
        }
    }

    public static IEnumerable ExceptSingleRegularSpace
    {
        get
        {
            yield return new TestCaseData("  ");
            yield return new TestCaseData("\v");
            yield return new TestCaseData("\f");
            yield return new TestCaseData("\t");
            yield return new TestCaseData("\n");
            yield return new TestCaseData("\r");
            yield return new TestCaseData("\r\n");
            yield return new TestCaseData("\n\r");
            yield return new TestCaseData("\u00a0");
            yield return new TestCaseData("\u00A0");
            yield return new TestCaseData("\u0020\u0020");
            yield return new TestCaseData("\u00A0\u00A0");
            yield return new TestCaseData("\u00a0\u00a0");
        }
    }

    public static IEnumerable InBetweenSingleRegularSpace
    {
        get
        {
            yield return new TestCaseData("   ");
            yield return new TestCaseData(" \v ");
            yield return new TestCaseData(" \f ");
            yield return new TestCaseData(" \t ");
            yield return new TestCaseData(" \n ");
            yield return new TestCaseData(" \r ");
            yield return new TestCaseData(" \r\n ");
            yield return new TestCaseData(" \n\r ");
            yield return new TestCaseData(" \t \f \v ");
            yield return new TestCaseData(" \u00a0 ");
            yield return new TestCaseData(" \u00A0 ");
            yield return new TestCaseData(" \u0020 \u0020 ");
            yield return new TestCaseData(" \u00A0 \u00A0 ");
            yield return new TestCaseData(" \u00a0 \u00a0 ");
        }
    }
}
