using System;
using System.Collections.Generic;
using System.Linq;
using With;

namespace Tests.Linq
{
    public static class TestExtensions
    {
        public static bool Even(this int self) => self % 2 == 0;
        public static bool Odd(this int self) => self % 2 != 0;
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
        public static IEnumerable<IEnumerable<T>> EachSlice<T>(this IEnumerable<T> self, int count, Action<IEnumerable<T>> slice) => self.EachSlice(count).Each(slice);
        public static IEnumerable<IEnumerable<T>> EachSlice<T>(this IEnumerable<T> self, int count) => self.BatchesOf(count);
    }
}
