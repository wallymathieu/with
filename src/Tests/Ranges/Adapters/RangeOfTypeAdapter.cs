using System;
using With;
using System.Collections;
using System.Collections.Generic;

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
        private readonly object _from;
        private readonly object _to;

        private static T C(object o)
        {
            return (T)Convert.ChangeType(o, typeof(T));
        }

        public RangeOfTypeAdapter(object from, object to, object step=null)
        {
            _from = @from;
            _to = to;
            if (step != null)
            {
                range = new Range<T>(C(@from), C(to), C(step));
            }
            else
            {
                range = new Range<T>(C(@from), C(to));
            }
        }

        public bool Contains(object o)
        {
            return range.Contains(C(o));
        }

        public IEnumerator<object> GetEnumerator()
        {
            foreach (object o in range)
            {
                yield return o;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IRange<object> Step(object step)
        {
            return new RangeOfTypeAdapter<T>(_from, _to, step);
        }
	}
}
