using System;
using System.Collections;
using System.Collections.Generic;

namespace With.RangePlumbing
{
	internal class Int32Range:IStep<Int32>
	{
		private readonly Int32 @from;
		private readonly Int32 @to;
		private readonly Int32 @step;
		public Int32Range (Int32 @from, Int32 @to, Int32 step)
		{
			this.@from = @from;
			this.@to = @to;
			this.@step = @step;
		}

		internal Int32Range (object @from, object @to, object step)
		{
			this.@from = (Int32)@from;
			this.@to = (Int32)@to;
			this.@step = (Int32)@step;
		}

		public IStep<Int32> Step(Int32 step){
			return new Int32Range (@from,@to,step);
		}

		public IEnumerator<Int32> GetEnumerator ()
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

