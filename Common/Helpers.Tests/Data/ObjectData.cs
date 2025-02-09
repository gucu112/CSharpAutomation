namespace Gucu112.CSharp.Automation.Helpers.Tests.Data;

public class ObjectData
{
    public static IEnumerable EmptyValue
    {
        get
        {
            static TestCaseData GenerateTestCaseData<T>()
            {
                return new TestCaseData(StringData.EmptyString) { TypeArgs = [typeof(T)] }.Returns(JsonData.EmptyJsonString);
            }

            yield return GenerateTestCaseData<string>().SetArgDisplayNames("EmptyValueToJsonString");
            yield return GenerateTestCaseData<TextWriter>().SetArgDisplayNames("EmptyValueWriterToJsonString");
            yield return GenerateTestCaseData<Stream>().SetArgDisplayNames("EmptyValueStreamToJsonString");
        }
    }

    public static IEnumerable WhitespaceValue
    {
        get
        {
            static TestCaseData GenerateTestCaseData<T>()
            {
                return new TestCaseData(WhitespaceData.MultipleRegularSpaces) { TypeArgs = [typeof(T)] }.Returns(@$"""{WhitespaceData.MultipleRegularSpaces}""");
            }

            yield return GenerateTestCaseData<string>().SetArgDisplayNames("WhitespaceValueToJsonString");
            yield return GenerateTestCaseData<TextWriter>().SetArgDisplayNames("WhitespaceValueWriterToJsonString");
            yield return GenerateTestCaseData<Stream>().SetArgDisplayNames("WhitespaceValueStreamToJsonString");
        }
    }

    public static IEnumerable StringValue
    {
        get
        {
            static TestCaseData GenerateTestCaseData<T>()
            {
                return new TestCaseData(StringData.HelloString) { TypeArgs = [typeof(T)] }.Returns(@$"""{StringData.HelloString}""");
            }

            yield return GenerateTestCaseData<string>().SetArgDisplayNames("StringValueToJsonString");
            yield return GenerateTestCaseData<TextWriter>().SetArgDisplayNames("StringValueWriterToJsonString");
            yield return GenerateTestCaseData<Stream>().SetArgDisplayNames("StringValueStreamToJsonString");
        }
    }

    public static IEnumerable BooleanValue
    {
        get
        {
            static TestCaseData GenerateTestCaseData<T>(bool value)
            {
                return new TestCaseData(value) { TypeArgs = [typeof(T)] }.Returns(value.ToString().ToLower());
            }

            yield return GenerateTestCaseData<string>(true).SetArgDisplayNames("TrueBooleanToJsonString");
            yield return GenerateTestCaseData<string>(false).SetArgDisplayNames("FalseBooleanToJsonString");
            yield return GenerateTestCaseData<TextWriter>(true).SetArgDisplayNames("TrueBooleanWriterToJsonString");
            yield return GenerateTestCaseData<Stream>(false).SetArgDisplayNames("FalseBooleanStreamToJsonString");
        }
    }

    public static IEnumerable EmptyList
    {
        get
        {
            static TestCaseData GenerateTestCaseData<T>()
            {
                return new TestCaseData(new List<object>()) { TypeArgs = [typeof(T)] }.Returns(JsonData.EmptyArrayString);
            }

            yield return GenerateTestCaseData<string>().SetArgDisplayNames("EmptyArrayToJsonString");
            yield return GenerateTestCaseData<TextWriter>().SetArgDisplayNames("EmptyArrayWriterToJsonString");
            yield return GenerateTestCaseData<Stream>().SetArgDisplayNames("EmptyArrayStreamToJsonString");
        }
    }

    public static IEnumerable EmptyObject
    {
        get
        {
            static TestCaseData GenerateTestCaseData<T>()
            {
                return new TestCaseData(new object()) { TypeArgs = [typeof(T)] }.Returns(JsonData.EmptyObjectString);
            }

            yield return GenerateTestCaseData<string>().SetArgDisplayNames("EmptyObjectToJsonString");
            yield return GenerateTestCaseData<TextWriter>().SetArgDisplayNames("EmptyObjectWriterToJsonString");
            yield return GenerateTestCaseData<Stream>().SetArgDisplayNames("EmptyObjectStreamToJsonString");
        }
    }

    public static IEnumerable SimpleList
    {
        get
        {
            static TestCaseData GenerateTestCaseData<T>()
            {
                List<int> list = [1, 2, 3];
                return new TestCaseData(list) { TypeArgs = [typeof(T)] }.Returns(JsonData.SimpleListString);
            }

            yield return GenerateTestCaseData<string>().SetArgDisplayNames("SimpleListToJsonString");
            yield return GenerateTestCaseData<TextWriter>().SetArgDisplayNames("SimpleListWriterToJsonString");
            yield return GenerateTestCaseData<Stream>().SetArgDisplayNames("SimpleListStreamToJsonString");
        }
    }

    public static IEnumerable SimpleDictionary
    {
        get
        {
            static TestCaseData GenerateTestCaseData<T>()
            {
                Dictionary<string, int> dictionary = new()
                {
                    { "First", 1 },
                    { "Second", 2 },
                    { "Third", 3 },
                };
                return new TestCaseData(dictionary) { TypeArgs = [typeof(T)] }.Returns(JsonData.SimpleDictionaryString);
            }

            yield return GenerateTestCaseData<string>().SetArgDisplayNames("SimpleDictionaryToJsonString");
            yield return GenerateTestCaseData<TextWriter>().SetArgDisplayNames("SimpleDictionaryWriterToJsonString");
            yield return GenerateTestCaseData<Stream>().SetArgDisplayNames("SimpleDictionaryStreamToJsonString");
        }
    }

    public static IEnumerable SimpleObject
    {
        get
        {
            static TestCaseData GenerateTestCaseData<T>()
            {
                JsonData.SimpleObjectModel customObject = new()
                {
                    InvalidProperty = null,
                    BooleanValue = false,
                    ListOfNumbers = [1, 2, 3],
                    DictionaryOfStrings = new()
                    {
                        { "Empty", string.Empty },
                    },
                    CurrentYearStart = new DateTime(2025, 1, 1),
                };
                return new TestCaseData(customObject) { TypeArgs = [typeof(T)] }.Returns(JsonData.SimpleObjectString);
            }

            yield return GenerateTestCaseData<string>().SetArgDisplayNames("SimpleObjectToJsonString");
            yield return GenerateTestCaseData<TextWriter>().SetArgDisplayNames("SimpleObjectWriterToJsonString");
            yield return GenerateTestCaseData<Stream>().SetArgDisplayNames("SimpleObjectStreamToJsonString");
        }
    }
}
