using Gucu112.CSharp.Automation.Helpers.Extensions;
using Gucu112.CSharp.Automation.Helpers.Models;
using Gucu112.CSharp.Automation.Helpers.Parsers;
using Gucu112.CSharp.Automation.Helpers.Tests.Data;
using Gucu112.CSharp.Automation.Helpers.Tests.Parsers.Json;

namespace Gucu112.CSharp.Automation.Helpers.Tests.Parsers.Settings;

[TestFixture]
public class JsonSettingsTest : BaseJsonTest
{
    private static readonly string ReadStreamData = JsonData.SimpleObjectStringWithComment;

    private static readonly HashSet<string> WriteStreamData = ["<", "'&'", ">"];

    private static readonly Mock<IFileSystem> Mock = new();

    [SetUp]
    public void MockFileSystem()
    {
        Mock.SetupSequence(fs => fs.ReadStream(It.IsAny<string>()))
            .Returns(new MemoryStream(ReadStreamData.GetBytes()))
            .Returns(new MemoryStream(ReadStreamData.GetBytes()));

        Mock.SetupSequence(fs => fs.WriteStream(It.IsAny<string>()))
           .Returns(GetMemoryStreamMock().Object)
           .Returns(GetMemoryStreamMock().Object);

        ParseSettings.FileSystem = Mock.Object;

        ParseSettings.Json = new JsonSettings()
        {
            Indentation = 4,
            NullValueHandling = NullValueHandling.Include,
        };
    }

    [Test]
    public void ReturnsJsonSettings()
    {
        var settings = ParseSettings.Json;

        Assert.That(settings, Is.TypeOf<JsonSettings>());

        using (Assert.EnterMultipleScope())
        {
            Assert.That(settings, Has.Property(nameof(settings.Formatting)).And.Property(nameof(settings.MaxDepth)));
            Assert.That(settings, Has.Property(nameof(settings.Indentation)).And.Property(nameof(settings.IndentChar)));
        }
    }

    [Test]
    public void ReturnsDefaultJsonSettings()
    {
        var settings = ParseSettings.Json;

        using (Assert.EnterMultipleScope())
        {
            Assert.That(settings.Encoding, Is.EqualTo(Encoding.UTF8));
            Assert.That(settings.Formatting, Is.EqualTo(Formatting.Indented));
            Assert.That(settings.MaxDepth, Is.EqualTo(16));
            Assert.That(settings.Indentation, Is.EqualTo(4));
            Assert.That(settings.IndentChar, Is.EqualTo(' '));
            Assert.That(settings.StringEscapeHandling, Is.EqualTo(StringEscapeHandling.Default));
            Assert.That(settings.TypeNameHandling, Is.EqualTo(TypeNameHandling.None));
        }
    }

    [Test]
    [NonParallelizable]
    public void ReturnsUpdatedJsonSettings()
    {
        ParseSettings.Json = new JsonSettings
        {
            Encoding = Encoding.Unicode,
            Formatting = Formatting.None,
            MaxDepth = 128,
            Indentation = 1,
            IndentChar = '\t',
            NullValueHandling = NullValueHandling.Ignore,
            TypeNameHandling = TypeNameHandling.All,
        };

        var settings = ParseSettings.Json;

        using (Assert.EnterMultipleScope())
        {
            Assert.That(settings.Encoding, Is.EqualTo(Encoding.Unicode));
            Assert.That(settings.Formatting, Is.EqualTo(Formatting.None));
            Assert.That(settings.MaxDepth, Is.EqualTo(128));
            Assert.That(settings.Indentation, Is.EqualTo(1));
            Assert.That(settings.IndentChar, Is.EqualTo('\t'));
            Assert.That(settings.NullValueHandling, Is.EqualTo(NullValueHandling.Ignore));
            Assert.That(settings.TypeNameHandling, Is.EqualTo(TypeNameHandling.All));
        }
    }

