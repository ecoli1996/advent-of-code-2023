var lines = File.ReadAllLines("../../../input.txt");
var sum = 0;

foreach (var line in lines)
{
    int? firstDigit = null;
    int? secondDigit = null;

    var lineLength = line.Length;

    var forwardIndex = 0;
    var backwardIndex = lineLength - 1;

    while (firstDigit == null || secondDigit == null)
    {
        if (firstDigit == null)
        {
            var firstDigitPossibility = line[forwardIndex].ToString();
            firstDigit = int.TryParse(firstDigitPossibility, out var number) ? number : null;
        }

        if (secondDigit == null)
        {
            var secondDigitPossibility = line[backwardIndex].ToString();
            secondDigit = int.TryParse(secondDigitPossibility, out var number) ? number : null;
        }

        forwardIndex++;
        backwardIndex--;
    }

    var calibrationValue = int.Parse($"{firstDigit}{secondDigit}");
    sum += calibrationValue;

    // Console.WriteLine($"Calibration value: {calibrationValue}");
}

Console.WriteLine($"Sum of calibration values: {sum}");
