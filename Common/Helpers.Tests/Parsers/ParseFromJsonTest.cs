using Gucu112.CSharp.Automation.Helpers.Parsers;
using Gucu112.CSharp.Automation.Helpers.Tests.Data;
using Newtonsoft.Json.Linq;

namespace Gucu112.CSharp.Automation.Helpers.Tests.Parsers;

[TestFixture]
public class ParseFromJsonTest
{
    [Test]
    public void ThrowsOnNull()
    {
        Assert.Throws<ArgumentNullException>(() => Parse.FromJson<object>((string)null!));
        Assert.Throws<ArgumentNullException>(() => Parse.FromJson<object>((StreamReader)null!));
    }

    [TestCaseSource(typeof(JsonData), nameof(JsonData.EmptyContent))]
    public void EmptyContentReturnsNull<T>(T content)
    {
        Assert.That(ParseFromJson<object>(content), Is.Null);
    }

    [TestCaseSource(typeof(JsonData), nameof(JsonData.WhitespaceContent))]
    public void WhitespaceContentReturnsNull<T>(T content)
    {
        Assert.That(ParseFromJson<object>(content), Is.Null);
    }

    [TestCaseSource(typeof(JsonData), nameof(JsonData.EmptyArray))]
    public void EmptyArrayReturnsEmpty<T>(T content)
    {
        Assert.That(ParseFromJson<object>(content), Is.Empty.And.TypeOf<JArray>());
    }

    [TestCaseSource(typeof(JsonData), nameof(JsonData.EmptyObject))]
    public void EmptyObjectReturnsEmpty<T>(T content)
    {
        Assert.That(ParseFromJson<object>(content), Is.Empty.And.TypeOf<JObject>());
    }

    [TestCaseSource(typeof(JsonData), nameof(JsonData.SimpleList))]
    public void ListReturnsExactlyThreeItems<T>(T content)
    {
        var jsonList = ParseFromJson<List<int>>(content);

        Assert.That(jsonList, Has.Exactly(3).Items);
        Assert.That(jsonList, Is.Ordered.And.SubsetOf(Enumerable.Range(1, 5)));
    }

    [TestCaseSource(typeof(JsonData), nameof(JsonData.SimpleDictionary))]
    public void DictionaryReturnsExactlyThreeItems<T>(T content)
    {
        var jsonDictionary = ParseFromJson<Dictionary<string, int>>(content);

        Assert.That(jsonDictionary, Has.Exactly(3).Items);
        Assert.That(jsonDictionary.Values, Is.Ordered.And.SubsetOf(Enumerable.Range(1, 5)));
    }

    [TestCaseSource(typeof(JsonData), nameof(JsonData.SimpleObject))]
    public void ObjectReturnsItemsUsingJsonType<T>(T content)
    {
        var jsonObject = ParseFromJson<JObject>(content);

        Assert.That(jsonObject, Has.Exactly(3).Items);

        Assert.That(jsonObject["invalidProperty"], Is.Null);

        Assert.Multiple(() =>
        {
            var itemToken = jsonObject["booleanValue"];
            Assert.That(itemToken, Has.Property("HasValues").EqualTo(false));

            var itemValue = itemToken?.Value<bool>();
            Assert.That(itemValue, Is.EqualTo(true));
        });

        Assert.Multiple(() =>
        {
            var listToken = jsonObject["listOfNumbers"];
            Assert.That(listToken, Has.Property("HasValues").EqualTo(true));

            var listValues = listToken?.Values<int>().ToList();
            Assert.That(listValues, Has.Exactly(5).Items.And.All.TypeOf<int>());
        });

        Assert.Multiple(() =>
        {
            var dictionaryToken = jsonObject["dictionaryOfStrings"];
            Assert.That(dictionaryToken, Has.Property("HasValues").EqualTo(true));

            var dictionaryValues = dictionaryToken?.ToObject<Dictionary<string, string>>();
            Assert.That(dictionaryValues, Has.Exactly(3).Items.And.All.Property("Key").And.Property("Value").TypeOf<string>());
        });
    }

    [TestCaseSource(typeof(JsonData), nameof(JsonData.SimpleObject))]
    public void ObjectReturnsItemsUsingCustomType<T>(T content)
    {
        var customObject = ParseFromJson<JsonData.SimpleObjectModel>(content);

        Assert.Multiple(() =>
        {
            Assert.That(customObject?.InvalidProperty, Is.EqualTo(null!));
            Assert.That(customObject?.BooleanValue, Is.EqualTo(true));
            Assert.That(customObject?.ListOfNumbers, Has.Exactly(5).Items.And.All.LessThan(10));
            Assert.That(customObject?.DictionaryOfStrings, Has.Exactly(3).Items);
            Assert.That(customObject?.DictionaryOfStrings?["empty"], Has.Length.EqualTo(0));
            Assert.That(customObject?.DictionaryOfStrings?["number"], Does.Contain(7.ToString()));
            Assert.That(customObject?.DictionaryOfStrings?["string"], Is.EqualTo("test").IgnoreCase);
        });
    }

    private static T? ParseFromJson<T>(dynamic? input)
    {
        return Parse.FromJson<T>(input);
    }
}
