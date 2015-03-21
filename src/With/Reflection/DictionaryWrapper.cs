using System;
using System.Collections;
using System.Collections.Generic;

namespace With.Reflection
{
    class DictionaryWrapper : IDictionary
    {
        private Type[] t;
        private dynamic dic;

        public DictionaryWrapper(dynamic that)
        {
            Type type = that.GetType();
            this.t = type.GetIDictionaryTypeParameters();
            dic = typeof(Dictionary<,>).MakeGenericType(t).GetConstructor(new Type[0]).Invoke(new object[0]);
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
                dynamic v = value;
                dynamic k = key;
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
            dynamic k = key;
            dynamic v = value;
            dic.Add(k, v);
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(object key)
        {
            dynamic k = key;
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
            dynamic k = key;
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
