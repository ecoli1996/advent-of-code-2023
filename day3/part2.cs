var lines = File.ReadAllLines("../../../input.txt");

var rowNumber = 0;
var gearRatios = new Dictionary<(int, int), List<int>>();

foreach (var line in lines)
{
    var numberBuilder = string.Empty;
    var numberIsPartial = false;
    var currentGearCoordinate = (0, 0);

    for (int columnNumber=0; columnNumber < line.Length; columnNumber++)
    {
        var currentCharacter = line[columnNumber];

        if (char.IsDigit(currentCharacter))
        {
            numberBuilder += currentCharacter;

            if (!numberIsPartial)
            {
                (numberIsPartial, currentGearCoordinate) = AddNumBasedOnColumn(columnNumber, rowNumber, lines, currentGearCoordinate);

                if (!numberIsPartial) (numberIsPartial, currentGearCoordinate) = AddNumBasedOnDiagonal(columnNumber, rowNumber, lines, line, currentGearCoordinate);
            }
        }
        else if (IsSymbol(currentCharacter))
        {
            currentGearCoordinate = (columnNumber, rowNumber);
            if (!string.IsNullOrEmpty(numberBuilder))
            {
                gearRatios.TryAdd(currentGearCoordinate, new List<int>());
                gearRatios[currentGearCoordinate].Add(int.Parse(numberBuilder));
            }
            numberBuilder = string.Empty;
            numberIsPartial = true;
        }
        else
        {
            if (numberIsPartial && !string.IsNullOrEmpty(numberBuilder))
            {
                gearRatios.TryAdd(currentGearCoordinate, new List<int>());
                gearRatios[currentGearCoordinate].Add(int.Parse(numberBuilder));
            }

            numberBuilder = string.Empty;
            numberIsPartial = false;
        }
    }

    if (numberIsPartial && !string.IsNullOrEmpty(numberBuilder)) {
        gearRatios.TryAdd(currentGearCoordinate, new List<int>());
        gearRatios[currentGearCoordinate].Add(int.Parse(numberBuilder));
    }
    rowNumber++;
}

var ratioSum = 0;
foreach (var gear in gearRatios)
{
    if (gear.Value.Count != 2) continue;

    var ratio = gear.Value.First() * gear.Value.Last();
    ratioSum += ratio;
}

Console.WriteLine($"Ratio sum: {ratioSum}");

static (bool, (int, int)) AddNumBasedOnColumn(int columnNumber, int rowNumber, string[] lines, (int, int) currentGearCoordinate)
{
    if (rowNumber == lines.Length - 1) return (false, currentGearCoordinate);
    var adjacentColumnChar = lines[rowNumber + 1][columnNumber];

    if (IsSymbol(adjacentColumnChar)) return (true, (columnNumber, rowNumber + 1));
    return (false, currentGearCoordinate);
}

static (bool, (int, int)) AddNumBasedOnDiagonal(int columnNumber, int rowNumber, string[] lines, string line, (int, int) currentGearCoordinate)
{
    var leftDiagonalLowerChar = columnNumber == 0 || rowNumber == lines.Length - 1 ? '.' : lines[rowNumber + 1][columnNumber - 1];
    if (IsSymbol(leftDiagonalLowerChar)) return (true, (columnNumber - 1, rowNumber + 1));

    var rightDiagonalLowerChar = columnNumber == line.Length - 1 || rowNumber == lines.Length - 1? '.' : lines[rowNumber + 1][columnNumber + 1];
    if (IsSymbol(rightDiagonalLowerChar)) return (true, (columnNumber + 1, rowNumber + 1));

    var leftDiagonalUpperChar = columnNumber == 0 || rowNumber == 0 ? '.' : lines[rowNumber - 1][columnNumber - 1];
    if (IsSymbol(leftDiagonalUpperChar)) return (true, (columnNumber - 1, rowNumber - 1));

    var rightDiagonalUpperChar = columnNumber == line.Length - 1 || rowNumber == 0 ? '.' : lines[rowNumber - 1][columnNumber + 1];
    if (IsSymbol(rightDiagonalUpperChar)) return (true, (columnNumber + 1, rowNumber - 1));

    return (false, currentGearCoordinate);
}

static bool IsSymbol(char character)
{
    return character == '*';
}
