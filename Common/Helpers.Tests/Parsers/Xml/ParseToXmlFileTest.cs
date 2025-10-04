using Gucu112.CSharp.Automation.Helpers.Models;
using Gucu112.CSharp.Automation.Helpers.Parsers;
using Gucu112.CSharp.Automation.Helpers.Tests.Data;

namespace Gucu112.CSharp.Automation.Helpers.Tests.Parsers.Xml;
public class ParseToXmlFileTest : BaseTest
{
    private static readonly Mock<IFileSystem> Mock = new();

    [OneTimeSetUp]
    public void MockFileSystem()
    {
        Mock.Setup(fs => fs.WriteStream(It.Is<string>(v => v == null)))
            .Throws<ArgumentNullException>().Verifiable();

        Mock.Setup(fs => fs.WriteStream(It.Is<string>(v => v == string.Empty)))
            .Throws<ArgumentException>().Verifiable();

        Mock.Setup(fs => fs.WriteStream(It.IsRegex("noDirectory")))
            .Throws<DirectoryNotFoundException>().Verifiable();

        Mock.Setup(fs => fs.WriteStream(It.IsRegex("notExisting")))
            .Throws<FileNotFoundException>().Verifiable();

        Mock.Setup(fs => fs.WriteStream(It.IsRegex("validString")))
            .Returns(GetMemoryStreamMock().Object).Verifiable();

        Mock.Setup(fs => fs.WriteStream(It.IsRegex("validRoot")))
            .Returns(GetMemoryStreamMock().Object).Verifiable();

        Mock.Setup(fs => fs.WriteStream(It.IsRegex("validDocument")))
            .Returns(GetMemoryStreamMock().Object).Verifiable();

        ParseSettings.FileSystem = Mock.Object;
    }

    [Test]
    public void ThrowsOnNullValue()
    {
        var path = "existing.xml";
        Assert.Throws<ArgumentNullException>(() => Parse.ToXmlFile(null!, path));
        Mock.Verify(fs => fs.WriteStream(path), Times.Exactly(1));
    }

    [Test]
    public void ThrowsOnNullPath()
    {
        Assert.Throws<ArgumentNullException>(() => Parse.ToXmlFile(new object(), null!));
        Mock.Verify(fs => fs.WriteStream(null!), Times.Exactly(1));
    }

    [Test]
    public void ThrowsOnEmptyPath()
    {
        Assert.Throws<ArgumentException>(() => Parse.ToXmlFile(new object(), string.Empty));
        Mock.Verify(fs => fs.WriteStream(string.Empty), Times.Exactly(1));
    }

    [Test]
    public void ThrowsOnNotExistingDirectory()
    {
        var path = @"noDirectory\any.xml";
        Assert.Throws<DirectoryNotFoundException>(() => Parse.ToXmlFile(new object(), path));
        Mock.Verify(fs => fs.WriteStream(path), Times.Exactly(1));
    }

    [Test]
    public void ThrowsOnNotExistingPath()
    {
        var path = @".\notExisting.xml";
        Assert.Throws<FileNotFoundException>(() => Parse.ToXmlFile(new object(), path));
        Mock.Verify(fs => fs.WriteStream(path), Times.Exactly(1));
    }

    [Test]
    public void CorrectPath_WritesString()
    {
        var path = "validString.xml";
        Parse.ToXmlFile(StringData.LoremIpsumString, path);
        Mock.Verify(fs => fs.WriteStream(path), Times.Exactly(1));

        var data = GetMemoryStreamData();
        TestContext.Out.WriteLine(data);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(data, Does.StartWith(XmlData.CorrectDeclarationString));
            Assert.That(data, Does.Contain(StringData.LoremIpsumString));
        }
    }

    [Test]
    public void CorrectPath_WritesArray()
    {
        var path = "validRoot.json";
        Parse.ToXmlFile(new XmlData.RootOnlyModel(), path);
        Mock.Verify(fs => fs.WriteStream(path), Times.Exactly(1));

        var data = GetMemoryStreamData();
        TestContext.Out.WriteLine(data);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(data, Does.StartWith(XmlData.CorrectDeclarationString));
            Assert.That(data, Does.Contain(XmlData.VoidRootElementString));
        }
    }

    [Test]
    public void CorrectPath_WritesObject()
    {
        var path = "validDocument.json";
        Parse.ToXmlFile(ObjectData.SimpleRootObjectValue, path);
        Mock.Verify(fs => fs.WriteStream(path), Times.Exactly(1));

        var data = GetMemoryStreamData();
        TestContext.Out.WriteLine(data);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(data, Does.StartWith(XmlData.CorrectDeclarationString));
            Assert.That(data, Does.Contain(@"<RootElement IsPrimary=""true"">"));
            Assert.That(data, Does.Contain("Humidity: 69%; Wind: 18 km/h"));
            Assert.That(data, Does.EndWith("</RootElement>"));
        }
    }
}
