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
        Formatting = Formatting.Indented,
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

    [TestCaseSource(typeof(ObjectData), nameof(ObjectData.EmptyValue))]
    public string EmptyValue_ReturnsEmptyJsonString<T>(object value)
    {
        return ParseToJson<T>(value);
    }

    [TestCaseSource(typeof(ObjectData), nameof(ObjectData.WhitespaceValue))]
    public string WhitespaceValue_ReturnsJsonString<T>(object value)
    {
        return ParseToJson<T>(value);
    }

    [TestCaseSource(typeof(ObjectData), nameof(ObjectData.StringValue))]
    public string StringValue_ReturnsJsonString<T>(object value)
    {
        return ParseToJson<T>(value);
    }

    [TestCaseSource(typeof(ObjectData), nameof(ObjectData.BooleanValue))]
    public string BooleanValue_ReturnsBooleanJsonValue<T>(object value)
    {
        return ParseToJson<T>(value);
    }

    [TestCaseSource(typeof(ObjectData), nameof(ObjectData.EmptyArray))]
    public string EmptyArray_ReturnsEmptyJsonArray<T>(object value)
    {
        return ParseToJson<T>(value).NormalizeSpace();
    }

    [TestCaseSource(typeof(ObjectData), nameof(ObjectData.EmptyObject))]
    public string EmptyObject_ReturnsEmptyJsonObject<T>(object value)
    {
        return ParseToJson<T>(value).NormalizeSpace();
    }

    [TestCaseSource(typeof(ObjectData), nameof(ObjectData.SimpleList))]
    public string List_ReturnsJsonArray<T>(object value)
    {
        return ParseToJson<T>(value).NormalizeSpace();
    }

    [TestCaseSource(typeof(ObjectData), nameof(ObjectData.SimpleDictionary))]
    public string Dictionary_ReturnsJsonObject<T>(object value)
    {
        return ParseToJson<T>(value, Settings).TrimStart(byteOrderMark);
    }

    [TestCaseSource(typeof(ObjectData), nameof(ObjectData.SimpleObject))]
    public string Object_ReturnsJsonObject<T>(object value)
    {
        return ParseToJson<T>(value, Settings).TrimStart(byteOrderMark);
    }
}
