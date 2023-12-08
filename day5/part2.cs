using AdventOfCode;

var lines = File.ReadAllLines("../../../input.txt");

var seedStartersAndRanges = lines[0].Split(' ').Skip(1).Select(ulong.Parse).ToList();

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
        var destinationNumber2 = destinationNumber + (range - 1);

        var mapping = new Mapping
        {
            MaxSourceNumber = sourceNumber2,
            MinSourceNumber = sourceNumber,
            DifferenceToDestination = differenceFromSourceToDestination,
            MinDestination = destinationNumber,
            MaxDestination = destinationNumber2
        };

        maps[currentMappingType].Add(mapping);
    }
}

ulong lowestLocationNumber = 0;
var seedRanges = new List<(ulong, ulong)>();

for (int i=0; i < seedStartersAndRanges.Count - 1; i+=2)
{
    var seedValueStart = seedStartersAndRanges[i];
    var endSeed = seedValueStart + seedStartersAndRanges[i + 1];
    seedRanges.Add((seedValueStart, endSeed));
}

var foundMatch = false;

while (!foundMatch)
{
    var matchingSeedValue = GetSeedValue(maps, lowestLocationNumber);
    foundMatch = seedRanges.Any(r => matchingSeedValue >= r.Item1 && matchingSeedValue <= r.Item2);

    lowestLocationNumber++;
}


Console.WriteLine($"Lowest location value: {lowestLocationNumber - 1}");

ulong GetSeedValue(Dictionary<MappingType, List<Mapping>> maps, ulong locationValue)
{
    var humidityValue = GetSourceNumber(maps, MappingType.HumidityToLocation, locationValue);
    var tempValue = GetSourceNumber(maps, MappingType.TemperatureToHumidity, humidityValue);
    var lightValue = GetSourceNumber(maps, MappingType.LightToTemperature, tempValue);
    var waterValue = GetSourceNumber(maps, MappingType.WaterToLight, lightValue);
    var fertilizerValue = GetSourceNumber(maps, MappingType.FertilizerToWater, waterValue);
    var soilValue = GetSourceNumber(maps, MappingType.SoilToFertilizer, fertilizerValue);
    var seedValue = GetSourceNumber(maps, MappingType.SeedToSoil, soilValue);

    return seedValue;
    
}

Mapping GetMatchingMapping(Dictionary<MappingType, List<Mapping>> maps, MappingType mappingType, ulong destinationValue)
{
    return maps[mappingType].FirstOrDefault(s => destinationValue >= s.MinDestination && destinationValue <= s.MaxDestination) ?? new Mapping
    {
        MaxDestination = destinationValue,
        MaxSourceNumber = destinationValue,
        MinDestination = destinationValue,
        MinSourceNumber = destinationValue,
        DifferenceToDestination = 0
    };
}

ulong GetSourceNumber(Dictionary<MappingType, List<Mapping>> maps, MappingType mappingType, ulong destinationValue)
{
    var difference = GetMatchingMapping(maps, mappingType, destinationValue).DifferenceToDestination;
    return destinationValue - difference;
}
