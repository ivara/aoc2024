namespace AdventOfCode2024.Solver.day09;

public class JohanDay9Solver: IPuzzleSolution<long>
{
    [Theory]
    // [InlineData("day09/test01.txt", 14)]
    [InlineData("day09/MyInput.txt", 252)]
    public async Task Part1(string file, long expected)
    {
        var input = await File.ReadAllLinesAsync(file);
        var johansolver = new JohanDay9Solver();
        
        var result = await johansolver.SolvePartOneAsync(input);
        Assert.Equal(expected, result);
    }
    
    [Theory]
    // [InlineData("day09/test01.txt", 34)]
    [InlineData("day09/MyInput.txt", 252)]
    public async Task Part2(string file, long expected)
    {
        var input = await File.ReadAllLinesAsync(file);
        var johansolver = new JohanDay9Solver();
        
        var result = await johansolver.SolvePartTwoAsync(input);
        Assert.Equal(expected, result);
    }
    
    public async Task<long> SolvePartOneAsync(string[] input)
    {
        return 0;
    }
    
    public async Task<long> SolvePartTwoAsync(string[] input)
    {
        return 0;
    }
}