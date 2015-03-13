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
            this.@step = Math.Abs(@step);
		}

		internal Int32Range (object @from, object @to, object step)
            :this((Int32)@from,(Int32)@to,(Int32)@step)
		{
		}

		public IStep<Int32> Step(Int32 step){
			return new Int32Range (@from,@to,step);
		}

		public bool Contains (int value)
		{
            if (@from <= @to)
            {
                return @from <= value && value <= @to && (value - @from) % step == 0;
            }
            return @to <= value && value <= @from && (value - @from) % step == 0;
		}

		public IEnumerator<Int32> GetEnumerator ()
        {
            if (@from <= @to)
            {
                for (var i = @from; i <= @to; i += step)
                {
                    yield return i;
                }
            }else{
                for (var i = @from; i >= @to; i -= step)
                {
                    yield return i;
                }
            }
		}
		IEnumerator IEnumerable.GetEnumerator ()
		{
			return this.GetEnumerator ();
		}
	}
}

