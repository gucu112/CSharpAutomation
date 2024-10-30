using Gucu112.CSharp.Automation.Helpers.Extensions;

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

            yield return GenerateTestCaseData<string>();
            yield return GenerateTestCaseData<TextWriter>();
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

            yield return GenerateTestCaseData<string>();
            yield return GenerateTestCaseData<TextWriter>();
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

            yield return GenerateTestCaseData<string>();
            yield return GenerateTestCaseData<TextWriter>();
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

            yield return GenerateTestCaseData<string>(true);
            yield return GenerateTestCaseData<string>(false);
            yield return GenerateTestCaseData<TextWriter>(true);
            yield return GenerateTestCaseData<TextWriter>(false);
        }
    }

    public static IEnumerable EmptyArray
    {
        get
        {
            static TestCaseData GenerateTestCaseData<T>()
            {
                return new TestCaseData(new List<object>()) { TypeArgs = [typeof(T)] }.Returns(JsonData.EmptyArrayString);
            }

            yield return GenerateTestCaseData<string>();
            yield return GenerateTestCaseData<TextWriter>();
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

            yield return GenerateTestCaseData<string>();
            yield return GenerateTestCaseData<TextWriter>();
        }
    }

    public static IEnumerable SimpleList
    {
        get
        {
            static TestCaseData GenerateTestCaseData<T>()
            {
                List<int> list = [1, 2, 3];
                return new TestCaseData(list) { TypeArgs = [typeof(T)] }.Returns(JsonData.SimpleListString.RemoveSpace());
            }

            yield return GenerateTestCaseData<string>();
            yield return GenerateTestCaseData<TextWriter>();
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
                return new TestCaseData(dictionary) { TypeArgs = [typeof(T)] }.Returns(JsonData.SimpleDictionaryString.RemoveSpace());
            }

            yield return GenerateTestCaseData<string>();
            yield return GenerateTestCaseData<TextWriter>();
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
                        { "Empty", string.Empty }
                    }
                };
                return new TestCaseData(customObject) { TypeArgs = [typeof(T)] }.Returns(JsonData.SimpleObjectString.RemoveSpace());
            }

            yield return GenerateTestCaseData<string>();
            yield return GenerateTestCaseData<TextWriter>();
        }
    }
}
