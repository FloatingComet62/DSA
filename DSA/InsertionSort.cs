namespace DSA;

public class InsertionSort: ITest
{
    public string Name()
    {
        return "InsertionSort";
    }

    private static IntList Implementation(IntList list)
    {
        if (list.Count <= 1)
        {
            return list;
        }
        foreach (var i in Enumerable.Range(1, list.Count - 1))
        {
            var insertIndex = i;
            var currentValue = list[i];
            
            for (var j = i - 1; j >= 0; j--)
            {
                if (list[j] <= currentValue)
                {
                    break;
                }
                list[j + 1] = list[j];
                insertIndex = j;
            }
            list[insertIndex] = currentValue;
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