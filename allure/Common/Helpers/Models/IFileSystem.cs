namespace Gucu112.CSharp.Automation.Helpers.Models;

/// <summary>
/// Represents a file system.
/// </summary>
public interface IFileSystem
{
    /// <summary>
    /// Reads a stream from the specified file path.
    /// </summary>
    /// <param name="path">The path of the file to read.</param>
    /// <returns>The output stream containing the file data.</returns>
    public Stream ReadStream(string path);

    /// <summary>
    /// Writes a stream to the specified file path.
    /// </summary>
    /// <param name="path">The path of the file to write.</param>
    /// <returns>The input stream to write to the file.</returns>
    public Stream WriteStream(string path);
}
