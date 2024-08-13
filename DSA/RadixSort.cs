namespace DSA;

public class RadixSort: ITest
{
    public string Name()
    {
        return "RadixSort";
    }

    private static IntList Implementation(IntList list)
    {
        if (list.Count <= 1)
        {
            return list;
        }
        
        var radixArray = Enumerable.Range(0, 100).Select((_) => new List<int>()).ToArray();
        var maxValue = list.Max();
        var exponent = 1;

        while (maxValue / exponent > 0)
        {
            list.ForEach((item) =>
            {
                var radixIndex = (item / exponent) % 10;
                radixArray[radixIndex].Add(item);
            });
            list.Clear();
            
            foreach (var bucket in radixArray)
            {
                list.AddRange(bucket);
                bucket.Clear();
            }

            exponent *= 10;
        }

        return list;
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