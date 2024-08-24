using Gucu112.PlaywrightXunitParallel.Models.Enum;
using Xunit.Sdk;
using BaseAttribute = System.Attribute;

namespace Gucu112.PlaywrightXunitParallel.Models.Attribute;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
[TraitDiscoverer(TraitDiscoverer.FullTypeName, TraitDiscoverer.AssemblyName)]
public class CategoryAttribute(TestCategory category) : BaseAttribute, ITraitAttribute
{
    public TestCategory Category { get; set; } = category;
}
