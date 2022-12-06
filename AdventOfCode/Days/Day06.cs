namespace AdventOfCode.Days;

public class Day06 : ISolution
{
    private int ProcessStream(string input, int messageLength)
    {
        for (var i = 0; i < input.Length; i++)
        {
            var toCheck = input.Substring(i, messageLength);
            if (toCheck.ToCharArray().ToHashSet().Count == messageLength)
            {
                return (i + messageLength);
            }
        }

        return -1;
    }
    
    public string PartOne(IEnumerable<string> input)
    {
        return ProcessStream(input.First(), 4).ToString();
    }

    public string PartTwo(IEnumerable<string> input)
    {
        return ProcessStream(input.First(), 14).ToString();
    }

    public int Day => 06;
}
