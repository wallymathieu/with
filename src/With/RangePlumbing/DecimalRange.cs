using System;
using System.Collections;
using System.Collections.Generic;

namespace With.RangePlumbing
{

	internal class DecimalRange:IEnumerable<Decimal>
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
		public DecimalRange (object @from, object @to, object step)
		{
			this.@from =(Decimal) @from;
			this.@to = (Decimal)@to;
			this.@step = (Decimal)@step;
		}

		public IEnumerator<Decimal> GetEnumerator ()
		{
			for (var i = @from; i<@to; i+=step) {
				yield return i;
			}
		}
		IEnumerator IEnumerable.GetEnumerator ()
		{
			return this.GetEnumerator ();
		}
	}

}
