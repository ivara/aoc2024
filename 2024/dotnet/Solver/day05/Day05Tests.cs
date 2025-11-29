using System.Diagnostics;
using AdventOfCode2024.Solver.day05;
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

For example:

47|53
97|13
97|61
97|47
75|29
61|13
75|53
29|13
97|29
53|29
61|53
97|53
61|29
47|13
75|47
97|75
47|61
75|61
47|29
75|13
53|13

75,47,61,53,29
97,61,53,29,13
75,29,13
75,97,47,61,53
61,13,29
97,13,75,29,47
The first section specifies the page ordering rules, one per line. The first rule, 47|53, means that if an update includes both page number 47 and page number 53, then page number 47 must be printed at some point before page number 53. (47 doesn't necessarily need to be immediately before 53; other pages are allowed to be between them.)

The second section specifies the page numbers of each update. Because most safety manuals are different, the pages needed in the updates are different too. The first update, 75,47,61,53,29, means that the update consists of page numbers 75, 47, 61, 53, and 29.

To get the printers going as soon as possible, start by identifying which updates are already in the right order.

In the above example, the first update (75,47,61,53,29) is in the right order:

75 is correctly first because there are rules that put each other page after it: 75|47, 75|61, 75|53, and 75|29.
47 is correctly second because 75 must be before it (75|47) and every other page must be after it according to 47|61, 47|53, and 47|29.
61 is correctly in the middle because 75 and 47 are before it (75|61 and 47|61) and 53 and 29 are after it (61|53 and 61|29).
53 is correctly fourth because it is before page number 29 (53|29).
29 is the only page left and so is correctly last.
Because the first update does not include some page numbers, the ordering rules involving those missing page numbers are ignored.

The second and third updates are also in the correct order according to the rules. Like the first update, they also do not include every page number, and so only some of the ordering rules apply - within each update, the ordering rules that involve missing page numbers are not used.

The fourth update, 75,97,47,61,53, is not in the correct order: it would print 75 before 97, which violates the rule 97|75.

The fifth update, 61,13,29, is also not in the correct order, since it breaks the rule 29|13.

The last update, 97,13,75,29,47, is not in the correct order due to breaking several rules.

For some reason, the Elves also need to know the middle page number of each update being printed. Because you are currently only printing the correctly-ordered updates, you will need to find the middle page number of each correctly-ordered update. In the above example, the correctly-ordered updates are:

75,47,61,53,29
97,61,53,29,13
75,29,13
These have middle page numbers of 61, 53, and 29 respectively. Adding these page numbers together gives 143.

Of course, you'll need to be careful: the actual list of page ordering rules is bigger and more complicated than the above example.

Determine which updates are already in the correct order. What do you get if you add up the middle page number from those correctly-ordered updates?

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
    [InlineData("day05/test01.txt", 143)]
    // [InlineData("day05/MyInput.txt", 4135)]
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


    // THIS IS THE CORRECT ANSWER
    [Fact]
    public void EuphoriumPartB() {
        // Arrange
        var expected = 5285;
        var file = "day05/MyInput.txt";
        var solver = new Day5Solver();

        // Act
        var input = File.ReadAllLines(file);
        var result = solver.SolvePartB(input);

        // Assert
        Assert.Equal(expected, int.Parse(result));
    }

    [Theory]
    // [InlineData("day05/test01.txt", 123)]
    [InlineData("day05/MyInput.txt", 5285)]
    public void Part2(string file, int expected)
    {
        RunTest(IvarSolvePart2, file, expected);
    }


    private void Sort2(List<(int left, int right)> rules, List<int> numbers)
    {
        bool sorted;
        do
        {
            sorted = true;
            for (int i = 0; i < numbers.Count - 1; i++)
            {
                for (int j = i + 1; j < numbers.Count; j++)
                {
                    var a = numbers[i];
                    var b = numbers[j];
                    var isOptionOne = rules.Any(tuple => tuple.left == a && tuple.right == b);
                    var isOptionTwo = rules.Any(tuple => tuple.left == b && tuple.right == a);

                    if (isOptionTwo)
                    {
                        // Swap a and b
                        numbers[i] = b;
                        numbers[j] = a;
                        sorted = false;
                    }
                }
            }
        } while (!sorted);
    }

    private int[] CreateSortedRules(List<(int left, int right)> rules)
    {
        // Create a list with the distinct numbers from the rules
        // then write a custom comparer to sort the list
        // according to the rules, a "left" int must always be left of a "right" int

        var distinctNumbers = rules.SelectMany(rule => new[] {rule.left, rule.right}).Distinct().ToList();
        Sort2(rules, distinctNumbers);
        // distinctNumbers.Sort((a, b) =>
        // {
        //     var aIndex = rules.FindIndex(rule => rule.right == a);
        //     var bIndex = rules.FindIndex(rule => rule.right == b);
        //     return aIndex.CompareTo(bIndex);
        // });

        List<int> Sort(List<(int left, int right)> rules, List<int> numbers)
        {
            var copy = new List<int>(numbers);
            numbers.Sort((a,b) =>
            {
                // option 1
                // a should be left of b
                var isOptionOne = rules.Where((tuple) => tuple.left == a && tuple.right == b).Any();
                var isOptionTwo = rules.Where((tuple) => tuple.left == b && tuple.right == a).Any();
                if (isOptionOne)
                {
                    return -1;
                }
                if(isOptionTwo)
                {
                    return 1;
                }

                return 0;
                // var aIndex = rules.FindIndex(rule => rule.right == a);
                // var bIndex = rules.FindIndex(rule => rule.right == b);
                // return aIndex.CompareTo(bIndex);
            });

            // Compare the orders of elements in the copy and numbers
            // if they are the same, return numbers
            // if they are different, call Sort again
            if(!copy.SequenceEqual(numbers))
            {
                return Sort(rules, numbers);
            }

            return numbers;
        }

        return distinctNumbers.ToArray();
    }


    private int IvarSolvePart2(string[] lines)
    {
        // Phase 1: read the rules and sort its numbers into correct solution order
        // using a custom comparer
        var rules = GetRules(lines);
        var uniqueNumbers = rules
            .SelectMany(rule => new[] {rule.left, rule.right})
            .Distinct()
            .ToHashSet();
        var r = rules.Select((item) => new Tuple<int,int>(item.left, item.right)).ToHashSet();

        var sortedRules = TopologicalSort.Sort(uniqueNumbers, r).ToArray();

        // var sortedRules = CreateSortedRules(rules);

        // Phase 2: get all invalid pages and reorder their ints according to the solution order
        var pages = GetPages(lines);
        var (validPages, invalidPages) = ValidatePages(pages, rules);

        var sum = 0;
        foreach (var invalidPage in invalidPages)
        {
            // Take the sortedRules and remove all numbers not in the invalidPage
            // that should give us an array that is the invalidPage but in correct order
            int[] d = sortedRules
                .Where(i => invalidPage.Contains(i))
                .ToArray();

            // var fixedPage = invalidPage.OrderBy(i => Array.IndexOf(sortedRules, i)).ToArray();
            sum += d[d.Length / 2];
        }
        return sum;
    }

    private List<int[]> GetPages(string[] lines)
    {
        var emptyLine = Array.IndexOf(lines, string.Empty);
        var pages = lines
            .Skip(emptyLine + 1)
            .Select(page => page.Split(',').Select(int.Parse).ToArray())
            .ToList();
        return pages;
    }

    private List<(int left, int right)> GetRules(string[] lines)
    {
        var emptyLine = Array.IndexOf(lines, string.Empty);
        var rules = lines
            .Take(emptyLine)
            .Select(rule =>
            {
                var sides = rule.Split('|');
                return (left: int.Parse(sides[0]), right: int.Parse(sides[1]));
            }).ToList();

        return rules;
    }

    private (List<int[]> validPages, List<int[]> invalidPages) ValidatePages(List<int[]> pages, List<(int left, int right)> rules)
    {
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

        return (validPages, invalidPages);
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
