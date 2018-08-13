using System;
using System.Collections.Generic;
using System.Linq;

namespace With
{
    /// <summary>
    /// Class that holds methods related to KeyValuePairs. The same pattern as <see cref="System.Tuple"/>
    /// </summary>
    public static class KeyValuePair
    {
        /// <summary>
        /// Creates a new KeyValuePair
        /// </summary>
        public static KeyValuePair<TKey,TValue> Create<TKey, TValue>(TKey key, TValue value)
        {
            return new KeyValuePair<TKey, TValue>(key, value);
        }
    }
}
