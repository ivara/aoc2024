using System.Diagnostics;
using BenchmarkDotNet.Attributes;
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
    [InlineData("day05/test01.txt", 143)]
    // [InlineData("day05/MyInput.txt", 2514)]
    public void Part1(string file, int expected)
    {
        RunTest(SolvePart1, file, expected);
    }

    private int SolvePart1(string[] lines)
    {
        return 0;
    }
    
    [Benchmark]
    public void Part1Benchmark()
    {
        RunTest(SolvePart1, "day05/MyInput.txt", 2514);
    }
}