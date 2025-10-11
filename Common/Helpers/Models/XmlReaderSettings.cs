using System.Xml;
using Gucu112.CSharp.Automation.Helpers.Parsers;
using BaseXmlReader = System.Xml.XmlReader;
using BaseXmlReaderSettings = System.Xml.XmlReaderSettings;

namespace Gucu112.CSharp.Automation.Helpers.Models;

/// <summary>
/// Represents the settings for XML reader.
/// </summary>
public class XmlReaderSettings(BaseXmlReaderSettings? readerSettings = null)
{
    private const bool DefaultNamespaces = false;
    private const bool DefaultNormalization = false;
    private const EntityHandling DefaultEntityHandling = EntityHandling.ExpandCharEntities;
    private const WhitespaceHandling DefaultWhitespaceHandling = WhitespaceHandling.None;
    private WhitespaceHandling whitespaceHandling = DefaultWhitespaceHandling;

    /// <summary>
    /// Gets or sets the type of XML text encoding to use. The default is <see cref="ParseSettings.DefaultEncoding"/>.
    /// </summary>
    public Encoding Encoding { get; set; } = ParseSettings.DefaultEncoding;

    /// <summary>
    /// Gets or sets a value that specifies how the reader handles entities. If no <c>EntityHandling</c> is specified, it defaults to <see cref="EntityHandling.ExpandCharEntities"/>.
    /// </summary>
    public EntityHandling EntityHandling { get; set; } = DefaultEntityHandling;

    /// <summary>
    /// Gets or sets a value indicating whether to do namespace support. The default is <c>false</c>.
    /// </summary>
    public bool Namespaces { get; set; } = DefaultNamespaces;

    /// <summary>
    /// Gets or sets a value indicating whether to normalize white space and attribute values. The default is <c>false</c>.
    /// </summary>
    public bool Normalization { get; set; } = DefaultNormalization;

    /// <summary>
    /// Gets or sets a value that specifies how white space is handled. The default is <see cref="WhitespaceHandling.All"/>.
    /// </summary>
    public WhitespaceHandling WhitespaceHandling
    {
        get => whitespaceHandling;
        set
        {
            whitespaceHandling = value;
            ReaderSettings.IgnoreWhitespace = whitespaceHandling == WhitespaceHandling.None;
        }
    }

    /// <summary>
    /// Gets the <see href="https://learn.microsoft.com/dotnet/api/system.xml.xmlreadersettings">XmlReaderSettings</see> object.
    /// </summary>
    public BaseXmlReaderSettings ReaderSettings { get; init; } = readerSettings ?? new();

    /// <summary>
    /// Creates a new instance of <see cref="BaseXmlReader"/> using the specified <see cref="TextReader"/>.
    /// </summary>
    /// <param name="reader">The <see cref="TextReader"/> used for XML deserialization.</param>
    /// <returns>A new instance of <see cref="BaseXmlReader"/>.</returns>
    public BaseXmlReader CreateReader(TextReader reader)
    {
        return BaseXmlReader.Create(
            new XmlTextReader(reader)
            {
                EntityHandling = EntityHandling,
                Namespaces = Namespaces,
                Normalization = Normalization,
                WhitespaceHandling = WhitespaceHandling,
            },
            ReaderSettings);
    }
}
