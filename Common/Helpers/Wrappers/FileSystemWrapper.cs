using Gucu112.CSharp.Automation.Helpers.Models.Interface;

namespace Gucu112.CSharp.Automation.Helpers.Wrappers;

/// <summary>
/// Represents an implementation of file system.
/// </summary>
public class FileSystemWrapper : IFileSystem
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
