namespace AdventofCode2024.Day05;

public class Topological
{

    [Theory]
    // [InlineData("day05/test01.txt", 123)]
    [InlineData("day05/MyInput.txt", 4140)]
    public void Part2(string filename, int expected)
    {
        var lines = File.ReadAllLines(filename);
        var result = SolvePart2(lines);
        Assert.Equal(expected, result);
    }

    private int SolvePart2(string[] lines)
    {
        var (rules, updates) = ParseInput(lines);
        var graph = BuildGraph(rules);
        var sortedOrder = TopologicalSort(graph);

        var (validUpdates, invalidUpdates) = ValidatePages(updates, rules);
        var reorderedInvalidUpdates = ReorderInvalidUpdates(invalidUpdates, sortedOrder);

        return CalculateMiddlePageSum(reorderedInvalidUpdates);
    }

    private (List<(int left, int right)> rules, List<int[]> updates) ParseInput(string[] lines)
    {
        var emptyLine = Array.IndexOf(lines, string.Empty);
        var rules = lines.Take(emptyLine)
            .Select(rule =>
            {
                var parts = rule.Split('|');
                return (left: int.Parse(parts[0]), right: int.Parse(parts[1]));
            }).ToList();

        var updates = lines.Skip(emptyLine + 1)
            .Select(update => update.Split(',').Select(int.Parse).ToArray())
            .ToList();

        return (rules, updates);
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
    private int CalculateMiddlePageSum(List<int[]> updates)
    {
        return updates.Sum(update => update[update.Length / 2]);
    }
    private List<int[]> ReorderInvalidUpdates(List<int[]> updates, List<int> sortedOrder)
    {
        var sortedSet = new HashSet<int>(sortedOrder);
        var reorderedUpdates = new List<int[]>();

        foreach (var update in updates)
        {
            var reorderedUpdate = sortedOrder.Where(sortedSet.Contains).Where(update.Contains).ToArray();
            reorderedUpdates.Add(reorderedUpdate);
        }

        return reorderedUpdates;
    }
    private Dictionary<int, List<int>> BuildGraph(List<(int left, int right)> rules)
    {
        var graph = new Dictionary<int, List<int>>();
        foreach (var (left, right) in rules)
        {
            if (!graph.ContainsKey(left))
                graph[left] = new List<int>();
            graph[left].Add(right);
        }
        return graph;
    }

    private List<int> TopologicalSort(Dictionary<int, List<int>> graph)
    {
        var inDegree = new Dictionary<int, int>();
        foreach (var node in graph.Keys)
        {
            if (!inDegree.ContainsKey(node))
                inDegree[node] = 0;
            foreach (var neighbor in graph[node])
            {
                if (!inDegree.ContainsKey(neighbor))
                    inDegree[neighbor] = 0;
                inDegree[neighbor]++;
            }
        }

        var queue = new Queue<int>(inDegree.Where(kv => kv.Value == 0).Select(kv => kv.Key));
        var sortedList = new List<int>();

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();
            sortedList.Add(node);

            if (!graph.ContainsKey(node)) continue;
            foreach (var neighbor in graph[node])
            {
                inDegree[neighbor]--;
                if (inDegree[neighbor] == 0)
                    queue.Enqueue(neighbor);
            }
        }

        return sortedList;
    }
}
