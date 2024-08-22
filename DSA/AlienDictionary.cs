using System.Text;

namespace DSA;

public class CharList : List<char>, IEquatable<CharList>
{
    public bool Equals(CharList? other)
    {
        if (other == null || (this.Count != other.Count))
        {
            return false;
        }
        
        for (var i = 0; i < this.Count; i++)
        {
            if (this[i] == other[i])
            {
                continue;
            }

            return false;
        }

        return true;
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.Append('[');
        for (var i = 0; i < this.Count; i++)
        {
            builder.Append(this[i]);
            if (i != this.Count - 1)
            {
                builder.Append(", ");
            }
        }
        builder.Append(']');
        return builder.ToString();
    }
}

public class DirectedGraph(int numberOfVertices)
{
    private readonly List<int>[] _graphData = Enumerable
        .Range(0, numberOfVertices)
        .Select((_) => new List<int>())
        .ToArray();

    public void AddEdge(int u, int v)
    {
        this._graphData[u].Add(v);
    }

    public bool CheckCycle(int numberOfVertices)
    {
        var colorArray = new int[numberOfVertices];

        return Enumerable
            .Range(0, numberOfVertices)
            .Select((i) => colorArray[i] == 0 && DepthFirstSearch(i))
            .Contains(true);
        
        bool DepthFirstSearch(int current)
        {
            colorArray[current] = 1;

            foreach (var currentNeighbor in this._graphData[current])
            {
                if (
                    colorArray[currentNeighbor] == 1 ||
                    (colorArray[currentNeighbor] == 0 && DepthFirstSearch(currentNeighbor))
                    )
                {
                    return true;
                }
            }

            colorArray[current] = 2;
            return false;
        }
    }

    public List<int> TopologicalSort(int numberOfVertices)
    { 
        var visited = new bool[numberOfVertices];
        var stack = new Stack<int>();

        _ = Enumerable.Range(0, numberOfVertices).Select((i) =>
        {
            if (visited[i])
            {
                return 0;
            }
            
            TopologicalSortRecursive(i);
            return 0;
        }).ToArray();
        return Enumerable
            .Range(0, stack.Count)
            .Select((_) => stack.Pop())
            .ToList();
        
        void TopologicalSortRecursive(int i)
        {
            visited[i] = true;
            this._graphData[i].ForEach((j) =>
            {
                if (visited[j])
                {
                    return;
                }
                TopologicalSortRecursive(j);
            });
            stack.Push(i);
        }
    }
}

public class AlienDictionary: ITest
{
    public string Name()
    {
        return "AlienDictionary";
    }

    private static CharList Implementation(List<string> list)
    {
        if (list.Count == 0)
        {
            return [];
        }
        var numberOfCharacters = 0;
        var frequency = new int[26];
        Array.Fill(frequency, 0);
        
        list.ForEach((word) =>
        {
            foreach (var charVal in word.ToList().Select((c) => c - 'a'))
            {
                frequency[charVal]++;
                if (frequency[charVal] == 1)
                {
                    numberOfCharacters++;
                }
            }
        });
        var graph = new DirectedGraph(numberOfCharacters);
        foreach (var i in Enumerable.Range(0, list.Count - 1))
        {
            var word1 = list[i];
            var word2 = list[i + 1];

            var j = 0;
            while (j < word1.Length && j < word2.Length)
            {
                if (word1[j] == word2[j])
                {
                    j++;
                    continue;
                }
                graph.AddEdge(word1[j] - 'a', word2[j] - 'a');
                break;
            }
        }
        
        if (graph.CheckCycle(numberOfCharacters))
        {
            return [];
        }

        CharList output = [];
        graph.TopologicalSort(numberOfCharacters).ForEach((i =>
        {
            output.Add((char)(i + 'a'));
        }));
        return output;
    }

    public bool RunTests()
    {
        var tests = new Testing<CharList>();
        
        tests.Assert(Implementation([]), []);
        tests.Assert(Implementation(["baa", "abcd", "abca", "cab", "cad"]), ['b', 'd', 'a', 'c']);

        return tests.Result;
    }
}
