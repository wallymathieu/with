using System;
using System.Text.RegularExpressions;

namespace With.SwitchPlumbing
{
    public abstract class RegexSwitch
    {
        public RegexCaseCondition<TRet> Case<TRet>(string regex, Func<Match, TRet> func)
        {
            return new RegexCaseCondition<TRet>(this, regex, func);
        }
        protected internal abstract bool TryGetValue(out object value);

        protected internal abstract string GetString();

    }
}