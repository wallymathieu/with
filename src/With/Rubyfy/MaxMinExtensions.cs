using System;
using System.Collections.Generic;
using System.Linq;

namespace With.Rubyfy
{
    public static class MaxMinExtensions
    {
        private static int CompareComparable<T>(T a,T b) where T:IComparable
        {
            return a.CompareTo(b);
        }
        private static Func<T,T,int> MapCompare<T,TComparable>(Func<T,TComparable> map) where TComparable:IComparable
        {
            return (a,b)=> map(a).CompareTo(map(b));
        }
        private static IEnumerable<T> GetEquivalentBy<T>(this IEnumerable<T> self, T current, Func<T,T,int> compare)
        {
            var enumerator = self.GetEnumerator();
            var list = new List<T>();
            while (enumerator.MoveNext())
            {
                var item = enumerator.Current;
                if (compare(current, item) == 0)
                {
                    list.Add(item);
                }
            }
            return list;
        }

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
            return self.GetMaxBy(CompareComparable).FirstOrDefault();
        }
        public static IEnumerable<T> Max<T>(this IEnumerable<T> self, Func<T,T,int> compare)
        {
            return self.GetMaxBy(compare);
        }
        public static IEnumerable<T> MaxBy<T,TComparable>(this IEnumerable<T> self, Func<T,TComparable> map)
            where TComparable:IComparable
        {
            return self.GetMaxBy(MapCompare(map));
        }
        private static IEnumerable<T> GetMaxBy<T>(this IEnumerable<T> self, Func<T,T,int> compare)
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
                if (compare(current,item) < 0)
                {
                    current = item;
                }
            }
            return self.GetEquivalentBy(current, compare);
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
            return self.GetMinBy(CompareComparable).FirstOrDefault();
        }
        public static IEnumerable<T> Min<T>(this IEnumerable<T> self, Func<T,T,int> compare)
        {
            return self.GetMinBy(compare);
        }
        public static IEnumerable<T> MinBy<T,TComparable>(this IEnumerable<T> self, Func<T,TComparable> map)
            where TComparable:IComparable
        {
            return self.GetMinBy( MapCompare(map) );
        }

        private static IEnumerable<T> GetMinBy<T>(this IEnumerable<T> self, Func<T,T,int> compare)
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
                if (compare(current,item) > 0)
                {
                    current = item;
                }
            }
            return self.GetEquivalentBy(current, compare);
        }
      
        public static MinMaxPartition<T> MinMax<T>(this IEnumerable<T> self)
            where T:IComparable
        {
            var array = self.ToArray();
            return new MinMaxPartition<T>(array.GetMinBy(CompareComparable), array.GetMaxBy(CompareComparable));
        }
        public static MinMaxPartition<T> MinMax<T>(this IEnumerable<T> self, Func<T,T,int> compare)
        {
            var array = self.ToArray();
            return new MinMaxPartition<T>(array.GetMinBy(compare), array.GetMaxBy(compare));
        }
        public static MinMaxPartition<T> MinMaxBy<T,TComparable>(this IEnumerable<T> self, Func<T,TComparable> map)
            where TComparable:IComparable
        {
            var array = self.ToArray();
            var compare = MapCompare(map);
            return new MinMaxPartition<T>(array.GetMinBy(compare), array.GetMaxBy(compare));
        }
       
        /*MinMax, MinMaxBy*/
    }
}

