using Gucu112.CSharp.Automation.Helpers.Models;

namespace Gucu112.CSharp.Automation.Helpers.Providers;

/// <summary>
/// Represents an implementation of file system.
/// </summary>
public class FileSystemProvider : IFileSystem
{
    /// <inheritdoc/>
    public Stream ReadStream(string path)
    {
        return File.OpenRead(path);
    }

    /// <inheritdoc/>
    public Stream WriteStream(string path)
    {
        return File.OpenWrite(path);
    }
}
