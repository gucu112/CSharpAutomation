using Gucu112.PlaywrightXunitParallel.Models.Enum;
using Xunit.Sdk;
using BaseAttribute = System.Attribute;

namespace Gucu112.PlaywrightXunitParallel.Models.Attribute;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
[TraitDiscoverer(TraitDiscoverer.FullTypeName, TraitDiscoverer.AssemblyName)]
public class CategoryAttribute : BaseAttribute, ITraitAttribute
{
    public TestCategory Category { get; set; }

    public CategoryAttribute(TestCategory category)
    {
        Category = category;
    }
}
