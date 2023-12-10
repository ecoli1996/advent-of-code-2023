var lines = File.ReadAllLines("../../../input.txt");
var faceRanks = new Dictionary<char, int>
{
    { '1', 1 },
    { '2', 2 },
    { '3', 3 },
    { '4', 4 },
    { '5', 5 },
    { '6', 6 },
    { '7', 7 },
    { '8', 8 },
    { '9', 9 },
    { 'T', 10 },
    { 'J', 11 },
    { 'Q', 12 },
    { 'K', 13 },
    { 'A', 14 },
};

var fivesOfAKind = new List<(string, int)>();
var foursOfAKind = new List<(string, int)>();
var fullHouses = new List<(string, int)>();
var threesOfAKind = new List<(string, int)>();
var twoPairs = new List<(string, int)>();
var onePairs = new List<(string, int)>();
var highCards = new List<(string, int)>();
var allHands = new Dictionary<HandType, List<(string, int)>>();

foreach (var line in lines)
{
    var handsAndBid = line.Split(' ');
    var hand = handsAndBid[0];
    var bid = int.Parse(handsAndBid[1]);

    if (IsFiveOfAKind(hand)) fivesOfAKind.Add((hand, bid));
    else if (IsFourOfAKind(hand)) foursOfAKind.Add((hand, bid));
    else if (IsAFullHouse(hand)) fullHouses.Add((hand, bid));
    else if (IsThreeOfAKind(hand)) threesOfAKind.Add((hand, bid));
    else if (IsATwoPair(hand)) twoPairs.Add((hand, bid));
    else if (IsAOnePair(hand)) onePairs.Add((hand, bid));
    else highCards.Add((hand, bid));
}

allHands.Add(HandType.HighCard, highCards);
allHands.Add(HandType.OnePair, onePairs);
allHands.Add(HandType.TwoPair, twoPairs);
allHands.Add(HandType.ThreeOfAKind, threesOfAKind);
allHands.Add(HandType.FullHouse, fullHouses);
allHands.Add(HandType.FourOfAKind, foursOfAKind);
allHands.Add(HandType.FiveOfAKind, fivesOfAKind);

var checkHandType = HandType.HighCard;
var currentRank = 1;
var totalWinnings = 0;

while (checkHandType != HandType.None)
{
    var getHandsByRank = allHands[checkHandType];

    if (getHandsByRank.Count == 1)
    {
        totalWinnings += getHandsByRank.First().Item2 * currentRank;
        currentRank++;
    }
    else if (getHandsByRank.Any())
    {
        var orderedHandRank = GetHandOrderedByCardFace(getHandsByRank);
        foreach (var hand in orderedHandRank)
        {
            totalWinnings += hand.Item2 * currentRank;
            currentRank++;
        }
    }

    checkHandType++;
}

Console.WriteLine(totalWinnings);

List<(string, int)> GetHandOrderedByCardFace(List<(string, int)> hands)
{
    return hands
        .OrderBy(h => faceRanks[h.Item1[0]])
        .ThenBy(h => faceRanks[h.Item1[1]])
        .ThenBy(h => faceRanks[h.Item1[2]])
        .ThenBy(h => faceRanks[h.Item1[3]])
        .ThenBy(h => faceRanks[h.Item1[4]])
        .ToList();
}

// Five of a kind, where all five cards have the same label: AAAAA
bool IsFiveOfAKind(string hand)
{
    var cardToMatch = hand[0];
    return !hand.Any(x => x != cardToMatch);
}

// Four of a kind, where four cards have the same label and one card has a different label: AA8AA
bool IsFourOfAKind(string hand)
{
    var hashSetShouldOnlyHave2 = hand.ToHashSet();
    if (hashSetShouldOnlyHave2.Count != 2) return false;

    var firstCharIsSingle = hand.Count(c => c == hashSetShouldOnlyHave2.ElementAt(0));
    if (firstCharIsSingle == 1) return true;

    var secondCharIsSingle = hand.Count(c => c == hashSetShouldOnlyHave2.ElementAt(1));
    return secondCharIsSingle == 1;
}

