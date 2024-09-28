using Gucu112.CSharp.Automation.PlaywrightXunitParallel.Models.Attribute;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Gucu112.CSharp.Automation.PlaywrightXunitParallel;

public class TraitDiscoverer : ITraitDiscoverer
{
    public const string FullTypeName = $"Gucu112.CSharp.Automation.{AssemblyName}.{nameof(TraitDiscoverer)}";
    public const string AssemblyName = "PlaywrightXunitParallel";

    public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
    {
        var reflectionInfo = traitAttribute as ReflectionAttributeInfo;

        switch (reflectionInfo?.Attribute)
        {
            case CategoryAttribute categoryAttribute:
                yield return new KeyValuePair<string, string>(CustomTrait.Category, categoryAttribute.Category.ToString());
                break;
            case PriorityAttribute priorityAttribute:
                yield return new KeyValuePair<string, string>(CustomTrait.Priority, priorityAttribute.Priority.ToString());
                break;
            default:
                throw new NotSupportedException($"Attribute '{traitAttribute}' not supported.");
        }
    }

    private class CustomTrait
    {
        public const string Category = nameof(Category);
        public const string Priority = nameof(Priority);
    }
}
