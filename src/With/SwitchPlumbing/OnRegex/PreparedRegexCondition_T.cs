using System;
using System.Text.RegularExpressions;

namespace With.SwitchPlumbing
{
    
    public class PreparedRegexCondition<TRet> : RegexCondition<TRet>
    {
		private readonly PreparedRegexCondition _preparedSwitch;

		public PreparedRegexCondition(PreparedRegexCondition preparedSwitch, string regex, Func<Match, TRet> @case)
            : base(preparedSwitch, regex, @case)
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
