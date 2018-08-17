using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using With.Linq;
namespace With.Linq
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> FlatMap<T>(this IEnumerable<T> self, Func<T, IEnumerable<T>> map) => self.Select(map).Flatten<T>();

        public static IEnumerable<T> FlatMap<T>(this IEnumerable self, Func<Object, IEnumerable> map) => self.Cast<Object>().Select(map).Flatten<T>();

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

        public static int FindIndex<T>(this IList<T> self, T item) => self.IndexOf(item);

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

        public static Partition<T> Partition<T>(this IEnumerable<T> self, Func<T, bool> partition)
        {
            var groups = self.GroupBy(partition);
            var trueArray = groups.SingleOrDefault(g => g.Key.Equals(true))?.ToArray() ?? new T[0];
            var falseArray = groups.SingleOrDefault(g => g.Key.Equals(false))?.ToArray() ?? new T[0];
            return new Partition<T>(trueArray, falseArray);
        }
    }
}
