using With.SwitchPlumbing;
using System;
using System.Text.RegularExpressions;

namespace With
{
    public static class Switch
    {
		public static PreparedTypeSwitch<T, TRet> Case<T, TRet>(this IPreparedTypeSwitch that, Func<T, TRet> func)
		{
			return new PreparedTypeSwitch<T, TRet>(that, func);
		}

		public static PreparedRegexCondition<TRet1> Case<TRet1>(this IPreparedRegexCondition that, string regex, Func<Match, TRet1> func)
		{
			return new PreparedRegexCondition<TRet1>(that, regex, func);
		}

		public static PreparedTypeSwitch On(object instance)
        {
			return new PreparedTypeSwitch().Tap(tc=>tc.Instance= instance);
        }

		public static PreparedRegexCondition Regex(string instance)
        {
			return new PreparedRegexCondition().Tap(c=>c.Instance = instance);
        }

		public static PreparedRegexCondition Regex()
        {
			return new PreparedRegexCondition();
        }


        public static PreparedTypeSwitch On()
        {
            return new PreparedTypeSwitch();
        }

		public static MatchSwitch<Ingoing> Match <Ingoing>(Ingoing value)
		{
			return new MatchSwitch<Ingoing>().Tap(c=>c.SetValue(value));
		}

		public static MatchSwitch<Ingoing,Outgoing> Match<Ingoing,Outgoing> (Ingoing value)
		{
			return new MatchSwitch<Ingoing,Outgoing>().Tap(c=>c.SetValue(value));
		}
    }
}
