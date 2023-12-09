var lines = File.ReadAllLines("../../../input.txt");

var maxTime = ulong.Parse(string.Join("", lines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1)));
var recordDistance = ulong.Parse(string.Join("", lines[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1)));

var numberOfPossibilities = 0;

// speedPerMs = number of ms holding down button 
for (ulong speedPerMs = 1; speedPerMs < maxTime; speedPerMs++)
{
    var timeToRace = maxTime - speedPerMs;
    var distance = speedPerMs * timeToRace;
    if (distance > recordDistance) numberOfPossibilities++;
}

Console.WriteLine(numberOfPossibilities);
