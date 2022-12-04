using System.Text.RegularExpressions;

namespace AdventOfCode.Days;

public partial class Day04 : ISolution
{
    private static List<int> GetAssignmentNumbers(string from, string to)
    {
        return Enumerable.Range(int.Parse(from), int.Parse(to)-int.Parse(from) + 1).ToList();
    }
    public string PartOne(IEnumerable<string> input)
    {
        var fullyEnclosed = (from pair in input select NumbersRegex().Split(pair) into numbers 
            let assignment1 = GetAssignmentNumbers(numbers[0], numbers[1]) 
            let assignment2 = GetAssignmentNumbers(numbers[2], numbers[3]) 
            let intersection = assignment1.Intersect(assignment2) 
            where intersection.Count() == Math.Min(assignment1.Count, assignment2.Count) 
            select assignment1).Count();

        return fullyEnclosed.ToString();
    }

    public string PartTwo(IEnumerable<string> input)
    {
        var overlapping = (from pair in input select NumbersRegex().Split(pair) into numbers 
            let assignment1 = GetAssignmentNumbers(numbers[0], numbers[1]) 
            let assignment2 = GetAssignmentNumbers(numbers[2], numbers[3]) 
            let intersection = assignment1.Intersect(assignment2)
            where intersection.Any()
            select intersection).Count();

        return overlapping.ToString();
    }

    public int Day => 04;

    
    [GeneratedRegex("\\D+")]
    private static partial Regex NumbersRegex();
}
