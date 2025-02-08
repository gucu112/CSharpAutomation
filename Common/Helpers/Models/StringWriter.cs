using BaseStringWriter = System.IO.StringWriter;

namespace Gucu112.CSharp.Automation.Helpers.Models;

/// <summary>
/// Initializes a new instance of the StringWriter class.
/// </summary>
/// <param name="builder">The StringBuilder to write to. If null, a new StringBuilder will be created.</param>
/// <param name="encoding">The encoding to use. If null, <see cref="Encoding.UTF8"/> will be used.</param>
/// <param name="formatProvider">The format provider to use. If null, the default format provider will be used.</param>
internal class StringWriter(
    StringBuilder? builder = null,
    Encoding? encoding = null,
    IFormatProvider? formatProvider = null)
    : BaseStringWriter(builder ?? new StringBuilder(), formatProvider)
{
    /// <summary>
    /// Initializes a new instance of the StringWriter class with the specified encoding.
    /// </summary>
    /// <param name="encoding">The encoding to use.</param>
    public StringWriter(Encoding encoding)
        : this(null, encoding, null)
    {
    }

    /// <summary>
    /// Gets the encoding used by this StringWriter. If no encoding was specified, <see cref="Encoding.UTF8"/> is used.
    /// </summary>
    public override Encoding Encoding => encoding ?? Encoding.UTF8;
}
