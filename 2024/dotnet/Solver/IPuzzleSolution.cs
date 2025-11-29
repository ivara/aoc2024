namespace AdventOfCode2024.Solver;

public interface IPuzzleSolution<T>
{
    Task<T> SolvePartOneAsync(string[] input);
    Task<T> SolvePartTwoAsync(string[] input);
}