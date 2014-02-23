using System;
using System.Text.RegularExpressions;

namespace With.SwitchPlumbing
{
    public interface IPreparedRegexSwitch
    {
        void SetString(string @string);

    }
    public class PreparedRegexSwitch : RegexSwitch,IPreparedRegexSwitch
    {
        private string _string;

        public void SetString(string @string)
        {
            _string = @string;
        }

        protected internal override bool TryGetValue(out object value)
        {
            value = null;
            return false;
        }

        protected internal override string GetString()
        {
            return _string;
        }

        public new PreparedRegexSwitchCase<TRet1> Case<TRet1>(string regex, Func<Match, TRet1> func)
        {
            return new PreparedRegexSwitchCase<TRet1>(this, regex, func);
        }
    }
    public class PreparedRegexSwitchCase<TRet> : RegexCaseCondition<TRet>, IPreparedRegexSwitch
    {
        private readonly IPreparedRegexSwitch _preparedSwitch;

        public PreparedRegexSwitchCase(IPreparedRegexSwitch preparedSwitch, string regex, Func<Match, TRet> @case)
            : base((RegexSwitch)preparedSwitch, regex, @case)
        {
            _preparedSwitch = preparedSwitch;
        }

        public object ValueOf(string instance)
        {
            SetString(instance);
            return Value();
        }

        public new PreparedRegexSwitchCase<TRet1> Case<TRet1>(string regex, Func<Match, TRet1> func)
        {
            return new PreparedRegexSwitchCase<TRet1>(this, regex, func);
        }

        public void SetString(string s)
        {
            _preparedSwitch.SetString(s);
        }

    }

}
