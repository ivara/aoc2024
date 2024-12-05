namespace AdventofCode2024.Day02;

public class Day02Tests
{
    [Theory]
    [InlineData("day02/test01.txt", 2)]
    [InlineData("day02/MyInput.txt", 421)]
    public void Part1(string file, int expected)
    {
        // Read all lines from a txt file
        var lines = File.ReadAllLines(file);

        var result = ParsePart1(lines);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("day02/test01.txt", 4)]
    [InlineData("day02/MyInput.txt", 476)]
    public void Part2(string file, int expected)
    {
        // Read all lines from a txt file
        var lines = File.ReadAllLines(file);

        var safeReports = 0;

        // Take each array entry in lines and split by space
        // 
        for (var i = 0; i < lines.Length; i++)
        {
            var report = lines[i]
                .Split(' ')
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(int.Parse)
                .ToArray();

            if (Part2IsSafeReport(report))
                safeReports++;
        }

        Assert.Equal(expected, safeReports);
    }

    private int ParsePart1(string[] lines)
    {
        var safeReports = 0;

        // Take each array entry in lines and split by space
        // 
        for (var i = 0; i < lines.Length; i++)
        {
            var report = lines[i]
                .Split(' ')
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(int.Parse)
                .ToArray();

            if (Part1IsSafeReport(report))
                safeReports++;
        }

        return safeReports;
    }


    [Theory]
    [InlineData(new int[] { 7, 6, 4, 2, 1 }, true)]
    [InlineData(new int[] { 1, 2, 7, 8, 9 }, false)]
    public void Part1__TestIsSafeReport(int[] report, bool expected)
    {
        var result = Part1IsSafeReport(report);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(new int[] { 7, 6, 4, 2, 1 }, true)]
    [InlineData(new int[] { 1, 3, 2, 4, 5 }, true)]
    [InlineData(new int[] { 8, 6, 4, 4, 1 }, true)]
    [InlineData(new int[] { 1, 2, 7, 8, 9 }, false)]
    [InlineData(new int[] { 1, 9, 2, 3 }, true)]
    [InlineData(new int[] { 1, 9, 9, 10 }, false)]
    [InlineData(new int[] { 1, 9, 9, 10, 16 }, false)] // more than one error
    [InlineData(new int[] { 1, 5, 4, 3  }, true)] // first number needs to be removed
    public void Part2__TestIsSafeReport(int[] report, bool expected)
    {
        var result = Part2IsSafeReport(report);
        Assert.Equal(expected, result);
    }
    
    private bool Part2IsSafeReport(int[] report)
    {
        var isSafe = Part1IsSafeReport(report);
        if (isSafe)
        {
            return true;
        }

        for (int i = 0; i < report.Length; i++)
        {
            // Create new array with one entry removed
            var subReport = report.Where((source, index) => index != i).ToArray();
            var isSubReportSafe = Part1IsSafeReport(subReport);
            if (isSubReportSafe)
            {
                return true;
            }
        }
        
        // If isIncreasing is true, figure out the first number that breaks this rule
        // and remove it from the report int[] array
        
        
        return false;
    }
    private bool Part1IsSafeReport(int[] report)
    {
        // Validate the Report (int array), each number is called a level
        // It is considered safe if
        // - The levels are either all increasing or all decreasing.
        // - Any two adjacent levels differ by at least one and at most three.
        var isIncreasing = false;


        for (int i = 0; i < report.Length - 1; i++)
        {
            // If this is the first iteration
            // we need to determine if the report is increasing or decreasing
            if (i == 0)
            {
                // determine if we are doing an icreasing or decreasing report
                isIncreasing = report[i] < report[i + 1];
            }

            var isSafeRule1 = isIncreasing ? report[i] < report[i + 1] : report[i] > report[i + 1];
            if (!isSafeRule1)
            {
                return false;
            }

            var tmp = Math.Abs(report[i] - report[i + 1]);
            var isSafeRule2 = tmp is >= 1 and <= 3;

            if (!isSafeRule2)
                return false;
        }

        return true;
    }


}