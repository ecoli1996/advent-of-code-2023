var lines = File.ReadAllLines("../../../input.txt");
var sum = 0;

foreach (var line in lines)
{
    var numbers = line.Split('|');
    var winningNumbers = numbers[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(2).ToDictionary(num => num, val => true);
    var pickedNumbers = numbers[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

    var checkedNumbers = new HashSet<string>();
    var currentScore = 0;

    foreach (var num in pickedNumbers)
    {
        // can't add because winning number exists but would also return false when a duplicate losing value gets added
        // so we'll keep track of a boolean to say if it's losing or winning
        if (!winningNumbers.TryAdd(num, false) || winningNumbers[num])
        {
            if (currentScore == 0) currentScore = 1;
            else currentScore *= 2;
        }
    }

    sum += currentScore;
}

Console.WriteLine($"Sum: {sum}");
