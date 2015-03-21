using System;
using System.Collections.Generic;

namespace With.Collections
{
    public static class Extensions
    {
        private static bool ReturnsTrue<T>(T element) { return true; }
        public static T Next<T>(this IList<T> that, int index, Func<T, bool> filter = null, Func<int, T> valueWhenOutOfRange = null)
        {
            if (null == filter) filter = ReturnsTrue;
            for (int i = index + 1; i < that.Count; i++)
            {
                var item = that[i];
                if (filter(item))
                    return item;
            }
            if (valueWhenOutOfRange != null)
            {
                return valueWhenOutOfRange(index);
            }
            throw new OutOfRangeException();
        }

        public static T Previous<T>(this IList<T> that, int index, Func<T, bool> filter = null, Func<int, T> valueWhenOutOfRange = null)
        {
            if (null == filter) filter = ReturnsTrue;
            for (int i = index - 1; 0 <= i; i--)
            {
                var item = that[i];
                if (filter(item))
                    return item;
            }
            if (valueWhenOutOfRange != null)
            {
                return valueWhenOutOfRange(index);
            }
            throw new OutOfRangeException();
        }
    }

    
}

