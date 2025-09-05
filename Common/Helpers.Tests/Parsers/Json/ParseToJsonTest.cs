using Gucu112.CSharp.Automation.Helpers.Extensions;
using Gucu112.CSharp.Automation.Helpers.Models;
using Gucu112.CSharp.Automation.Helpers.Parsers;
using Gucu112.CSharp.Automation.Helpers.Tests.Data;

namespace Gucu112.CSharp.Automation.Helpers.Tests.Parsers.Json;

[TestFixture]
public class ParseToJsonTest : BaseJsonTest
{
    private static readonly JsonSettings Settings = new()
    {
        Encoding = Encoding.Unicode,
        Formatting = JsonFormatting.Indented,
        Indentation = 4,
        IndentChar = ' ',
        NullValueHandling = NullValueHandling.Include,
    };

    private readonly char byteOrderMark = BitConverter.ToChar(Settings.Encoding.GetPreamble());

    [Test]
    public void ThrowsOnNull()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.Throws<ArgumentNullException>(() => Parse.ToJsonString(null!));
            Assert.Throws<ArgumentNullException>(() => Parse.ToJsonWriter<StringWriter>(null!));
            Assert.Throws<ArgumentNullException>(() => Parse.ToJsonStream<MemoryStream>(null!));
        }
    }

    [TestCaseSource(typeof(ObjectToJsonData), nameof(ObjectToJsonData.EmptyValue))]
    public string EmptyValue_ReturnsEmptyJsonString<T>(object value)
    {
        return ParseToJson<T>(value);
    }

    [TestCaseSource(typeof(ObjectToJsonData), nameof(ObjectToJsonData.WhitespaceValue))]
    public string WhitespaceValue_ReturnsJsonString<T>(object value)
    {
        return ParseToJson<T>(value);
    }

    [TestCaseSource(typeof(ObjectToJsonData), nameof(ObjectToJsonData.StringValue))]
    public string StringValue_ReturnsJsonString<T>(object value)
    {
        return ParseToJson<T>(value);
    }

    [TestCaseSource(typeof(ObjectToJsonData), nameof(ObjectToJsonData.BooleanValue))]
    public string BooleanValue_ReturnsBooleanJsonValue<T>(object value)
    {
        return ParseToJson<T>(value);
    }

    [TestCaseSource(typeof(ObjectToJsonData), nameof(ObjectToJsonData.EmptyList))]
    public string EmptyList_ReturnsEmptyJsonArray<T>(object value)
    {
        return ParseToJson<T>(value);
    }

    [TestCaseSource(typeof(ObjectToJsonData), nameof(ObjectToJsonData.EmptyObject))]
    public string EmptyObject_ReturnsEmptyJsonObject<T>(object value)
    {
        return ParseToJson<T>(value);
    }

    [TestCaseSource(typeof(ObjectToJsonData), nameof(ObjectToJsonData.SimpleList))]
    public string SimpleList_ReturnsJsonArray<T>(object value)
    {
        return ParseToJson<T>(value).NormalizeSpace();
    }

    [TestCaseSource(typeof(ObjectToJsonData), nameof(ObjectToJsonData.SimpleDictionary))]
    public string SimpleDictionary_ReturnsJsonObject<T>(object value)
    {
        return ParseToJson<T>(value, Settings).TrimStart(byteOrderMark);
    }

    [TestCase(TypeArgs = [typeof(string)], TestName = nameof(SimpleObject_ReturnsJsonObject) + "UsingString")]
    [TestCase(TypeArgs = [typeof(TextWriter)], TestName = nameof(SimpleObject_ReturnsJsonObject) + "UsingWriter")]
    public void SimpleObject_ReturnsJsonObject<T>()
    {
        var input = (object)ObjectData.SimpleObjectValue;
        var output = JsonData.SimpleObjectString;

        Assert.That(ParseToJson<T>(input, Settings), Is.EqualTo(output));
    }

    [TestCase(TypeArgs = [typeof(Stream)], TestName = nameof(SimpleObject_ReturnsJsonObjectWithBOM) + "UsingStream")]
    public void SimpleObject_ReturnsJsonObjectWithBOM<T>()
    {
        var input = (object)ObjectData.SimpleObjectValue;
        var output = byteOrderMark + JsonData.SimpleObjectString;

        Assert.That(ParseToJson<T>(input, Settings), Is.EqualTo(output));
    }
}
