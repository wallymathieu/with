using System;
using With;

namespace Tests.Intervals.Adapters
{
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
