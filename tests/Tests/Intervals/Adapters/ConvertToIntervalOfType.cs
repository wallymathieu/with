using System;
using System.Linq;
using With;
using With.Collections;

namespace Tests.Intervals.Adapters
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

        public Interval<WrapComparable> Interval(int from, int to)
        {
            return new Interval<WrapComparable>(ToVal( @from), ToVal(to));
        }

        public WrapComparable ToVal(int v)
        {
            if (typeof(T) ==typeof( DateTime))
            {
                return new WrapComparable(new DateTime(2001,1,1).AddDays(v));
            }
            return new WrapComparable(C(v));
        }
    }

}
