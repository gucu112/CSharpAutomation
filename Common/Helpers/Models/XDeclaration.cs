using Gucu112.CSharp.Automation.Helpers.Extensions;
using BaseXDeclaration = System.Xml.Linq.XDeclaration;

namespace Gucu112.CSharp.Automation.Helpers.Models;

/// <summary>
/// Represents an XML declaration. An XML declaration is used to declare the XML version,
/// the encoding, and whether or not the XML document is standalone.
/// </summary>
/// <param name="version">The version of the XML, usually "1.0".</param>
/// <param name="encoding">The encoding for the XML document.</param>
/// <param name="standalone">Specifies whether the XML is standalone or requires external entities to be resolved.</param>
public class XDeclaration(string? version = null, Encoding? encoding = null, bool? standalone = null)
    : BaseXDeclaration(version, encoding?.WebName, standalone.ToLocalizedString()?.ToLower())
{
}
