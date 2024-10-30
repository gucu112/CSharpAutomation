using Gucu112.CSharp.Automation.Helpers.Extensions;

namespace Gucu112.CSharp.Automation.Helpers.Tests.Data;

public static class JsonData
{
    public const string EmptyArrayString = "[]";

    public const string ValidArrayString = "[true]";

    public const string EmptyObjectString = "{}";

    public const string InvalidObjectString = "false{}";

    public const string SimpleListString = "[1, 2, 3]";

    public const string SimpleDictionaryString = """
        {
            "one": 1,
            "two": 2,
            "three": 3
        }
        """;

    public const string SimpleObjectString = """
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
            yield return new TestCaseData(StringData.EmptyString) { TypeArgs = [typeof(string)] };
            yield return new TestCaseData(new StringReader(StringData.EmptyString)) { TypeArgs = [typeof(StringReader)] };
            yield return new TestCaseData(new MemoryStream(StringData.EmptyString.GetBytes())) { TypeArgs = [typeof(MemoryStream)] };
        }
    }

    public static IEnumerable WhitespaceContent
    {
        get
        {
            yield return new TestCaseData(WhitespaceData.MultipleRegularSpaces) { TypeArgs = [typeof(string)] };
            yield return new TestCaseData(new StringReader(WhitespaceData.MultipleRegularSpaces)) { TypeArgs = [typeof(StringReader)] };
            yield return new TestCaseData(new MemoryStream(WhitespaceData.MultipleRegularSpaces.GetBytes())) { TypeArgs = [typeof(MemoryStream)] };
        }
    }

    public static IEnumerable EmptyArray
    {
        get
        {
            yield return new TestCaseData(EmptyArrayString) { TypeArgs = [typeof(string)] };
            yield return new TestCaseData(new StringReader(EmptyArrayString)) { TypeArgs = [typeof(StringReader)] };
            yield return new TestCaseData(new MemoryStream(EmptyArrayString.GetBytes())) { TypeArgs = [typeof(MemoryStream)] };
        }
    }

    public static IEnumerable EmptyObject
    {
        get
        {
            yield return new TestCaseData(EmptyObjectString) { TypeArgs = [typeof(string)] };
            yield return new TestCaseData(new StringReader(EmptyObjectString)) { TypeArgs = [typeof(StringReader)] };
            yield return new TestCaseData(new MemoryStream(EmptyObjectString.GetBytes())) { TypeArgs = [typeof(MemoryStream)] };
        }
    }

    public static IEnumerable SimpleList
    {
        get
        {
            yield return new TestCaseData(SimpleListString) { TypeArgs = [typeof(string)] };
            yield return new TestCaseData(new StringReader(SimpleListString)) { TypeArgs = [typeof(StringReader)] };
            yield return new TestCaseData(new MemoryStream(SimpleListString.GetBytes())) { TypeArgs = [typeof(MemoryStream)] };
        }
    }

    public static IEnumerable SimpleDictionary
    {
        get
        {
            yield return new TestCaseData(SimpleDictionaryString) { TypeArgs = [typeof(string)] };
            yield return new TestCaseData(new StringReader(SimpleDictionaryString)) { TypeArgs = [typeof(StringReader)] };
            yield return new TestCaseData(new MemoryStream(SimpleDictionaryString.GetBytes())) { TypeArgs = [typeof(MemoryStream)] };
        }
    }

    public static IEnumerable SimpleObject
    {
        get
        {
            yield return new TestCaseData(SimpleObjectString) { TypeArgs = [typeof(string)] };
            yield return new TestCaseData(new StringReader(SimpleObjectString)) { TypeArgs = [typeof(StringReader)] };
            yield return new TestCaseData(new MemoryStream(SimpleObjectString.GetBytes())) { TypeArgs = [typeof(MemoryStream)] };
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
