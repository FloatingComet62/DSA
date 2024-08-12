namespace DSA;

public class ColorPrinting
{
    public ColorPrinting(ConsoleColor color, Action callback)
    {
        var oldColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        callback();
        Console.ForegroundColor = oldColor;
    }
}