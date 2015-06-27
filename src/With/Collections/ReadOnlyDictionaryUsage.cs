using System;
using System.Collections;
using System.Collections.Generic;

namespace With.Collections
{
    public class ReadOnlyDictionaryUsage<TKey,TValue>: IReadOnlyDictionary<TKey,TValue>
    {
        private readonly IReadOnlyDictionary<TKey,TValue> data;
        private readonly Action<TKey,TValue> onUsage;
        public ReadOnlyDictionaryUsage (IDictionary<TKey,TValue> data, Action<TKey,TValue> onUsage)
            : this(new ReadOnlyDictionary<TKey,TValue>(data), onUsage)
        {
        }

        public ReadOnlyDictionaryUsage (IReadOnlyDictionary<TKey,TValue> data, Action<TKey,TValue> onUsage)
        {
            this.data = data;
            this.onUsage = onUsage;
        }

        public bool ContainsKey (TKey key)
        {
            return data.ContainsKey (key);
        }

        public bool TryGetValue (TKey key, out TValue value)
        {
            var found = data.TryGetValue (key, out value);
            if (found){
                onUsage(key, value);
            }
            return found;
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
                onUsage (key, data [key]);
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
                throw new NotImplementedException ("Usage for IEnumerable<TValue> not implemented");
            }
        }

        public IDictionary<TKey,TValue> ToDictionary()
        {
            throw new NotImplementedException ("Usage for dictionary not implemented");
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator ()
        {
            throw new NotImplementedException ("Usage for IEnumerator not implemented");
        }

        IEnumerator IEnumerable.GetEnumerator ()
        {
            throw new NotImplementedException ("Usage for IEnumerator not implemented");
        }
    }
}

