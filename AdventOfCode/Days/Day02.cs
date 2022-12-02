using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace AdventOfCode.Days;

public class Day02 : ISolution
{
    public string PartOne(IEnumerable<string> input)
    {
        var score = input.Select(row => row.Split())
            .Select(split => (split[0], split[1]))
            .Select(game => game switch
            {
                ("A", "Y") => 8,
                ("B", "Z") => 9,
                ("C", "X") => 7,
                ("A", "X") => 4,
                ("B", "Y") => 5,
                ("C", "Z") => 6,
                (_, "X") => 1,
                (_, "Y") => 2,
                (_, "Z") => 3,
                _ => throw new ArgumentOutOfRangeException(nameof(game))
            })
            .Sum();
        return score.ToString();
    }

    public string PartTwo(IEnumerable<string> input)
    {
        var score = input.Select(row => row.Split())
            .Select(split => (split[0], split[1]))
            .Select(game => game switch
            {
                ("A", "X") => 3,
                ("A", "Y") => 4,
                ("A", "Z") => 8,
                ("B", "X") => 1,
                ("B", "Y") => 5,
                ("B", "Z") => 9,
                ("C", "X") => 2,
                ("C", "Y") => 6,
                ("C", "Z") => 7,
                _ => throw new ArgumentOutOfRangeException(nameof(game), game, null)
            })
            .Sum();
        return score.ToString();
    }

    public int Day => 02;
}
