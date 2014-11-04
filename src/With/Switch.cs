using With.SwitchPlumbing;
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

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

		public static IMatchSwitch<Ingoing> Case <Ingoing>(this IMatchSwitch<Ingoing> that, Ingoing expected, Action result)
		{
			return new MatchSwitchSingle<Ingoing> (that, expected,result);
		}

		public static IMatchSwitch<Ingoing> Case<Ingoing> (this IMatchSwitch<Ingoing> that, IEnumerable<Ingoing> expected, Action<Ingoing> result)
		{
			return new MatchSwitchArray<Ingoing> (that, expected, result);
		}

		public static void Else <Ingoing> (this IMatchSwitch<Ingoing> that,Action<Ingoing> result)
		{
			if (!that.TryMatch ())
				result (that.Instance);
		}

		public static IMatchSwitch<Ingoing> Match <Ingoing>(Ingoing value)
		{
			return new MatchSwitch<Ingoing>().Tap(c=>c.Instance =value);
		}

		public static IMatchSwitch<Ingoing,Outgoing> Case <Ingoing,Outgoing>(this IMatchSwitch<Ingoing,Outgoing> that,Ingoing expected, Func<Outgoing> result)
		{
			throw new NotImplementedException ();
		}

		public static IMatchSwitch<Ingoing,Outgoing> Case <Ingoing,Outgoing>(this IMatchSwitch<Ingoing,Outgoing> that,IEnumerable<Ingoing> expected, Func<Ingoing,Outgoing> result)
		{
			throw new NotImplementedException ();
		}

		public static MatchSwitch<Ingoing,Outgoing> Else<Ingoing,Outgoing> (this IMatchSwitch<Ingoing,Outgoing> that,Func<Ingoing,Outgoing> result)
		{
			throw new NotImplementedException ();
		}

		public static IMatchSwitch<Ingoing,Outgoing> Match<Ingoing,Outgoing> (Ingoing value)
		{
			return new MatchSwitch<Ingoing,Outgoing>().Tap(c=>c.Instance = value);
		}
    }
}
