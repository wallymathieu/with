using System;
using System.Collections.Generic;
using System.Linq;
using With.Collections;
namespace With.Rubyfy
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

        internal static IEnumerable<T> Max<T>(this IEnumerable<T> self, int count)
            where T:IComparable
        {
            throw new NotImplementedException();
            //return self.OrderBy(Comparer<T>.Default).Take(count);
        }
        internal static IEnumerable<T> Max<T>(this IEnumerable<T> self, int count, Func<T,T,int> compare)
        {
            throw new NotImplementedException();

            //return self.OrderBy(Comparer.Create(compare)).Take(count);
        }
        internal static IEnumerable<T> MaxBy<T,TComparable>(this IEnumerable<T> self, int count, Func<T,TComparable> map)
            where TComparable:IComparable
        {
            throw new NotImplementedException();

            //return self.OrderBy(Comparer.Create(map)).Take(count);
        }

        internal static IEnumerable<T> Min<T>(this IEnumerable<T> self, int count)
            where T:IComparable
        {
            throw new NotImplementedException();

            //return self.OrderBy(Comparer<T>.Default).Take(count);
        }
        internal static IEnumerable<T> Min<T>(this IEnumerable<T> self, int count, Func<T,T,int> compare)
        {
            throw new NotImplementedException();

            //return self.OrderBy(Comparer.Create(compare)).Take(count);
        }
        internal static IEnumerable<T> MinBy<T,TComparable>(this IEnumerable<T> self, int count, Func<T,TComparable> map)
            where TComparable:IComparable
        {
            throw new NotImplementedException();

            //return self.OrderBy( Comparer.Create(map) ).Take(count);
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
            return self.GetMax(Comparer<T>.Default).FirstOrDefault();
        }

        public static T Max<T>(this IEnumerable<T> self, Func<T,T,int> compare)
        {
            return self.GetMax(Comparer.Create(compare)).FirstOrDefault();
        }
      
        public static T MaxBy<T,TComparable>(this IEnumerable<T> self, Func<T,TComparable> map)
            where TComparable:IComparable
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
                if (compare.Compare(current,item) < 0)
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
            return self.GetMin(Comparer<T>.Default).FirstOrDefault();
        }

        public static T Min<T>(this IEnumerable<T> self, Func<T,T,int> compare)
        {
            return self.GetMin(Comparer.Create(compare)).FirstOrDefault();
        }

        public static T MinBy<T,TComparable>(this IEnumerable<T> self, Func<T,TComparable> map)
            where TComparable:IComparable
        {
            return self.GetMin( Comparer.Create(map) ).FirstOrDefault();
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
                if (compare.Compare(current,item) > 0)
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
            var comparer = Comparer<T>.Default;
            return new MinMaxPartition<T>(array.GetMin(comparer), array.GetMax(comparer));
        }
        public static MinMaxPartition<T> MinMax<T>(this IEnumerable<T> self, Func<T,T,int> compare)
        {
            var array = self.ToArray();
            var comparer = Comparer.Create(compare);
            return new MinMaxPartition<T>(array.GetMin(comparer), array.GetMax(comparer));
        }
        public static MinMaxPartition<T> MinMaxBy<T,TComparable>(this IEnumerable<T> self, Func<T,TComparable> map)
            where TComparable:IComparable
        {
            var array = self.ToArray();
            var comparer = Comparer.Create(map);
            return new MinMaxPartition<T>(array.GetMin(comparer), array.GetMax(comparer));
        }
       
        /*MinMax, MinMaxBy*/
    }
}

