using System.Dynamic;
using System.Numerics;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Windows.Markup;
using AdventOfCode.Common;

namespace AdventOfCode.Days;

public class Day09 : ISolution
{
    private (int x, int y) MoveTail((int x, int y) head, (int x, int y) tail)
    {
        (int x, int y) diff;
        
        if (Grid.Neighbours2(head.x, head.y).Contains(tail) || head == tail)
        {
            diff = (0, 0);
        }
        else if (head.x != tail.x && head.y != tail.y)
        {
            diff = (head.x > tail.x ? +1 : -1, head.y > tail.y ? +1 : -1);
        }
        else if (head.x != tail.x)
        {
            diff = (head.x > tail.x ? +1 : -1,0);
        }
        else
        {
            diff = (0,head.y > tail.y ? +1 : -1); 
        }

        return (tail.x + diff.x, tail.y + diff.y);

    }

    public string PartOne(IEnumerable<string> input)
    {
        return MoveSnake(input,2).ToString();
    }

    public string PartTwo(IEnumerable<string> input)
    {
        return MoveSnake(input,10).ToString();
    }

    private int MoveSnake(IEnumerable<string> input, int snakeLength)
    {
        List<(int x, int y)> snake = Enumerable.Range(1, snakeLength).Select(_ => (0, 0)).ToList();
        var tailPath = new HashSet<(int, int)> { (0, 0) };

        foreach (var row in input)
        {
            var split = row.Split(" ");
            (string direction, int length) command = (split[0], int.Parse(split[1]));

            (int x, int y) difference = command.direction switch
            {
                "U" => (0, 1),
                "D" => (0, -1),
                "R" => (1, 0),
                "L" => (-1, 0),
                _ => (0, 0)
            };

            foreach (var _ in Enumerable.Range(0, command.length))
            {
                var leadingPiece = snake[0];
                snake[0] = (leadingPiece.x + difference.x, leadingPiece.y + difference.y);

                foreach (var trailingPiece in Enumerable.Range(1, snake.Count - 1))
                {
                    snake[trailingPiece] = MoveTail(snake[trailingPiece - 1], snake[trailingPiece]);
                }

                tailPath.Add(snake[^1]);
            }
        }

        return tailPath.Count;
    }

    public int Day => 09;
}
