namespace Gucu112.CSharp.Automation.Helpers.Models;

public sealed class JsonSettings : JsonSerializerSettings
{
    internal const int DefaultIndentation = 2;
    internal const char DefaultIndentChar = ' ';
    internal const int DefaultMaxDepth = 16;
    internal static readonly Encoding DefaultEncoding = Encoding.UTF8;
    internal static readonly Formatting DefaultFormatting = Formatting.Indented;
    internal static readonly NullValueHandling DefaultNullValueHandling = NullValueHandling.Ignore;

    internal Encoding? encoding;
    internal int? indentation;
    internal char? indentChar;

    public Encoding Encoding
    {
        get => encoding ?? DefaultEncoding;
        set => encoding = value;
    }

    public int Indentation
    {
        get => indentation ?? DefaultIndentation;
        set => indentation = value;
    }

    public char IndentChar
    {
        get => indentChar ?? DefaultIndentChar;
        set => indentChar = value;
    }

    public JsonSettings()
    {
        Formatting = DefaultFormatting;
        MaxDepth = DefaultMaxDepth;
        NullValueHandling = DefaultNullValueHandling;
    }

    internal static JsonTextReader CreateReader(TextReader reader)
    {
        return new JsonTextReader(reader);
    }

    internal JsonTextWriter CreateWriter(TextWriter writer)
    {
        return new JsonTextWriter(writer)
        {
            Indentation = Indentation,
            IndentChar = IndentChar,
        };
    }
}
