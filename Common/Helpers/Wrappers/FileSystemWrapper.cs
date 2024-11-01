using Gucu112.CSharp.Automation.Helpers.Models.Interface;

namespace Gucu112.CSharp.Automation.Helpers.Wrappers;

public class FileSystemWrapper : IFileSystem
{
    public Stream ReadStream(string path)
    {
        return File.OpenRead(path);
    }

    public Stream WriteStream(string path)
    {
        return File.OpenWrite(path);
    }
}
