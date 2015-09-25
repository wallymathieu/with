using System;
using With;

namespace Tests.Interval.Adapters
{
    /// <summary>
    /// we want to test against an api that is simpler to write test code against. Objects instead of specific types.
    /// </summary>
    public class Interval : Interval<WrapComparable>
    {
        public Interval(IComparable @from, IComparable @to)
            :base(new WrapComparable(@from), new WrapComparable(@to))
        {
        }

        public bool Contains(IComparable v)
        {
            return base.Contains(new WrapComparable(v));
        }
    }
    public class WrapComparable : IComparable, IComparable<WrapComparable>
    {
        protected readonly IComparable self;

        public WrapComparable(IComparable self)
        {
            this.self = self;
        }
        public int CompareTo(object obj)
        {
            if (obj is WrapComparable)
            {
                return CompareTo((WrapComparable)obj);
            }
            else
            {
                return self.CompareTo(obj);
            }
        }

        public int CompareTo(WrapComparable other)
        {
            return self.CompareTo(other.self);
        }
    }
}
