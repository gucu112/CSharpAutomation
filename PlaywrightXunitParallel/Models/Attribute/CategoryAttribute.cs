using Gucu112.CSharp.Automation.PlaywrightXunitParallel.Models.Enum;
using BaseAttribute = System.Attribute;

namespace Gucu112.CSharp.Automation.PlaywrightXunitParallel.Models.Attribute;

/// <summary>
/// Represents an attribute that specifies the category of a test.
/// </summary>
/// <param name="category">The category of the test.</param>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
[TraitDiscoverer(TraitDiscoverer.FullTypeName, TraitDiscoverer.AssemblyName)]
public class CategoryAttribute(TestCategory category)
    : BaseAttribute, ITraitAttribute
{
    /// <summary>
    /// Gets or sets the test category.
    /// </summary>
    public TestCategory Category { get; set; } = category;
}
