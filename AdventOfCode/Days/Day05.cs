using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;

namespace AdventOfCode.Days;

public partial class Day05 : ISolution
{
    public string PartOne(IEnumerable<string> input)
    {
        var (stacks, instructions) = ParseInput(input);

        foreach (var instruction in instructions)
        {
            var tmpQueue = new Queue<char>();
 
            for (var i = 0; i < instruction.Count; i++)
            {
                tmpQueue.Enqueue(stacks[instruction.From - 1].Pop());
            }

            while (tmpQueue.Count > 0)
            {
                stacks[instruction.To - 1].Push(tmpQueue.Dequeue());
            }
        }
        
        return string.Join("", stacks.Select(stack => stack.Pop()));
    }

    public string PartTwo(IEnumerable<string> input)
    {
        var (stacks, instructions) = ParseInput(input);

        foreach (var instruction in instructions)
        {
            var tmpQueue = new Stack<char>();
 
            for (var i = 0; i < instruction.Count; i++)
            {
                tmpQueue.Push(stacks[instruction.From - 1].Pop());
            }

            while (tmpQueue.Count > 0)
            {
                stacks[instruction.To - 1].Push(tmpQueue.Pop());
            }
        }

        return string.Join("", stacks.Select(stack => stack.Pop()));
    }

    private (List<Stack<char>> stacks, List<(int Count, int From, int To)> instructions) ParseInput(
        IEnumerable<string> input)
    {
        List<List<char>> rawStacks = new();
        List<(int Count, int From, int To)> instructions = new();
        
        //parsing the data. Gross
        foreach (var row in input)
        {
            if (row.Contains('['))
            {
                var counter = 0; 
                foreach (var chars in row.Chunk(4))
                {
  
                    if (rawStacks.Count < counter+1)
                    {
                        rawStacks.Add(new List<char>());
                    }

                    if (chars[1] != ' ')
                    {
                        rawStacks[counter].Add(chars[1]);
                    }
                    counter++;
                }
            }

            if (row.Contains("move"))
            {
                var numbers = NumbersRegex().Matches(row);
                instructions.Add((int.Parse(numbers[0].Value), int.Parse(numbers[1].Value), int.Parse(numbers[2].Value)));
            }
        }

        var stacks = new List<Stack<char>>();
        foreach (var rawStack in rawStacks)
        {
            rawStack.Reverse();
            stacks.Add(new Stack<char>(rawStack));
        }

        return (stacks, instructions);
    }
    
    public int Day => 05;
    
    [GeneratedRegex("\\d+")]
    private static partial Regex NumbersRegex();
}
