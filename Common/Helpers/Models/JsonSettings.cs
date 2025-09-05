using Gucu112.CSharp.Automation.Helpers.Parsers;

namespace Gucu112.CSharp.Automation.Helpers.Models;

/// <summary>
/// Represents the settings for JSON serialization and deserialization.
/// </summary>
public sealed class JsonSettings : JsonSerializerSettings
{
    private const int DefaultIndentation = 2;

    private const char DefaultIndentChar = ' ';

    private const int DefaultMaxDepth = 16;

    private static readonly Formatting DefaultFormatting = Formatting.Indented;

    private static readonly NullValueHandling DefaultNullValueHandling = NullValueHandling.Ignore;

    private Encoding? encoding;

    private int? indentation;

    private char? indentChar;

    /// <summary>
    /// Assigns default values to the base JSON settings object.
    /// </summary>
    public JsonSettings()
    {
        Formatting = DefaultFormatting;
        MaxDepth = DefaultMaxDepth;
        NullValueHandling = DefaultNullValueHandling;
    }

    /// <summary>
    /// Gets or sets the type of JSON text encoding to use. The default is <see cref="Encoding.UTF8"/>.
    /// </summary>
    public Encoding Encoding
    {
        get => encoding ?? ParseSettings.DefaultEncoding;
        set => encoding = value;
    }

    /// <summary>
    /// Gets or sets the number of characters to use for each level in the hierarchy when formatting JSON. The default is 2.
    /// </summary>
    public int Indentation
    {
        get => indentation ?? DefaultIndentation;
        set => indentation = value;
    }

    /// <summary>
    /// Gets or sets which character to use for indenting when formatting JSON. The default is space.
    /// </summary>
    public char IndentChar
    {
        get => indentChar ?? DefaultIndentChar;
        set => indentChar = value;
    }

    /// <summary>
    /// Creates a new instance of <see cref="JsonTextReader"/> using the specified <see cref="TextReader"/>.
    /// </summary>
    /// <param name="reader">The <see cref="TextReader"/> used for JSON deserialization.</param>
    /// <returns>A new instance of <see cref="JsonTextReader"/>.</returns>
    internal static JsonTextReader CreateReader(TextReader reader)
    {
        return new JsonTextReader(reader);
    }

    /// <summary>
    /// Creates a new instance of <see cref="JsonTextWriter"/> using the specified <see cref="TextWriter"/>.
    /// </summary>
    /// <param name="writer">The <see cref="TextWriter"/> used for JSON serialization.</param>
    /// <returns>A new instance of <see cref="JsonTextWriter"/>.</returns>
    internal JsonTextWriter CreateWriter(TextWriter writer)
    {
        return new JsonTextWriter(writer)
        {
            Indentation = Indentation,
            IndentChar = IndentChar,
        };
    }
}
