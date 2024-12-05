using System.Diagnostics;
using System.Text.RegularExpressions;
using Xunit.Abstractions;

namespace AdventofCode2024.Day03;

/*
 --- Day 3: Mull It Over ---
"Our computers are having issues, so I have no idea if we have any Chief Historians in stock! You're welcome to check the warehouse, though," says the mildly flustered shopkeeper at the North Pole Toboggan Rental Shop. The Historians head out to take a look.

The shopkeeper turns to you. "Any chance you can see why our computers are having issues again?"

The computer appears to be trying to run a program, but its memory (your puzzle input) is corrupted. All of the instructions have been jumbled up!

It seems like the goal of the program is just to multiply some numbers. It does that with instructions like mul(X,Y), where X and Y are each 1-3 digit numbers. For instance, mul(44,46) multiplies 44 by 46 to get a result of 2024. Similarly, mul(123,4) would multiply 123 by 4.

However, because the program's memory has been corrupted, there are also many invalid characters that should be ignored, even if they look like part of a mul instruction. Sequences like mul(4*, mul(6,9!, ?(12,34), or mul ( 2 , 4 ) do nothing.

For example, consider the following section of corrupted memory:

xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))
Only the four highlighted sections are real mul instructions. Adding up the result of each instruction produces 161 (2*4 + 5*5 + 11*8 + 8*5).

Scan the corrupted memory for uncorrupted mul instructions. What do you get if you add up all of the results of the multiplications?
*/
public class Day03Tests(ITestOutputHelper output)
{
    [Theory]
    [InlineData("day03/test01.txt", 161)]
    [InlineData("day03/MyInput.txt", 180233229)]
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
        var pattern = @"mul\((\d{1,3}),(\d{1,3})\)";
        var mul = lines
            .SelectMany(l => Regex.Matches(l, pattern)
                .Select(m => int.Parse(m.Groups[1].Value) * int.Parse(m.Groups[2].Value)))
            .Sum();
        return mul;
    }
    
    
    [Theory]
    [InlineData("day03/test02.txt", 48)]
    [InlineData("day03/MyInput.txt", 95411583)]
    public void Part2(string file, int expected)
    {
        // WRONG result for part2: 106266128
        
        // Issue is do( and don't( can span over multiple lines
        var stopWatch = new Stopwatch();
        stopWatch.Start();

        // Read all lines from a txt file
        var lines = File.ReadAllLines(file);

        var result = ParsePart2(lines);

        stopWatch.Stop();
        output.WriteLine($"Time: {stopWatch.ElapsedMilliseconds}ms");

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(new string[] { "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))" }, 48)]
    public void ParsePart2__tests(string[] lines, int expected)
    {
        Assert.Equal(expected, ParsePart2(lines));
    }

    private int ParsePart2(string[] lines)
    {
        // Join all the array strings into one big string
        var joined = string.Join("", lines);
        
        var sum = 0;
        
        var splits = joined.Split("do(");
        foreach (var split in splits)
        {
            var b = split.Split("don't(");
            sum += ParsePart1([b[0]]);
        }
    
        return sum;
    }
}