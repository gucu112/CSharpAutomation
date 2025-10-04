using Gucu112.CSharp.Automation.Helpers.Extensions;
using Gucu112.CSharp.Automation.Helpers.Models;
using Gucu112.CSharp.Automation.Helpers.Parsers;
using Gucu112.CSharp.Automation.Helpers.Tests.Data;
using XmlReaderSettings = Gucu112.CSharp.Automation.Helpers.Models.XmlReaderSettings;

namespace Gucu112.CSharp.Automation.Helpers.Tests.Parsers.Xml;

public class XmlReaderSettingsTest : BaseXmlTest
{
    private static readonly string ReadStreamData = XmlData.RootObjectDocumentString;

    private static readonly Mock<IFileSystem> Mock = new();

    [OneTimeSetUp]
    public void MockFileSystem()
    {
        Mock.SetupSequence(fs => fs.ReadStream(It.IsAny<string>()))
            .Returns(new MemoryStream(ReadStreamData.GetBytes()))
            .Returns(new MemoryStream(ReadStreamData.GetBytes()));

        ParseSettings.FileSystem = Mock.Object;
    }

    [SetUp]
    public void SetupSettings()
    {
        ParseSettings.XmlRead = new XmlReaderSettings()
        {
            Namespaces = true,
            Normalization = true,
        };
    }

    [TearDown]
    public void ResetSettings()
    {
        ParseSettings.XmlRead = new XmlReaderSettings();
    }

    [Test]
    public void ReturnsXmlReaderSettings()
    {
        var settings = ParseSettings.XmlRead;

        Assert.That(settings, Is.TypeOf<XmlReaderSettings>());

        using (Assert.EnterMultipleScope())
        {
            Assert.That(settings, Has.Property(nameof(settings.Namespaces)).And.Property(nameof(settings.EntityHandling)));
            Assert.That(settings, Has.Property(nameof(settings.Normalization)).And.Property(nameof(settings.WhitespaceHandling)));
        }
    }

    [Test]
    public void ReturnsDefaultXmlReaderSettings()
    {
        var settings = ParseSettings.XmlRead;

        using (Assert.EnterMultipleScope())
        {
            Assert.That(settings.Encoding, Is.EqualTo(ParseSettings.DefaultEncoding));
            Assert.That(settings.Namespaces, Is.True);
            Assert.That(settings.Normalization, Is.True);
            Assert.That(settings.EntityHandling, Is.EqualTo(EntityHandling.ExpandCharEntities));
            Assert.That(settings.WhitespaceHandling, Is.EqualTo(WhitespaceHandling.None));
        }
    }

    [Test]
    [NonParallelizable]
    public void ReturnsUpdatedXmlReaderSettings()
    {
        ParseSettings.XmlRead = new XmlReaderSettings
        {
            Encoding = Encoding.Unicode,
            Namespaces = false,
            Normalization = false,
            EntityHandling = EntityHandling.ExpandEntities,
            WhitespaceHandling = WhitespaceHandling.Significant,
        };

        var settings = ParseSettings.XmlRead;

        using (Assert.EnterMultipleScope())
        {
            Assert.That(settings.Encoding, Is.EqualTo(Encoding.Unicode));
            Assert.That(settings.Namespaces, Is.False);
            Assert.That(settings.Normalization, Is.False);
            Assert.That(settings.EntityHandling, Is.EqualTo(EntityHandling.ExpandEntities));
            Assert.That(settings.WhitespaceHandling, Is.EqualTo(WhitespaceHandling.Significant));
        }
    }

    [TestCaseSource(typeof(XmlData), nameof(XmlData.RootObject))]
    public void FromXml_UsingSettingsCorrectly<T>(T content)
    {
        var globalSettings = ParseSettings.XmlRead;

        var localSettings = new XmlReaderSettings
        {
            Namespaces = false,
            Normalization = false,
        };

        using (Assert.EnterMultipleScope())
        {
            Assert.That(globalSettings.Namespaces, Is.True);
            Assert.That(globalSettings.Normalization, Is.True);
            Assert.That(localSettings.Namespaces, Is.False);
            Assert.That(localSettings.Normalization, Is.False);
        }

        Assert.DoesNotThrow(() => ParseFromXml<XmlData.RootObjectModel>(content, localSettings));

        if (typeof(T) == typeof(string))
        {
            Assert.DoesNotThrow(() => ParseFromXml<XmlData.RootObjectModel>(content));
        }
    }

