var lines = File.ReadAllLines("../../../input.txt");

var times = lines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1);
var distances = lines[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1);

var productOfPossibilities = 1;

for (int i=0; i < times.Count(); i++)
{
    var maxTime = int.Parse(times.ElementAt(i));
    var recordDistance = int.Parse(distances.ElementAt(i));
    var numberOfPossibilities = 0;

    // speedPerMs = number of ms holding down button 
    for (int speedPerMs = 1; speedPerMs < maxTime; speedPerMs++)
    {
        var timeToRace = maxTime - speedPerMs;
        var distance = speedPerMs * timeToRace;
        if (distance > recordDistance) numberOfPossibilities++;
    }

    productOfPossibilities *= numberOfPossibilities;
}

Console.WriteLine(productOfPossibilities);
