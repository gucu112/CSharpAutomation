using Gucu112.CSharp.Automation.Helpers.Parsers;
using Gucu112.CSharp.Automation.Helpers.Tests.Data;

namespace Gucu112.CSharp.Automation.Helpers.Tests.Parsers;

[TestFixture]
public class ParseToJsonTest
{
    [Test]
    public void ThrowsOnNull()
    {
        Assert.Multiple(() =>
        {
            Assert.Throws<ArgumentNullException>(() => Parse.ToJsonString(null!));
            Assert.Throws<ArgumentNullException>(() => Parse.ToJsonWriter<StringWriter>(null!));
            Assert.Throws<ArgumentNullException>(() => Parse.ToJsonStream<MemoryStream>(null!));
        });
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
        return ParseToJson<T>(value);
    }

    [TestCaseSource(typeof(ObjectData), nameof(ObjectData.EmptyObject))]
    public string EmptyObject_ReturnsEmptyJsonObject<T>(object value)
    {
        return ParseToJson<T>(value);
    }

    [TestCaseSource(typeof(ObjectData), nameof(ObjectData.SimpleList))]
    public string List_ReturnsJsonArray<T>(object value)
    {
        return ParseToJson<T>(value);
    }

    [TestCaseSource(typeof(ObjectData), nameof(ObjectData.SimpleDictionary))]
    public string Dictionary_ReturnsJsonObject<T>(object value)
    {
        return ParseToJson<T>(value);
    }

    [TestCaseSource(typeof(ObjectData), nameof(ObjectData.SimpleObject))]
    public string Object_ReturnsJsonObject<T>(object value)
    {
        return ParseToJson<T>(value);
    }

    private static string ParseToJson<T>(object? value)
    {
        if (typeof(T) == typeof(Stream))
        {
            using var stream = Parse.ToJsonStream<MemoryStream>(value);
            return Encoding.UTF8.GetString(stream.GetBuffer(), 0, (int)stream.Length);
        }

        if (typeof(T) == typeof(TextWriter))
        {
            using var writer = Parse.ToJsonWriter<StringWriter>(value);
            return writer.GetStringBuilder().ToString();
        }

        return Parse.ToJsonString(value);
    }
}
