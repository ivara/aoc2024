namespace AdventOfCode2024.Solver.day07;

public class JohanDay7Solver: IPuzzleSolution<long>
{
    [Theory]
    // [InlineData("day07/test01.txt", 3749)]
    [InlineData("day07/MyInput.txt", 1708857123053)]
    public async Task Part1(string file, long expected)
    {
        var input = await File.ReadAllLinesAsync(file);
        var johansolver = new JohanDay7Solver();
        
        var result = await johansolver.SolvePartOneAsync(input);
        Assert.Equal(expected, result);
    }
    
    public async Task<long> SolvePartOneAsync(string[] input)
    {
        var dict = new Dictionary<long, int[]>();
        foreach (var line in input)
        {
            var asdf = line.Split(": ");
            var expectedResult = long.Parse(asdf[0]);
            var terms = asdf[1].Split(" ").Select(int.Parse).ToArray();
            dict.Add(expectedResult, terms);
        }

        long sum = 0;

        foreach (var equation in dict)
        {
            sum  += SolveEquation(equation.Key, equation.Value);
        }

        return sum;
    }

    private long SolveEquation(long equationKey, int[] equationTerms)
    {
        return SolveEquationRecursive(equationKey, equationTerms, 0, 0) ||
               SolveEquationRecursive(equationKey, equationTerms, 0, 1) ? equationKey : 0;
    }
    
    private static bool SolveEquationRecursive(long equationKey, int[] equationTerms, int index, long currentResult)
    {
        if (index == equationTerms.Length)
        {
            return currentResult == equationKey;
        }

        var addResult = currentResult + equationTerms[index];
        var multiplyResult = currentResult * equationTerms[index];

        var length = equationTerms[index].ToString().Length;
        var multiplier = (long)Math.Pow(10, length);
        var concatResult = currentResult * multiplier + equationTerms[index];

        return SolveEquationRecursive(equationKey, equationTerms, index + 1, addResult) ||
               SolveEquationRecursive(equationKey, equationTerms, index + 1, multiplyResult) ||
               SolveEquationRecursive(equationKey, equationTerms, index + 1, concatResult);
    }

    public Task<long> SolvePartTwoAsync(string[] input)
    {
        throw new NotImplementedException();
    }
}