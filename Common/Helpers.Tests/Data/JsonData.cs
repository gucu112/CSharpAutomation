namespace Gucu112.CSharp.Automation.Helpers.Tests.Data;

public static class JsonData
{
    #region EmptyContent

    public static IEnumerable EmptyContent => new List<TestCaseData>
    {
        EmptyStringContent,
        EmptyStringReaderContent
    };

    private static TestCaseData EmptyStringContent => new(StringData.EmptyString) { TypeArgs = [typeof(string)] };

    private static TestCaseData EmptyStringReaderContent => new(new StringReader(StringData.EmptyString)) { TypeArgs = [typeof(StringReader)] };

    #endregion

    #region WhitespaceContent

    public static IEnumerable WhitespaceContent => new List<TestCaseData>
    {
        WhitespaceStringContent,
        WhitespaceStringReaderContent
    };

    private static TestCaseData WhitespaceStringContent => new(WhitespaceData.MultipleRegularSpaces) { TypeArgs = [typeof(string)] };

    private static TestCaseData WhitespaceStringReaderContent => new(new StringReader(WhitespaceData.MultipleRegularSpaces)) { TypeArgs = [typeof(StringReader)] };

    #endregion

    #region EmptyArray

    public static IEnumerable EmptyArray => new List<TestCaseData>
    {
        EmptyArrayStringContent,
        EmptyArrayStringReaderContent
    };

    private const string EmptyArrayString = "[]";

    private static TestCaseData EmptyArrayStringContent => new(EmptyArrayString) { TypeArgs = [typeof(string)] };

    private static TestCaseData EmptyArrayStringReaderContent => new(new StringReader(EmptyArrayString)) { TypeArgs = [typeof(StringReader)] };

    #endregion

    #region EmptyObject

    public static IEnumerable EmptyObject => new List<TestCaseData>
    {
        EmptyObjectStringContent,
        EmptyObjectStringReaderContent
    };

    private const string EmptyObjectString = "{}";

    private static TestCaseData EmptyObjectStringContent => new(EmptyObjectString) { TypeArgs = [typeof(string)] };

    private static TestCaseData EmptyObjectStringReaderContent => new(new StringReader(EmptyObjectString)) { TypeArgs = [typeof(StringReader)] };

    #endregion

    #region SimpleList

    public static IEnumerable SimpleList => new List<TestCaseData>
    {
        SimpleListStringContent,
        SimpleListStringReaderContent
    };

    private const string SimpleListString = "[1, 2, 3]";

    private static TestCaseData SimpleListStringContent => new(SimpleListString) { TypeArgs = [typeof(string)] };

    private static TestCaseData SimpleListStringReaderContent => new(new StringReader(SimpleListString)) { TypeArgs = [typeof(StringReader)] };

    #endregion

    #region SimpleDictionary

    public static IEnumerable SimpleDictionary => new List<TestCaseData>
    {
        SimpleDictionaryStringContent,
        SimpleDictionaryStringReaderContent
    };

    private const string SimpleDictionaryString = """
        {
            "one": 1,
            "two": 2,
            "three": 3
        }
        """;

    private static TestCaseData SimpleDictionaryStringContent => new(SimpleDictionaryString) { TypeArgs = [typeof(string)] };

    private static TestCaseData SimpleDictionaryStringReaderContent => new(new StringReader(SimpleDictionaryString)) { TypeArgs = [typeof(StringReader)] };

    #endregion

    #region SimpleObject

    public static IEnumerable SimpleObject => new List<TestCaseData>
    {
        SimpleObjectStringContent,
        SimpleObjectStringReaderContent
    };

    public class SimpleObjectModel
    {
        public string? InvalidProperty;

        public bool? BooleanValue;

        public List<int>? ListOfNumbers;

        public Dictionary<string, string>? DictionaryOfStrings;
    }

    private const string SimpleObjectString = """
        {
            /* This is example JSON */
            "booleanValue": true,
            "listOfNumbers": [9,8,7,6,5],
            "dictionaryOfStrings": {
                "empty": "",
                "number": "007",
                "string": "Test",
            }
        }
        """;

    private static TestCaseData SimpleObjectStringContent => new(SimpleObjectString) { TypeArgs = [typeof(string)] };

    private static TestCaseData SimpleObjectStringReaderContent => new(new StringReader(SimpleObjectString)) { TypeArgs = [typeof(StringReader)] };

    #endregion
}
