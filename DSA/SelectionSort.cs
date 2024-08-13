namespace DSA;

public class SelectionSort: ITest
{
    public string Name()
    {
        return "SelectionSort";
    }

    private static IntList Implementation(IntList list)
    {
        foreach (var i in Enumerable.Range(0, list.Count))
        {
            var minValue = list[i];
            var minValueIndex = i;
            
            for (var j = i + 1; j < list.Count; j++)
            {
                if (list[j] >= minValue)
                {
                    continue;
                }
                minValue = list[j];
                minValueIndex = j;
            }
            (list[i], list[minValueIndex]) = (list[minValueIndex], list[i]);
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