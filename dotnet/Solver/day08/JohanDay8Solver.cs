namespace AdventOfCode2024.Solver.day08;

public class JohanDay8Solver: IPuzzleSolution<long>
{
    [Theory]
    [InlineData("day08/test01.txt", 14)]
    // [InlineData("day08/MyInput.txt", 1708857123053)]
    public async Task Part1(string file, long expected)
    {
        var input = await File.ReadAllLinesAsync(file);
        var johansolver = new JohanDay8Solver();
        
        var result = await johansolver.SolvePartOneAsync(input);
        Assert.Equal(expected, result);
    }
    
    public async Task<long> SolvePartOneAsync(string[] input)
    {
        var sum = 0;
        
        return sum;
    }
    

    public Task<long> SolvePartTwoAsync(string[] input)
    {
        throw new NotImplementedException();
    }
}