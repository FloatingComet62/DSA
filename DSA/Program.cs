using DSA;

List<ITest> programs = [new BubbleSort()];

foreach (var program in programs)
{
    HandleProgram(program);
}

return;
void HandleProgram(ITest program)
{
    Console.WriteLine($"Running {program.Name()} Tests");
    if (!program.RunTests())
    {
        Console.WriteLine($"{program.Name()} Tests ");
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
