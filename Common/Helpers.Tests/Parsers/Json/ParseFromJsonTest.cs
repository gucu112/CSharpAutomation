using Gucu112.CSharp.Automation.Helpers.Parsers;
using Gucu112.CSharp.Automation.Helpers.Tests.Data;

namespace Gucu112.CSharp.Automation.Helpers.Tests.Parsers.Json;

[TestFixture]
public class ParseFromJsonTest : BaseJsonTest
{
    [Test]
    public void ThrowsOnNull()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.Throws<ArgumentNullException>(() => Parse.FromJson<object>((string)null!));
            Assert.Throws<ArgumentNullException>(() => Parse.FromJson<object>((StreamReader)null!));
            Assert.Throws<ArgumentNullException>(() => Parse.FromJson<object>((MemoryStream)null!));
        }
    }

    [TestCaseSource(typeof(JsonData), nameof(JsonData.EmptyContent))]
    public void EmptyContent_ReturnsNull<T>(T content)
    {
        Assert.That(ParseFromJson<object>(content), Is.Null);
    }

    [TestCaseSource(typeof(JsonData), nameof(JsonData.EmptyArray))]
    public void EmptyArray_ReturnsEmpty<T>(T content)
    {
        TestContext.Out.WriteLine(content);

        Assert.That(ParseFromJson<object>(content), Is.Empty.And.TypeOf<JArray>());
    }

    [TestCaseSource(typeof(JsonData), nameof(JsonData.EmptyObject))]
    public void EmptyObject_ReturnsEmpty<T>(T content)
    {
        TestContext.Out.WriteLine(content);

        Assert.That(ParseFromJson<object>(content), Is.Empty.And.TypeOf<JObject>());
    }

    [TestCaseSource(typeof(JsonData), nameof(JsonData.SimpleList))]
    public void List_ReturnsExactlyThreeItems<T>(T content)
    {
        TestContext.Out.WriteLine(content);

        var jsonList = ParseFromJson<List<int>>(content);

        Assert.That(jsonList, Has.Exactly(3).Items);
        Assert.That(jsonList, Is.Ordered.And.SubsetOf(Enumerable.Range(1, 5)));
    }

    [TestCaseSource(typeof(JsonData), nameof(JsonData.SimpleDictionary))]
    public void Dictionary_ReturnsExactlyThreeItems<T>(T content)
    {
        TestContext.Out.WriteLine(content);

        var jsonDictionary = ParseFromJson<Dictionary<string, int>>(content);

        Assert.That(jsonDictionary, Has.Exactly(3).Items);
        Assert.That(jsonDictionary.Values, Is.Ordered.And.SubsetOf(Enumerable.Range(1, 5)));
    }

    [TestCaseSource(typeof(JsonData), nameof(JsonData.SimpleObject))]
    public void Object_ReturnsItemsUsingJsonType<T>(T content)
    {
        TestContext.Out.WriteLine(content);

        var jsonObject = ParseFromJson<JObject>(content);

        Assert.That(jsonObject, Has.Exactly(4).Items);

        Assert.That(jsonObject["invalidProperty"], Is.Null);

        using (Assert.EnterMultipleScope())
        {
            var itemToken = jsonObject["booleanValue"];
            Assert.That(itemToken, Has.Property("HasValues").EqualTo(false));

            var itemValue = itemToken?.Value<bool>();
            Assert.That(itemValue, Is.True);
        }

        using (Assert.EnterMultipleScope())
        {
            var listToken = jsonObject["listOfNumbers"];
            Assert.That(listToken, Has.Property("HasValues").EqualTo(true));

            var listValues = listToken?.Values<int>().ToList();
            Assert.That(listValues, Has.Exactly(5).Items.And.All.TypeOf<int>());
        }

        using (Assert.EnterMultipleScope())
        {
            var dictionaryToken = jsonObject["dictionaryOfStrings"];
            Assert.That(dictionaryToken, Has.Property("HasValues").EqualTo(true));

            var dictionaryValues = dictionaryToken?.ToObject<Dictionary<string, string>>();
            Assert.That(dictionaryValues, Has.Exactly(3).Items.And.All.Property("Key").And.Property("Value").TypeOf<string>());
        }

        using (Assert.EnterMultipleScope())
        {
            var dateTimeToken = jsonObject["currentYearStart"];
            Assert.That(dateTimeToken, Has.Property("Type").EqualTo(JTokenType.Date));

            var dateTimeValue = dateTimeToken?.ToObject<DateTime>();
            Assert.That(dateTimeValue, Is.GreaterThanOrEqualTo(new DateTime(2025, 1, 1)));
        }
    }

    [TestCaseSource(typeof(JsonData), nameof(JsonData.SimpleObject))]
    public void Object_ReturnsItemsUsingCustomType<T>(T content)
    {
        TestContext.Out.WriteLine(content);

        var customObject = ParseFromJson<JsonData.SimpleObjectModel>(content);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(customObject?.InvalidProperty, Is.Null);
            Assert.That(customObject?.BooleanValue, Is.True);
            Assert.That(customObject?.ListOfNumbers, Has.Exactly(6).Items.And.All.LessThan(10));
            Assert.That(customObject?.DictionaryOfStrings, Has.Exactly(3).Items);
            Assert.That(customObject?.DictionaryOfStrings?["empty"], Has.Length.EqualTo(0));
            Assert.That(customObject?.DictionaryOfStrings?["number"], Does.Contain(7.ToString()));
            Assert.That(customObject?.DictionaryOfStrings?["string"], Is.EqualTo("test").IgnoreCase);
            Assert.That(customObject?.CurrentYearStart?.Year, Is.EqualTo(2025));
        }
    }
}
