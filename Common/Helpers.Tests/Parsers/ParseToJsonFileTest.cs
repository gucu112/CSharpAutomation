using Gucu112.CSharp.Automation.Helpers.Extensions;
using Gucu112.CSharp.Automation.Helpers.Models.Interface;
using Gucu112.CSharp.Automation.Helpers.Parsers;
using Gucu112.CSharp.Automation.Helpers.Tests.Data;

namespace Gucu112.CSharp.Automation.Helpers.Tests.Parsers;

[TestFixture]
public class ParseToJsonFileTest : BaseJsonTest
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

        ParseSettings.FileSystem = Mock.Object;
    }

    [SetUp]
    public void MockMemoryStream()
    {
        var streamMock = GetMemoryStreamMock();

        Mock.Setup(fs => fs.WriteStream(It.IsRegex("validString")))
            .Returns(streamMock.Object).Verifiable();

        Mock.Setup(fs => fs.WriteStream(It.IsRegex("validArray")))
            .Returns(streamMock.Object).Verifiable();

        Mock.Setup(fs => fs.WriteStream(It.IsRegex("validObject")))
            .Returns(streamMock.Object).Verifiable();
    }

    [Test]
    public void ThrowsOnNullValue()
    {
        var path = "existing.json";
        Assert.Throws<ArgumentNullException>(() => Parse.ToJsonFile(null!, path));
        Mock.Verify(fs => fs.WriteStream(path), Times.Exactly(1));
    }

    [Test]
    public void ThrowsOnNullPath()
    {
        Assert.Throws<ArgumentNullException>(() => Parse.ToJsonFile(new object(), null!));
        Mock.Verify(fs => fs.WriteStream(null!), Times.Exactly(1));
    }

    [Test]
    public void ThrowsOnEmptyPath()
    {
        Assert.Throws<ArgumentException>(() => Parse.ToJsonFile(new object(), string.Empty));
        Mock.Verify(fs => fs.WriteStream(string.Empty), Times.Exactly(1));
    }

    [Test]
    public void ThrowsOnNotExistingDirectory()
    {
        var path = @"noDirectory\any.json";
        Assert.Throws<DirectoryNotFoundException>(() => Parse.ToJsonFile(new object(), path));
        Mock.Verify(fs => fs.WriteStream(path), Times.Exactly(1));
    }

    [Test]
    public void ThrowsOnNotExistingPath()
    {
        var path = @".\notExisting.json";
        Assert.Throws<FileNotFoundException>(() => Parse.ToJsonFile(new object(), path));
        Mock.Verify(fs => fs.WriteStream(path), Times.Exactly(1));
    }

    [Test]
    public void CorrectPath_WritesHelloString()
    {
        var path = "validString.json";
        Parse.ToJsonFile(StringData.HelloString, path);
        Mock.Verify(fs => fs.WriteStream(path), Times.Exactly(1));

        var data = GetMemoryStreamData().RemoveSpace();
        Assert.That(data, Is.EqualTo(JsonData.HelloJsonString));
    }

    [Test]
    public void CorrectPath_WritesArray()
    {
        var path = "validArray.json";
        Parse.ToJsonFile(new List<bool>([true]), path);
        Mock.Verify(fs => fs.WriteStream(path), Times.Exactly(1));

        var data = GetMemoryStreamData().RemoveSpace();
        Assert.That(data, Is.EqualTo(JsonData.ValidArrayString));
    }

    [Test]
    public void CorrectPath_WritesObject()
    {
        var path = "validObject.json";
        Parse.ToJsonFile(new object(), path);
        Mock.Verify(fs => fs.WriteStream(path), Times.Exactly(1));

        var data = GetMemoryStreamData().RemoveSpace();
        Assert.That(data, Is.EqualTo(JsonData.EmptyObjectString));
    }
}
