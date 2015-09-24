using System.Collections.Generic;
using System.Linq;
using System;

namespace With.SwitchPlumbing
{
    public class Switch<In, Out> : MatchCollector<Switch<In,Out>, In,Out>
    {
        private readonly IMatcher<In, Out>[] matchers=new IMatcher<In, Out>[0];
        public Switch()
        {
        }
        private Switch(IMatcher<In,Out> matcher, IMatcher<In,Out>[] matchers)
        {
            if (matchers == null)
            {
                throw new ArgumentException("missing matchers!");
            }
            this.matchers = matchers.ToList()
                .Tap(l=>l.Add(matcher))
                .ToArray();// copy and add
        }

        public bool TryMatch(In instance, out Out value)
        {
            foreach (var matcher in matchers)
            {
                Out tryValue;
                if (matcher.TryMatch(instance, out tryValue))
                {
                    value = tryValue;
                    return true;
                }
            }
            value = default(Out);
            return false;
        }

        public override Switch<In, Out> Add(IMatcher<In, Out> m)
        {
            return new Switch<In, Out>(m, this.matchers);
        }

        public Out ValueOf(In instance)
        {
            Out value;
            if (TryMatch(instance, out value))
                return value;
            throw new NoMatchFoundException();
        }
    }

}