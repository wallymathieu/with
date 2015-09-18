using System;
using System.Collections.Generic;
using System.Linq;
using With.Collections;
namespace With.Linq
{
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

        public static T Max<T>(this IEnumerable<T> self, Func<T, T, int> compare)
        {
            return self.GetMax(Comparer.Create(compare)).FirstOrDefault();
        }

        public static T MaxBy<T, TComparable>(this IEnumerable<T> self, Func<T, TComparable> map)
            where TComparable : IComparable
        {
            return self.GetMax(Comparer.Create(map)).FirstOrDefault();
        }

        private static IEnumerable<T> GetMax<T>(this IEnumerable<T> self, IComparer<T> compare)
        {
            var enumerator = self.GetEnumerator();
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

        public static T Min<T>(this IEnumerable<T> self, Func<T, T, int> compare)
        {
            return self.GetMin(Comparer.Create(compare)).FirstOrDefault();
        }

        public static T MinBy<T, TComparable>(this IEnumerable<T> self, Func<T, TComparable> map)
            where TComparable : IComparable
        {
            return self.GetMin(Comparer.Create(map)).FirstOrDefault();
        }

        private static IEnumerable<T> GetMin<T>(this IEnumerable<T> self, IComparer<T> compare)
        {
            //var list = self.ToList(compare).Min(); does not work when using comparer
            var enumerator = self.GetEnumerator();
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

        public static MinMaxPartition<T> MinMax<T>(this IEnumerable<T> self)
            where T : IComparable
        {
            var array = self.ToArray();
            var comparer = Comparer<T>.Default;
            return new MinMaxPartition<T>(array.GetMin(comparer), array.GetMax(comparer));
        }
        public static MinMaxPartition<T> MinMax<T>(this IEnumerable<T> self, Func<T, T, int> compare)
        {
            var array = self.ToArray();
            var comparer = Comparer.Create(compare);
            return new MinMaxPartition<T>(array.GetMin(comparer), array.GetMax(comparer));
        }
        public static MinMaxPartition<T> MinMaxBy<T, TComparable>(this IEnumerable<T> self, Func<T, TComparable> map)
            where TComparable : IComparable
        {
            var array = self.ToArray();
            var comparer = Comparer.Create(map);
            return new MinMaxPartition<T>(array.GetMin(comparer), array.GetMax(comparer));
        }

        /*MinMax, MinMaxBy*/
    }
}

