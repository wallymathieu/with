using System;
namespace With.SwitchPlumbing
{
    public class MatchElse<In, Out> : IMatcher<In,Out>
    {
        private readonly Func<In, Out> result;

        public MatchElse(Func<In, Out> result)
        {
            this.result = result;
        }
        public bool TryMatch(In instance,out Out value)
        {
            value = result(instance);
            return true;
        }
    }
}