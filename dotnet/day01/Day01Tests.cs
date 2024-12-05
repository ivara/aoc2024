namespace AdventofCode2024.Day01;

public class Day01Tests
{
    [Theory]
    [InlineData("day01/test01.txt", 11)]
    [InlineData("day01/MyInput.txt", 1506483)]
    public void SolvePartOne(string file, int expected)
    {
        // Read all lines from a txt file
        var lines = File.ReadAllLines(file);

        var result = ParseInput(lines);
        
        Assert.Equal(expected, result);
    }
    
    private int ParseInput(string[] lines)
    {
        // Take each array entry in lines and split it by space character
        // then create two dictionaries, one for the first number and one for the second number
        // and include the array index
        var dict1 = new Dictionary<int, int>();
        var dict2 = new Dictionary<int, int>();
        
        for (var i = 0; i < lines.Length; i++)
        {
            var parts = lines[i].Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            dict1.Add(i, int.Parse(parts[0]));
            dict2.Add(i, int.Parse(parts[1]));
        }
        
        // sort both dictionaries by value
        var sortedDict1 = dict1.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        var sortedDict2 = dict2.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

        var sum = 0;
        for(int i = 0; i < sortedDict1.Count; i++)
        {
            var value1 = sortedDict1.ElementAt(i).Value;
            var value2 = sortedDict2.ElementAt(i).Value;

            sum += Math.Abs(value1 - value2);
        }

        return sum;
    }

    
    private int ParseInput2(string[] lines)
    {
        // Take each array entry in lines and split it by space character
        // then create two dictionaries, one for the first number and one for the second number
        // and include the array index
        
        // Declare a new array of integers with size of lines.length
        
        var list1 = new List<int>();
        var list2 = new List<int>();
        
        for (var i = 0; i < lines.Length; i++)
        {
            var parts = lines[i].Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            
            list1.Add(int.Parse(parts[0]));
            list2.Add(int.Parse(parts[1]));
        }
        
        var group = list2.GroupBy(x => x).Select(x => new { Key = x.Key, Count = x.Count() }).ToList();
        
        var sum = 0;
        foreach (var t in list1)
        {
            var multiplier = group
                .Where(x => x.Key == t)
                .Select(x => x.Count)
                .FirstOrDefault();
            
            var value = t * multiplier;

            sum += value;
        }

        return sum;
    }
    
    
    [Theory]
    [InlineData("day01/test01.txt", 31)]
    [InlineData("day01/MyInput.txt", 23126924)]
    public void Part2(string file, int expected)
    {
        // Read all lines from a txt file
        var lines = File.ReadAllLines(file);

        var result = ParseInput2(lines);
        
        Assert.Equal(expected, result);
    }
    
 
   
}