using Gucu112.CSharp.Automation.PlaywrightXunitParallel.Models.Enum;
using BaseAttribute = System.Attribute;

namespace Gucu112.CSharp.Automation.PlaywrightXunitParallel.Models.Attribute;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
[TraitDiscoverer(TraitDiscoverer.FullTypeName, TraitDiscoverer.AssemblyName)]
public class PriorityAttribute(TestPriority priority) : BaseAttribute, ITraitAttribute
{
    public TestPriority Priority { get; set; } = priority;
}
