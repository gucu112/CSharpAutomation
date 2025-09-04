using System.Xml.Linq;
using System.Xml.Serialization;

namespace Gucu112.CSharp.Automation.Helpers.Extensions;

/// <summary>
/// Provides extension methods for the XElement class.
/// </summary>
public static class XElementExtensions
{
    /// <summary>
    /// Deserializes the XElement into an object of type TOutput.
    /// </summary>
    /// <typeparam name="TOutput">The type of the object to deserialize to.</typeparam>
    /// <param name="element">The XElement to deserialize.</param>
    /// <returns>An instance of TOutput or a new instance if deserialization fails.</returns>
    public static TOutput? Deserialize<TOutput>(this XElement element)
        where TOutput : new()
    {
        try
        {
            var overrides = GetXmlSerializerOverrides<TOutput>(element);
            using var stringReader = new StringReader(element.ToString());
            var serializer = new XmlSerializer(typeof(TOutput), overrides);
            return (TOutput?)serializer.Deserialize(stringReader);
        }
        catch (Exception)
        {
            return new TOutput();
        }
    }

    /// <summary>
    /// Gets the XmlAttributeOverrides for the specified type based on the XElement.
    /// </summary>
    /// <typeparam name="TOutput">The type for which to get the overrides.</typeparam>
    /// <param name="rootElement">The root XElement to base the overrides on.</param>
    /// <returns>An XmlAttributeOverrides object containing the overrides.</returns>
    public static XmlAttributeOverrides GetXmlSerializerOverrides<TOutput>(this XElement rootElement)
    {
        var overrides = new XmlAttributeOverrides();
        var rootAttributes = new XmlAttributes { XmlRoot = new($"{rootElement.Name}") };
        overrides.Add(typeof(TOutput), rootAttributes);

        foreach (var property in typeof(TOutput).GetProperties())
        {
            if (!rootElement.Elements().Any(element => element.Name == property.Name))
            {
                var attributes = new XmlAttributes { XmlIgnore = true };
                attributes.XmlElements.Add(new XmlElementAttribute(property.Name));
                overrides.Add(typeof(TOutput), property.Name, attributes);
            }
        }

        return overrides;
    }
}
