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
        Assert.Throws<ArgumentNullException>(() => Parse.FromJson<object>(null!));
    }

    [TestCase(StringData.EmptyString)]
    [TestCase(WhitespaceData.MultipleRegularSpaces)]
    public void EmptyStringReturnsNull(string content)
    {
        Assert.That(Parse.FromJson<object>(content), Is.Null);
    }

    [TestCaseSource(typeof(JsonData), nameof(JsonData.EmptyObjects))]
    public void EmptyObjectReturnsEmpty(string content)
    {
        Assert.That(Parse.FromJson<object>(content), Is.Empty);
    }

    [TestCaseSource(typeof(JsonData), nameof(JsonData.SimpleElements))]
    public void ObjectReturnsExactlyThreeItems(string content)
    {
        Assert.That(Parse.FromJson<object>(content), Has.Exactly(3).Items);
    }

    [TestCaseSource(typeof(JsonData), nameof(JsonData.SimpleObjects))]
    public void ObjectReturnsItemsUsingJsonType(string content)
    {
        var jsonObject = Parse.FromJson<JObject>(content);

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

    [TestCaseSource(typeof(JsonData), nameof(JsonData.SimpleObjects))]
    public void ObjectReturnsItemsUsingCustomType(string content)
    {
        var customObject = Parse.FromJson<JsonData.SimpleObjectsModel>(content);

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
}
