using System;
using System.Collections;
using System.Collections.Generic;

namespace With.RangePlumbing
{
    internal class Int64Range : IRange<Int64>
    {
        private readonly Int64 @from;
        private readonly Int64 @to;
        private readonly Int64 @step;
        internal Int64Range(object @from, object @to, object step)
        {
            this.@from = (Int64)@from;
            this.@to = (Int64)@to;
            this.@step = Math.Abs((Int64)@step);
        }
        public Int64Range(Int64 @from, Int64 @to, Int64 step)
        {
            this.@from = @from;
            this.@to = @to;
            this.@step = @step;
        }

        public IRange<Int64> Step(Int64 step)
        {
            return new Int64Range(@from, @to, step);
        }

        public bool Contains(long value)
        {
            if (@from <= @to)
            {
                return @from <= value && value <= @to && (value - @from) % step == 0;
            }
            return @to <= value && value <= @from && (value - @from) % step == 0;

        }

        public IEnumerator<Int64> GetEnumerator()
        {
            if (@from <= @to)
            {
                for (var i = @from; i <= @to; i += step)
                {
                    yield return i;
                }
            }
            else
            {
                for (var i = @from; i >= @to; i -= step)
                {
                    yield return i;
                }
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

    }

}
