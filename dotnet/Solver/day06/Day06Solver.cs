using System.Diagnostics;
using AdventOfCode2024.Solver.day05;
using Xunit.Abstractions;

namespace AdventOfCode2024.Solver.day06;

public class Day06Solver(ITestOutputHelper output)
{
    // WIP: this is not impressive :D
    private void RunTest(Func<string[], int> solver, string file, int expected)
    {
        // Read all lines from a txt file
        var lines = File.ReadAllLines(file);

        var stopWatch = Stopwatch.StartNew();
        // output.WriteLine($"Read file from disk to memory: {stopWatch.ElapsedMilliseconds}ms");

        var result = solver(lines);
        output.WriteLine($"Time: {stopWatch.ElapsedMilliseconds}ms");

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("day06/test01.txt", 41)]
    // [InlineData("day05/MyInput.txt", 4135)]
    public void SolvePart1(string file, int expected)
    {
        var lines = File.ReadAllLines(file);
        var result = IvarSolvePart1(lines);
        Assert.Equal(expected, result);
    }

    private enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }


    private record Position(int x, int y);

    private record Board(string[,] Squares);
    
    private static (Position currentPosition, Direction currentDirection) GetNextPositionAndDirection(Board board,
        Position currentPosition, Direction currentDirection, int levelsDeep = 0)
    {
        if (levelsDeep > 3)
        {
            throw new Exception("what the heeeeck happened. I'm lost");
        }

        var nextPosition = currentDirection switch
        {
            Direction.Up => currentPosition with { y = currentPosition.y - 1 },
            Direction.Right => currentPosition with { x = currentPosition.x + 1 },
            Direction.Down => currentPosition with { y = currentPosition.y + 1 },
            Direction.Left => currentPosition with { x = currentPosition.x - 1 },
            _ => new Position(currentPosition.x, currentPosition.y)
        };

        if(!IsInsideMap(board, nextPosition))
            return (nextPosition, currentDirection);
        
        if (board.Squares[nextPosition.y, nextPosition.x] != "#") 
            return (nextPosition, currentDirection);
        
        // cannot go there! change direction and try moving agian
        var nextDirection = currentDirection switch
        {
            Direction.Up => Direction.Right,
            Direction.Right => Direction.Down,
            Direction.Down => Direction.Left,
            Direction.Left => Direction.Up,
            _ => currentDirection
        };
        
        return GetNextPositionAndDirection(board, currentPosition, nextDirection, ++levelsDeep);
    }

    private int IvarSolvePart1(string[] lines)
    {
        var board = CreateBoard(lines);
        var distinctPositions = new HashSet<Position>();
        var (currentPosition, currentDirection) = GetCurrentPositionAndDirection(board);

        while (true)
        {
            // Add current position to list of unique positions the guard travelled
            distinctPositions.Add(currentPosition);

            // Calculate next position
            var (nextPosition, nextDirection) = GetNextPositionAndDirection(board, currentPosition, currentDirection);

            if (!IsInsideMap(board, nextPosition))
                break;

            currentPosition = nextPosition;
        }

        return distinctPositions.Count();
    }

    private static bool IsInsideMap(Board board, Position position)
    {
        int maxX = board.Squares.GetLength(0);
        int maxY = board.Squares.GetLength(1);
        
        return position.x >= 0 && position.x < maxX
                                  && position.y >= 0 
                                  && position.y < maxY;
    }

    private static (Position currentPosition, Direction currentDirection) GetCurrentPositionAndDirection(
        Board board)
    {
        for (var y = 0; y < board.Squares.GetLength(0); y++)
        {
            for (var x = 0; x < board.Squares.GetLength(1); x++)
            {
                switch (board.Squares[y, x])
                {
                    case "^":
                        return (new Position(x, y), Direction.Up);
                    case ">":
                        return (new Position(x, y), Direction.Right);
                    case "v":
                        return (new Position(x, y), Direction.Down);
                    case "<":
                        return (new Position(x, y), Direction.Left);
                }
            }
        }

        throw new Exception("No starting position found");
    }

    private Board CreateBoard(string[] lines)
    {
        var squares = new string[lines.Length, lines[0].Length];

        for (var y = 0; y < lines.Length; y++)
        {
            for (var x = 0; x < lines[y].Length; x++)
            {
                squares[y,x] = lines[y][x].ToString();
            }
        }
      
        return new Board(squares);
    }
}