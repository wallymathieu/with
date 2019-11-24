using System;
using System.Collections.Generic;
using System.Linq;

namespace With.Collections
{
    /// <summary>
    /// </summary>
    public static class MaxMinExtensions
    {
        private static IEnumerable<T> GetEquivalentBy<T>(this IEnumerable<T> self, T current, IComparer<T> compare)
        {
            foreach (var item in self)
            {
                if (compare.Compare(current, item) == 0)
                {
                    yield return item;
                }
            }
        }
        /// <summary>
        /// Returns the first maximum based on the compare function.
        /// </summary>
        /// <returns>The maximum or default if no maximum can be found.</returns>
        /// <param name="self">The sequence that contains the values</param>
        /// <param name="compare">The compare function.</param>
        public static T Max<T>(this IEnumerable<T> self, Func<T, T, int> compare)
        {
            return self.GetMax(Comparer.Create(compare)).FirstOrDefault();
        }

        /// <summary>
        /// Returns the first maximum based on the mapped values.
        /// </summary>
        /// <returns>The maximum or default if no maximum can be found.</returns>
        /// <param name="self">The sequence that contains the values</param>
        /// <param name="map">A map from the element to a value used for calculating max.</param>
        public static T MaxBy<T, TComparable>(this IEnumerable<T> self, Func<T, TComparable> map)
            where TComparable : IComparable
        {
            return self.GetMax(Comparer.Create(map)).FirstOrDefault();
        }

        private static IEnumerable<T> GetMax<T>(this IEnumerable<T> self, IComparer<T> compare)
        {
            using (var enumerator = self.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    return new T[0];
                }

                var current = enumerator.Current;
                while (enumerator.MoveNext())
                {
                    var item = enumerator.Current;
                    if (compare.Compare(current, item) < 0)
                    {
                        current = item;
                    }
                }
                return self.GetEquivalentBy(current, compare);
            }
        }
        /// <summary>
        /// Returns the first minimum based on the compare function.
        /// </summary>
        /// <returns>The minimum or default if no maximum can be found.</returns>
        /// <param name="self">The sequence that contains the minimum value</param>
        /// <param name="compare">The compare function.</param>
        public static T Min<T>(this IEnumerable<T> self, Func<T, T, int> compare)
        {
            return self.GetMin(Comparer.Create(compare)).FirstOrDefault();
        }

        /// <summary>
        /// Returns the first minimum based on the mapped values.
        /// </summary>
        /// <returns>The minimum or default if no maximum can be found.</returns>
        /// <param name="self">The sequence that contains the minimum value</param>
        /// <param name="map">A map from the element to a value used for calculating min.</param>
        public static T MinBy<T, TComparable>(this IEnumerable<T> self, Func<T, TComparable> map)
            where TComparable : IComparable
        {
            return self.GetMin(Comparer.Create(map)).FirstOrDefault();
        }

        private static IEnumerable<T> GetMin<T>(this IEnumerable<T> self, IComparer<T> compare)
        {
            //var list = self.ToList(compare).Min(); does not work when using comparer
            using (var enumerator = self.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    return new T[0];
                }

                var current = enumerator.Current;
                while (enumerator.MoveNext())
                {
                    var item = enumerator.Current;
                    if (compare.Compare(current, item) > 0)
                    {
                        current = item;
                    }
                }
                return self.GetEquivalentBy(current, compare);
            }
        }

    }
}

