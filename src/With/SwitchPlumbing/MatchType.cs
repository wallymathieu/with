using System;

namespace With.SwitchPlumbing
{
    public class MatchType<On, In, Out> : ISwitch<In, Out>
        where On : In
    {
        private readonly ISwitch<In, Out> inner;
        private readonly Func<On, Out> _func;
        public MatchType(ISwitch<In, Out> inner, Func<On, Out> @case)
        {
            this.inner = inner;
            _func = @case;
        }

        public override In Instance
        {
            get { return inner.Instance; }
            set { inner.Instance = value; }
        }

        public override bool TryMatch(out Out value)
        {
            if (inner.TryMatch(out value))
            {
                return true;
            }
            var instance = Instance;
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