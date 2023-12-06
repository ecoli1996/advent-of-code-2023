using AdventOfCode;

var lines = File.ReadAllLines("../../../input.txt");
var powerSum = 0;

foreach (var line in lines)
{
    var gameArray = line.Split(' ');
    var game = new Game
    {
        MaxBlue = 0,
        MaxGreen = 0,
        MaxRed = 0,
        Cubes = gameArray.Skip(2)
    };

    for (int i=0; i < game.Cubes.Count() - 1; i += 2)
    {
        var cubeTotal = int.Parse(game.Cubes.ElementAt(i));
        var color = GetColor(game.Cubes.ElementAt(i + 1));

        if (color == Color.Red && cubeTotal > game.MaxRed) game.MaxRed = cubeTotal;
        if (color == Color.Green && cubeTotal > game.MaxGreen) game.MaxGreen = cubeTotal;
        if (color == Color.Blue && cubeTotal > game.MaxBlue) game.MaxBlue = cubeTotal;
    }

    var power = game.MaxBlue * game.MaxRed * game.MaxGreen;
    powerSum += power;
}

Console.WriteLine($"Sum of powers: {powerSum}");

static Color GetColor(string possibleColor)
{
    return possibleColor[0] switch
    {
        'r' => Color.Red,
        'b' => Color.Blue,
        _ => Color.Green,
    };
}
