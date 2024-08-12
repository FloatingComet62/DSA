using System.Text;

namespace DSA;

public class IntList : List<int>, IEquatable<IntList>
{
    public bool Equals(IntList? other)
    {
        if (other == null)
        {
            return false;
        }
        if (this.Count != other.Count)
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

public class BubbleSort: ITest
{
    private static IntList Implementation(IntList list) {
        for (var i = 0; i < list.Count; i++)
        {
            for (var j = 0; j < list.Count - i - 1; j++)
            {
                if (list[j] <= list[j + 1])
                {
                    continue;
                }
                (list[j], list[j + 1]) = (list[j + 1], list[j]);
            }
        }

        return list;
    }

    public void RunTests()
    {
        var tests = new Testing<IntList>();
        
        tests.Assert(Implementation([]), []);
        tests.Assert(Implementation([1, 3, 2]), [1, 2, 3]);
        tests.Assert(Implementation([6, 2, 6, 9, 3]), [2, 3, 6, 6, 9]);
        tests.Assert(Implementation([0, 4, 7, 3, 2, 2, 5, 1, 4, 9]), [0, 1, 2, 2, 3, 4, 4, 5, 7, 9]);
    }
}