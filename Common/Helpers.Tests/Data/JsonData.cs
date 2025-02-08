using Gucu112.CSharp.Automation.Helpers.Extensions;

namespace Gucu112.CSharp.Automation.Helpers.Tests.Data;

public static class JsonData
{
    public const string EmptyJsonString = @"""""";

    public const string HelloJsonString = @"""hello""";

    public const string EmptyArrayString = "[]";

    public const string ValidArrayString = "[true]";

    public const string EmptyObjectString = "{}";

    public const string InvalidObjectString = "false{}";

    public const string SimpleListString = "[ 1, 2, 3 ]";

    public const string SimpleDictionaryString = """
        {
            "First": 1,
            "Second": 2,
            "Third": 3
        }
        """;

    public const string SimpleObjectString = """
        {
            "InvalidProperty": null,
            "BooleanValue": false,
            "ListOfNumbers": [
                1,
                2,
                3
            ],
            "DictionaryOfStrings": {
                "Empty": ""
            },
            "CurrentYearStart": "2025-01-01T00:00:00"
        }
        """;

    public const string SimpleObjectStringWithComment = """
        {
            /* This is example JSON */
            "booleanValue": true,
            "listOfNumbers": [9,8,7,6,5],
            "dictionaryOfStrings": {
                "empty": "",
                "number": "007",
                "string": "Test"
            },
            "currentYearStart": "2025-01-01T00:00:00.000Z"
        }
        """;

    public static IEnumerable EmptyContent
    {
        get
        {
            yield return new TestCaseData(StringData.EmptyString)
                .SetArgDisplayNames("EmptyString");
            yield return new TestCaseData(new StringReader(StringData.EmptyString))
                .SetArgDisplayNames("EmptyStringReader");
            yield return new TestCaseData(new MemoryStream(StringData.EmptyString.GetBytes()))
                .SetArgDisplayNames("EmptyStringStream");
            yield return new TestCaseData(WhitespaceData.MultipleRegularSpaces)
                .SetArgDisplayNames("WhitespaceString");
            yield return new TestCaseData(new StringReader(WhitespaceData.MultipleRegularSpaces))
                .SetArgDisplayNames("WhitespaceStringReader");
            yield return new TestCaseData(new MemoryStream(WhitespaceData.MultipleRegularSpaces.GetBytes()))
                .SetArgDisplayNames("WhitespaceStringStream");
        }
    }

    public static IEnumerable EmptyArray
    {
        get
        {
            yield return new TestCaseData(EmptyArrayString)
                .SetArgDisplayNames("EmptyArrayString");
            yield return new TestCaseData(new StringReader(EmptyArrayString))
                .SetArgDisplayNames("EmptyArrayStringReader");
            yield return new TestCaseData(new MemoryStream(EmptyArrayString.GetBytes()))
                .SetArgDisplayNames("EmptyArrayStringStream");
        }
    }

    public static IEnumerable EmptyObject
    {
        get
        {
            yield return new TestCaseData(EmptyObjectString)
                .SetArgDisplayNames("EmptyObjectString");
            yield return new TestCaseData(new StringReader(EmptyObjectString))
                .SetArgDisplayNames("EmptyObjectStringReader");
            yield return new TestCaseData(new MemoryStream(EmptyObjectString.GetBytes()))
                .SetArgDisplayNames("EmptyObjectStringStream");
        }
    }

    public static IEnumerable SimpleList
    {
        get
        {
            yield return new TestCaseData(SimpleListString)
                .SetArgDisplayNames("SimpleListString");
            yield return new TestCaseData(new StringReader(SimpleListString))
                .SetArgDisplayNames("SimpleListStringReader");
            yield return new TestCaseData(new MemoryStream(SimpleListString.GetBytes()))
                .SetArgDisplayNames("SimpleListStringStream");
        }
    }

    public static IEnumerable SimpleDictionary
    {
        get
        {
            yield return new TestCaseData(SimpleDictionaryString)
                .SetArgDisplayNames("SimpleDictionaryString");
            yield return new TestCaseData(new StringReader(SimpleDictionaryString))
                .SetArgDisplayNames("SimpleDictionaryStringReader");
            yield return new TestCaseData(new MemoryStream(SimpleDictionaryString.GetBytes()))
                .SetArgDisplayNames("SimpleDictionaryStringStream");
        }
    }

    public static IEnumerable SimpleObject
    {
        get
        {
            yield return new TestCaseData(SimpleObjectStringWithComment)
                .SetArgDisplayNames("SimpleObjectString");
            yield return new TestCaseData(new StringReader(SimpleObjectStringWithComment))
                .SetArgDisplayNames("SimpleObjectStringReader");
            yield return new TestCaseData(new MemoryStream(SimpleObjectStringWithComment.GetBytes()))
                .SetArgDisplayNames("SimpleObjectStringStream");
        }
    }

    public class SimpleObjectModel
    {
        public string? InvalidProperty { get; init; }

        public bool? BooleanValue { get; init; }

        public List<int> ListOfNumbers { get; init; } = [0];

        public Dictionary<string, string>? DictionaryOfStrings { get; init; }

        public DateTime? CurrentYearStart { get; init; }
    }
}
