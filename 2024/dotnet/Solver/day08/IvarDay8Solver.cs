namespace AdventOfCode2024.Solver.day08;

public class IvarDay8Solver : IPuzzleSolution<long>
{
    [Theory]
    [InlineData("day08/test01.txt", 14)]
    // [InlineData("day08/IvarInput.txt", 252)]
    public async Task Part1(string file, long expected)
    {
        var input = await File.ReadAllLinesAsync(file);
        var ivarDay8Solver = new IvarDay8Solver();

        var result = await ivarDay8Solver.SolvePartOneAsync(input);
        Assert.Equal(expected, result);
    }

    [Theory]
    // [InlineData("day08/test01.txt", 34)]
    [InlineData("day08/MyInput.txt", 252)]
    public async Task Part2(string file, long expected)
    {
        var input = await File.ReadAllLinesAsync(file);
        var ivarDay8Solver = new IvarDay8Solver();

        var result = await ivarDay8Solver.SolvePartTwoAsync(input);
        Assert.Equal(expected, result);
    }

    private record Position(int X, int Y);


    public async Task<long> SolvePartOneAsync(string[] input)
    {
        var antennas = GetAntennaPositions(input);
        var antinodes = new List<Position>();
        foreach (var antenna in antennas)
        {
            antinodes.AddRange(GetAntinodes(antenna.Value));
        }

        var uniqueAntinodes = antinodes.ToList();
        uniqueAntinodes.RemoveAll(p => !IsInsideMap(p, input[0].Length, input.Length));
        return uniqueAntinodes.Count();
    }

    private bool IsInsideMap(Position position, int maxX, int maxY)
    {
        return position.X > 0 || position.X < maxX ||
               position.Y > 0 || position.Y < maxY;
    }

    private List<Position> GetAntinodes(List<Position> positions)
    {
        var result = new List<Position>();
        if (positions.Count == 1)
            return result;

        foreach(var position1 in positions)
        {
            foreach (var position2 in positions)
            {
                if (position1.X == position2.X && position1.Y == position2.Y)
                    continue;
                // Calculate position 1's antinode in comparison to position 2
                var xDiff = position1.X - position2.X;
                var yDiff = position1.Y - position2.Y;
                var antinode = new Position(position1.X + xDiff, position1.Y + yDiff);
                if (!result.Contains(antinode))
                {
                    result.Add(antinode);
                }
            }
        }

        return result;
    }

    private Dictionary<string,List<Position>> GetAntennaPositions(string[] input)
    {
        var antennaPositionDictionary = new Dictionary<string, List<Position>>();

        var width = input[0].Length;
        var height = input.Length;

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var antenna = input[y][x].ToString();
                if (antenna == ".")
                {
                    // not an antenna, skip
                    continue;
                }

                if (!antennaPositionDictionary.ContainsKey(antenna))
                {
                    antennaPositionDictionary[antenna] = [];
                }

                var positions = antennaPositionDictionary[antenna];
                positions.Add(new Position(x, y));
            }
        }

        return antennaPositionDictionary;
    }

    public Task<long> SolvePartTwoAsync(string[] input)
    {
        throw new NotImplementedException();
    }
}
