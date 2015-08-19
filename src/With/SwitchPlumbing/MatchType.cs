using System;

namespace With.SwitchPlumbing
{
    public class MatchType<On, In, Out> : IMatcher<In,Out>
        where On : In
    {
        private readonly Func<On, Out> _func;
        public MatchType(Func<On, Out> @case)
        {
            _func = @case;
        }
            
        public bool TryMatch(In instance, out Out value)
        {
            if (instance is On)
            {
                value = _func((On)instance);
                return true;
            }
            value = default(Out);
            return false;
        }
    }
}