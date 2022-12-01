using static System.Int32;

namespace AdventOfCode.Days;

public class Day01 : ISolution
{
    public string PartOne(IEnumerable<string> input)
    {
        var calories = ParseElfCalories(input);

        return calories.Max().ToString();
    }
    
    public string PartTwo(IEnumerable<string> input)
    {
        var calories = ParseElfCalories(input);

        return calories.OrderDescending().Take(3).Sum().ToString();
    }
    
    private static IEnumerable<int> ParseElfCalories(IEnumerable<string> input)
    {
        var calories = new List<int>();
        var elfCalorie = 0;
        foreach (var calorie in input)
        {
            if (calorie == "")
            {
                calories.Add(elfCalorie);
                elfCalorie = 0;
            }
            else
            {
                elfCalorie += Parse(calorie);
            }
        }
        
        //purge the last elf. I always make that mistake at some point
        calories.Add(elfCalorie);
        
        return calories;
    }
    public int Day => 01;
}
