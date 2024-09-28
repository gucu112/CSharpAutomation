using System.Xml.Linq;
using System.Xml.Serialization;

namespace Gucu112.CSharp.Automation.PlaywrightXunit.Extensions;

public static class XElementExtensions
{
    public static T? Deserialize<T>(this XElement element) where T : new()
    {
        try
        {
            var overrides = GetXmlSerializerOverrides<T>(element);
            using var stringReader = new StringReader(element.ToString());
            var serializer = new XmlSerializer(typeof(T), overrides);
            return (T?)serializer.Deserialize(stringReader);
        }
        catch (Exception)
        {
            return new T();
        }
    }

    private static XmlAttributeOverrides GetXmlSerializerOverrides<T>(XElement rootElement)
    {
        var overrides = new XmlAttributeOverrides();
        var rootAttributes = new XmlAttributes { XmlRoot = new($"{rootElement.Name}") };
        overrides.Add(typeof(T), rootAttributes);

        foreach (var property in typeof(T).GetProperties())
        {
            if (!rootElement.Elements().Any(element => element.Name == property.Name))
            {
                var attributes = new XmlAttributes { XmlIgnore = true };
                attributes.XmlElements.Add(new XmlElementAttribute(property.Name));
                overrides.Add(typeof(T), property.Name, attributes);
            }
        }

        return overrides;
    }
}
