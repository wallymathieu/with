using System;
using System.Linq;

namespace Tests.Interval.Adapters
{
    public class ConvertToIntervalOfType<T> : IIntervalConverter where T : IComparable, IComparable<T>
    {
        private static T C(object o)
        {
            return (T)Convert.ChangeType(o, typeof(T));
        }

        public object[] ToArrayOf<SourceType>(SourceType[] array)
        {
            return array.Select(i => C(i)).Cast<object>().ToArray();
        }

        public Interval Interval(IComparable from, IComparable to)
        {
            return new Interval(@from, to);
        }
    }

}
