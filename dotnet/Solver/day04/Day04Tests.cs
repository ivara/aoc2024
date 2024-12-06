using System.Diagnostics;
using BenchmarkDotNet.Attributes;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace AdventofCode2024.Day04;

/*

*/
public class Day04Tests(ITestOutputHelper output)
{

    [Benchmark]
    public void Benchmark()
    {
        Part1("day04/MyInput.txt", 2514);
    }
    
    [Theory]
    [InlineData("day04/test01.txt", 18)]
    [InlineData("day04/MyInput.txt", 2514)]
    public void Part1(string file, int expected)
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();

        // Read all lines from a txt file
        var lines = File.ReadAllLines(file);

        var result = ParsePart1(lines);

        stopWatch.Stop();
        output.WriteLine($"Time: {stopWatch.ElapsedMilliseconds}ms");

        Assert.Equal(expected, result);
    }

    private int ParsePart1(string[] lines)
    {
        var sum = 0;
        for (var y = 0; y < lines.Length; y++)
        {
            for (var x = 0; x < lines[y].Length; x++)
            {
                
                if (lines[y][x] == 'X')
                {
                    var canGoRight = x + 3 < lines[y].Length;
                    var canGoUp = y > 2;
                    var canGoDown = y + 3 < lines.Length;
                    var canGoLeft = x > 2;
                    
                    // Can we go to the right side?
                    if (canGoRight)
                    {
                        if (lines[y][x + 1] == 'M')
                        {
                            if (lines[y][x + 2] == 'A')
                            {
                                if (lines[y][x + 3] == 'S')
                                {
                                    sum++;
                                } 
                            } 
                        }
                    }
                    
                    if (canGoRight && canGoDown)
                    {
                        if (lines[y + 1][x + 1] == 'M')
                        {
                            if (lines[y + 2][x + 2] == 'A')
                            {
                                if (lines[y + 3][x + 3] == 'S')
                                {
                                    sum++;
                                } 
                            } 
                        }
                    }
                    
                    if (canGoDown)
                    {
                        if (lines[y + 1][x] == 'M')
                        {
                            if (lines[y + 2][x] == 'A')
                            {
                                if (lines[y + 3][x] == 'S')
                                {
                                    sum++;
                                } 
                            } 
                        }
                    }

                    if (canGoDown && canGoLeft)
                    {
                        if (lines[y + 1][x - 1] == 'M')
                        {
                            if (lines[y + 2][x - 2] == 'A')
                            {
                                if (lines[y + 3][x - 3] == 'S')
                                {
                                    sum++;
                                } 
                            } 
                        }
                    }

                    if (canGoLeft)
                    {
                        if (lines[y][x - 1] == 'M')
                        {
                            if (lines[y][x - 2] == 'A')
                            {
                                if (lines[y][x - 3] == 'S')
                                {
                                    sum++;
                                } 
                            } 
                        }
                    }

                    if (canGoLeft && canGoUp)
                    {
                        if (lines[y - 1][x - 1] == 'M')
                        {
                            if (lines[y - 2][x - 2] == 'A')
                            {
                                if (lines[y - 3][x - 3] == 'S')
                                {
                                    sum++;
                                } 
                            } 
                        }
                    }

                    if (canGoUp)
                    {
                        if (lines[y - 1][x] == 'M')
                        {
                            if (lines[y - 2][x] == 'A')
                            {
                                if (lines[y - 3][x] == 'S')
                                {
                                    sum++;
                                } 
                            } 
                        }
                    }
                    
                    if (canGoRight && canGoUp)
                    {
                        if (lines[y - 1][x + 1] == 'M')
                        {
                            if (lines[y - 2][x + 2] == 'A')
                            {
                                if (lines[y - 3][x + 3] == 'S')
                                {
                                    sum++;
                                } 
                            } 
                        }
                    }
                }
            }
        }

        return sum;
    }

    [Theory]
    // [InlineData("day04/test02.txt", 9)]
    [InlineData("day04/MyInput.txt", 1888)]
    public void Part2(string file, int expected)
    {
        // Read all lines from a txt file
        var lines = File.ReadAllLines(file);
        var startTime = Stopwatch.GetTimestamp();
       
        var result = ParsePart2(lines);

        output.WriteLine($"Time: {Stopwatch.GetElapsedTime(startTime).TotalMilliseconds} ms");

        Assert.Equal(expected, result);
    }

    private int ParsePart2(string[] lines)
    {
        var sum = 0;

        for (var y = 1; y < lines.Length - 1; y++)
        {
            for (var x = 1; x < lines[y].Length - 1; x++)
            {
                if (lines[y][x] != 'A')
                    continue;

                var firstString = $"{lines[y - 1][x - 1]}A{lines[y + 1][x + 1]}";
                var secondString = $"{lines[y - 1][x + 1]}A{lines[y + 1][x - 1]}";
                
                if (IsMas(firstString) && IsMas(secondString))
                {
                    sum++;
                }
            }
        }
        

        return sum;
    }

    private static bool IsMas(string str)
    {
        return str is "MAS" or "SAM";
    }
}