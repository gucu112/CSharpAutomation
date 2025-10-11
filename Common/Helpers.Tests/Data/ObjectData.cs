namespace Gucu112.CSharp.Automation.Helpers.Tests.Data;

public class ObjectData
{
    public static readonly Dictionary<string, int> SimpleDictionaryValue = new()
    {
        { "First", 1 },
        { "Second", 2 },
        { "Third", 3 },
    };

    public static readonly JsonData.SimpleObjectModel SimpleObjectValue = new()
    {
        InvalidProperty = null,
        BooleanValue = false,
        ListOfNumbers = [1, 2, 3],
        DictionaryOfStrings = new()
        {
            { "Empty", string.Empty },
        },
        CurrentYearStart = new DateTime(2025, 1, 1),
    };

    public static readonly XmlData.RootObjectModel SimpleRootObjectValue = new()
    {
        Environment = "Humidity: 69%; Wind: 18 km/h",
        IsPrimary = true,
        CapThickness = 0.0005f,
        GeneticCode = ["AGG", "CUC", "UAA"],
        ApexStructure = new()
        {
            [2] = new object(),
            [3] = new object(),
            [5] = new object(),
        },
        VegetationPeriodStart = new DateTime(2025, 4, 1),
    };
}
