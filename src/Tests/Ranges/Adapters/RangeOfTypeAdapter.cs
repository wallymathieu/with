using System;
using With;
using System.Linq;
using System.Collections;

namespace Tests.Ranges.Adapters
{
    
    /// <summary>
    /// A class that changes from source type to destination type (i.e. decimal or long).
    /// 
    /// This is since we want to test against a untyped api
    /// </summary>
    public class RangeOfTypeAdapter<T>: IRange where T:IComparable,IComparable<T>
    {
        private readonly Range<T> range;
        private static T C(object o)
        {
            return (T)Convert.ChangeType(o, typeof(T));
        }

        public RangeOfTypeAdapter(object from, object to, object step)
        {
            range = new Range<T>(C(@from), C(to), C(step));
        }
        public RangeOfTypeAdapter(object from, object to)
        {
            range = new Range<T>(C(@from), C(to));
        }
        public bool Contains(object o)
        {
            return range.Contains(C(o));
        }
        public object[] ToArray()
        {
            return range.Cast<object>().ToArray();
        }

		public IEnumerator GetEnumerator()
		{
			return range.GetEnumerator();
		}
	}
}
