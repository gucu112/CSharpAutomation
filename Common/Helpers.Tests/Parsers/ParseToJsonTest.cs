using System.Text.RegularExpressions;
using Gucu112.CSharp.Automation.Helpers.Parsers;
using Gucu112.CSharp.Automation.Helpers.Tests.Data;

namespace Gucu112.CSharp.Automation.Helpers.Tests.Parsers;

[TestFixture]
public class ParseToJsonTest
{
    [Test]
    public void ThrowsOnNull()
    {
        Assert.Throws<ArgumentNullException>(() => Parse.ToJsonString(null!));
    }

    [TestCase(StringData.EmptyString)]
    [TestCase(WhitespaceData.MultipleRegularSpaces)]
    public void EmptyContent_ReturnsEmptyJsonString(object value)
    {
        Assert.That(Parse.ToJsonString(value), Is.EqualTo($"\"{value}\""));
    }

    [TestCaseSource(typeof(StringData), nameof(StringData.ShortWordsWithoutSpaces))]
    public void Content_ReturnsJsonString(object value)
    {
        Assert.That(Parse.ToJsonString(value), Is.EqualTo($"\"{value}\""));
    }

    [TestCase(ExpectedResult = JsonData.EmptyArrayString)]
    public string EmptyArray_ReturnsEmptyJsonArray()
    {
        return Parse.ToJsonString(new List<object>());
    }

    [TestCase(ExpectedResult = JsonData.EmptyObjectString)]
    public string EmptyObject_ReturnsEmptyJsonObject()
    {
        return Parse.ToJsonString(new object());
    }

    [TestCase(JsonData.SimpleListString)]
    public void List_ReturnsJsonArray(string listString)
    {
        listString = listString.Replace(" ", "");
        List<int> list = [ 1, 2, 3 ];
        Assert.That(Parse.ToJsonString(list), Is.EqualTo(listString));
    }

    [TestCase(JsonData.SimpleDictionaryString)]
    public void Dictionary_ReturnsJsonObject(string dictionaryString)
    {
        dictionaryString = dictionaryString.Replace(" ", "").Replace("\r\n", "");
        Dictionary<string, int> dictionary = new()
        {
            { "one", 1 },
            { "two", 2 },
            { "three", 3 },
        };
        Assert.That(Parse.ToJsonString(dictionary), Is.EqualTo(dictionaryString));
    }

    [TestCase(JsonData.SimpleObjectString)]
    public void Object_ReturnsJsonObject(string expectedObjectString)
    {
        expectedObjectString = expectedObjectString.Replace(" ", "").Replace("\r\n", "");
        expectedObjectString = Regex.Replace(expectedObjectString, @"/\*\w+\*/", "");
        var customObject = new JsonData.SimpleObjectModel
        {
            BooleanValue = true,
            ListOfNumbers = [9, 8, 7, 6, 5],
            DictionaryOfStrings = new()
            {
                { "Empty", string.Empty },
                { "Number", "007" },
                { "String", "Test" },
            }
        };
        var actualObjectString = Parse.ToJsonString(customObject).Replace("\"InvalidProperty\":null,", "");
        Assert.That(actualObjectString, Is.EqualTo(expectedObjectString).IgnoreCase);
    }
}
