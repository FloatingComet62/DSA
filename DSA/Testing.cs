namespace DSA;

public interface ITest
{
    public String Name();
    public bool RunTests();
}

public class Testing<T> where T: IEquatable<T>
{
    private uint _iteration;
    public bool Result = true;
    
    public void Assert(T left, T right)
    {
        Console.Write($"Test {this._iteration}: ");
        this._iteration += 1;
        if (left.Equals(right))
        {
            _ = new ColorPrinting(ConsoleColor.Green, () =>
            {
                Console.WriteLine($"Success");
            });
            return;
        }

        _ = new ColorPrinting(ConsoleColor.Red, () =>
        {
            Console.WriteLine($"Failed");
        });
        
        Console.WriteLine($" {left} and {right} don't match");
        this.Result = false;
    }
}