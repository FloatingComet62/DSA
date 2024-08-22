using DSA;

List<ITest> allPrograms =
[
    new BubbleSort(),
    new SelectionSort(),
    new InsertionSort(),
    new QuickSort(),
    new CountingSort(),
    new RadixSort(),
    new AlienDictionary(),
];
List<ITest> currentPrograms = [new AlienDictionary()];

foreach (var program in currentPrograms)
{
    HandleProgram(program);
}

return;
void HandleProgram(ITest program)
{
    Console.WriteLine($"Running {program.Name()} Tests");
    if (!program.RunTests())
    {
        Console.Write($"{program.Name()} Tests ");
        _ = new ColorPrinting(ConsoleColor.Red, () =>
        {
            Console.WriteLine("Failed");
        });
        return;
    }
    Console.Write($"All {program.Name()} Tests ");
    _ = new ColorPrinting(ConsoleColor.Green, () =>
    {
        Console.WriteLine("Successful");
    });
}
