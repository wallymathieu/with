using System;
using System.Collections.Generic;
using System.Linq;
namespace With.SwitchPlumbing
{
    public class Match<In, Out> : IMatcher<In, Out>
    {
        private readonly Func<In, bool> expected;
        private readonly Func<In, Out> result;

        public bool TryMatch(In instance, out Out value)
        {
            if (expected(instance))
            {
                value = result(instance);
                return true;
            }
            value = default(Out);
            return false;
        }

        public Match(IEnumerable<In> expected, Func<In, Out> result)
        {
            if (expected is IContainer<In>)
            {
                this.expected = ((IContainer<In>)(expected)).Contains;
            }
            else
            {
                this.expected = expected.Contains;
            }
            this.result = result;
        }

        public Match(Func<In, bool> expected, Func<In, Out> result)
        {
            this.expected = expected;
            this.result = result;
        }
    }

}