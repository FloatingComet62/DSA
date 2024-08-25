using DSA;
using DSA.Structures;


// List<ITest> allPrograms =
// [
//     new BubbleSort(),
//     new SelectionSort(),
//     new InsertionSort(),
//     new QuickSort(),
//     new CountingSort(),
//     new RadixSort(),
//     new AlienDictionary(),
// ];
// List<ITest> currentPrograms = [new AlienDictionary()];
//
// foreach (var program in currentPrograms)
// {
//     HandleProgram(program);
// }
//
// return;
// void HandleProgram(ITest program)
// {
//     Console.WriteLine($"Running {program.Name()} Tests");
//     if (!program.RunTests())
//     {
//         Console.Write($"{program.Name()} Tests ");
//         _ = new ColorPrinting(ConsoleColor.Red, () =>
//         {
//             Console.WriteLine("Failed");
//         });
//         return;
//     }
//     Console.Write($"All {program.Name()} Tests ");
//     _ = new ColorPrinting(ConsoleColor.Green, () =>
//     {
//         Console.WriteLine("Successful");
//     });
// }

var x = new Graph(5);
x.AddEdge(0, 1);
x.AddEdge(1, 2);
x.AddEdge(1, 3);
x.AddEdge(2, 3);
x.AddEdge(3, 4);

// var coloring = new GraphColoring(x);
// var result = coloring.GreedyColoring();
// foreach (var item in result)
// {
//     Console.Write($"{item} ");
// }
//
// var tsp = new TravellingSalesMan(x);
// Console.WriteLine(tsp.Solve(0));

// var shortestPath = new ShortestPath(x.GetBidirectional());
// foreach (var f in shortestPath.Dijkstra(0))
// {
//     Console.WriteLine($"{f}");
// }
// Console.WriteLine("---");
// foreach (var f in shortestPath.BellmanFord(0))
// {
//     Console.WriteLine($"{f}");
// }
// Console.WriteLine("---");
// foreach (var f in shortestPath.FloydWarshall()[0])
// {
//     Console.WriteLine($"{f}");
// }
