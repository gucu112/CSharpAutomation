using Gucu112.CSharp.Automation.Helpers.Extensions;
using Gucu112.CSharp.Automation.Helpers.Models.Interface;
using Gucu112.CSharp.Automation.Helpers.Parsers;
using Gucu112.CSharp.Automation.Helpers.Tests.Data;

namespace Gucu112.CSharp.Automation.Helpers.Tests.Parsers;

[TestFixture]
public class ParseFromJsonFileTest
{
    [OneTimeSetUp]
    public void MockFileSystem()
    {
        var fileSystemMock = new Mock<IFileSystem>();

        fileSystemMock.Setup(fs => fs.ReadStream(It.Is<string>(v => v == null)))
            .Throws<ArgumentNullException>();

        fileSystemMock.Setup(fs => fs.ReadStream(It.Is<string>(v => v == string.Empty)))
            .Throws<ArgumentException>();

        fileSystemMock.Setup(fs => fs.ReadStream(It.IsNotIn(null!, string.Empty)))
            .Throws<DirectoryNotFoundException>();

        fileSystemMock.Setup(fs => fs.ReadStream(It.IsRegex("notValid")))
            .Returns(new MemoryStream(JsonData.InvalidObjectString.GetBytes()));

        fileSystemMock.SetupSequence(fs => fs.ReadStream(It.IsRegex("valid")))
            .Returns(new MemoryStream(JsonData.ValidArrayString.GetBytes()))
            .Returns(new MemoryStream(JsonData.ValidArrayString.GetBytes()));

        ParseSettings.FileSystem = fileSystemMock.Object;
    }

    [Test]
    public void ThrowsOnNullOrEmptyPath()
    {
        Assert.Throws<ArgumentNullException>(() => Parse.FromJsonFile<object>(null!));
        Assert.Throws<ArgumentException>(() => Parse.FromJsonFile<object>(string.Empty));
    }

    [Test]
    public void ThrowsOnNotExistingPath()
    {
        Assert.Throws<DirectoryNotFoundException>(() => Parse.FromJsonFile<object>(@"notExisting.json"));
    }

    [Test]
    public void ThrowsOnInvalidSyntax()
    {
        Assert.Throws<JsonReaderException>(() => Parse.FromJsonFile<JValue>(@"notValid.json"));
    }

    [Test]
    public void ThrowsOnInvalidType()
    {
        Assert.Throws<JsonSerializationException>(() => Parse.FromJsonFile<JObject>(@"valid.json"));
    }

    [Test]
    public void CorrectPathReturnsEmptyArray()
    {
        Assert.That(Parse.FromJsonFile<JArray>(@"valid.json"), Is.Not.Empty);
    }
}
