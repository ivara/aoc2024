using System.Diagnostics;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace AdventofCode2024.Day05;

/*
--- Day 5: Print Queue ---
Satisfied with their search on Ceres, the squadron of scholars suggests subsequently scanning the stationery stacks of sub-basement 17.

The North Pole printing department is busier than ever this close to Christmas, and while The Historians continue their search of this historically significant facility, an Elf operating a very familiar printer beckons you over.

The Elf must recognize you, because they waste no time explaining that the new sleigh launch safety manual updates won't print correctly. Failure to update the safety manuals would be dire indeed, so you offer your services.

Safety protocols clearly indicate that new pages for the safety manuals must be printed in a very specific order. The notation X|Y means that if both page number X and page number Y are to be produced as part of an update, page number X must be printed at some point before page number Y.

The Elf has for you both the page ordering rules and the pages to produce in each update (your puzzle input), but can't figure out whether each update has the pages in the right order.
*/
public class Day05Tests(ITestOutputHelper output)
{
    // WIP: this is not impressive :D
    private void RunTest(Func<string[], int> solver, string file, int expected)
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();

        // Read all lines from a txt file
        var lines = File.ReadAllLines(file);
        output.WriteLine($"Read file from disk to memory: {stopWatch.ElapsedMilliseconds}ms");
        
        var result = solver(lines);
        output.WriteLine($"Time: {stopWatch.ElapsedMilliseconds}ms");

        Assert.Equal(expected, result);
    }
    
    [Theory]
    [InlineData("day05/test01.txt", 18)]
    [InlineData("day05/MyInput.txt", 2514)]
    public void Part1(string file, int expected)
    {
        RunTest(SolvePart1, file, expected);
    }

    private int SolvePart1(string[] lines)
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