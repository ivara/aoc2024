public record struct Point(int X, int Y)
{
    public static Point operator +(Point a, Point b) => new Point(a.X + b.X, a.Y + b.Y);
    
    public static Point operator -(Point a, Point b) => new Point(a.X - b.X, a.Y - b.Y);

    public static Point operator *(Point point, int multiple) => new Point(point.X * multiple, point.Y * multiple);

    public Point Normalize() => new Point(X != 0 ? X / Math.Abs(X) : 0, Y != 0 ? Y / Math.Abs(Y) : 0);

    public static implicit operator Point((int X, int Y) tuple) => new Point(tuple.X, tuple.Y);

    public int ManhattanDistance(Point b) => Math.Abs(X - b.X) + Math.Abs(Y - b.Y);
}

public class AoC2024Day6Part2
{
    private int _width;
    private int _height;
    private char[,] _map;
    private Point _startingPoint;

    public async Task<string> SolveAsync(StreamReader inputReader)
    {
        List<string> lines = new();
        while (await inputReader.ReadLineAsync() is { } line)
        {
            _width = line.Length;
            _height++;

            lines.Add(line);
        }

        _map = new char[_width, _height];
        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                _map[x, y] = lines[y][x];
                if (_map[x, y] == '^')
                {
                    _startingPoint = new Point(x, y);
                }
            }
        }

        var potentialObstructions = GetPotentialObstructionPositions(_startingPoint);

        int obstructionCount = 0;
        foreach (var potentialObstruction in potentialObstructions.Except([_startingPoint]))
        {
            if (DoesGuardLoop(_startingPoint, potentialObstruction))
            {
                obstructionCount++;
            }
        }

        return obstructionCount.ToString();
    }

    private bool DoesGuardLoop(Point start, Point newObstruction)
    {
        HashSet<(Point point, Point direction)> visited = new();

        var currentDirection = new Point(0, -1);
        var currentPoint = start;

        while (true)
        {
            if (visited.Contains((currentPoint, currentDirection)))
            {
                return true;
            }

            visited.Add((currentPoint, currentDirection));
            var nextPosition = currentPoint + currentDirection;
            if (IsOutOfBounds(nextPosition))
            {
                break;
            }

            if (_map[nextPosition.X, nextPosition.Y] == '#' ||
                (nextPosition.X == newObstruction.X && nextPosition.Y == newObstruction.Y))
            {
                // Turn right
                currentDirection = new Point(-currentDirection.Y, currentDirection.X);
                nextPosition = currentPoint;
            }

            if (IsOutOfBounds(nextPosition))
            {
                break;
            }

            currentPoint = nextPosition;
        }

        return false;
    }

    private HashSet<Point> GetPotentialObstructionPositions(Point start)
    {
        HashSet<Point> visited = new();

        var currentDirection = new Point(0, -1);
        var currentPoint = start;
        while (true)
        {
            visited.Add(currentPoint);
            var nextPosition = currentPoint + currentDirection;
            if (IsOutOfBounds(nextPosition))
            {
                break;
            }

            if (_map[nextPosition.X, nextPosition.Y] == '#')
            {
                // Turn right
                currentDirection = new Point(-currentDirection.Y, currentDirection.X);
                nextPosition = currentPoint;
            }

            if (IsOutOfBounds(nextPosition))
            {
                break;
            }

            currentPoint = nextPosition;
        }

        return visited;
    }

    private bool IsOutOfBounds(Point position)
    {
        return position.X < 0 || position.Y < 0 || position.X >= _width || position.Y >= _height;
    }
}