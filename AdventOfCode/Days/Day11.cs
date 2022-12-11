using System.Text.RegularExpressions;
namespace AdventOfCode.Days;


public partial class Day11 : ISolution
{
    // lcm of primes up to 23. 
    private const long Lcm = 223092870; 
    
    public string PartOne(IEnumerable<string> input)
    {
        var monkeys = ParseMonkeys(input, false);
        return CalculateMonkeyBusiness(monkeys, 20);
    }
    
    public string PartTwo(IEnumerable<string> input)
    {
        var monkeys = ParseMonkeys(input, true);
        return CalculateMonkeyBusiness(monkeys, 10000);
    }

    private static string CalculateMonkeyBusiness(List<Monkey> monkeys, int iterationCount)
    {
        for (var i = 0; i < iterationCount; i++)
        {
            foreach (var monkey in monkeys)
            {
                while (monkey.CanInspect)
                {
                    monkey.Inspect(monkeys);
                }
            }
        }

        return monkeys.Select(x => x.Inspected)
            .OrderDescending()
            .Take(2)
            .Aggregate((x, y) => x * y)
            .ToString();
    }

    private static List<Monkey> ParseMonkeys(IEnumerable<string> input, bool isPartTwo)
    {
        var monkeys = new List<Monkey>();
        Monkey? monkey = null;

        foreach (var rawRow in input)
        {
            var row = rawRow.Trim();
            if (row.StartsWith("Monkey"))
            {
                monkey = new Monkey(isPartTwo ? l => l % Lcm : l => Convert.ToInt64(Math.Floor(l / 3.0)));
                monkeys.Add(monkey);
            }
            else if (row.StartsWith("Starting items"))
            {
                monkey!.Items = new Queue<long>(row.Split(":")[1].Split(",").Select(x => long.Parse(x.Trim())).ToList());
            }
            else if (row.StartsWith("Operation"))
            {
                monkey!.Operation = row.Split(":")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();
            }
            else if (row.StartsWith("Test"))
            {
                monkey!.Condition = int.Parse(NumbersRegex().Matches(row)[0].Value);
            }
            else if (row.Contains("true"))
            {
                monkey!.True = int.Parse(NumbersRegex().Matches(row)[0].Value);
            }
            else if (row.Contains("false"))
            {
                monkey!.False = int.Parse(NumbersRegex().Matches(row)[0].Value);
            }
        }

        return monkeys;
    }
    
    
    [GeneratedRegex("\\d+")]
    private static partial Regex NumbersRegex();

    public int Day => 11;
}

public class Monkey
{
    private readonly Func<long, long> _worryControl;

    public Monkey(Func<long, long> worryControl)
    {
        _worryControl = worryControl;
    }
    public Queue<long> Items { get; set; } = new();

    public string[] Operation { get; set; } = new string[3];
        
    public long Condition { get; set; }
        
    public int True { get; set; }
        
    public int False { get; set; }

    public long Inspected { get; private set; }

    private long ApplyWorryLevel(long item)
    {
        var l = Operation[2] == "old" ? item : long.Parse(Operation[2]);
        var r = Operation[4] == "old" ? item : long.Parse(Operation[4]);

        var worryLevel = Operation[3] == "*" ? l * r : l + r;

        return _worryControl(worryLevel);
    }

    private int PassItemTo(long item)
    {
        var isTrue = item % Condition == 0;
        return isTrue ? True : False;
    }

    public bool CanInspect => Items.Count > 0;
        
    public void Inspect(List<Monkey> monkeys)
    {
        var item = Items.Dequeue();
        item = ApplyWorryLevel(item);
        var toPassTo = PassItemTo(item);
        monkeys[toPassTo].Items.Enqueue(item);
        Inspected++;
    }
}
