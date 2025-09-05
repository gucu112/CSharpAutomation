namespace Gucu112.CSharp.Automation.Helpers.Tests.Data;

public class ObjectToJsonData : BaseData
{
    public static IEnumerable EmptyValue
    {
        get
        {
            var input = StringData.EmptyString;
            var output = JsonData.EmptyJsonString;

            yield return Create<string>(input, output).SetArgDisplayNames("EmptyValueToJsonString");
            yield return Create<TextWriter>(input, output).SetArgDisplayNames("EmptyValueWriterToJsonString");
            yield return Create<Stream>(input, output).SetArgDisplayNames("EmptyValueStreamToJsonString");
        }
    }

    public static IEnumerable WhitespaceValue
    {
        get
        {
            var input = WhitespaceData.MultipleRegularSpaces;
            var output = @$"""{WhitespaceData.MultipleRegularSpaces}""";

            yield return Create<string>(input, output).SetArgDisplayNames("WhitespaceValueToJsonString");
            yield return Create<TextWriter>(input, output).SetArgDisplayNames("WhitespaceValueWriterToJsonString");
            yield return Create<Stream>(input, output).SetArgDisplayNames("WhitespaceValueStreamToJsonString");
        }
    }

    public static IEnumerable StringValue
    {
        get
        {
            var input = StringData.HelloString;
            var output = @$"""{StringData.HelloString}""";

            yield return Create<string>(input, output).SetArgDisplayNames("StringValueToJsonString");
            yield return Create<TextWriter>(input, output).SetArgDisplayNames("StringValueWriterToJsonString");
            yield return Create<Stream>(input, output).SetArgDisplayNames("StringValueStreamToJsonString");
        }
    }

    public static IEnumerable BooleanValue
    {
        get
        {
            var trueValueString = true.ToString().ToLower();
            var falseValueString = false.ToString().ToLower();

            yield return Create<string>(true, trueValueString).SetArgDisplayNames("TrueBooleanToJsonString");
            yield return Create<string>(false, falseValueString).SetArgDisplayNames("FalseBooleanToJsonString");
            yield return Create<TextWriter>(true, trueValueString).SetArgDisplayNames("TrueBooleanWriterToJsonString");
            yield return Create<Stream>(false, falseValueString).SetArgDisplayNames("FalseBooleanStreamToJsonString");
        }
    }

    public static IEnumerable EmptyList
    {
        get
        {
            var input = new List<object>();
            var output = JsonData.EmptyArrayString;

            yield return Create<string>(input, output).SetArgDisplayNames("EmptyArrayToJsonString");
            yield return Create<TextWriter>(input, output).SetArgDisplayNames("EmptyArrayWriterToJsonString");
            yield return Create<Stream>(input, output).SetArgDisplayNames("EmptyArrayStreamToJsonString");
        }
    }

    public static IEnumerable EmptyObject
    {
        get
        {
            var input = new object();
            var output = JsonData.EmptyObjectString;

            yield return Create<string>(input, output).SetArgDisplayNames("EmptyObjectToJsonString");
            yield return Create<TextWriter>(input, output).SetArgDisplayNames("EmptyObjectWriterToJsonString");
            yield return Create<Stream>(input, output).SetArgDisplayNames("EmptyObjectStreamToJsonString");
        }
    }

    public static IEnumerable SimpleList
    {
        get
        {
            var input = new List<int> { 1, 2, 3 };
            var output = JsonData.SimpleListString;

            yield return Create<string>(input, output).SetArgDisplayNames("SimpleListToJsonString");
            yield return Create<TextWriter>(input, output).SetArgDisplayNames("SimpleListWriterToJsonString");
            yield return Create<Stream>(input, output).SetArgDisplayNames("SimpleListStreamToJsonString");
        }
    }

    public static IEnumerable SimpleDictionary
    {
        get
        {
            var input = ObjectData.SimpleDictionaryValue;
            var output = JsonData.SimpleDictionaryString;

            yield return Create<string>(input, output).SetArgDisplayNames("SimpleDictionaryToJsonString");
            yield return Create<TextWriter>(input, output).SetArgDisplayNames("SimpleDictionaryWriterToJsonString");
            yield return Create<Stream>(input, output).SetArgDisplayNames("SimpleDictionaryStreamToJsonString");
        }
    }
}
