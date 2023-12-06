using AdventOfCode;

var lines = File.ReadAllLines("../../../input.txt");
var sum = 0;

var MAX_RED_CUBES = 12;
var MAX_GREEN_CUBES = 13;
var MAX_BLUE_CUBES = 14;

var gameId = 1;

foreach (var line in lines)
{
    var game = line.Split(' ');
    var gameCanHappen = true;
    var currentIndex = 2;

    while (currentIndex < game.Length - 3 && gameCanHappen)
    {
        var tempCubes2 = 0;
        var tempCubes3 = 0;

        var tempCubes1 = int.Parse(game[currentIndex]);
        var cubeColor1Str = game[currentIndex + 1];
        var cubeColor1Enum = GetColor(cubeColor1Str);

        gameCanHappen = CanGameHappen(cubeColor1Enum, tempCubes1);

        if (!gameCanHappen) break;
        if (EndOfGame(cubeColor1Str)) break;

        currentIndex += 2;

        if (cubeColor1Str[^1] == ',')
        {
            tempCubes2 = int.Parse(game[currentIndex]);
            var cubeColor2Str = game[currentIndex + 1];
            var cubeColor2Enum = GetColor(cubeColor2Str);

            gameCanHappen = CanGameHappen(cubeColor2Enum, tempCubes2);
            if (!gameCanHappen) break;
            if (EndOfGame(cubeColor2Str)) break;

            currentIndex += 2;

            if (cubeColor2Str[^1] == ',')
            {
                tempCubes3 = int.Parse(game[currentIndex]);
                var cubeColor3Str = game[currentIndex + 1];
                var cubeColor3Enum = GetColor(cubeColor3Str);

                gameCanHappen = CanGameHappen(cubeColor3Enum, tempCubes3);
                if (!gameCanHappen) break;
                if (EndOfGame(cubeColor3Str)) break;
            }
        }
    }

    if (gameCanHappen)
    {
        sum += gameId;
        Console.WriteLine($"Game id: {gameId}");
    }
    gameId++;
}

Console.WriteLine($"Sum of ids: {sum}");

static Color GetColor(string possibleColor)
{
    return possibleColor[0] switch
    {
        'r' => Color.Red,
        'b' => Color.Blue,
        _ => Color.Green,
    };
}

static bool EndOfGame(string cubeColorString)
{
    var cubeColor1EndChar = cubeColorString[^1];
    return cubeColor1EndChar != ';' && cubeColor1EndChar != ',';
}

bool CanGameHappen(Color color, int numberOfCubes)
{
    if (color == Color.Blue && numberOfCubes > MAX_BLUE_CUBES) return false;
    else if (color == Color.Green && numberOfCubes > MAX_GREEN_CUBES) return false;
    else if (color == Color.Red && numberOfCubes > MAX_RED_CUBES) return false;
    return true;
}
