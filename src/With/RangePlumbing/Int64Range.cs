using System;
using System.Collections;
using System.Collections.Generic;

namespace With.RangePlumbing
{

	internal class Int64Range:IEnumerable<Int64>
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
		public Int64Range (Int64 @from, Int64 @to, Int64 step)
		{
			this.@from = @from;
			this.@to = @to;
			this.@step = @step;
		}

		public IEnumerator<Int64> GetEnumerator ()
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