    [TestCaseSource(typeof(XmlData), nameof(XmlData.RootObjectResolver))]
    public void FromXml_UsingEntityHandlingSetting_ExpandEntitiesIsSlower<T>(Func<T> contentResolver)
    {
        var slowerSettings = new XmlReaderSettings
        {
            EntityHandling = EntityHandling.ExpandEntities,
        };

        var (slowerDataObject, slowerElapsedTime) = MeasureExecutionTime(
            () => ParseFromXml<XmlData.RootObjectModel>(contentResolver(), slowerSettings));
        var slowerDataObjectJsonString = JsonConvert.SerializeObject(slowerDataObject);
        TestContext.Out.WriteLine($"EntityHandling.ExpandEntities in {slowerElapsedTime}");

        var quickerSettings = new XmlReaderSettings
        {
            EntityHandling = EntityHandling.ExpandCharEntities,
        };

        var (quickerDataObject, quickerElapsedTime) = MeasureExecutionTime(
            () => ParseFromXml<XmlData.RootObjectModel>(contentResolver(), quickerSettings));
        var quickerDataObjectJsonString = JsonConvert.SerializeObject(quickerDataObject);
        TestContext.Out.WriteLine($"EntityHandling.ExpandCharEntities in {quickerElapsedTime}");

        using (Assert.EnterMultipleScope())
        {
            Assert.That(slowerDataObjectJsonString, Is.EqualTo(quickerDataObjectJsonString));
            Assert.That(slowerElapsedTime, Is.GreaterThan(quickerElapsedTime));
        }

        if (typeof(T) == typeof(StreamReader))
        {
            var reader = contentResolver() as StreamReader;
            Assert.That(reader?.CurrentEncoding, Is.EqualTo(ParseSettings.DefaultEncoding));
        }
    }

    [TestCaseSource(typeof(XmlData), nameof(XmlData.RootObjectSpaceResolver))]
    public void FromXml_UsingWhitespaceHandlingSetting_NoneIsQuicker<T>(Func<T> contentResolver)
    {
        var slowerSettings = new XmlReaderSettings
        {
            WhitespaceHandling = WhitespaceHandling.All,
        };

        var (slowerDataObject, slowerElapsedTime) = MeasureExecutionTime(
            () => ParseFromXml<XmlData.RootObjectModel>(contentResolver(), slowerSettings));
        var slowerDataObjectJsonString = JsonConvert.SerializeObject(slowerDataObject);
        TestContext.Out.WriteLine($"WhitespaceHandling.All in {slowerElapsedTime}");

        var quickerSettings = new XmlReaderSettings
        {
            WhitespaceHandling = WhitespaceHandling.None,
        };

        var (quickerDataObject, quickerElapsedTime) = MeasureExecutionTime(
            () => ParseFromXml<XmlData.RootObjectModel>(contentResolver(), quickerSettings));
        var quickerDataObjectJsonString = JsonConvert.SerializeObject(quickerDataObject);
        TestContext.Out.WriteLine($"WhitespaceHandling.None in {quickerElapsedTime}");

        using (Assert.EnterMultipleScope())
        {
            Assert.That(slowerDataObjectJsonString, Is.EqualTo(quickerDataObjectJsonString));
            Assert.That(slowerElapsedTime, Is.GreaterThan(quickerElapsedTime));
        }

        if (typeof(T) == typeof(StreamReader))
        {
            var reader = contentResolver() as StreamReader;
            Assert.That(reader?.CurrentEncoding, Is.EqualTo(ParseSettings.DefaultEncoding));
        }
    }

    [Test]
    public void FromXmlFile_UsingSettingsCorrectly()
    {
        var globalSettings = ParseSettings.XmlRead;

        var localSettings = new XmlReaderSettings
        {
            Namespaces = true,
            Normalization = false,
        };

        using (Assert.EnterMultipleScope())
        {
            Assert.That(globalSettings.Normalization, Is.True);
            Assert.That(localSettings.Normalization, Is.False);
        }

        var localObject = Parse.FromXmlFile<XmlData.RootObjectModel>("local.xml", localSettings);
        var localDataObjectJsonString = JsonConvert.SerializeObject(localObject);
        Assert.That(localObject?.GeneticCode, Has.Count.EqualTo(4).And.Contains("GUC"));

        var globalObject = Parse.FromXmlFile<XmlData.RootObjectModel>("global.xml");
        var globalDataObjectJsonString = JsonConvert.SerializeObject(globalObject);
        Assert.That(globalObject?.GeneticCode, Has.Count.EqualTo(4).And.Contains("GUC"));

        Mock.Verify(fs => fs.ReadStream(It.IsAny<string>()), Times.Exactly(2));
        Assert.That(localDataObjectJsonString, Is.EqualTo(globalDataObjectJsonString));
    }
}
