namespace Gucu112.CSharp.Automation.PlaywrightXunitParallel.Models.Interface;

public interface ISettings
{
    string Environment { get; }
    string RootPath { get; }
    IList<EntryPoint> EntryPoints { get; }

    string BrowserName { get; }
    float ExpectTimeout { get; }
    BrowserNewContextOptions BrowserOptions { get; }
    BrowserTypeLaunchOptions LaunchOptions { get; }

    bool IsVideoEnabled { get; }
    string? RecordVideoDir { get; }
}
