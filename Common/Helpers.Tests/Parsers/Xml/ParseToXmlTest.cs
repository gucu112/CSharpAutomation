using Gucu112.CSharp.Automation.Helpers.Parsers;
using Gucu112.CSharp.Automation.Helpers.Tests.Data;

namespace Gucu112.CSharp.Automation.Helpers.Tests.Parsers.Xml;

[TestFixture]
public class ParseToXmlTest : BaseXmlTest
{
    [Test]
    public void ThrowsOnNull()
    {
        Assert.Throws<ArgumentNullException>(() => Parse.ToXmlString(null!));
    }

    [TestCaseSource(typeof(ObjectToXmlData), nameof(ObjectToXmlData.EmptyValue))]
    public string EmptyValue_ReturnsVoidRootString<T>(object value)
    {
        return ParseToXml<T>(value);
    }

    [TestCaseSource(typeof(ObjectToXmlData), nameof(ObjectToXmlData.WhitespaceValue))]
    public string WhitespaceValue_ReturnsEmptyRootString<T>(object value)
    {
        return ParseToXml<T>(value);
    }

    [TestCaseSource(typeof(ObjectToXmlData), nameof(ObjectToXmlData.StringValue))]
    public string StringValue_ReturnsRootString<T>(object value)
    {
        return ParseToXml<T>(value);
    }

    [TestCaseSource(typeof(ObjectToXmlData), nameof(ObjectToXmlData.BooleanValue))]
    public string BooleanValue_ReturnsRootBoolean<T>(object value)
    {
        return ParseToXml<T>(value);
    }

    [TestCaseSource(typeof(ObjectToXmlData), nameof(ObjectToXmlData.EmptyList))]
    public string EmptyList_ReturnsVoidArray<T>(object value)
    {
        return ParseToXml<T>(value);
    }

    [TestCaseSource(typeof(ObjectToXmlData), nameof(ObjectToXmlData.EmptyObject))]
    public string EmptyObject_ReturnsVoidAnyType<T>(object value)
    {
        return ParseToXml<T>(value);
    }

    [TestCaseSource(typeof(ObjectToXmlData), nameof(ObjectToXmlData.SimpleList))]
    public string SimpleList_ReturnsArrayOfInteger<T>(object value)
    {
        return ParseToXml<T>(value);
    }

    [Test]
    public void SimpleDictionary_ThrowsNotSupported()
    {
        var dictionary = ObjectData.SimpleDictionaryValue;

        Assert.Throws<NotSupportedException>(() => Parse.ToXmlString(dictionary));
    }

    [TestCase(TypeArgs = [typeof(string)], TestName = nameof(SimpleObject_ReturnsXmlDocument) + "UsingString")]
    [TestCase(TypeArgs = [typeof(TextWriter)], TestName = nameof(SimpleObject_ReturnsXmlDocument) + "UsingWriter")]
    public void SimpleObject_ReturnsXmlDocument<T>()
    {
        var simpleObject = ObjectData.SimpleRootObjectValue;

        var xmlString = ParseToXml<T>(simpleObject);

        using (Assert.EnterMultipleScope())
        {
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
}
