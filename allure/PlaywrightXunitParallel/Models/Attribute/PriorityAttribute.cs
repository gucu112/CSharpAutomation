using Gucu112.CSharp.Automation.PlaywrightXunitParallel.Models.Enum;
using BaseAttribute = System.Attribute;

namespace Gucu112.CSharp.Automation.PlaywrightXunitParallel.Models.Attribute;

/// <summary>
/// Represents an attribute that specifies the priority of a test.
/// </summary>
/// <param name="priority">The priority of the test.</param>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
[TraitDiscoverer(TraitDiscoverer.FullTypeName, TraitDiscoverer.AssemblyName)]
public class PriorityAttribute(TestPriority priority)
    : BaseAttribute, ITraitAttribute
{
    /// <summary>
    /// Gets or sets the test priority.
    /// </summary>
    public TestPriority Priority { get; set; } = priority;
}
