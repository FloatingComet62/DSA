namespace DSA;

public class QuickSort: ITest
{
    public string Name()
    {
        return "QuickSort";
    }

    private static int Partition(IntList array, int low, int high)
    {
        var pivot = array[high];
        var i = low - 1;

        foreach (var j in Enumerable.Range(low, high - low))
        {
            if (array[j] > pivot)
            {
                continue;
            }

            i++;
            (array[i], array[j]) = (array[j], array[i]);
        }

        i++;
        (array[i], array[high]) = (array[high], array[i]);
        return i;
    }

    private static void QuickSortImplementation(IntList list, int low, int high)
    {
        if (low >= high)
        {
            return;
        }

        var pivotIndex = Partition(list, low, high);
        QuickSortImplementation(list, low, pivotIndex - 1);
        QuickSortImplementation(list, pivotIndex + 1, high);
    }

    private static IntList Implementation(IntList list)
    {
        QuickSortImplementation(list, 0, list.Count - 1);
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