using System;
using With;

namespace Tests.Intervals.Adapters
{
    /// <summary>
    /// Able to convert from object values to a specific type of interval. Also able to convert array to specific type of array.
    /// </summary>
    public interface IIntervalConverter
    {
        Interval<WrapComparable> Interval(int @from, int @to);
        WrapComparable ToVal(int v);
        object[] ToArrayOf<T>(T[] array);
    }
}
