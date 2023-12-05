using AdventOfCode;

var lines = File.ReadAllLines("../../../input.txt");
var sum = 0;

foreach (var line in lines)
{

    var lineLength = line.Length;

    var startingIndex = 0;
    var endingIndex = lineLength - 1;

    var firstDigit = ForwardChecker.CheckDigitForwards(line, startingIndex);
    var secondDigit = BackwardChecker.CheckDigitBackwards(line, endingIndex);

    var calibrationValue = int.Parse($"{firstDigit}{secondDigit}");
    sum += calibrationValue;

}

Console.WriteLine($"Sum of calibration values: {sum}");
