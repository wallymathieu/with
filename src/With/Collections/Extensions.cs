using System;
using System.Collections.Generic;
using System.Linq;

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
        
        /// <summary>
        /// Returns sub lists of at most 'count' elements from the IEnumerable
        /// </summary>
        /// <returns>An IEnumerable of IEnumerable with Count less than 'count'</returns>
        /// <param name="enumerable"></param>
        /// <param name="count">The number of elements that should be at most found in each "batch".</param>
        public static IEnumerable<IEnumerable<T>> BatchesOf<T>(this IEnumerable<T> enumerable, int count)
        {
            using (var enumerator = enumerable.GetEnumerator())
            {
                while (true)
                {
                    var list = new List<T>(count);
                    for (int i = 0; i < count && enumerator.MoveNext(); i++)
                    {
                        list.Add(enumerator.Current);
                    }
                    if (!list.Any())
                    {
                        break;
                    }
                    yield return list;
                }
            }
        }
    }

    
}

