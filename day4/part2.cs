var scratchCardTotals = new Dictionary<int, int>();

var lines = File.ReadAllLines("../../../input.txt");
var currentScratchCard = 1;

var allScratchCards = new List<string>();

foreach (var line in lines)
{
    var copyOfCardExists = !scratchCardTotals.TryAdd(currentScratchCard, 1);
    if (copyOfCardExists)
    {
        scratchCardTotals[currentScratchCard] = scratchCardTotals[currentScratchCard] + 1;
    };

    var numberOfWinningNumbers = GetNumberOfWinningNumbers(currentScratchCard, line);

    for (int i = 1; i <= numberOfWinningNumbers; i++)
    {
        if (!scratchCardTotals.TryAdd(currentScratchCard + i, scratchCardTotals[currentScratchCard]))
        {
            scratchCardTotals[currentScratchCard + i] = scratchCardTotals[currentScratchCard + i] + scratchCardTotals[currentScratchCard];
        }
    }

    currentScratchCard++;
}


Console.WriteLine($"Sum: {scratchCardTotals.Values.Sum()}");

static int GetNumberOfWinningNumbers(int currentScratchCard, string line)
{
    var numbers = line.Split('|');
    var winningNumbers = numbers[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(2).ToDictionary(num => num, val => true);
    var pickedNumbers = numbers[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

    var checkedNumbers = new HashSet<string>();
    var currentScore = 0;

    foreach (var num in pickedNumbers)
    {
        if (!winningNumbers.TryAdd(num, false) || winningNumbers[num]) currentScore++;
    }

    return currentScore;
}
