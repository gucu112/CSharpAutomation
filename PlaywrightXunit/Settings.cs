using System.Collections;
using System.Xml.Linq;
using Gucu112.PlaywrightXunit.Extensions;

namespace Gucu112.PlaywrightXunit;

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
        var hashtable = Environment.GetEnvironmentVariables() as Hashtable ?? new Hashtable();
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
}
