namespace Gucu112.CSharp.Automation.Helpers.Tests.Data;

public class ObjectToXmlData : BaseData
{
    public static IEnumerable EmptyValue
    {
        get
        {
            var input = StringData.EmptyString;
            var output = XmlData.CorrectDeclarationString + XmlData.VoidStringElement;

            yield return Create<string>(input, output).SetArgDisplayNames("EmptyValueToXmlString");
            yield return Create<TextWriter>(input, output).SetArgDisplayNames("EmptyValueWriterToXmlString");
            yield return Create<Stream>(input, output).SetArgDisplayNames("EmptyValueStreamToXmlString");
        }
    }

    public static IEnumerable WhitespaceValue
    {
        get
        {
            var input = WhitespaceData.MultipleRegularSpaces;
            var output = XmlData.CorrectDeclarationString + XmlData.EmptyStringElement
                .Replace("><", $">{WhitespaceData.MultipleRegularSpaces}<");

            yield return Create<string>(input, output).SetArgDisplayNames("WhitespaceValueToXmlString");
            yield return Create<TextWriter>(input, output).SetArgDisplayNames("WhitespaceValueWriterToXmlString");
            yield return Create<Stream>(input, output).SetArgDisplayNames("WhitespaceValueStreamToXmlString");
        }
    }

    public static IEnumerable StringValue
    {
        get
        {
            var input = StringData.LoremIpsumString;
            var output = XmlData.CorrectDeclarationString + XmlData.EmptyStringElement
                .Replace("><", $">{StringData.LoremIpsumString}<");

            yield return Create<string>(input, output).SetArgDisplayNames("StringValueToXmlString");
            yield return Create<TextWriter>(input, output).SetArgDisplayNames("StringValueWriterToXmlString");
            yield return Create<Stream>(input, output).SetArgDisplayNames("StringValueStreamToXmlString");
        }
    }

    public static IEnumerable BooleanValue
    {
        get
        {
            var trueValueString = XmlData.CorrectDeclarationString + $"<boolean>{true.ToString().ToLower()}</boolean>";
            var falseValueString = XmlData.CorrectDeclarationString + $"<boolean>{false.ToString().ToLower()}</boolean>";

            yield return Create<string>(true, trueValueString).SetArgDisplayNames("TrueBooleanToXmlString");
            yield return Create<string>(false, falseValueString).SetArgDisplayNames("FalseBooleanToXmlString");
            yield return Create<TextWriter>(true, trueValueString).SetArgDisplayNames("TrueBooleanWriterToXmlString");
            yield return Create<Stream>(false, falseValueString).SetArgDisplayNames("FalseBooleanStreamToXmlString");
        }
    }

    public static IEnumerable EmptyList
    {
        get
        {
            var input = new List<double>();
            var output = XmlData.CorrectDeclarationString + "<ArrayOfDouble />";

            yield return Create<string>(input, output).SetArgDisplayNames("EmptyArrayOfDoubleToXmlString");
            yield return Create<TextWriter>(input, output).SetArgDisplayNames("EmptyArrayOfDoubleWriterToXmlString");
            yield return Create<Stream>(input, output).SetArgDisplayNames("EmptyArrayOfDoubleStreamToXmlString");
        }
    }

    public static IEnumerable EmptyObject
    {
        get
        {
            var input = new XmlData.RootOnlyModel();
            var output = XmlData.CorrectDeclarationString + XmlData.VoidRootElementString;

            yield return Create<string>(input, output).SetArgDisplayNames("EmptyRootOnlyObjectToXmlString");
            yield return Create<TextWriter>(input, output).SetArgDisplayNames("EmptyRootOnlyObjectWriterToXmlString");
            yield return Create<Stream>(input, output).SetArgDisplayNames("EmptyRootOnlyObjectStreamToXmlString");
        }
    }

    public static IEnumerable SimpleList
    {
        get
        {
            var input = new List<int> { 5, 4, 3 };
            var output = XmlData.CorrectDeclarationString + "<ArrayOfInt>"
                + "<int>5</int><int>4</int><int>3</int>" + "</ArrayOfInt>";

            yield return Create<string>(input, output).SetArgDisplayNames("ArrayOfIntToXmlString");
            yield return Create<TextWriter>(input, output).SetArgDisplayNames("ArrayOfIntWriterToXmlString");
            yield return Create<Stream>(input, output).SetArgDisplayNames("ArrayOfIntStreamToXmlString");
        }
    }
}
