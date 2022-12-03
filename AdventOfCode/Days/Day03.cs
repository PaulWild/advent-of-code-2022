namespace AdventOfCode.Days;

public class Day03 : ISolution
{
    private readonly Dictionary<char, int> _letterPriorityMap = new()
    {
        { 'a', 1 },
        { 'b', 2 },
        { 'c', 3 },
        { 'd', 4 },
        { 'e', 5 },
        { 'f', 6 },
        { 'g', 7 },
        { 'h', 8 },
        { 'i', 9 },
        { 'j', 10 },
        { 'k', 11 },
        { 'l', 12 },
        { 'm', 13 },
        { 'n', 14 },
        { 'o', 15 },
        { 'p', 16 },
        { 'q', 17 },
        { 'r', 18 },
        { 's', 19 },
        { 't', 20 },
        { 'u', 21 },
        { 'v', 22 },
        { 'w', 23 },
        { 'x', 24 },
        { 'y', 25 },
        { 'z', 26 },
    };

    private int GetPriority(char item)
    {
        return char.IsUpper(item) 
            ? _letterPriorityMap[char.ToLower(item)] + 26 
            : _letterPriorityMap[item];
    }
    public string PartOne(IEnumerable<string> input)
    {
        var priorities = (from rucksacks in input 
            let rucksack1 = rucksacks[..(rucksacks.Length / 2)].ToCharArray()
            let rucksack2 = rucksacks[(rucksacks.Length / 2)..].ToCharArray()
            select rucksack1.Intersect(rucksack2)
                .Select(GetPriority)
                .Sum()).ToList();
        
        return priorities.Sum().ToString();
    }

    public string PartTwo(IEnumerable<string> input)
    {
        var priorities = (from rucksacks in input.Chunk(3)
            let rucksack1 = rucksacks[0].ToCharArray()
            let rucksack2 = rucksacks[1].ToCharArray()
            let rucksack3 = rucksacks[2].ToCharArray()
            select rucksack1.Intersect(rucksack2)
                .Intersect(rucksack3)
                .Select(GetPriority)
                .Sum()).ToList();

        return priorities.Sum().ToString();
    }

    public int Day => 03;
}
