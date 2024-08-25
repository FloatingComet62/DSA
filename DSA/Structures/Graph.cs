namespace DSA.Structures;

public class Graph
{
    public readonly float[][] AdjacencyMatrix;
    public readonly int NumberOfNodes;

    public Graph(int numberOfNodes)
    {
        AdjacencyMatrix = Enumerable
            .Range(0, numberOfNodes)
            .Select(_ => Enumerable
                .Range(0, numberOfNodes)
                .Select(_ => 0.0f)
                .ToArray()
            )
            .ToArray();
        NumberOfNodes = numberOfNodes;
    }

    private Graph(float[][] adjacencyMatrix)
    {
        this.AdjacencyMatrix = adjacencyMatrix;
        NumberOfNodes = adjacencyMatrix[0].Length;
    }

    public void AddEdge(int from, int to, float weight = 1)
    {
        AdjacencyMatrix[from][to] = weight;
    }
    
    public void AddBidirectionalEdge(int node1, int node2, float weight = 1)
    {
        AdjacencyMatrix[node1][node2] = weight;
        AdjacencyMatrix[node2][node1] = weight;
    }
    
    public int GetDegree(int node)
    {
        return AdjacencyMatrix[node].Select(i => i > 0 ? 1 : 0).Sum();
    }

    private Graph GetTranspose()
    {
        return new Graph(Enumerable
            .Range(0, NumberOfNodes)
            .Select(from => Enumerable
                .Range(0, NumberOfNodes)
                .Select(to => AdjacencyMatrix[to][from])
                .ToArray()
            )
            .ToArray());
    }

    public Graph GetBidirectional()
    {
        var transposedMatrix = GetTranspose().AdjacencyMatrix;
        var newMatrix = AdjacencyMatrix.Select((normalWeights, i) =>
            normalWeights.Select((normalWeight, j) => normalWeight != 0.0f ? normalWeight : transposedMatrix[i][j])
                .ToArray()
            ).ToArray();
        return new Graph(newMatrix);
    }
    
    static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
    {
        if (length == 1) return list.Select(t => new T[] { t });

        return GetPermutations(list, length - 1)
            .SelectMany(t => list.Where(e => !t.Contains(e)),
                (t1, t2) => t1.Concat(new T[] { t2 }));
    }

    public IEnumerable<int[]> GetEdges()
    {
        return GetPermutations(Enumerable.Range(0, NumberOfNodes), 2)
            .Select(x => x.ToArray())
            .Where(combinations =>
            {
                var from = combinations[0];
                var to = combinations[1];
                return AdjacencyMatrix[from][to] > 0;
            });
    }
}

public class GraphColoring(Graph graph)
{
    private readonly bool[][] _matrix = graph.GetBidirectional().AdjacencyMatrix
            .Select(weights =>
                weights.Select(weight => weight > 0)
                    .ToArray()
            )
            .ToArray();

    public bool CanColor(int numberOfColors)
    {
        var colors = Enumerable.Range(0, graph.NumberOfNodes).Select(_ => 0).ToArray();
        return SolveColoring(0);

        bool SolveColoring(int v)
        {
            if (v == graph.NumberOfNodes)
            {
                return true;
            }

            foreach (
                var color in Enumerable
                     .Range(1, numberOfColors+1)
                     .Where(color => !Enumerable
                         .Range(0, graph.NumberOfNodes)
                         .Select(i => _matrix[v][i] && color == colors[i])
                         .Contains(true)
                     )
                )
            {
                colors[v] = color;
                if (SolveColoring(v + 1))
                {
                    return true;
                }

                colors[v] = 0;
            }

            return false;
        }
    }

    public int[] GreedyColoring()
    {
        var result = Enumerable.Range(0, graph.NumberOfNodes).Select(i => i == 0 ? 0 : -1).ToArray();
        foreach (var node in Enumerable.Range(1, graph.NumberOfNodes - 1))
        {
            var available = Enumerable.Range(0, graph.NumberOfNodes).Select(_ => true).ToArray();
            foreach (var neighborNode in _matrix[node]
                 .Select((n, i) => (i, n))
                 .Where(data => data.n)
                 .Select(data => data.i)
                 .Where(i => result[i] != -1))
            {
                available[result[neighborNode]] = false;
            }

            var color = Enumerable.Range(0, graph.NumberOfNodes).First(color => available[color]);
            result[node] = color;
        }

        return result;
    }
}

public class TravellingSalesMan(Graph graph)
{
    private readonly float[][] _matrix = graph.GetBidirectional().AdjacencyMatrix;

    public float Solve(int startingNode)
    {
        var visited = Enumerable
            .Range(0, graph.NumberOfNodes)
            .Select((_, i) => i == startingNode)
            .ToArray();
        
        var result = float.PositiveInfinity;
        SolveRecursive(startingNode, 1, 0.0f);
        return result;

        void SolveRecursive(int currentPosition, int count, float cost)
        {
            if (count == graph.NumberOfNodes && _matrix[currentPosition][startingNode] > 0)
            {
                result = float.Min(result, cost + _matrix[currentPosition][startingNode]);
                return;
            }

            foreach (var i in Enumerable
                 .Range(0, graph.NumberOfNodes)
                 .Where(i => !visited[i] && _matrix[currentPosition][i] > 0)
             )
            {
                visited[i] = true;
                SolveRecursive(i, count + 1, cost + _matrix[currentPosition][i]);
                visited[i] = false;
            }
        }
    }
}