// Full house, where three cards have the same label, and the remaining two cards share a different label: 23332
bool IsAFullHouse(string hand)
{
    char? matchChar1 = null;
    char? matchChar2 = null;

    var numberOfMatchChar1 = 0;
    var numberOfMatchChar2 = 0;

    for (int i = 0; i < hand.Length; i++)
    {
        if (i == 0)
        {
            matchChar1 = hand[0];
            numberOfMatchChar1++;
        }
        else
        {
            var card = hand[i];
            if (card == matchChar1) numberOfMatchChar1++;
            else if (matchChar2 == null)
            {
                matchChar2 = card;
                numberOfMatchChar2++;
            }
            else if (card == matchChar2) numberOfMatchChar2++;
            else return false;
        }
    }

    return (numberOfMatchChar2 == 3 && numberOfMatchChar1 == 2) || (numberOfMatchChar2 == 2 && numberOfMatchChar1 == 3);
}

// Three of a kind, where three cards have the same label, and the rest are distinct: TTT98
bool IsThreeOfAKind(string hand)
{
    var cards = new Dictionary<char, int>();

    for (int i = 0; i < hand.Length; i++)
    {
        if (i == 0)
        {
            cards.Add(hand[i], 1);
        }
        else
        {
            var card = hand[i];
            if (!cards.TryAdd(card, 1)) cards[card] = cards[card] + 1;
        }
    }

    if (cards.Count != 3) return false;

    var hasAThree = false;
    var hasADistinct1 = false;
    var hasADistinct2 = false;

    foreach (var card in cards)
    {
        if (card.Value == 2) return false;
        if (card.Value == 3) hasAThree = true;
        if (card.Value == 1 && !hasADistinct1) hasADistinct1 = true;
        else if (card.Value == 1 && !hasADistinct2) hasADistinct2 = true;
    }

    return hasAThree && hasADistinct1 && hasADistinct2;
}

// Two pair, where two cards share one label, two other cards share a second label, and the remaining card has a third label: 23432
bool IsATwoPair(string hand)
{
    var cards = new Dictionary<char, int>();

    for (int i = 0; i < hand.Length; i++)
    {
        if (i == 0)
        {
            cards.Add(hand[i], 1);
        }
        else
        {
            var card = hand[i];
            if (!cards.TryAdd(card, 1)) cards[card] = cards[card] + 1;
        }
    }

    if (cards.Count != 3) return false;

    var isDistinct = false;
    var hasTwo1 = false;
    var hasTwo2 = false;

    foreach (var card in cards)
    {
        if (card.Value > 2) return false;
        if (card.Value == 1) isDistinct = true;
        if (card.Value == 2 && !hasTwo1) hasTwo1 = true;
        else if (card.Value == 1 && !hasTwo2) hasTwo2 = true;
    }

    return isDistinct && hasTwo1 && hasTwo2;
}

// One pair, where two cards share one label, and the other three cards have a different label from the pair and each other: A23A4
bool IsAOnePair(string hand)
{
    var cards = new Dictionary<char, int>();

    for (int i = 0; i < hand.Length; i++)
    {
        if (i == 0)
        {
            cards.Add(hand[i], 1);
        }
        else
        {
            var card = hand[i];
            if (!cards.TryAdd(card, 1)) cards[card] = cards[card] + 1;
        }
    }

    if (cards.Count != 4) return false;

    var isDistinct1 = false;
    var isDistinct2 = false;
    var isDistinct3 = false;
    var hasTwo = false;

    foreach (var card in cards)
    {
        if (card.Value > 2) return false;
        if (card.Value == 2) hasTwo = true;
        if (card.Value == 1 && !isDistinct1) isDistinct1 = true;
        else if (card.Value == 1 && !isDistinct2) isDistinct2 = true;
        else if (card.Value == 1 && !isDistinct3) isDistinct3 = true;
    }

    return hasTwo && isDistinct1 && isDistinct2 && isDistinct3;
}

// High card, where all cards' labels are distinct: 23456
bool IsHighCard(string hand)
{
    var set = hand.ToHashSet();
    return set.Count == 5;
}

enum HandType
{
    HighCard = 1,
    OnePair,
    TwoPair,
    ThreeOfAKind,
    FullHouse,
    FourOfAKind,
    FiveOfAKind,
    None
}
