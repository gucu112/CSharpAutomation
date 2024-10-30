using Gucu112.CSharp.Automation.Helpers.Extensions;

namespace Gucu112.CSharp.Automation.Helpers.Tests.Data;

public static class JsonData
{
    public const string EmptyJsonString = @"""""";

    public const string EmptyArrayString = "[]";

    public const string ValidArrayString = "[true]";

    public const string EmptyObjectString = "{}";

    public const string InvalidObjectString = "false{}";

    public const string SimpleListString = "[1, 2, 3]";

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
            "ListOfNumbers": [1, 2, 3],
            "DictionaryOfStrings": {
                "Empty": ""
            }
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
            }
        }
        """;

    public static IEnumerable EmptyContent
    {
        get
        {
            yield return new TestCaseData(StringData.EmptyString);
            yield return new TestCaseData(new StringReader(StringData.EmptyString));
            yield return new TestCaseData(new MemoryStream(StringData.EmptyString.GetBytes()));
        }
    }

    public static IEnumerable WhitespaceContent
    {
        get
        {
            yield return new TestCaseData(WhitespaceData.MultipleRegularSpaces);
            yield return new TestCaseData(new StringReader(WhitespaceData.MultipleRegularSpaces));
            yield return new TestCaseData(new MemoryStream(WhitespaceData.MultipleRegularSpaces.GetBytes()));
        }
    }

    public static IEnumerable EmptyArray
    {
        get
        {
            yield return new TestCaseData(EmptyArrayString);
            yield return new TestCaseData(new StringReader(EmptyArrayString));
            yield return new TestCaseData(new MemoryStream(EmptyArrayString.GetBytes()));
        }
    }

    public static IEnumerable EmptyObject
    {
        get
        {
            yield return new TestCaseData(EmptyObjectString);
            yield return new TestCaseData(new StringReader(EmptyObjectString));
            yield return new TestCaseData(new MemoryStream(EmptyObjectString.GetBytes()));
        }
    }

    public static IEnumerable SimpleList
    {
        get
        {
            yield return new TestCaseData(SimpleListString);
            yield return new TestCaseData(new StringReader(SimpleListString));
            yield return new TestCaseData(new MemoryStream(SimpleListString.GetBytes()));
        }
    }

    public static IEnumerable SimpleDictionary
    {
        get
        {
            yield return new TestCaseData(SimpleDictionaryString);
            yield return new TestCaseData(new StringReader(SimpleDictionaryString));
            yield return new TestCaseData(new MemoryStream(SimpleDictionaryString.GetBytes()));
        }
    }

    public static IEnumerable SimpleObject
    {
        get
        {
            yield return new TestCaseData(SimpleObjectStringWithComment);
            yield return new TestCaseData(new StringReader(SimpleObjectStringWithComment));
            yield return new TestCaseData(new MemoryStream(SimpleObjectStringWithComment.GetBytes()));
        }
    }

    public class SimpleObjectModel
    {
        public string? InvalidProperty;

        public bool? BooleanValue;

        public List<int>? ListOfNumbers;

        public Dictionary<string, string>? DictionaryOfStrings;
    }
}
