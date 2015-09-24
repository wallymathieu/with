using System;
using System.Collections.Generic;
using System.Linq;
namespace With.Linq
{
    public static class ToDictionaryExtensions
    {
        public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> self)
        {
            return self.ToDictionary(kv => kv.Key, kv => kv.Value);
        }
        public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<Tuple<TKey, TValue>> self)
        {
            return self.ToDictionary(kv => kv.Item1, kv => kv.Item2);
        }
        public static IDictionary<T, T> ToDictionary<T>(this IEnumerable<T[]> self)
        {
            return self.ToDictionary(kv => Get(kv, 0), kv => Get(kv, 1));
        }
        private static T Get<T>(T[] val, int position)
        {
            if (val.Length <= position)
            {
                throw new WrongArrayLengthException(val.Length, position - 1);
            }
            return val[position];
        }
        public static ILookup<TKey, TValue> ToLookup<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> self)
        {
            return self.ToLookup(kv => kv.Key, kv => kv.Value);
        }
        public static ILookup<TKey, TValue> ToLookup<TKey, TValue>(this IEnumerable<Tuple<TKey, TValue>> self)
        {
            return self.ToLookup(kv => kv.Item1, kv => kv.Item2);
        }
    }
}

