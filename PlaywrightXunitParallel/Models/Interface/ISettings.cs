namespace Gucu112.PlaywrightXunitParallel.Models.Interface;

public interface ISettings
{
    string Environment { get; }
    string RootPath { get; }
    IList<EntryPoint> EntryPoints { get; }

    public string BrowserName { get; }
    public int ExpectTimeout { get; }
    public BrowserNewContextOptions BrowserOptions { get; }
    public BrowserTypeLaunchOptions LaunchOptions { get; }

    public string ScreenshotDir { get; }
    public string RecordVideoDir { get; }
}
