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
        // Read all lines from a txt file
        var lines = File.ReadAllLines(file);

        var stopWatch = Stopwatch.StartNew();
        // output.WriteLine($"Read file from disk to memory: {stopWatch.ElapsedMilliseconds}ms");

        var result = solver(lines);
        output.WriteLine($"Time: {stopWatch.ElapsedMilliseconds}ms");

        Assert.Equal(expected, result);
    }

    [Theory]
    // [InlineData("day05/test01.txt", 143)]
    [InlineData("day05/MyInput.txt", 5087)]
    public void Part1(string file, int expected)
    {
        RunTest(IvarSolvePart1, file, expected);
        RunTest(SolvePart1, file, expected);
    }
    
    // --- Part Two ---
    // While the Elves get to work printing the correctly-ordered updates, you have a little time to fix the rest of them.
    //
    //     For each of the incorrectly-ordered updates, use the page ordering rules to put the page numbers in the right order. For the above example, here are the three incorrectly-ordered updates and their correct orderings:
    //
    // 75,97,47,61,53 becomes 97,75,47,61,53.
    // 61,13,29 becomes 61,29,13.
    // 97,13,75,29,47 becomes 97,75,47,29,13.
    // After taking only the incorrectly-ordered updates and ordering them correctly, their middle page numbers are 47, 29, and 47. Adding these together produces 123.
    //
    // Find the updates which are not in the correct order. What do you get if you add up the middle page numbers after correctly ordering just those updates?
    
    [Theory]
    // [InlineData("day05/test01.txt", 143)]
    [InlineData("day05/test01.txt", 123)]
    public void Part2(string file, int expected)
    {
        RunTest(SolvePart2, file, expected);
    }

    private int IvarSolvePart1(string[] lines)
    {
        // split lines into rules and pages
        // by finding an empty line
        var emptyLine = Array.IndexOf(lines, string.Empty);
        var rules = lines
            .Take(emptyLine)
            .Select(rule =>
            {
                var sides = rule.Split('|');
                return (left: int.Parse(sides[0]), right: int.Parse(sides[1]));
            }).ToList();
        
        var pages = lines
            .Skip(emptyLine + 1)
            .Select(page => page.Split(',').Select(int.Parse).ToArray())
            .ToList();
        

        // Find valid pages
        var validPages = new List<int[]>();
        var invalidPages = new List<int[]>();
        foreach (var page in pages)
        {
            var isValidPage = true;
            
            // test all rules against the page and make sure they are all valid
            for (var r = 0; r < rules.Count && isValidPage; r++)
            {
                var (left, right) = rules[r];
               
                var leftIndex = Array.IndexOf(page, left);
                var rightIndex = Array.IndexOf(page, right);
                if (leftIndex == -1 || rightIndex == -1)
                {
                    continue;
                }

                if (leftIndex > rightIndex)
                {
                    isValidPage = false;
                    invalidPages.Add(page);
                }
            }

            // If isValidPage is still true, we add the page to validPages
            if (isValidPage)
            {
                validPages.Add(page);
            }
        }
        
        // get mid value from valid pages
        var sum = validPages.Sum(page => page[page.Length / 2]);

        return sum;
    }
    
    private int SolvePart1(string[] lines)
    {
        // Find the empty line
        var emptyLine = Array.IndexOf(lines, string.Empty);
        var rules = lines.Take(emptyLine);
        var pages = lines.Skip(emptyLine + 1);

        // Create a dictionary with the rules
        var rightDict = new Dictionary<int, List<int>>();
        var leftDict = new Dictionary<int, List<int>>();
        foreach (var rule in rules)
        {
            var parts = rule.Split('|');
            var left = int.Parse(parts[0]);
            var right = int.Parse(parts[1]);
            if (!rightDict.TryGetValue(left, out var rValue))
            {
                rValue = ([]);
                rightDict[left] = rValue;
            }

            rValue.Add(right);

            if (!leftDict.TryGetValue(right, out var lValue))
            {
                lValue = ([]);
                leftDict[right] = lValue;
            }

            lValue.Add(left);
        }

        var okPages = new List<string>();
        var notOkPages = new List<string>();

        foreach (var page in pages)
        {
            var pageOk = true;

            var parts = page.Split(',');
            for (var i = 0; i < parts.Length; i++)
            {
                var key = int.Parse(parts[i]);
                // assign lefty and righty  variables the dictionary entry if exists, otherwise assign an empty list
                var lefty = leftDict.TryGetValue(key, value: out var value) ? value : [];
                var righty = rightDict.TryGetValue(key, out var value1) ? value1 : [];
                // check left and right side of current entry against the dictionaries
                var leftSide = parts.Take(i);
                var rightSide = parts.Skip(i).Take(parts.Length - i);
                var leftNotOk = lefty.Any(left => rightSide.Contains(left.ToString()));
                var rightNotOk = righty.Any(right => leftSide.Contains(right.ToString()));
                if (leftNotOk || rightNotOk)
                {
                    pageOk = false;
                    break;
                }
            }

            if (pageOk)
            {
                okPages.Add(page);
            }
            else
            {
                notOkPages.Add(page);
            }
        }

        var sum = 0;

        foreach (var okPage in okPages)
        {
            var okParts = okPage.Split(',');
            // Get the middle entry in the string
            var middle = okParts[okParts.Length / 2];
            sum += int.Parse(middle);
        }

        return sum;
    }
    
    private int SolvePart2(string[] lines)
    {
        // Find the empty line
        var emptyLine = Array.IndexOf(lines, string.Empty);
        var rules = lines.Take(emptyLine);
        var pages = lines.Skip(emptyLine + 1);
        
        // Create a dictionary with the rules
        var rightDict = new Dictionary<int, List<int>>();
        var leftDict = new Dictionary<int, List<int>>();
        foreach (var rule in rules)
        {
            var parts = rule.Split('|');
            var left = int.Parse(parts[0]);
            var right = int.Parse(parts[1]);
            if (!rightDict.TryGetValue(left, out var rValue))
            {
                rValue = ([]);
                rightDict[left] = rValue;
            }

            rValue.Add(right);

            if (!leftDict.TryGetValue(right, out var lValue))
            {
                lValue = ([]);
                leftDict[right] = lValue;
            }

            lValue.Add(left);
        }
        
        // Go through  every page in pages and create one int array of all lines in pages
        var allPages = pages.SelectMany(page => page.Split(',').Select(int.Parse)).ToArray().Distinct();
        // Select all keys from leftDict to int array
        var leftKeys = leftDict.Keys.ToArray();
        // Compare if any of the keys in leftKeys are not in allPages
        var invalidPages = leftKeys.Where(key => !allPages.Contains(key)).ToArray();
        
        // 
        
        // Create hashmap with the rule numbers
        // Remove from hashmap the numbers that never exists in the input
        // Create a sorted array of the remaining numbers in hashmap according to sorting rules - Answer
        // For each invalid page, remove all numbers from Answer that are not in the invalid page. Remaining numbers in Answer is our fixed page
        return 0;
    }
}