using System;
using System.Text.RegularExpressions;

namespace With.SwitchPlumbing
{
    
    public class PreparedRegexSwitchCase<TRet> : RegexCaseCondition<TRet>, IPreparedRegexSwitch
    {
        private readonly IPreparedRegexSwitch _preparedSwitch;

        public PreparedRegexSwitchCase(IPreparedRegexSwitch preparedSwitch, string regex, Func<Match, TRet> @case)
            : base((PreparedRegexSwitch)preparedSwitch, regex, @case)
        {
            _preparedSwitch = preparedSwitch;
        }

        public object ValueOf(string instance)
        {
            SetString(instance);
            return Value();
        }

		public override void SetString(string s)
        {
            _preparedSwitch.SetString(s);
        }

    }

}
