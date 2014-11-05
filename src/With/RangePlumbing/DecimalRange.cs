using System;
using System.Collections;
using System.Collections.Generic;

namespace With.RangePlumbing
{
	internal class DecimalRange:IStep<Decimal>
	{
		private readonly Decimal @from;
		private readonly Decimal @to;
		private readonly Decimal @step;
		public DecimalRange (Decimal @from, Decimal @to, Decimal step)
		{
			this.@from = @from;
			this.@to = @to;
			this.@step = @step;
		}
		internal DecimalRange (object @from, object @to, object step)
		{
			this.@from =(Decimal) @from;
			this.@to = (Decimal)@to;
			this.@step = (Decimal)@step;
		}

		public IStep<Decimal> Step(Decimal step){
			return new DecimalRange (@from,@to,step);
		}

		public bool Contain (decimal value)
		{
			return @from <= value && value <= @to && (value-@from)%step==0; 
		}

		public IEnumerator<Decimal> GetEnumerator ()
		{
			for (var i = @from; i<=@to; i+=step) {
				yield return i;
			}
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return this.GetEnumerator ();
		}
	}

}
