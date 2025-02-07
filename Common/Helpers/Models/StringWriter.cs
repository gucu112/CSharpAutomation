using BaseStringWriter = System.IO.StringWriter;

namespace Gucu112.CSharp.Automation.Helpers.Models;

internal class StringWriter(
    StringBuilder? builder = null,
    Encoding? encoding = null,
    IFormatProvider? formatProvider = null
) : BaseStringWriter(builder ?? new StringBuilder(), formatProvider)
{
    public StringWriter(Encoding encoding)
        : this(null, encoding, null)
    {
    }

    public override Encoding Encoding => encoding ?? Encoding.UTF8;
}
