using Gucu112.CSharp.Automation.Helpers.Extensions;
using Gucu112.CSharp.Automation.Helpers.Models.Interface;
using Gucu112.CSharp.Automation.Helpers.Parsers;
using Gucu112.CSharp.Automation.Helpers.Tests.Data;

namespace Gucu112.CSharp.Automation.Helpers.Tests.Parsers;

[TestFixture]
public class ParseFromJsonFileTest
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
            .Returns(new MemoryStream(JsonData.InvalidObjectString.GetBytes()));

        Mock.SetupSequence(fs => fs.ReadStream(It.IsRegex("validString")))
            .Returns(new MemoryStream(JsonData.EmptyJsonString.GetBytes()))
            .Returns(new MemoryStream(JsonData.EmptyJsonString.GetBytes()));

        Mock.Setup(fs => fs.ReadStream(It.IsRegex("validArray")))
            .Returns(new MemoryStream(JsonData.ValidArrayString.GetBytes()));

        Mock.Setup(fs => fs.ReadStream(It.IsRegex("validObject")))
            .Returns(new MemoryStream(JsonData.SimpleDictionaryString.GetBytes()));

        ParseSettings.FileSystem = Mock.Object;
    }

    [Test]
    public void ThrowsOnNullPath()
    {
        Assert.Throws<ArgumentNullException>(() => Parse.FromJsonFile<object>(null!));
        Mock.Verify(fs => fs.ReadStream(null!), Times.Exactly(1));
    }

    [Test]
    public void ThrowsOnEmptyPath()
    {
        Assert.Throws<ArgumentException>(() => Parse.FromJsonFile<object>(string.Empty));
        Mock.Verify(fs => fs.ReadStream(string.Empty), Times.Exactly(1));
    }

    [Test]
    public void ThrowsOnNotExistingDirectory()
    {
        var path = @"noDirectory\any.json";
        Assert.Throws<DirectoryNotFoundException>(() => Parse.FromJsonFile<object>(path));
        Mock.Verify(fs => fs.ReadStream(path), Times.Exactly(1));
    }

    [Test]
    public void ThrowsOnNotExistingPath()
    {
        var path = @".\notExisting.json";
        Assert.Throws<FileNotFoundException>(() => Parse.FromJsonFile<object>(path));
        Mock.Verify(fs => fs.ReadStream(path), Times.Exactly(1));
    }

    [Test]
    public void ThrowsOnInvalidSyntax()
    {
        Assert.Throws<JsonReaderException>(() => Parse.FromJsonFile<JValue>("notValid.json"));
    }

    [Test]
    public void ThrowsOnInvalidType()
    {
        Assert.Throws<JsonSerializationException>(() => Parse.FromJsonFile<JObject>("validString.json"));
    }

    [Test]
    public void CorrectPath_ReadsEmptyString()
    {
        Assert.That(Parse.FromJsonFile<JValue>("validString.json"), Has.Property("Value").TypeOf<string>().And.Empty);
    }

    [Test]
    public void CorrectPath_ReadsArray()
    {
        Assert.That(Parse.FromJsonFile<JArray>("validArray.json"), Has.ItemAt(0).Property("Value").EqualTo(true));
    }

    [Test]
    public void CorrectPath_ReadsObject()
    {
        Assert.That(Parse.FromJsonFile<JObject>("validObject.json"), Has.Exactly(3).Items.And.ItemAt("First").Property("Value").EqualTo(1));
    }
}
