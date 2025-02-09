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

    [Test]
    public void EmptyValue_ReturnsVoidRootString()
    {
        var text = Parse.ToXmlString(StringData.EmptyString);

        Assert.That(text, Does.StartWith(XmlData.CorrectDeclarationString));
        Assert.That(text, Does.EndWith("<string />"));
    }

    [Test]
    public void WhitespaceValue_ReturnsEmptyRootString()
    {
        var text = Parse.ToXmlString(WhitespaceData.MultipleRegularSpaces);

        Assert.That(text, Does.StartWith(XmlData.CorrectDeclarationString));
        Assert.That(text, Does.EndWith("<string>   </string>"));
    }

    [Test]
    public void StringValue_ReturnsRootString()
    {
        var text = Parse.ToXmlString(StringData.LoremIpsumString);

        Assert.That(text, Does.StartWith(XmlData.CorrectDeclarationString));
        Assert.That(text, Does.EndWith("<string>Lorem ipsum dolor sit amet</string>"));
    }

    [TestCase(true)]
    [TestCase(false)]
    public void BooleanValue_ReturnsRootBoolean(bool value)
    {
        var text = Parse.ToXmlString(value);

        Assert.That(text, Does.StartWith(XmlData.CorrectDeclarationString));
        Assert.That(text, Does.EndWith($"<boolean>{value.ToString().ToLower()}</boolean>"));
    }

    [Test]
    public void EmptyList_ReturnsVoidArray()
    {
        var text = Parse.ToXmlString(new List<double>());

        Assert.That(text, Does.StartWith(XmlData.CorrectDeclarationString));
        Assert.That(text, Does.EndWith($"<ArrayOfDouble />"));
    }

    [Test]
    public void EmptyObject_ReturnsVoidAnyType()
    {
        var text = Parse.ToXmlString(new object());

        Assert.That(text, Does.StartWith(XmlData.CorrectDeclarationString));
        Assert.That(text, Does.EndWith($"<anyType />"));
    }

    [Test]
    public void SimpleList_ReturnsArrayOfInteger()
    {
        List<int> list = [1, 2, 3];

        var text = Parse.ToXmlString(list);

        Assert.That(text, Does.StartWith(XmlData.CorrectDeclarationString));
        Assert.That(text, Does.Contain("<ArrayOfInt>"));
        Assert.That(text, Does.Contain("<int>1</int>"));
        Assert.That(text, Does.Contain("<int>2</int>"));
        Assert.That(text, Does.Contain("<int>3</int>"));
        Assert.That(text, Does.EndWith($"</ArrayOfInt>"));
    }

    [Test]
    public void SimpleDictionary_ThrowsNotSupported()
    {
        Dictionary<string, int> dictionary = new()
        {
            { "First", 1 },
            { "Second", 2 },
            { "Third", 3 },
        };

        Assert.Throws<NotSupportedException>(() => Parse.ToXmlString(dictionary));
    }

    [Test]
    public void SimpleObject_ReturnsXmlDocument()
    {
        var simpleObject = new XmlData.RootObjectModel
        {
            Environment = "Humidity: 69%; Wind: 18 km/h",
            IsPrimary = true,
            CapThickness = 0.0005f,
            GeneticCode = ["AGG", "CUC", "UAA"],
            ApexStructure = new()
            {
                [1] = new object(),
                [3] = new object(),
                [5] = new object(),
            },
            VegetationPeriodStart = new DateTime(2025, 4, 1),
        };

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
