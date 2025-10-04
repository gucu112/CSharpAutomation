using Gucu112.CSharp.Automation.Helpers.Extensions;
using Gucu112.CSharp.Automation.Helpers.Models;
using Gucu112.CSharp.Automation.Helpers.Parsers;
using Gucu112.CSharp.Automation.Helpers.Tests.Data;

namespace Gucu112.CSharp.Automation.Helpers.Tests.Parsers.Xml;

[TestFixture]
public class ParseFromXmlFileTest
{
    private static readonly Mock<IFileSystem> Mock = new();

    [OneTimeSetUp]
    public void MockFileSystem()
    {
        Mock.Setup(fs => fs.ReadStream(It.Is<string>(v => v == null)))
            .Throws<ArgumentNullException>().Verifiable();

        Mock.Setup(fs => fs.ReadStream(It.Is<string>(v => v == string.Empty)))
            .Throws<ArgumentException>().Verifiable();

        Mock.Setup(fs => fs.ReadStream(It.IsRegex("noDirectory")))
            .Throws<DirectoryNotFoundException>().Verifiable();

        Mock.Setup(fs => fs.ReadStream(It.IsRegex("notExisting")))
            .Throws<FileNotFoundException>().Verifiable();

        Mock.Setup(fs => fs.ReadStream(It.IsRegex("notValid")))
            .Returns(new MemoryStream(XmlData.IncorrectDeclarationString.GetBytes()));

        Mock.SetupSequence(fs => fs.ReadStream(It.IsRegex("validRoot")))
            .Returns(new MemoryStream(XmlData.EmptyRootElementString.GetBytes()))
            .Returns(new MemoryStream(XmlData.EmptyRootElementString.GetBytes()));

        Mock.Setup(fs => fs.ReadStream(It.IsRegex("validDocument")))
            .Returns(new MemoryStream(XmlData.RootObjectDocumentString.GetBytes()));

        ParseSettings.FileSystem = Mock.Object;
    }

    [Test]
    public void ThrowsOnNullPath()
    {
        Assert.Throws<ArgumentNullException>(() => Parse.FromXmlFile<object>(null!));
        Mock.Verify(fs => fs.ReadStream(null!), Times.Exactly(1));
    }

    [Test]
    public void ThrowsOnEmptyPath()
    {
        Assert.Throws<ArgumentException>(() => Parse.FromXmlFile<object>(string.Empty));
        Mock.Verify(fs => fs.ReadStream(string.Empty), Times.Exactly(1));
    }

    [Test]
    public void ThrowsOnNotExistingDirectory()
    {
        var path = @"noDirectory\any.xml";
        Assert.Throws<DirectoryNotFoundException>(() => Parse.FromXmlFile<object>(path));
        Mock.Verify(fs => fs.ReadStream(path), Times.Exactly(1));
    }

    [Test]
    public void ThrowsOnNotExistingPath()
    {
        var path = @".\notExisting.xml";
        Assert.Throws<FileNotFoundException>(() => Parse.FromXmlFile<object>(path));
        Mock.Verify(fs => fs.ReadStream(path), Times.Exactly(1));
    }

    [Test]
    public void ThrowsOnInvalidSyntax()
    {
        var exception = Assert.Catch(() => Parse.FromXmlFile<XContainer>("notValid.xml")).InnerException;
        Assert.That(exception, Is.TypeOf<XmlException>());
    }

    [Test]
    public void ThrowsOnInvalidType()
    {
        var exception = Assert.Catch(() => Parse.FromXmlFile<XObject>("validRoot.xml")).InnerException;
        Assert.That(exception, Is.TypeOf<InvalidOperationException>());
    }

    [Test]
    public void CorrectPath_ReadsElement()
    {
        var xmlElement = Parse.FromXmlFile<XElement>("validRoot.xml");

        TestContext.Out.WriteLine(xmlElement);

        Assert.That(xmlElement?.Name, Has.Property("LocalName").EqualTo("Root"));
    }

    [Test]
    public void CorrectPath_ReadsDocument()
    {
        var xmlDocument = Parse.FromXmlFile<XDocument>("validDocument.xml");

        TestContext.Out.WriteLine(xmlDocument);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(
                xmlDocument?.Root?.FirstAttribute?.Name,
                Has.Property("LocalName").EqualTo("IsPrimary"));
            Assert.That(
                xmlDocument?.Root?.Elements().ToList(),
                Has.Count.EqualTo(3).And.Some.Property("HasElements").EqualTo(true));
        }
    }
}
