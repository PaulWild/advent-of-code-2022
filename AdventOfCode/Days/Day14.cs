namespace AdventOfCode.Days;

public class Day14 : ISolution
{
    private (int x, int y) toCoordinate(string item)
    {
        var tmp = item.Split(",");
        return (int.Parse(tmp[0]), int.Parse(tmp[1]));
    }
    public string PartOne(IEnumerable<string> input)
    {
        var grid = ParseGrid(input);
        var sand = new HashSet<(int x, int y)>();
        (int x, int y) sandSource = (500, 0);

        var currentSandLocation = sandSource;
        var maxY = grid.Select(pos => pos.y).Max();
        while (true)
        {
            if (currentSandLocation.y >= maxY)
            {
                break;
            }
            if (!grid.Contains((currentSandLocation.x, currentSandLocation.y+1)) &&
                     !sand.Contains((currentSandLocation.x, currentSandLocation.y+1)))
            {
                // can move directly down
                currentSandLocation = (currentSandLocation.x, currentSandLocation.y+1);
            }
            else if (!grid.Contains((currentSandLocation.x - 1, currentSandLocation.y + 1)) &&
                     !sand.Contains((currentSandLocation.x - 1, currentSandLocation.y + 1)))
            {
                currentSandLocation = (currentSandLocation.x - 1, currentSandLocation.y+1);         
            }
            else if (!grid.Contains((currentSandLocation.x + 1, currentSandLocation.y + 1)) &&
                     !sand.Contains((currentSandLocation.x + 1, currentSandLocation.y + 1)))
            {
                currentSandLocation = (currentSandLocation.x + 1, currentSandLocation.y+1);        
            }
            else
            {
                sand.Add(currentSandLocation);
                currentSandLocation = sandSource;
            }
        }
        


        return sand.Count.ToString();
    }

    private HashSet<(int x, int y)> ParseGrid(IEnumerable<string> input)
    {
        var grid = new HashSet<(int, int)>();
        foreach (var row in input)
        {
            var spots = row.Split("->", StringSplitOptions.TrimEntries).Select(toCoordinate).ToList();

            for (int i = 0; i < spots.Count - 1; i++)
            {
                var from = spots[i];
                var to = spots[i + 1];

                (int x, int y) diff = (0, 0);
                if (from.x < to.x)
                {
                    diff = (1, 0);
                }
                else if (from.x > to.x)
                {
                    diff = (-1, 0);
                }
                else if (from.y < to.y)
                {
                    diff = (0, 1);
                }
                else if (from.y > to.y)
                {
                    diff = (0, -1);
                }

                var steps = Math.Max(Math.Abs(from.x - to.x), Math.Abs(from.y - to.y));

                for (var j = 0; j <= steps; j++)
                {
                    grid.Add(from);
                    from = (from.x + diff.x, from.y + diff.y);
                }
            }
        }

        return grid;
    }

    public string PartTwo(IEnumerable<string> input)
    {
        var grid = ParseGrid(input);
        var sand = new HashSet<(int x, int y)>();
        (int x, int y) sandSource = (500, 0);

        var currentSandLocation = sandSource;
        var maxY = grid.Select(pos => pos.y).Max();
        var floor = maxY + 2;

        while (true)
        {
            
            if (currentSandLocation.y+1 == floor)
            {
                sand.Add(currentSandLocation);
                currentSandLocation = sandSource;
            }
            if (!grid.Contains((currentSandLocation.x, currentSandLocation.y+1)) &&
                !sand.Contains((currentSandLocation.x, currentSandLocation.y+1)))
            {
                // can move directly down
                currentSandLocation = (currentSandLocation.x, currentSandLocation.y+1);
            }
            else if (!grid.Contains((currentSandLocation.x - 1, currentSandLocation.y + 1)) &&
                     !sand.Contains((currentSandLocation.x - 1, currentSandLocation.y + 1)))
            {
                currentSandLocation = (currentSandLocation.x - 1, currentSandLocation.y+1);         
            }
            else if (!grid.Contains((currentSandLocation.x + 1, currentSandLocation.y + 1)) &&
                     !sand.Contains((currentSandLocation.x + 1, currentSandLocation.y + 1)))
            {
                currentSandLocation = (currentSandLocation.x + 1, currentSandLocation.y+1);        
            }
            else
            {
   
                sand.Add(currentSandLocation);
                if (currentSandLocation == sandSource)
                {
                    break;
                }

                currentSandLocation = sandSource;
            }
        }
        


        return sand.Count.ToString();
    }

    public int Day => 14;
}