    [TestCaseSource(typeof(JsonData), nameof(JsonData.SimpleDictionary))]
    public void FromJson_UsingSettingsCorrectly<T>(T content)
    {
        var globalSettings = ParseSettings.Json;

        var localSettings = new JsonSettings
        {
            MissingMemberHandling = MissingMemberHandling.Error,
        };

        using (Assert.EnterMultipleScope())
        {
            Assert.That(globalSettings.MissingMemberHandling, Is.EqualTo(MissingMemberHandling.Ignore));
            Assert.That(localSettings.MissingMemberHandling, Is.EqualTo(MissingMemberHandling.Error));
        }

        Assert.Throws<JsonSerializationException>(() => ParseFromJson<JsonData.SimpleObjectModel>(content, localSettings));

        if (typeof(T) == typeof(string))
        {
            Assert.DoesNotThrow(() => ParseFromJson<JsonData.SimpleObjectModel>(content));
        }
    }

    [Test]
    public void FromJsonFile_UsingSettingsCorrectly()
    {
        var globalSettings = ParseSettings.Json;

        var localSettings = new JsonSettings
        {
            ObjectCreationHandling = ObjectCreationHandling.Replace,
        };

        using (Assert.EnterMultipleScope())
        {
            Assert.That(globalSettings.ObjectCreationHandling, Is.EqualTo(ObjectCreationHandling.Auto));
            Assert.That(localSettings.ObjectCreationHandling, Is.EqualTo(ObjectCreationHandling.Replace));
        }

        var localObject = Parse.FromJsonFile<JsonData.SimpleObjectModel>("local.json", localSettings);
        Assert.That(localObject?.ListOfNumbers, Has.Count.EqualTo(5).And.Not.Contains(0));

        var globalObject = Parse.FromJsonFile<JsonData.SimpleObjectModel>("global.json");
        Assert.That(globalObject?.ListOfNumbers, Has.Count.EqualTo(6).And.Contains(0));
    }

    [TestCaseSource(typeof(ObjectData), nameof(ObjectData.SimpleObject))]
    public string ToJson_UsingSettingsCorrectly<T>(object value)
    {
        var globalSettings = ParseSettings.Json;

        var localSettings = new JsonSettings
        {
            DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
        };

        using (Assert.EnterMultipleScope())
        {
            Assert.That(globalSettings.DateFormatHandling, Is.EqualTo(DateFormatHandling.IsoDateFormat));
            Assert.That(localSettings.DateFormatHandling, Is.EqualTo(DateFormatHandling.MicrosoftDateFormat));
        }

        var localContent = ParseToJson<T>(value, localSettings);
        Assert.That(localContent, Does.Match(@"CurrentYearStart.+Date.?173568"));

        var globalContent = ParseToJson<T>(value);
        Assert.That(globalContent, Does.Not.Match(@"CurrentYearStart.+Date.?173568"));

        return globalContent;
    }

    [Test]
    public void ToJsonFile_UsingSettingsCorrectly()
    {
        var globalSettings = ParseSettings.Json;

        var localSettings = new JsonSettings
        {
            StringEscapeHandling = StringEscapeHandling.EscapeHtml,
        };

        using (Assert.EnterMultipleScope())
        {
            Assert.That(globalSettings.StringEscapeHandling, Is.EqualTo(StringEscapeHandling.Default));
            Assert.That(localSettings.StringEscapeHandling, Is.EqualTo(StringEscapeHandling.EscapeHtml));
        }

        Parse.ToJsonFile(WriteStreamData, "local.json", localSettings);
        var localData = GetMemoryStreamData();
        Assert.That(localData, Does.Not.Contain('<').Or.Contain('&').Or.Contain('>'));

        Parse.ToJsonFile(WriteStreamData, "global.json");
        var globalData = GetMemoryStreamData();
        Assert.That(globalData, Does.Contain("<").And.Contain('&').And.Contain('>'));
    }
}
