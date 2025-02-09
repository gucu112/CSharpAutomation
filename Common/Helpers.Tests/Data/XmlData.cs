using System.Xml.Serialization;
using Gucu112.CSharp.Automation.Helpers.Extensions;

namespace Gucu112.CSharp.Automation.Helpers.Tests.Data;

public class XmlData
{
    public static readonly string IncorrectDeclarationString = new XDeclaration(string.Empty).ToString()[..13] + ">";

    public static readonly string CorrectDeclarationString = new XDeclaration("1.0").ToString();

    public static readonly string VoidRootElementString = new XElement("root").ToString();

    public static readonly string EmptyRootElementString = new XElement("root", string.Empty).ToString();

    public static readonly string RootObjectDocumentString = GetXmlDocumentString();

    public static IEnumerable EmptyContent
    {
        get
        {
            yield return new TestCaseData(StringData.EmptyString)
                .SetArgDisplayNames("EmptyString");
            yield return new TestCaseData(new StringReader(StringData.EmptyString))
                .SetArgDisplayNames("EmptyStringReader");
            yield return new TestCaseData(new MemoryStream(StringData.EmptyString.GetBytes()))
                .SetArgDisplayNames("EmptyStringStream");

            yield return new TestCaseData(WhitespaceData.MultipleRegularSpaces)
                .SetArgDisplayNames("WhitespaceString");
            yield return new TestCaseData(new StringReader(WhitespaceData.MultipleRegularSpaces))
                .SetArgDisplayNames("WhitespaceStringReader");
            yield return new TestCaseData(new MemoryStream(WhitespaceData.MultipleRegularSpaces.GetBytes()))
                .SetArgDisplayNames("WhitespaceStringStream");
        }
    }

    public static IEnumerable IncorrectDeclaration
    {
        get
        {
            yield return new TestCaseData(new XDeclaration().ToString())
                .SetArgDisplayNames("EmptyDeclaration");
            yield return new TestCaseData(new StringReader(new XDeclaration().ToString()))
                .SetArgDisplayNames("EmptyDeclarationReader");
            yield return new TestCaseData(new MemoryStream(new XDeclaration().ToString().GetBytes()))
                .SetArgDisplayNames("EmptyDeclarationStream");

            yield return new TestCaseData(IncorrectDeclarationString)
                .SetArgDisplayNames("IncorrectDeclaration");
            yield return new TestCaseData(new StringReader(IncorrectDeclarationString))
                .SetArgDisplayNames("IncorrectDeclarationReader");
            yield return new TestCaseData(new MemoryStream(IncorrectDeclarationString.GetBytes()))
                .SetArgDisplayNames("IncorrectDeclarationStream");
        }
    }

    public static IEnumerable CorrectDeclaration
    {
        get
        {
            yield return new TestCaseData(new XDeclaration("1.0").ToString())
                .SetArgDisplayNames("CorrectDeclarationVersion");
            yield return new TestCaseData(new StringReader(new XDeclaration("1.0").ToString()))
                .SetArgDisplayNames("CorrectDeclarationVersionReader");
            yield return new TestCaseData(new MemoryStream(new XDeclaration("1.0").ToString().GetBytes()))
                .SetArgDisplayNames("CorrectDeclarationVersionStream");

            yield return new TestCaseData(CorrectDeclarationString)
                .SetArgDisplayNames("CorrectDeclarationEncoding");
            yield return new TestCaseData(new StringReader(CorrectDeclarationString))
                .SetArgDisplayNames("CorrectDeclarationEncodingReader");
            yield return new TestCaseData(new MemoryStream(CorrectDeclarationString.GetBytes()))
                .SetArgDisplayNames("CorrectDeclarationEncodingStream");

            yield return new TestCaseData(new XDeclaration("1.0", Encoding.UTF8, true).ToString())
                .SetArgDisplayNames("CorrectDeclarationStandalone");
            yield return new TestCaseData(new StringReader(new XDeclaration("1.0", Encoding.UTF8, true).ToString()))
                .SetArgDisplayNames("CorrectDeclarationStandaloneReader");
            yield return new TestCaseData(new MemoryStream(new XDeclaration("1.0", Encoding.UTF8, true).ToString().GetBytes()))
                .SetArgDisplayNames("CorrectDeclarationStandaloneStream");
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
            yield return new TestCaseData(new MemoryStream(VoidRootElementString.GetBytes()))
                .SetArgDisplayNames("VoidRootElementStream");

            yield return new TestCaseData(EmptyRootElementString)
                .SetArgDisplayNames("EmptyRootElement");
            yield return new TestCaseData(new StringReader(EmptyRootElementString))
                .SetArgDisplayNames("EmptyRootElementReader");
            yield return new TestCaseData(new MemoryStream(EmptyRootElementString.GetBytes()))
                .SetArgDisplayNames("EmptyRootElementStream");

            yield return new TestCaseData(CorrectDeclarationString + VoidRootElementString)
                .SetArgDisplayNames("DeclarationAndVoidRootElement");
            yield return new TestCaseData(new StringReader(CorrectDeclarationString + VoidRootElementString))
                .SetArgDisplayNames("DeclarationAndVoidRootElementReader");
            yield return new TestCaseData(new MemoryStream((CorrectDeclarationString + VoidRootElementString).GetBytes()))
                .SetArgDisplayNames("DeclarationAndVoidRootElementStream");

            yield return new TestCaseData(CorrectDeclarationString + EmptyRootElementString)
                .SetArgDisplayNames("DeclarationAndEmptyRootElement");
            yield return new TestCaseData(new StringReader(CorrectDeclarationString + EmptyRootElementString))
                .SetArgDisplayNames("DeclarationAndEmptyRootElementReader");
            yield return new TestCaseData(new MemoryStream((CorrectDeclarationString + EmptyRootElementString).GetBytes()))
                .SetArgDisplayNames("DeclarationAndEmptyRootElementStream");
        }
    }

    public static IEnumerable RootObject
    {
        get
        {
            yield return new TestCaseData(RootObjectDocumentString)
                .SetArgDisplayNames("RootObjectModel");
            yield return new TestCaseData(new StringReader(RootObjectDocumentString))
                .SetArgDisplayNames("RootObjectModelReader");
            yield return new TestCaseData(new MemoryStream(RootObjectDocumentString.GetBytes()))
                .SetArgDisplayNames("RootObjectModelStream");
        }
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

        return new XDeclaration("1.0", Encoding.UTF8).ToString() + '\n'
            + new XDocument(new XElement("root", objects)).ToString();
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
