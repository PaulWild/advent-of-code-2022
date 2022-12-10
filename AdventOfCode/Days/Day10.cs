using System.Text;
using AdventOfCode.Common;

namespace AdventOfCode.Days;

public class Day10 : ISolution
{
    private static IEnumerable<int> SpritePosition(IEnumerable<string> input)
    {
        var register = 1;

        foreach (var row in input)
        {
            var split = row.Split(" ");

            var inc = split[0] == "noop" ? 0 : int.Parse(split[1]);
            var loop = split[0] == "noop" ? 1 : 2;

            foreach (var _ in Enumerable.Range(1,loop))
            {
                yield return register;
            }

            register += inc;
        }
    }
    public string PartOne(IEnumerable<string> input)
    {
        var iteration = 1;
        var signalStrength = 0;
        foreach (var register in SpritePosition(input))
        {
            if ((iteration + 20) % 40 == 0)
            {
                signalStrength += iteration * register;
                
            }

            iteration++;
        }
        
        return signalStrength.ToString();
    }

    private static bool PrintPixel(int crtLocation, int spritePosition)
    {
        return crtLocation == spritePosition || crtLocation == spritePosition - 1 || crtLocation == spritePosition + 1;
    }
    
    public string PartTwo(IEnumerable<string> input)
    {
        var drawing = new List<char>();
        
        foreach (var (spritePosition, index) in SpritePosition(input).WithIndex())
        {
            var crtLocation = index % 40;
            drawing.Add(PrintPixel(crtLocation, spritePosition) ? '#' : '.');
        }

        var sb = new StringBuilder();
        sb.AppendLine("");
        foreach (var row in drawing.Chunk(40))
        {
            sb.AppendLine(string.Join("", row));
        }
        
        return sb.ToString();
    }

    public int Day => 10;
}