public class ShortestPath(Graph graph)
{
    public float[] Dijkstra(int startingNode)
    {
        var distancesFromStartingNode = Enumerable
            .Range(0, graph.NumberOfNodes)
            .Select((_, i) => i == startingNode ? 0 : float.PositiveInfinity)
            .ToArray();
        var pickedSet = Enumerable.Range(0, graph.NumberOfNodes).Select(_ => false).ToArray();

        foreach (var _ in Enumerable.Range(0, graph.NumberOfNodes - 1))
        {
            var from = MinimumDistance();
            pickedSet[from] = true;
            foreach (var to in Enumerable
                .Range(0, graph.NumberOfNodes)
                .Where(to =>
                    !pickedSet[to]
                    && graph.AdjacencyMatrix[from][to] != 0
                )
            )
            {
                distancesFromStartingNode[to] = float.Min(
                    distancesFromStartingNode[to],
                    distancesFromStartingNode[from] + graph.AdjacencyMatrix[from][to]
                );
            }
        }

        return distancesFromStartingNode;

        int MinimumDistance()
        {
            var min = float.PositiveInfinity;
            var minIndex = -1;
            foreach (var vertex in Enumerable
                 .Range(0, graph.NumberOfNodes)
                 .Where(vertex => !pickedSet[vertex] && distancesFromStartingNode[vertex] <= min)
             )
            {
                min = distancesFromStartingNode[vertex];
                minIndex = vertex;
            }

            return minIndex;
        }
    }

    public float[] BellmanFord(int startingNode)
    {
        var distancesFromStartingNode = Enumerable
            .Range(0, graph.NumberOfNodes)
            .Select((_, i) => i == startingNode ? 0 : float.PositiveInfinity)
            .ToArray();

        var edges = graph.GetEdges().ToArray();
        foreach (var _ in Enumerable.Range(1, graph.NumberOfNodes - 1))
        {
            foreach (var edge in edges)
            {
                var from = edge[0];
                var to = edge[1];
                var weight = graph.AdjacencyMatrix[from][to];
                
                if (float.IsInfinity(distancesFromStartingNode[from]))
                {
                    continue;
                }
                
                distancesFromStartingNode[to] = float.Min(
                    distancesFromStartingNode[to],
                    distancesFromStartingNode[from] + weight
                );
            }
        }
        
        return distancesFromStartingNode;
    }

    public float[][] FloydWarshall()
    {
        var distances = Enumerable
            .Range(0, graph.NumberOfNodes)
            .Select(i => Enumerable
                .Range(0, graph.NumberOfNodes)
                .Select(j =>
                {
                    if (i == j)
                    {
                        return 0;
                    }
                    return graph.AdjacencyMatrix[i][j] == 0 ?
                        float.PositiveInfinity :
                        graph.AdjacencyMatrix[i][j];
                })
                .ToArray())
            .ToArray();
        foreach (var k in Enumerable.Range(0, graph.NumberOfNodes))
        {
            foreach (var i in Enumerable.Range(0, graph.NumberOfNodes))
            {
                foreach (var j in Enumerable.Range(0, graph.NumberOfNodes))
                {
                    distances[i][j] = float.Min(distances[i][j], distances[i][k] + distances[k][j]);
                }
            }
        }
        return distances;
    }
}

public class Traversal(Graph graph)
{
    public int[] DepthFirstSearch(int sourceNode)
    {
        List<int> output = [];
        var visited = Enumerable.Range(0, graph.NumberOfNodes).Select(_ => false).ToArray();
        
        SearchRecursive(sourceNode);
        
        return output.ToArray();

        void SearchRecursive(int currentNode)
        {
            visited[currentNode] = true;
            output.Add(currentNode);
            foreach (var (_, nextNode) in graph
                .AdjacencyMatrix[currentNode]
                .Select((weight, node) => (weight, node))
                .Where(data => data.weight > 0 && !visited[data.node])
            )
            {
                SearchRecursive(nextNode);
            }
        }
    }
    public int[] BreastFirstSearch(int sourceNode)
    {
        List<int> output = [];
        var queue = new Queue<int>();
        var visited = Enumerable.Range(0, graph.NumberOfNodes).Select(_ => false).ToArray();
        visited[sourceNode] = true;
        queue.Enqueue(sourceNode);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            output.Add(current);

            foreach (var (_, nodeIndex) in graph
                .AdjacencyMatrix[current]
                .Select((weight, nodeIndex) => (weight, nodeIndex))
                .Where(data => data.weight > 0 && !visited[data.nodeIndex])
            )
            {
                visited[nodeIndex] = true;
                queue.Enqueue(nodeIndex);
            }
        }

        return output.ToArray();
    }
}