using System;
using System.Collections;
using System.Collections.Generic;

namespace With.Reflection
{
    /// <summary>
    /// A wrapper around a stronly typed dictionary in order to be able to write code where we do not have the static type.
    /// </summary>
    class DictionaryAdapter : IDictionary
    {
        private IDictionary dic;

        public DictionaryAdapter(IDictionary that, IDictionary dictionary)
        {
            dic = dictionary;

            foreach (var item in that.Keys)
            {
                dic[item] = that[item];
            }
        }
        public object this[object key]
        {
            get
            {
                return dic[key];
            }
            set
            {
                var v = value;
                var k = key;
                dic[k] = v;
            }
        }

        public int Count
        {
            get
            {
                return dic.Count;
            }
        }

        public bool IsFixedSize
        {
            get
            {
                return false;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public bool IsSynchronized
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ICollection Keys
        {
            get
            {
                return dic.Keys;
            }
        }

        public object SyncRoot
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ICollection Values
        {
            get
            {
                return dic.Values;
            }
        }

        public void Add(object key, object value)
        {
            var k = key;
            var v = value;
            dic.Add(k, v);
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(object key)
        {
            var k = key;
            return dic.Contains(k);
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public IDictionaryEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Remove(object key)
        {
            var k = key;
            dic.Remove(k);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public object ToDictionary()
        {
            return dic;
        }
    }
}
