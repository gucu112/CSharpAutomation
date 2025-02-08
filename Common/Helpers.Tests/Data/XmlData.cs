using System.Xml.Serialization;
using Gucu112.CSharp.Automation.Helpers.Extensions;

namespace Gucu112.CSharp.Automation.Helpers.Tests.Data;

public class XmlData
{
    public static readonly string IncorrectDeclarationString = GetXmlDeclarationString(string.Empty)[..13] + ">";

    public static readonly string CorrectDeclarationString = GetXmlDeclarationString("1.0", Encoding.UTF8);

    public static readonly string VoidRootElementString = new XElement("root").ToString();

    public static readonly string EmptyRootElementString = new XElement("root", string.Empty).ToString();

    public static IEnumerable EmptyContent
    {
        get
        {
            yield return StringData.EmptyString;
            yield return WhitespaceData.MultipleRegularSpaces;
        }
    }

    public static IEnumerable IncorrectDeclaration
    {
        get
        {
            yield return GetXmlDeclarationString();
            yield return IncorrectDeclarationString;
        }
    }

    public static IEnumerable CorrectDeclaration
    {
        get
        {
            yield return GetXmlDeclarationString("1.0");
            yield return CorrectDeclarationString;
            yield return GetXmlDeclarationString("1.0", Encoding.UTF8, true);
        }
    }

    public static IEnumerable EmptyRootElement
    {
        get
        {
            yield return VoidRootElementString;
            yield return EmptyRootElementString;
            yield return CorrectDeclarationString + VoidRootElementString;
            yield return CorrectDeclarationString + EmptyRootElementString;
        }
    }

    private static string GetXmlDeclarationString(string? version = null, Encoding? encoding = null, bool? standalone = null)
    {
        return new XDeclaration(version, encoding?.WebName, standalone.ToLocalizedString()?.ToLower()).ToString();
    }

    [XmlType("root")]
    public class RootObjectModel
    {
        public string Environment { get; init; } = null!;

        [XmlAttribute(nameof(IsPrimary))]
        public bool IsPrimary { get; init; }

        public float CapThickness { get; init; }

        [XmlArray(nameof(GeneticCode))]
        [XmlArrayItem("codon")]
        public List<string> GeneticCode { get; init; } = [];

        [XmlIgnore]
        public Dictionary<int, object> ApexStructure { get; init; } = null!;

        public DateTime VegetationPeriodStart { get; init; }
    }
}
