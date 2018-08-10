using System;
using System.Collections.Generic;

namespace With.Collections
{
    /// <summary>
    /// Collection extensions
    /// </summary>
    public static class Extensions
    {
        private static bool ReturnsTrue<T>(T element) { return true; }
        /// <summary>
        /// Get next value in list
        /// </summary>
        /// <param name="that"></param>
        /// <param name="index"></param>
        /// <param name="filter"></param>
        /// <param name="valueWhenOutOfRange"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="OutOfRangeException"></exception>
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

        /// <summary>
        /// Get previous value in list
        /// </summary>
        /// <param name="that"></param>
        /// <param name="index"></param>
        /// <param name="filter"></param>
        /// <param name="valueWhenOutOfRange"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="OutOfRangeException"></exception>
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

