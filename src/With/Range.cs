using System;
using System.Collections.Generic;
using System.Collections;
using With.RangePlumbing;

namespace With
{
    public class Range<T>:IEnumerable<T>, IStep<T>
		where T: IComparable, IComparable<T>
	{
		private readonly IStep<T> inner;
		public Range (T @from,T @to)
			:this(@from,@to, (T)Convert.ChangeType(1,typeof (T)))
		{
		}

		public Range (T @from,T @to, T step)
		{
			if (typeof(T) == typeof(Int32)) {
				inner = (IStep<T>)new Int32Range (@from, @to, @step);
			} else if (typeof(T) == typeof(Int64)) {
				inner = (IStep<T>)new Int64Range (@from, @to, @step);
			} else if (typeof(T) == typeof(Decimal)) {
				inner = (IStep<T>)new DecimalRange (@from, @to, @step);
			} else {
				throw new Exception (String.Format("There is no implementation for type {0}",typeof(T).Name));
			}
		}

		public IEnumerator<T> GetEnumerator ()
		{
			foreach (var i in inner) {
				yield return i;
			}
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return inner.GetEnumerator ();
		}

        public IStep<T> Step(T step)
        {
            return inner.Step(step);
        }

        public bool Contain(T value)
        {
            return inner.Contain(value);
        }
	}
}

