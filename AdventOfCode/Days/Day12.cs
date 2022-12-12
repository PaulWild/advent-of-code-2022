using AdventOfCode.Common;

namespace AdventOfCode.Days;

public class Day12 : ISolution
{
    public string PartOne(IEnumerable<string> input)
    {
        var (grid, start, end) = ParseGrid(input);

        return (ShortestPath(start, grid, end)).ToString();
    }

    private static int ShortestPath((int x, int y) start, Dictionary<(int x, int y), int> grid, (int x, int y) end)
    {
        var paths = new List<Stack<(int x, int y)>>();
        var startPath = new Stack<(int x, int y)>();
        startPath.Push(start);
        paths.Add(startPath);
        var visited = new HashSet<(int, int)>();

        while (true)
        {
            var newPaths = new List<Stack<(int x, int y)>>();
            foreach (var path in paths)
            {
                foreach (var next in grid.DirectNeighbours(path.Peek()))
                {
                    if (grid[next] <= grid[path.Peek()] + 1 && !visited.Contains(next))
                    {
                        visited.Add(next);
                        var newPath = new Stack<(int x, int y)>(path);
                        
                        if (next == end)
                        {
                            return newPath.Count;
                        }
                        newPath.Push(next);
                        newPaths.Add(newPath);
                    }
                }
            }

            paths = newPaths;
        }
    }

    private static int ShortestPathPart2((int x, int y) start, Dictionary<(int x, int y), int> grid)
    {
        var paths = new List<Stack<(int x, int y)>>();
        var startPath = new Stack<(int x, int y)>();
        startPath.Push(start);
        paths.Add(startPath);
        var visited = new HashSet<(int, int)>();

        while (true)
        {
            var newPaths = new List<Stack<(int x, int y)>>();
            foreach (var path in paths)
            {
                foreach (var next in grid.DirectNeighbours(path.Peek()))
                {
                    if (grid[next] >= grid[path.Peek()] -1 && !visited.Contains(next))
                    {
                        visited.Add(next);
                        var newPath = new Stack<(int x, int y)>(path);
                        
                        if (grid[next] == 1)
                        {
                            return newPath.Count;
                        }
                        newPath.Push(next);
                        newPaths.Add(newPath);
                    }
                }
            }

            paths = newPaths;
        }
    }

    public string PartTwo(IEnumerable<string> input)
    {
        var (grid, start,end) = ParseGrid(input);

        return (ShortestPathPart2(end, grid)).ToString();
    }
    
    private static (Dictionary<(int x, int y), int> grid, (int x, int y) start, (int x, int y) end) ParseGrid(IEnumerable<string> input)
    {
        (int x, int y) start = (0,0);
        (int x, int y) end = (0,0);

        var grid = new Dictionary<(int x, int y), int>();
        var x = 0;
        foreach (var row in input)
        {
            var y = 0;
            foreach (var item in row)
            {
                switch (item)
                {
                    case 'S':
                        grid.Add((x, y), 1);
                        start = (x, y);
                        break;
                    case 'E':
                        grid.Add((x, y), 26);
                        end = (x, y);
                        break;
                    default:
                        grid.Add((x, y), item - 96);
                        break;
                }

                y++;
            }

            x++;
        }

        return (grid, start, end);
    }

    public int Day => 12;
}
