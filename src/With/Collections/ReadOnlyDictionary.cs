using System;
using System.Collections;
using System.Collections.Generic;
namespace With.Collections
{
    public class ReadOnlyDictionary<TKey,TValue>:IReadOnlyDictionary<TKey,TValue>
    {
        private IDictionary<TKey,TValue> data;
        public ReadOnlyDictionary (IDictionary<TKey,TValue> data)
        {
            this.data = data;
        }

        public bool ContainsKey (TKey key)
        {
            return data.ContainsKey (key);
        }

        public bool TryGetValue (TKey key, out TValue value)
        {
            return data.TryGetValue (key, out value);
        }

        public int Count 
        {
            get {
                return data.Count;
            }
        }

        public TValue this [TKey key] 
        {
            get {
                return data[key];
            }
        }

        public IEnumerable<TKey> Keys 
        {
            get {
                return data.Keys;
            }
        }

        public IEnumerable<TValue> Values 
        {
            get {
                return data.Values;
            }
        }

        public IDictionary<TKey,TValue> ToDictionary()
        {
            return new Dictionary<TKey, TValue> (data);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator ()
        {
            return data.GetEnumerator ();
        }

        IEnumerator IEnumerable.GetEnumerator ()
        {
            return data.GetEnumerator ();
        }
    }
}

