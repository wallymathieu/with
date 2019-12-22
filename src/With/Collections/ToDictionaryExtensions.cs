using System;
using System.Collections.Generic;
using System.Linq;

namespace With.Collections
{
    /// <summary>
    ///
    /// </summary>
    public static class ToDictionaryExtensions
    {
        /// <summary>
        /// Returns a dictionary from a <see cref="System.Collections.Generic.KeyValuePair{TKey, TValue}"/> where the Key is the key and the Value is the value
        /// </summary>
        public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> self)
        {
            return self.ToDictionary(kv => kv.Key, kv => kv.Value);
        }
        /// <summary>
        /// Return a dictionary from a <see cref="System.Tuple{T1, T2}"/> where the first item in the tuple is the key, and the second is the value
        /// </summary>
        public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<Tuple<TKey, TValue>> self)
        {
            return self.ToDictionary(kv => kv.Item1, kv => kv.Item2);
        }
        /// <summary>
        /// Returns a lookup from a <see cref="System.Collections.Generic.KeyValuePair{TKey, TValue}"/> where the Key is the key and the Value is the value
        /// </summary>
        public static ILookup<TKey, TValue> ToLookup<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> self)
        {
            return self.ToLookup(kv => kv.Key, kv => kv.Value);
        }
        /// <summary>
        /// Return a lookup from a <see cref="System.Tuple{T1, T2}"/> where the first item in the tuple is the key, and the second is the value
        /// </summary>
        public static ILookup<TKey, TValue> ToLookup<TKey, TValue>(this IEnumerable<Tuple<TKey, TValue>> self)
        {
            return self.ToLookup(kv => kv.Item1, kv => kv.Item2);
        }
    }
}

