using System.Diagnostics.CodeAnalysis;
using Gucu112.CSharp.Automation.Helpers.Parsers;
using Gucu112.CSharp.Automation.Helpers.Tests.Data;

namespace Gucu112.CSharp.Automation.Helpers.Tests.Parsers.Xml;

[TestFixture]
public class ParseFromXmlTest : BaseXmlTest
{
    [Test]
    [SuppressMessage("nullable", "NX0002", Justification = "test")]
    public void ThrowsOnNull()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.Throws<ArgumentNullException>(() => Parse.FromXml<object>((string)null!));
            Assert.Throws<ArgumentNullException>(() => Parse.FromXml<object>((StreamReader)null!));
            Assert.Throws<ArgumentNullException>(() => Parse.FromXml<object>((Stream)null!));
        }
    }

    [TestCaseSource(typeof(XmlData), nameof(XmlData.EmptyContent))]
    public void EmptyContent_ThrowsRootElementMissing<T>(T content)
    {
        var exception = Assert.Catch(() => ParseFromXml<object>(content)).InnerException;

        using (Assert.EnterMultipleScope())
        {
            Assert.That(exception, Is.TypeOf<XmlException>());
            Assert.That(exception?.Message, Does.StartWith("root element is missing").IgnoreCase);
        }
    }

    [TestCaseSource(typeof(XmlData), nameof(XmlData.IncorrectDeclaration))]
    public void IncorrectDeclaration_ThrowsUnexpectedToken<T>(T content)
    {
        TestContext.Out.WriteLine(content);

        var exception = Assert.Catch(() => ParseFromXml<object>(content)).InnerException;

        using (Assert.EnterMultipleScope())
        {
            Assert.That(exception, Is.TypeOf<XmlException>());
            Assert.That(exception?.Message, Does.Contain("declaration is invalid").Or.Contain("unexpected token"));
        }
    }

    [TestCaseSource(typeof(XmlData), nameof(XmlData.CorrectDeclaration))]
    public void CorrectDeclarationAlone_ThrowsRootElementMissing<T>(T content)
    {
        TestContext.Out.WriteLine(content);

        var exception = Assert.Catch(() => ParseFromXml<object>(content)).InnerException;

        using (Assert.EnterMultipleScope())
        {
            Assert.That(exception, Is.TypeOf<XmlException>());
            Assert.That(exception?.Message, Does.StartWith("Root element is missing"));
        }
    }

    [TestCaseSource(typeof(XmlData), nameof(XmlData.EmptyRootElement))]
    public void EmptyRootElement_ThrowsRootElementNotExpected<T>(T content)
    {
        TestContext.Out.WriteLine(content);

        var exception = Assert.Catch(() => ParseFromXml<object>(content)).InnerException;

        using (Assert.EnterMultipleScope())
        {
            Assert.That(exception, Is.TypeOf<InvalidOperationException>());
            Assert.That(exception?.Message, Does.Match("Root .+ not expected"));
        }
    }

    [TestCaseSource(typeof(XmlData), nameof(XmlData.EmptyRootElement))]
    public void EmptyRootElement_ReturnEmptyObject<T>(T content)
    {
        TestContext.Out.WriteLine(content);

        var customObject = ParseFromXml<XmlData.RootOnlyModel>(content);

        Assert.That(customObject, Is.Not.Null);
        Assert.That(customObject?.RootObject, Is.Null);
    }

    [TestCaseSource(typeof(XmlData), nameof(XmlData.RootObject))]
    public void CorrectXmlObjects_ReturnObject<T>(T content)
    {
        TestContext.Out.WriteLine(content);

        var customObject = ParseFromXml<XmlData.RootObjectModel>(content)
            ?? throw new AssertionException($"{nameof(XmlData.RootObjectModel)} object is null.");

        using (Assert.EnterMultipleScope())
        {
            Assert.That(customObject.IsPrimary, Is.False);
            Assert.That(customObject.CapThickness, Is.EqualTo(0.0003f));
            Assert.That(customObject.GeneticCode, Has.Count.GreaterThanOrEqualTo(3)
                .And.All.Length.EqualTo(3));
            Assert.That(customObject.ApexStructure, Is.Empty);
            Assert.That(customObject.VegetationPeriodStart, Is.GreaterThan(new DateTime(2025, 1, 1))
                .And.Property("Month").EqualTo(4));
        }
    }

    [Test]
    public void CorrectXmlObjects_ReturnXDocumentObject()
    {
        var content = XmlData.RootObjectDocumentString;

        TestContext.Out.WriteLine(content);

        var expectedDeclaration = new XDeclaration("1.0", Encoding.UTF8).ToString();
        var document = ParseFromXml<XDocument>(content)
            ?? throw new AssertionException($"{nameof(XDocument)} object is null.");
        var descendants = document.Descendants().ToList();
        var elements = document.Elements().ToList();

        using (Assert.EnterMultipleScope())
        {
            Assert.That(document.Declaration?.ToString(), Is.EqualTo(expectedDeclaration));
            Assert.That(document.Root?.Name.LocalName, Is.EqualTo("RootElement"));
            Assert.That(descendants, Has.One.Property("Name").EqualTo(XName.Get("GeneticCode")));
            Assert.That(elements, Has.Count.EqualTo(1));
            Assert.That(elements[0], Is.EqualTo(descendants[0]));
        }
    }
}
