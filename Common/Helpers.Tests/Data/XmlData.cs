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
            yield return new TestCaseData(StringData.EmptyString)
                .SetArgDisplayNames("EmptyString");
            yield return new TestCaseData(new StringReader(StringData.EmptyString))
                .SetArgDisplayNames("EmptyStringReader");
            yield return new TestCaseData(WhitespaceData.MultipleRegularSpaces)
                .SetArgDisplayNames("MultipleRegularSpaces");
            yield return new TestCaseData(new StringReader(WhitespaceData.MultipleRegularSpaces))
                .SetArgDisplayNames("MultipleRegularSpacesReader");
        }
    }

    public static IEnumerable IncorrectDeclaration
    {
        get
        {
            yield return new TestCaseData(GetXmlDeclarationString())
                .SetArgDisplayNames("EmptyDeclaration");
            yield return new TestCaseData(new StringReader(GetXmlDeclarationString()))
                .SetArgDisplayNames("EmptyDeclarationReader");
            yield return new TestCaseData(IncorrectDeclarationString)
                .SetArgDisplayNames("IncorrectDeclaration");
            yield return new TestCaseData(new StringReader(IncorrectDeclarationString))
                .SetArgDisplayNames("IncorrectDeclarationReader");
        }
    }

    public static IEnumerable CorrectDeclaration
    {
        get
        {
            yield return new TestCaseData(GetXmlDeclarationString("1.0"))
                .SetArgDisplayNames("CorrectDeclarationVersion");
            yield return new TestCaseData(new StringReader(GetXmlDeclarationString("1.0")))
                .SetArgDisplayNames("CorrectDeclarationVersionReader");
            yield return new TestCaseData(CorrectDeclarationString)
                .SetArgDisplayNames("CorrectDeclarationEncoding");
            yield return new TestCaseData(new StringReader(CorrectDeclarationString))
                .SetArgDisplayNames("CorrectDeclarationEncodingReader");
            yield return new TestCaseData(GetXmlDeclarationString("1.0", Encoding.UTF8, true))
                .SetArgDisplayNames("CorrectDeclarationStandalone");
            yield return new TestCaseData(new StringReader(GetXmlDeclarationString("1.0", Encoding.UTF8, true)))
                .SetArgDisplayNames("CorrectDeclarationStandaloneReader");
        }
    }

    public static IEnumerable EmptyRootElement
    {
        get
        {
            yield return new TestCaseData(VoidRootElementString)
                .SetArgDisplayNames("VoidRootElement");
            yield return new TestCaseData(new StringReader(VoidRootElementString))
                .SetArgDisplayNames("VoidRootElementReader");
            yield return new TestCaseData(EmptyRootElementString)
                .SetArgDisplayNames("EmptyRootElement");
            yield return new TestCaseData(new StringReader(EmptyRootElementString))
                .SetArgDisplayNames("EmptyRootElementReader");
            yield return new TestCaseData(CorrectDeclarationString + VoidRootElementString)
                .SetArgDisplayNames("DeclarationAndVoidRootElement");
            yield return new TestCaseData(new StringReader(CorrectDeclarationString + VoidRootElementString))
                .SetArgDisplayNames("DeclarationAndVoidRootElementReader");
            yield return new TestCaseData(CorrectDeclarationString + EmptyRootElementString)
                .SetArgDisplayNames("DeclarationAndEmptyRootElement");
            yield return new TestCaseData(new StringReader(CorrectDeclarationString + EmptyRootElementString))
                .SetArgDisplayNames("DeclarationAndEmptyRootElementReader");
        }
    }

    public static IEnumerable RootObject
    {
        get
        {
            yield return new TestCaseData(GetXmlDocumentString())
                .SetArgDisplayNames("RootObjectModel");
            yield return new TestCaseData(new StringReader(GetXmlDocumentString()))
                .SetArgDisplayNames("RootObjectModelReader");
        }
    }

    private static string GetXmlDeclarationString(string? version = null, Encoding? encoding = null, bool? standalone = null)
    {
        return new XDeclaration(version, encoding?.WebName, standalone.ToLocalizedString()?.ToLower()).ToString();
    }

    private static string GetXmlDocumentString()
    {
        List<XObject> objects = [
            new XAttribute("IsPrimary", false),
            new XElement("CapThickness", 0.0003d),
            new XElement(
                "GeneticCode",
                new List<XElement>
                {
                    new("codon", "GUC"),
                    new("codon", "CAA"),
                    new("codon", "AUG"),
                    new("codon", "UGA"),
                }),
            new XElement("VegetationPeriodStart", new DateTime(2025, 4, 1))
        ];

        return new XDocument(new XElement("root", objects)).ToString();
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
