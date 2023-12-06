var lines = File.ReadAllLines("../../../input.txt");
var engineSchematic = 0;

var rowNumber = 0;
foreach (var line in lines)
{
    var numberBuilder = string.Empty;
    var numberIsPartial = false;

    for (int columnNumber=0; columnNumber < line.Length; columnNumber++)
    {
        var currentCharacter = line[columnNumber];

        if (currentCharacter == '.')
        {
            if (numberIsPartial && !string.IsNullOrEmpty(numberBuilder)) engineSchematic += int.Parse(numberBuilder);

            numberBuilder = string.Empty;
            numberIsPartial = false;
        }
        else if (char.IsDigit(currentCharacter))
        {
            numberBuilder += currentCharacter;

            if (!numberIsPartial)
            {
                if (AddNumBasedOnColumn(columnNumber, rowNumber, lines)) numberIsPartial = true;
                else numberIsPartial = AddNumBasedOnDiagonal(columnNumber, rowNumber, lines, line);
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(numberBuilder)) engineSchematic += int.Parse(numberBuilder);
            numberBuilder = string.Empty;
            numberIsPartial = true;
        }
    }

    if (numberIsPartial && !string.IsNullOrEmpty(numberBuilder)) engineSchematic += int.Parse(numberBuilder);
    rowNumber++;
}

static bool AddNumBasedOnColumn(int columnNumber, int rowNumber, string[] lines)
{
    if (rowNumber == lines.Length - 1) return false;
    var adjacentColumnChar = lines[rowNumber + 1][columnNumber];
    return IsSymbol(adjacentColumnChar);
}

Console.WriteLine($"Engine schematic: {engineSchematic}");

static bool AddNumBasedOnDiagonal(int columnNumber, int rowNumber, string[] lines, string line)
{
    var leftDiagonalLowerChar = columnNumber == 0 || rowNumber == lines.Length - 1 ? '.' : lines[rowNumber + 1][columnNumber - 1];
    if (IsSymbol(leftDiagonalLowerChar)) return true;

    var rightDiagonalLowerChar = columnNumber == line.Length - 1 || rowNumber == lines.Length - 1? '.' : lines[rowNumber + 1][columnNumber + 1];
    if (IsSymbol(rightDiagonalLowerChar)) return true;

    var leftDiagonalUpperChar = columnNumber == 0 || rowNumber == 0 ? '.' : lines[rowNumber - 1][columnNumber - 1];
    if (IsSymbol(leftDiagonalUpperChar)) return true;

    var rightDiagonalUpperChar = columnNumber == line.Length - 1 || rowNumber == 0 ? '.' : lines[rowNumber - 1][columnNumber + 1];
    return IsSymbol(rightDiagonalUpperChar);
}

static bool IsSymbol(char character)
{
    return character != '.' && !char.IsDigit(character);
}
