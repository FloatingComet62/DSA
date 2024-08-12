using DSA;

List<ITest> programs = [new BubbleSort()];

foreach (var program in programs)
{
    program.RunTests();
}