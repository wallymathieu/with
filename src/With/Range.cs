using System;
using System.Collections.Generic;

namespace With
{
	public class Range<T>:IEnumerable<T>
		where T: IComparable, IComparable<T>
	{
		private readonly T @from;
		private readonly T @to;
		private readonly Func<T,T> step;
		public Range (T @from,T @to, T step)
			:this(@from, @to, GetStepper(step))
		{
		}
		public Range (T @from,T @to, Func<T,T> step=null)
		{
			this.@from = @from;
			this.@to = @to;
			if (step == null) {
				var one = (T)Convert.ChangeType(1,typeof (T));
				if (typeof(T) == typeof(Int32)) {
					this.step = Int32DefaultStep(one);
				} else if (typeof(T) == typeof(Int64)) {
					this.step = Int64DefaultStep(one);
				} else if (typeof(T) == typeof(Decimal)) {
					this.step = DecimalDefaultStep(one);
				}else if (typeof(T) == typeof(float)) {
					this.step = FloatDefaultStep(one);
				} else {
					throw new Exception (String.Format("There is no default step for type {0}",typeof(T).Name));
				}
			} else {
				this.step = step;
			}
		}

		private static Func<T,T> GetStepper (T step)
		{
			if (typeof(T) == typeof(Int32)) {
				return Int32DefaultStep(step);
			} else if (typeof(T) == typeof(Int64)) {
				return Int64DefaultStep(step);
			} else if (typeof(T) == typeof(Decimal)) {
				return DecimalDefaultStep(step);
			}else if (typeof(T) == typeof(float)) {
				return FloatDefaultStep(step);
			} else {
				throw new Exception (String.Format("There is no default + for type {0}",typeof(T).Name));
			}
		}

		private static Func<T,T> Int32DefaultStep (T step)
		{
			// il emit?
			var s = (Int32)(object)step;
			return (i)=> (T)(object)((Int32)(object)i).Plus(s);
		}
		private static Func<T,T> Int64DefaultStep (T step)
		{
			// il emit?
			var s = (Int64)(object)step;
			return (i)=>(T)(object)((Int64)(object)i).Plus(s);
		}
		private static Func<T,T> DecimalDefaultStep (T step)
		{
			// il emit?
			var s = (Decimal)(object)step;
			return (i)=>(T)(object)((Decimal)(object)i).Plus(s);
		}
		private static Func<T,T> FloatDefaultStep (T step)
		{
			// il emit?
			var s = (float)(object)step;
			return (i)=>(T)(object)((float)(object)i).Plus(s);
		}
		public IEnumerator<T> GetEnumerator ()
		{
			for (T i = @from; i.CompareTo(@to)<0; i=step(i)) {
				yield return i;				
			}
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
		{
			return this.GetEnumerator ();
		}

	}
}

