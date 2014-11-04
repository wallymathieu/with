using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
namespace With
{
	public class Range<T>:IEnumerable<T>
		where T: IComparable, IComparable<T>
	{
		class Int32Range:IEnumerable
		{
			private readonly Int32 @from;
			private readonly Int32 @to;
			private readonly Int32 @step;
			public Int32Range (object @from, object @to, object step)
			{
				this.@from = (Int32)@from;
				this.@to = (Int32)@to;
				this.@step = (Int32)@step;
			}

			public IEnumerator GetEnumerator ()
			{
				for (var i = @from; i<@to; i+=step) {
					yield return i;
				}
			}
		}

		class Int64Range:IEnumerable
		{
			private readonly Int64 @from;
			private readonly Int64 @to;
			private readonly Int64 @step;
			public Int64Range (object @from, object @to, object step)
			{
				this.@from =(Int64) @from;
				this.@to = (Int64)@to;
				this.@step = (Int64)@step;
			}

			public IEnumerator GetEnumerator ()
			{
				for (var i = @from; i<@to; i+=step) {
					yield return i;
				}
			}

		}

		class DecimalRange:IEnumerable
		{
			private readonly Decimal @from;
			private readonly Decimal @to;
			private readonly Decimal @step;
			public DecimalRange (object @from, object @to, object step)
			{
				this.@from =(Decimal) @from;
				this.@to = (Decimal)@to;
				this.@step = (Decimal)@step;
			}

			public IEnumerator GetEnumerator ()
			{
				for (var i = @from; i<@to; i+=step) {
					yield return i;
				}
			}
		}

		class SingleRange:IEnumerable
		{
			private readonly Single @from;
			private readonly Single @to;
			private readonly Single @step;
			public SingleRange (object @from, object @to, object step)
			{
				this.@from = (Single)@from;
				this.@to =  (Single)@to;
				this.@step =  (Single)@step;
			}

			public IEnumerator GetEnumerator ()
			{
				for (var i = @from; i<@to; i+=step) {
					yield return i;
				}
			}
		}

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
			} else if (typeof(T) == typeof(Single)) {
				inner = new SingleRange (@from, @to, @step);
			} else {
				throw new Exception (String.Format("There is implementation for type {0}",typeof(T).Name));
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

	}
}

