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
        
    }
    
    [Theory]
    [InlineData("day06/test01.txt", 143)]
    // [InlineData("day05/MyInput.txt", 4135)]
    public void SolvePart2(string file, int expected)
    {
        
    }
}
