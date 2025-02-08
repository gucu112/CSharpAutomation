using Gucu112.CSharp.Automation.Helpers.Parsers;
using Gucu112.CSharp.Automation.Helpers.Tests.Data;

namespace Gucu112.CSharp.Automation.Helpers.Tests.Parsers.Xml;

[TestFixture]
public class ParseFromXmlTest : BaseXmlTest
{
    [Test]
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
        var exception = Assert.Catch(() => ParseFromXml<object>(content)).InnerException;

        using (Assert.EnterMultipleScope())
        {
            Assert.That(exception, Is.TypeOf<XmlException>());
            Assert.That(exception?.Message, Does.StartWith("root element is missing").IgnoreCase);
        }
    }

    [TestCaseSource(typeof(XmlData), nameof(XmlData.EmptyRootElement))]
    public void EmptyRootElement_ThrowsRootElementNotExpected<T>(T content)
    {
        var exception = Assert.Catch(() => ParseFromXml<object>(content)).InnerException;

        using (Assert.EnterMultipleScope())
        {
            Assert.That(exception, Is.TypeOf<InvalidOperationException>());
            Assert.That(exception?.Message, Does.Match("root .+ not expected"));
        }
    }

    [TestCaseSource(typeof(XmlData), nameof(XmlData.EmptyRootElement))]
    public void EmptyRootElement_ReturnEmptyObject<T>(T content)
    {
        var customObject = ParseFromXml<XmlData.RootObjectModel>(content);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(customObject, Is.Not.Null);
            Assert.That(customObject?.Environment, Is.Null);
            Assert.That(customObject?.IsPrimary, Is.False);
            Assert.That(customObject?.CapThickness, Is.EqualTo(0));
            Assert.That(customObject?.GeneticCode, Is.Empty);
            Assert.That(customObject?.ApexStructure, Is.Null);
            Assert.That(customObject?.VegetationPeriodStart, Is.EqualTo(DateTime.MinValue));
        }
    }

    [TestCaseSource(typeof(XmlData), nameof(XmlData.RootObject))]
    public void CorrectXmlObjects_ReturnObject<T>(T content)
    {
        var customObject = ParseFromXml<XmlData.RootObjectModel>(content);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(customObject?.IsPrimary, Is.False);
            Assert.That(customObject?.CapThickness, Is.EqualTo(0.0003f));
            Assert.That(customObject?.GeneticCode, Has.Count.GreaterThanOrEqualTo(3)
                .And.All.Length.EqualTo(3));
            Assert.That(customObject?.ApexStructure, Is.Null);
            Assert.That(customObject?.VegetationPeriodStart, Is.GreaterThan(new DateTime(2025, 1, 1))
                .And.Property("Month").EqualTo(4));
        }
    }
}
