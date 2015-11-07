using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace With.Collections
{
    public class ReadOnlyDictionaryUsage<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
    {
        private readonly IReadOnlyDictionary<TKey, TValue> data;
        private readonly Action<TKey, TValue> onUsage;
        public ReadOnlyDictionaryUsage(IDictionary<TKey, TValue> data, Action<TKey, TValue> onUsage)
            : this(new ReadOnlyDictionary<TKey, TValue>(data), onUsage)
        {
        }

        public ReadOnlyDictionaryUsage(IReadOnlyDictionary<TKey, TValue> data, Action<TKey, TValue> onUsage)
        {
            this.data = data;
            this.onUsage = onUsage;
        }

        public bool ContainsKey(TKey key)
        {
            return data.ContainsKey(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            var found = data.TryGetValue(key, out value);
            if (found)
            {
                onUsage(key, value);
            }
            return found;
        }

        public int Count
        {
            get
            {
                return data.Count;
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                onUsage(key, data[key]);
                return data[key];
            }
        }

        public IEnumerable<TKey> Keys
        {
            get
            {
                return data.Keys;
            }
        }

        private class OnUsageEnumerator : IEnumerator<TValue>, IEnumerator
        {
            private readonly IReadOnlyDictionary<TKey, TValue> data;
            private readonly KeyValuePair<TKey, TValue>[] values;
            private KeyValuePair<TKey,TValue> current;
            private readonly Action<TKey, TValue> onUsage;

            private int index;
            public OnUsageEnumerator(IReadOnlyDictionary<TKey, TValue> data, Action<TKey, TValue> onUsage)
            {
                this.data = data;
                this.values = data.ToArray();
                this.onUsage = onUsage;
                this.index = -1;
            }

            public TValue Current
            {
                get
                {
                    onUsage(current.Key, current.Value);
                    return current.Value;
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    onUsage(current.Key, current.Value);
                    return current.Value;
                }
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                //Avoids going beyond the end of the collection.
                if (++index >= values.Length)
                {
                    return false;
                }
                else
                {
                    // Set current box to next item in collection.
                    current = values[index];
                }
                return true;
            }

            public void Reset()
            {
                index = -1;
            }
        }

        private class OnUsageEnumerable : IEnumerable<TValue>
        {
            private readonly IReadOnlyDictionary<TKey, TValue> data;
            private readonly Action<TKey, TValue> onUsage;

            public OnUsageEnumerable(IReadOnlyDictionary<TKey, TValue> data, Action<TKey, TValue> onUsage)
            {
                this.data = data;
                this.onUsage = onUsage;
            }

            public IEnumerator<TValue> GetEnumerator()
            {
                return new OnUsageEnumerator(data, onUsage);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return new OnUsageEnumerator(data, onUsage);
            }
        }

        public IEnumerable<TValue> Values
        {
            get
            {
                return new OnUsageEnumerable(data, onUsage);
            }
        }

        public IDictionary<TKey, TValue> ToDictionary()
        {
            throw new NotImplementedException("Usage for dictionary not implemented");
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw new NotImplementedException("Usage for IEnumerator not implemented");
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException("Usage for IEnumerator not implemented");
        }
    }
}

