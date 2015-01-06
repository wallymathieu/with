using System;
using System.Collections.Generic;
using System.Linq;

namespace With.Rubyfy
{
    public static class MaxMinExtensions
    {

        public static long Max(this IEnumerable<long> self)
        {
            return Enumerable.Max(self);
        }
        public static decimal Max(this IEnumerable<decimal> self)
        {
            return Enumerable.Max(self);
        }
        public static T Max<T>(this IEnumerable<T> self)
            where T:IComparable
        {
            return self.GetMaxBy(e => e);
        }
        public static IEnumerable<T> MaxBy<T,TComparable>(this IEnumerable<T> self, Func<T,TComparable> map)
            where TComparable:IComparable
        {
            var array = self.ToArray();
            var min = array.GetMaxBy(map);
            return array.Where(a => min.Equals( map(a) ));
        }
        private static TComparable GetMaxBy<T,TComparable>(this IEnumerable<T> self, Func<T,TComparable> map)
            where TComparable:IComparable
        {
            var enumerator = self.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return default(TComparable);
            }

            var current = Tuple.Create(enumerator.Current,map(enumerator.Current));
            while (enumerator.MoveNext())
            {
                var item = Tuple.Create(enumerator.Current, map(enumerator.Current));
                if (current.Item2.CompareTo(item.Item2) < 0)
                {
                    current = item;
                }
            }
            return current.Item2;
        }

        public static long Min(this IEnumerable<long> self)
        {
            return Enumerable.Min(self);
        }
        public static decimal Min(this IEnumerable<decimal> self)
        {
            return Enumerable.Min(self);
        }
        public static T Min<T>(this IEnumerable<T> self)
            where T:IComparable
        {
            return self.MinBy(e=>e).SingleOrDefault();
        }
        public static IEnumerable<T> MinBy<T,TComparable>(this IEnumerable<T> self, Func<T,TComparable> map)
            where TComparable:IComparable
        {
            var array = self.ToArray();
            var min = array.GetMinBy(map);
            return array.Where(a => min.Equals(map(a)));
        }
        private static TComparable GetMinBy<T,TComparable>(this IEnumerable<T> self, Func<T,TComparable> map)
            where TComparable:IComparable
        {
            var enumerator = self.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return default(TComparable);
            }

            var current = Tuple.Create(enumerator.Current,map(enumerator.Current));
            while (enumerator.MoveNext())
            {
                var item = Tuple.Create(enumerator.Current, map(enumerator.Current));
                if (current.Item2.CompareTo(item.Item2) > 0)
                {
                    current = item;
                }
            }
            return current.Item2;
        }
        /*MinMax, MinMaxBy*/
    }
}

