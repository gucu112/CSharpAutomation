using Gucu112.CSharp.Automation.Helpers.Parsers;
using Gucu112.CSharp.Automation.Helpers.Tests.Data;

namespace Gucu112.CSharp.Automation.Helpers.Tests.Parsers.Xml;

[TestFixture]
public class ParseToXmlTest
{
    [Test]
    public void ThrowsOnNull()
    {
        Assert.Throws<ArgumentNullException>(() => Parse.ToXmlString(null!));
    }

    [TestCaseSource(typeof(ObjectToXmlData), nameof(ObjectToXmlData.EmptyValue))]
    public string EmptyValue_ReturnsVoidRootString(object value)
    {
        return Parse.ToXmlString(value);
    }

    [TestCaseSource(typeof(ObjectToXmlData), nameof(ObjectToXmlData.WhitespaceValue))]
    public string WhitespaceValue_ReturnsEmptyRootString(object value)
    {
        return Parse.ToXmlString(value);
    }

    [TestCaseSource(typeof(ObjectToXmlData), nameof(ObjectToXmlData.StringValue))]
    public string StringValue_ReturnsRootString(object value)
    {
        return Parse.ToXmlString(value);
    }

    [TestCaseSource(typeof(ObjectToXmlData), nameof(ObjectToXmlData.BooleanValue))]
    public string BooleanValue_ReturnsRootBoolean(object value)
    {
        return Parse.ToXmlString(value);
    }

    [TestCaseSource(typeof(ObjectToXmlData), nameof(ObjectToXmlData.EmptyList))]
    public string EmptyList_ReturnsVoidArray(object value)
    {
        return Parse.ToXmlString(value);
    }

    [TestCaseSource(typeof(ObjectToXmlData), nameof(ObjectToXmlData.EmptyObject))]
    public string EmptyObject_ReturnsVoidAnyType(object value)
    {
        return Parse.ToXmlString(value);
    }

    [TestCaseSource(typeof(ObjectToXmlData), nameof(ObjectToXmlData.SimpleList))]
    public string SimpleList_ReturnsArrayOfInteger(object value)
    {
        return Parse.ToXmlString(value);
    }

    [Test]
    public void SimpleDictionary_ThrowsNotSupported()
    {
        var dictionary = ObjectData.SimpleDictionaryValue;

        Assert.Throws<NotSupportedException>(() => Parse.ToXmlString(dictionary));
    }

    [Test]
    public void SimpleObject_ReturnsXmlDocument()
    {
        var simpleObject = ObjectData.SimpleRootObjectValue;

        var xmlString = Parse.ToXmlString(simpleObject);

        Assert.That(xmlString, Does.StartWith(XmlData.CorrectDeclarationString));
        Assert.That(xmlString, Does.Contain(@"<RootElement IsPrimary=""true"">"));
        Assert.That(xmlString, Does.Match("<Environment>Humidity.+ Wind.+</Environment>"));
        Assert.That(xmlString, Does.Contain("<CapThickness>0.0005</CapThickness>"));
        Assert.That(xmlString, Does.Contain("<GeneticCode>"));
        Assert.That(xmlString, Does.Contain("<Codon>AGG</Codon><Codon>CUC</Codon>"));
        Assert.That(xmlString, Does.Contain("<Codon>UAA</Codon>"));
        Assert.That(xmlString, Does.Contain("</GeneticCode>"));
        Assert.That(xmlString, Does.Match(@"<Vegetation\w+>2025-04.+</Vegetation\w+>"));
        Assert.That(xmlString, Does.EndWith("</RootElement>"));
    }
}
