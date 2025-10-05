using Gucu112.CSharp.Automation.PlaywrightXunitParallel.Models.Attribute;

namespace Gucu112.CSharp.Automation.PlaywrightXunitParallel;

/// <summary>
/// Represents class for discovering traits used for categorizing tests.
/// </summary>
public class TraitDiscoverer : ITraitDiscoverer
{
    /// <summary>
    /// Represents the assembly name in which the trait discoverer is defined.
    /// </summary>
    public const string AssemblyName = "PlaywrightXunitParallel";

    /// <summary>
    /// Represents the full type name of the trait discoverer.
    /// </summary>
    public const string FullTypeName = $"Gucu112.CSharp.Automation.{AssemblyName}.{nameof(TraitDiscoverer)}";

    /// <summary>
    /// Gets the traits from the given attribute information.
    /// </summary>
    /// <param name="traitAttribute">The attribute information.</param>
    /// <returns>The traits as key-value pairs.</returns>
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
