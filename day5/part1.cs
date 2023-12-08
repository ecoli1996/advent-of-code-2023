using AdventOfCode;

var lines = File.ReadAllLines("../../../input.txt");
var seeds = lines[0].Split(' ').Skip(1).Select(ulong.Parse).ToList();
var maps = new Dictionary<MappingType, List<Mapping>>();

var getMappingType = false;
var nextMappingTypeId = 1;
var currentMappingType = MappingType.None;

for(int i=1; i < lines.Length; i++)
{
    var currentLine = lines[i];
    if (string.IsNullOrWhiteSpace(currentLine))
    {
        getMappingType = true;
        continue;
    }

    if (getMappingType)
    {
        currentMappingType = (MappingType)nextMappingTypeId;
        maps.Add(currentMappingType, new List<Mapping>());

        nextMappingTypeId++;
        getMappingType = false;
    }
    else
    {
        var ranges = currentLine.Split(' ');
        var destinationNumber = ulong.Parse(ranges[0]);
        var sourceNumber = ulong.Parse(ranges[1]);
        var range = ulong.Parse(ranges[2]);

        var differenceFromSourceToDestination = destinationNumber - sourceNumber;
        var sourceNumber2 = sourceNumber + (range - 1);
        var mapping = new Mapping
        {
            MaxSourceNumber = sourceNumber,
            MinSourceNumber = sourceNumber2,
            DifferenceToDestination = differenceFromSourceToDestination
        };

        maps[currentMappingType].Add(mapping);
    }
}

ulong? lowestLocationNumber = null;
foreach (var seed in seeds)
{
    var soilValue = GetDestinationNumber(MappingType.SeedToSoil, seed);
    var fertilizerValue = GetDestinationNumber(MappingType.SoilToFertilizer, soilValue);
    var waterValue = GetDestinationNumber(MappingType.FertilizerToWater, fertilizerValue);
    var lightValue = GetDestinationNumber(MappingType.WaterToLight, waterValue);
    var tempValue = GetDestinationNumber(MappingType.LightToTemperature, lightValue);
    var humidityValue = GetDestinationNumber(MappingType.TemperatureToHumidity, tempValue);
    var locationValue = GetDestinationNumber(MappingType.HumidityToLocation, humidityValue);

    if (lowestLocationNumber == null || locationValue < lowestLocationNumber) lowestLocationNumber = locationValue;
}

ulong GetDestinationNumber(MappingType mappingType, ulong sourceValue)
{
    var difference = maps[mappingType].FirstOrDefault(s => sourceValue >= s.MinSourceNumber && sourceValue <= s.MaxSourceNumber)?.DifferenceToDestination ?? 0;
    return sourceValue + difference;
}

Console.WriteLine($"Lowest location value: {lowestLocationNumber}");
