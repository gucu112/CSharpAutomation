using System.Collections;
using System.Xml.Linq;
using Gucu112.CSharp.Automation.PlaywrightXunit.Extensions;

namespace Gucu112.CSharp.Automation.PlaywrightXunit;

public class Settings
{
    private const string RunSettingsFilePath = ".runsettings";
    private const string RunSettingsRootNodeName = "RunSettings";

    private readonly XDocument document;
    private readonly IList<XElement> elements;

    public Settings()
    {
        document = XDocument.Load(RunSettingsFilePath);
        elements = (document.Root ?? new XElement(RunSettingsRootNodeName)).Elements().ToList();
    }

    public static IDictionary<string, string> GetEnvironmentVariables()
    {
        var hashtable = Environment.GetEnvironmentVariables() as Hashtable ?? [];
        return hashtable.Cast<DictionaryEntry>().ToDictionary(e => $"{e.Key}", e => $"{e.Value}");
    }

    public static string GetEnvironmentVariable(string name)
    {
        return Environment.GetEnvironmentVariable(name) ?? string.Empty;
    }

    public string GetBrowserName()
    {
        var element = elements.First(element => element.Name == "Playwright").Elements()
            .First(element => element.Name == "BrowserName");

        return element.Value;
    }

    public int GetExpectTimeout()
    {
        var element = elements.First(element => element.Name == "Playwright").Elements()
            .First(element => element.Name == "ExpectTimeout");

        return Convert.ToInt32(element.Value);
    }

    public BrowserNewContextOptions GetBrowserOptions()
    {
        var element = elements.First(element => element.Name == "Playwright").Elements()
            .First(element => element.Name == "BrowserOptions");

        return element.Deserialize<BrowserNewContextOptions>() ?? new BrowserNewContextOptions();
    }

    public BrowserTypeLaunchOptions GetLaunchOptions()
    {
        var element = elements.First(element => element.Name == "Playwright").Elements()
            .First(element => element.Name == "LaunchOptions");

        return element.Deserialize<BrowserTypeLaunchOptions>() ?? new BrowserTypeLaunchOptions();
    }

    public IDictionary<string, string> GetTestParameters()
    {
        return elements.First(element => element.Name == "TestRunParameters").Elements()
            .Where(element => !string.IsNullOrEmpty(element.Attribute("name")?.Value))
            .ToDictionary(element => element.Attribute("name")?.Value ?? string.Empty,
                element => element.Attribute("value")?.Value ?? string.Empty);
    }

    public string GetTestParameter(string name)
    {
        return elements.First(element => element.Name == "TestRunParameters").Elements()
            .Single(element => element.Attribute("name")?.Value == name)
            .Attribute("value")?.Value ?? string.Empty;
    }
}
