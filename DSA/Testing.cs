namespace DSA;

public interface ITest
{
    public void RunTests();
}

public class Testing<T> where T: IEquatable<T>
{
    private uint _iteration = 0;
    
    public void Assert(T left, T right)
    {
        Console.Write($"Test {this._iteration}: ");
        this._iteration += 1;
        if (left.Equals(right))
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Success");
            Console.ForegroundColor = oldColor;
            return;
        }

        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Failed");
            Console.ForegroundColor = oldColor;
        }
        
        Console.WriteLine($"{left} and {right} don't match");
    }
}