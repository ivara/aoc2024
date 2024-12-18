namespace AdventOfCode2024.Solver.day08;

public class JohanDay8Solver: IPuzzleSolution<long>
{
    [Theory]
    // [InlineData("day08/test01.txt", 14)]
    [InlineData("day08/MyInput.txt", 252)]
    public async Task Part1(string file, long expected)
    {
        var input = await File.ReadAllLinesAsync(file);
        var johansolver = new JohanDay8Solver();
        
        var result = await johansolver.SolvePartOneAsync(input);
        Assert.Equal(expected, result);
    }
    
    [Theory]
    // [InlineData("day08/test01.txt", 34)]
    [InlineData("day08/MyInput.txt", 252)]
    public async Task Part2(string file, long expected)
    {
        var input = await File.ReadAllLinesAsync(file);
        var johansolver = new JohanDay8Solver();
        
        var result = await johansolver.SolvePartTwoAsync(input);
        Assert.Equal(expected, result);
    }
    
    public async Task<long> SolvePartOneAsync(string[] input)
    {
        var sum = 0;
        
        var xLength = input[0].Length;
        var yLength = input.Length;
        
        var map = new string[xLength, yLength];
        
        // Create dictionary of character and a list of its coordinates
        var coordDict = new Dictionary<string, List<(int, int)>>();
        
        for (var y = 0; y < yLength; y++)
        {
            for (var x = 0; x < xLength; x++)
            {
                var coord = input[y][x].ToString();
                map[x, y] = coord;
                if (coord == ".") continue;
                // Add or update in dictionary
                if (coordDict.TryGetValue(coord, out var value))
                {
                    value.Add((x, y));
                }
                else
                {
                    coordDict.Add(coord, [(x, y)]);
                }
            }
        }
        
        var antinodes = new List<(int, int)>();

        foreach (var antennaDictionary in coordDict)
        {
            // Get the coordinates of the current character
            var antennaCoords = antennaDictionary.Value;
            // Find the distance to all other of same character and check if same space between and same x,y diff is still on the map. if os add to a list of antinodes
            foreach (var (x, y) in antennaCoords)
            {
                foreach (var (x2, y2) in antennaCoords)
                {
                    if (x == x2 && y == y2) continue;
                    var xDiff = x2 - x;
                    var yDiff = y2 - y;
                    
                    var xAntiNode = x2 + xDiff;
                    var yAntiNode = y2 + yDiff;
                    if(InsideMap(xAntiNode, yAntiNode, xLength, yLength))
                    {
                        antinodes.Add((xAntiNode, yAntiNode));
                    }
                }
            }
            
        }


        return antinodes.Distinct().Count();
    }
    
    private bool InsideMap(int x, int y, int xLength, int yLength)
    {
        return x >= 0 && x < xLength && y >= 0 && y < yLength;
    }

    private void CheckAntinodesRecursively(int x, int y, int xDiff, int yDiff, int xLength, int yLength, List<(int, int)> antinodes)
    {
        var xAntiNode = x + xDiff;
        var yAntiNode = y + yDiff;

        if (InsideMap(xAntiNode, yAntiNode, xLength, yLength))
        {
            antinodes.Add((xAntiNode, yAntiNode));
            CheckAntinodesRecursively(xAntiNode, yAntiNode, xDiff, yDiff, xLength, yLength, antinodes);
        }
    }

    public async Task<long> SolvePartTwoAsync(string[] input)
    {
        var xLength = input[0].Length;
        var yLength = input.Length;

        var map = new string[xLength, yLength];

        var coordDict = new Dictionary<string, List<(int, int)>>();

        for (var y = 0; y < yLength; y++)
        {
            for (var x = 0; x < xLength; x++)
            {
                var coord = input[y][x].ToString();
                map[x, y] = coord;
                if (coord == ".") continue;
                if (coordDict.TryGetValue(coord, out var value))
                {
                    value.Add((x, y));
                }
                else
                {
                    coordDict.Add(coord, [(x, y)]);
                }
            }
        }

        var antinodes = new List<(int, int)>();
        var keysWithMoreThanOneValue = coordDict.Where(x => x.Value.Count > 1).ToList();
        // Add all keys with more than one value to antinodes, all coordinates where each key exists
        foreach (var (key, value) in keysWithMoreThanOneValue)
        {
            antinodes.AddRange(value);
        }

        foreach (var antennaDictionary in coordDict)
        {
            var antennaCoords = antennaDictionary.Value;
            foreach (var (x, y) in antennaCoords)
            {
                foreach (var (x2, y2) in antennaCoords)
                {
                    if (x == x2 && y == y2) continue;
                    var xDiff = x2 - x;
                    var yDiff = y2 - y;

                    CheckAntinodesRecursively(x2, y2, xDiff, yDiff, xLength, yLength, antinodes);
                }
            }
        }

        return antinodes.Distinct().Count();
    }
}