namespace Gucu112.CSharp.Automation.Helpers.Tests.Data;

public static class JsonData
{
    public static IEnumerable EmptyObjects
    {
        get
        {
            yield return "[]";
            yield return "{}";
        }
    }

    public static IEnumerable SimpleElements
    {
        get
        {
            yield return """
                [1, 2, 3]
                """;
            yield return """
                {
                    "one": 1,
                    "two": 2,
                    "three": 3
                }
                """;
        }
    }

    public static IEnumerable SimpleObjects
    {
        get
        {
            yield return """
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
        }
    }

    public class SimpleObjectsModel
    {
        public string? InvalidProperty;

        public bool? BooleanValue;

        public List<int>? ListOfNumbers;

        public Dictionary<string, string>? DictionaryOfStrings;
    }
}
