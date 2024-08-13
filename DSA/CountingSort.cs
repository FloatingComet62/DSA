namespace DSA;

public class CountingSort: ITest
{
    public string Name()
    {
        return "CountingSort";
    }

    private static IntList Implementation(IntList list)
    {
        if (list.Count <= 1)
        {
            return list;
        }

        var count = new List<int>(new int[list.Max() + 1]);

        foreach (var item in list)
        {
            count[item]++;
        }

        var output = new IntList();
        output.EnsureCapacity(list.Count);

        for (var i = 0; i < count.Count; i++)
        {
            var itemCount = count[i];
            
            // var binding = new List<int>(new int[itemCount]);
            // var filled = binding.ConvertAll((_) => i);
            // output.AddRange(filled);

            for (var j = 0; j < itemCount; j++)
            {
                output.Add(i);
            }
        }
        
        return output;
    }

    public bool RunTests()
    {
        var tests = new Testing<IntList>();
        
        tests.Assert(Implementation([]), []);
        tests.Assert(Implementation([1, 3, 2]), [1, 2, 3]);
        tests.Assert(Implementation([6, 2, 6, 9, 3]), [2, 3, 6, 6, 9]);
        tests.Assert(Implementation([0, 4, 7, 3, 2, 2, 5, 1, 4, 9]), [0, 1, 2, 2, 3, 4, 4, 5, 7, 9]);

        return tests.Result;
    }
}