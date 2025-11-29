namespace AdventOfCode2024.Solver.day07;

public class IvarDay07Solver : IPuzzleSolution<long>
{
    public async Task<long> SolvePartOneAsync(string[] input)
    {
        var sum = 0L;
        var equations = GetEquations(input);
        foreach (var equation in equations)
        {
            if (IsSolvable(equation))
                sum += equation.Expected;
        }

        return sum;
    }

    public async Task<long> SolvePartTwoAsync(string[] input)
    {
        var sum = 0L;
        var equations = GetEquations(input);
        foreach (var equation in equations)
        {
            if (IsSolvablePartTwo(equation))
                sum += equation.Expected;
        }

        return sum;
    }

    private static List<Equation> GetEquations(string[] input)
    {
        var equations = new List<Equation>();
        foreach (var equation in input)
        {
            equations.Add(ParseEquationLine(equation));
        }

        return equations;
    }

    private static Equation ParseEquationLine(string equation)
    {
        var answer = long.Parse(equation.Split(":")[0]);
        var parts = equation
            .Split(":")[1]
            .Trim()
            .Split(" ")
            .Select(long.Parse)
            .ToList();
        return new Equation(answer, parts);
    }

    private record Equation(long Expected, List<long> Numbers);

    private bool IsSolvable(Equation equation)
    {
        return IsSolvableRecursive(equation.Numbers, equation.Expected, 0, equation.Numbers[0]);
    }

    private bool IsSolvablePartTwo(Equation equation)
    {
        return IsSolvablePartTwoRecursive(equation.Numbers, equation.Expected, 0, equation.Numbers[0]);
    }

    private bool IsSolvableRecursive(List<long> numbers, long target, int index, long current)
    {
        if (index == numbers.Count - 1)
        {
            return current == target;
        }

        var nextIndex = index + 1;
        return IsSolvableRecursive(numbers, target, nextIndex, current + numbers[nextIndex]) ||
               IsSolvableRecursive(numbers, target, nextIndex, current * numbers[nextIndex]);
    }

    private bool IsSolvablePartTwoRecursive(List<long> numbers, long target, int index, long current)
    {
        if (index == numbers.Count - 1)
        {
            return current == target;
        }

        var nextIndex = index + 1;
        var nextNumber = numbers[nextIndex];
        // +
        var plus = current + nextNumber;

        // *
        var multiply = current * nextNumber;

        // ||
        var c = string.Join("", current.ToString(), nextNumber.ToString());
        var concat = long.Parse(c);

        return IsSolvablePartTwoRecursive(numbers, target, nextIndex, plus) ||
               IsSolvablePartTwoRecursive(numbers, target, nextIndex, multiply) ||
               IsSolvablePartTwoRecursive(numbers, target, nextIndex, concat);
    }

    [Theory]
    [InlineData("day07/test01.txt", 3749)]
    [InlineData("day07/MyInput.txt", 2654749936343)]
    public async Task TestPartOne(string filename, long expected)
    {
        // Arrange
        var solver = new IvarDay07Solver();
        var input = await File.ReadAllLinesAsync(filename);

        // Act
        var actual = await solver.SolvePartOneAsync(input);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("day07/test01.txt", 11387)]
    [InlineData("day07/MyInput.txt", 124060392153684)]
    public async Task TestPartTwo(string filename, long expected)
    {
        // Arrange
        var solver = new IvarDay07Solver();
        var input = await File.ReadAllLinesAsync(filename);

        // Act
        var actual = await solver.SolvePartTwoAsync(input);

        // Assert
        Assert.Equal(expected, actual);
    }
}