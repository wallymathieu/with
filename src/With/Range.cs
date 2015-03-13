using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using With.RangePlumbing;

namespace With
{
    public class Range<T>:IEnumerable<T>, IStep<T>
		where T: IComparable, IComparable<T>
	{


		private readonly IEnumerable inner;
		public Range (T @from,T @to)
			:this(@from,@to, (T)Convert.ChangeType(1,typeof (T)))
		{
		}

		public Range (T @from,T @to, T step)
		{
			if (typeof(T) == typeof(Int32)) {
				inner = new Int32Range (@from, @to, @step);
			} else if (typeof(T) == typeof(Int64)) {
				inner = new Int64Range (@from, @to, @step);
			} else if (typeof(T) == typeof(Decimal)) {
				inner = new DecimalRange (@from, @to, @step);
			} else {
				throw new Exception (String.Format("There is no implementation for type {0}",typeof(T).Name));
			}
		}

		public IEnumerator<T> GetEnumerator ()
		{
			foreach (var i in inner) {
				yield return (T)i;
			}
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
		{
			return this.inner.GetEnumerator ();
		}

        public IStep<T> Step(T step)
        {
            dynamic i = inner;
            return i.Step(step);
        }

        public bool Contain(T value)
        {
            dynamic i = inner;
            return i.Contain(value);
        }
	}
}

