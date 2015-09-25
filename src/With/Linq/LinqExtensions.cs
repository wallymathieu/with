using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using With.Linq;
namespace With.Linq
{
    public static class LinqExtensions
    {
        public static bool Even(this int self)
        {
            return self % 2 == 0;
        }
        public static bool Odd(this int self)
        {
            return self % 2 != 0;
        }

        public static bool IsEmpty<T>(this IEnumerable<T> self)
        {
            return !self.Any();
        }

        public static TRet MapDetect<T, TRet>(this IEnumerable<T> collection, Func<T, TRet> func) where TRet : class
        {
            foreach (var value in collection)
            {
                TRet result;
                if ((result = func(value)) != null)
                {
                    return result;
                }
            }
            return null;
        }

        public static IEnumerable<T> Sort<T>(this IEnumerable<T> self, Func<T, T, int> compare)
        {
            return self.OrderBy(compare);
        }

        public static IEnumerable<T> CollectConcat<T>(this IEnumerable<T> self, Func<T, IEnumerable<T>> map)
        {
            return self.FlatMap(map);
        }
        public static IEnumerable<T> FlatMap<T>(this IEnumerable<T> self, Func<T, IEnumerable<T>> map)
        {
            return self.Select(map).Flatten<T>();
        }
        public static IEnumerable<T> CollectConcat<T>(this IEnumerable self, Func<Object, IEnumerable> map)
        {
            return self.FlatMap<T>(map);
        }
        public static IEnumerable<T> FlatMap<T>(this IEnumerable self, Func<Object, IEnumerable> map)
        {
            return self.Cast<Object>().Select(map).Flatten<T>();
        }

        public static IEnumerable<T> Cycle<T>(this IEnumerable<T> self, int? n = null)
        {
            while (n == null || n-- > 0)
            {
                foreach (var item in self)
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<T> Each<T>(this IEnumerable<T> self, Action<T> action)
        {
            foreach (var elem in self)
            {
                action(elem);
            }
            return self;
        }
        public static IEnumerable<Tuple<T1, T2>> Each<T1, T2>(this IEnumerable<Tuple<T1, T2>> self, Action<T1, T2> action)
        {
            foreach (var elem in self)
            {
                action(elem.Item1, elem.Item2);
            }
            return self;
        }
        public static IEnumerable<IGrouping<T1, T2>> Each<T1, T2>(this IEnumerable<IGrouping<T1, T2>> self, Action<T1, IEnumerable<T2>> action)
        {
            foreach (var elem in self)
            {
                action(elem.Key, elem);
            }
            return self;
        }

        public static IEnumerable<IEnumerable<T>> EachSlice<T>(this IEnumerable<T> self, int count, Action<IEnumerable<T>> slice)
        {
            return self.EachSlice(count).Each(slice);
        }
        public static IEnumerable<IEnumerable<T>> EachSlice<T>(this IEnumerable<T> self, int count)
        {
            return self.BatchesOf(count);
        }

        public static int FindIndex<T>(this IEnumerable<T> self, T item)
        {
            return self.FindIndex(elem => item.Equals(elem));
        }
        public static int FindIndex<T>(this IList<T> self, T item)
        {
            return self.IndexOf(item);
        }
        public static int FindIndex<T>(this IEnumerable<T> self, Func<T, bool> predicate)
        {
            var index = 0;
            foreach (var item in self)
            {
                if (predicate(item))
                {
                    return index;
                }

                index++;
            }
            return -1;
        }

        public static bool None<T>(this IEnumerable<T> self, Func<T, bool> predicate)
        {
            return !Enumerable.Any(self, predicate);
        }
        public static bool None<T>(this IEnumerable<T> self)
        {
            return !Enumerable.Any(self);
        }

        public static bool One<T>(this IEnumerable<T> self, Func<T, bool> predicate)
        {
            return Enumerable.Count(self, predicate) == 1;
        }
        public static bool One<T>(this IEnumerable<T> self)
        {
            return Enumerable.Count(self) == 1;
        }

        public static Partition<T> Partition<T>(this IEnumerable<T> self, Func<T, bool> partition)
        {
            var groups = self.GroupBy(partition);
            var trueArray = groups.SingleOrDefault(g => g.Key.Equals(true));
            var falseArray = groups.SingleOrDefault(g => g.Key.Equals(false));
            return new With.Linq.Partition<T>(trueArray.ToArray(), falseArray.ToArray());
        }

        public static IEnumerable<T> Reject<T>(this IEnumerable<T> self, Func<T, bool> predicate)
        {
            return self.Where(elem => !predicate(elem));
        }
    }
}
