using System;

namespace Tests.Interval.Adapters
{
    /// <summary>
    /// Able to convert from object values to a specific type of interval. Also able to convert array to specific type of array.
    /// </summary>
    public interface IIntervalConverter
    {
        Interval Interval(IComparable @from, IComparable @to);
        object[] ToArrayOf<T>(T[] array);
    }
}
