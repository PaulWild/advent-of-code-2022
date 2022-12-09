namespace AdventOfCode.Days;

public class Day08 : ISolution
{
    public string PartOne(IEnumerable<string> input)
    {

        var grid = ParseGrid(input);
        var maxX = grid.Keys.Select(x => x.x).Max();
        var maxY = grid.Keys.Select(y => y.y).Max();


        var visibleTrees = new HashSet<(int x, int y)>();

        for (var x = 0; x < maxX; x++)
        {
            var maxHeight = -1;
            for (var y = 0; y < maxY; y++)
            {
                var currentTree = grid[(x, y)];
                if (currentTree > maxHeight)
                {
                    visibleTrees.Add((x, y));
                    maxHeight = currentTree;
                }
            }
        }
        
        for (var y = 0; y < maxY; y++)
        {
            var maxHeight = -1;
            for (var x = 0; x < maxX; x++)     
            {
                var currentTree = grid[(x, y)];
                if (currentTree > maxHeight)
                {
                    visibleTrees.Add((x, y));
                    maxHeight = currentTree;
                }
            }
        }
        
        for (var x = maxX; x >=0; x--)
        {
            var maxHeight = -1;
            for (var y = maxY; y >=0; y--)
            {
                var currentTree = grid[(x, y)];
                if (currentTree > maxHeight)
                {
                    visibleTrees.Add((x, y));
                    maxHeight = currentTree;
                }
            }
        }
        
        for (var y = maxY; y >=0; y--)
        {
            var maxHeight = -1;
            for (var x = maxX; x >=0; x--)    
            {
                var currentTree = grid[(x, y)];
                if (currentTree > maxHeight)
                {
                    visibleTrees.Add((x, y));
                    maxHeight = currentTree;
                }
            }
        }

        return visibleTrees.Count.ToString();
    }

    private static Dictionary<(int x, int y), int> ParseGrid(IEnumerable<string> input)
    {
        var grid = new Dictionary<(int x, int y), int>();
        var x = 0;
        foreach (var row in input)
        {
            var y = 0;
            foreach (var item in row)
            {
                grid.Add((x, y), int.Parse(item.ToString()));
                y++;
            }

            x++;
        }

        return grid;
    }

    public string PartTwo(IEnumerable<string> input)
    {
        var grid = ParseGrid(input);
        var maxX = grid.Keys.Select(x => x.x).Max();
        var maxY = grid.Keys.Select(y => y.y).Max();

        
        var scenicLevel = new Dictionary<(int x, int y), int>();

        for (var treeX = 0; treeX <= maxX; treeX++)
        for (var treeY = 0; treeY <= maxY; treeY++)
        {
            var maxHeight = grid[(treeX, treeY)];

            var xPos = 0;
            for (var x = treeX+1; x <= maxX; x++)
            {
                xPos++;
                var currentTree = grid[(x, treeY)];
                if (currentTree >= maxHeight)
                {
                    break;
                }
            }

            var yPos = 0;
            for (var y = treeY+1; y <= maxY; y++)
            {
                var currentTree = grid[(treeX, y)];
                yPos++;
                if (currentTree >= maxHeight)
                {
                    break;
                }
            }

            var xNeg = 0;
            for (var x = treeX-1; x >= 0; x--)
            {
                xNeg++;
                var currentTree = grid[(x, treeY)];
                if (currentTree >= maxHeight )
                {
                    break;
                }
            }

            var yNeg = 0;
            for (var y = treeY-1; y >= 0; y--)
            {
                yNeg++;
                var currentTree = grid[(treeX, y)];
                if (currentTree >= maxHeight)
                {
                    break;
                }
            }
            
            scenicLevel.Add((treeX,treeY), xPos * yPos * xNeg * yNeg);
        }

        return scenicLevel.Values.Max().ToString();
    }

    public int Day => 08;
}
