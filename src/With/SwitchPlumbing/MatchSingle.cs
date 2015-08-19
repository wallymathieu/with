using System;
namespace With.SwitchPlumbing
{
    public class MatchSingle<In, Out> : IMatcher<In,Out>
    {
        private readonly In expected;
        private readonly Func<In, Out> result;
        public bool TryMatch(In instance, out Out value)
        {
            if (expected.Equals(instance))
            {
                value = result(instance);
                return true;
            }
            value = default(Out);
            return false;
        }

        public MatchSingle(In expected, Func<In, Out> result)
        {
            this.expected = expected;
            this.result = result;
        }

    }
}