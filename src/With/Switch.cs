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

		public static PreparedTypeSwitch On(object instance)
        {
			return new PreparedTypeSwitch().Tap(tc=>tc.Instance= instance);
        }

        public static PreparedTypeSwitch On()
        {
            return new PreparedTypeSwitch();
        }

        public static IMatchSwitch<Ingoing, Nothing> Match<Ingoing>(Ingoing value)
        {
            return new MatchSwitch<Ingoing, Nothing>().Tap(c => c.Instance = value);
        }
        public static IMatchSwitch<Ingoing, Outgoing> Match<Ingoing, Outgoing>(Ingoing value)
        {
            return new MatchSwitch<Ingoing, Outgoing>().Tap(c => c.Instance = value);
        }

        public static IMatchSwitch<Ingoing, Nothing> Match<Ingoing>()
        {
            return new MatchSwitch<Ingoing, Nothing>();
        }
        public static IMatchSwitch<Ingoing, Outgoing> Match<Ingoing, Outgoing>()
        {
            return new MatchSwitch<Ingoing, Outgoing>();
        }
    }


    public static class IMatchSwitchExtensions
    {
        public static IMatchSwitch<Ingoing, NothingOrPrepared> Case<Ingoing, NothingOrPrepared>(this IMatchSwitch<Ingoing, NothingOrPrepared> that, Ingoing expected, Action<Ingoing> result)
        {
            return new MatchSwitchSingle<Ingoing, NothingOrPrepared>(that, expected, ReturnDefault<Ingoing, NothingOrPrepared>(result));
        }
        public static IMatchSwitch<Ingoing, NothingOrPrepared> Case<Ingoing, NothingOrPrepared>(this IMatchSwitch<Ingoing, NothingOrPrepared> that, Ingoing expected, Action result)
        {
            return new MatchSwitchSingle<Ingoing, NothingOrPrepared>(that, expected, ReturnDefault<Ingoing, NothingOrPrepared>((i) => result()));
        }

        public static IMatchSwitch<Ingoing, NothingOrPrepared> Case<Ingoing, NothingOrPrepared>(this IMatchSwitch<Ingoing, NothingOrPrepared> that, IEnumerable<Ingoing> expected, Action<Ingoing> result)
        {
            return new MatchSwitchFunc<Ingoing, NothingOrPrepared>(that, expected, ReturnDefault<Ingoing, NothingOrPrepared>(result));
        }
        public static IMatchSwitch<Ingoing, NothingOrPrepared> Case<Ingoing, NothingOrPrepared>(this IMatchSwitch<Ingoing, NothingOrPrepared> that, IEnumerable<Ingoing> expected, Action result)
        {
            return new MatchSwitchFunc<Ingoing, NothingOrPrepared>(that, expected, ReturnDefault<Ingoing, NothingOrPrepared>((i) => result()));
        }
        public static IMatchSwitch<Ingoing, NothingOrPrepared> Case<Ingoing, NothingOrPrepared>(this IMatchSwitch<Ingoing, NothingOrPrepared> that, Func<Ingoing, bool> expected, Action<Ingoing> result)
        {
            return new MatchSwitchFunc<Ingoing, NothingOrPrepared>(that, expected, ReturnDefault<Ingoing, NothingOrPrepared>(result));
        }
        public static IMatchSwitch<Ingoing, NothingOrPrepared> Case<Ingoing, NothingOrPrepared>(this IMatchSwitch<Ingoing, NothingOrPrepared> that, Func<Ingoing, bool> expected, Action result)
        {
            return new MatchSwitchFunc<Ingoing, NothingOrPrepared>(that, expected, ReturnDefault<Ingoing, NothingOrPrepared>((i) => result()));
        }
        public static IMatchSwitch<string, NothingOrPrepared> Regex<NothingOrPrepared>(this IMatchSwitch<string, NothingOrPrepared> that, string expected, Action<Match> result)
        {
            var regex = new Regex(expected);
            return new MatchSwitchFunc<string, NothingOrPrepared>(that, regex.IsMatch, ReturnDefault<string,NothingOrPrepared>( (i) => result(regex.Match(i))));
        }
        public static IMatchSwitch<string, NothingOrPrepared> Regex<NothingOrPrepared>(this IMatchSwitch<string, NothingOrPrepared> that, string expected, Action result)
        {
            return new MatchSwitchFunc<string, NothingOrPrepared>(that, new Regex(expected).IsMatch, ReturnDefault<string, NothingOrPrepared>((s) => result()));
        }

        private static Func<T, TReturn> ReturnDefault<T, TReturn>(Action<T> action)
        {
            return (incoming) =>
            {
                action(incoming);
                return default(TReturn);
            };
        }

        public static void Else<Ingoing>(this IMatchSwitch<Ingoing, Nothing> that, Action<Ingoing> result)
        {
            var els = new MatchSwitchElse<Ingoing, Nothing>(that, ReturnDefault<Ingoing, Nothing>(result));
            Nothing value;
            els.TryMatch(out value);
        }

        public static IMatchSwitch<Ingoing, Prepared> Else<Ingoing>(this IMatchSwitch<Ingoing, Prepared> that, Action<Ingoing> result)
        {
            return new MatchSwitchElse<Ingoing, Prepared>(that, ReturnDefault<Ingoing, Prepared>(result));
        }

        public static IMatchSwitch<Ingoing, Outgoing> Case<Ingoing, Outgoing>(this IMatchSwitch<Ingoing, Outgoing> that, Ingoing expected, Func<Ingoing, Outgoing> result)
        {
            return new MatchSwitchSingle<Ingoing, Outgoing>(that, expected, result);
        }

        public static IMatchSwitch<Ingoing, Outgoing> Case<Ingoing, Outgoing>(this IMatchSwitch<Ingoing, Outgoing> that, Ingoing expected, Func<Outgoing> result)
        {
            return new MatchSwitchSingle<Ingoing, Outgoing>(that, expected, (i) => result());
        }
        public static IMatchSwitch<string, Outgoing> Regex<Outgoing>(this IMatchSwitch<string, Outgoing> that, string expected, Func<Match, Outgoing> result)
        {
            var regex = new Regex(expected);
            return new MatchSwitchFunc<string, Outgoing>(that, regex.IsMatch, (i)=>result(regex.Match(i)));
        }
        public static IMatchSwitch<string, Outgoing> Regex<Outgoing>(this IMatchSwitch<string, Outgoing> that, string expected, Func<Outgoing> result)
        {
            return new MatchSwitchFunc<string, Outgoing>(that, new Regex(expected).IsMatch, (s) => result());
        }
        public static IMatchSwitch<Ingoing, Outgoing> Case<Ingoing, Outgoing>(this IMatchSwitch<Ingoing, Outgoing> that, IEnumerable<Ingoing> expected, Func<Ingoing, Outgoing> result)
        {
            return new MatchSwitchFunc<Ingoing, Outgoing>(that, expected, result);
        }
        public static IMatchSwitch<Ingoing, Outgoing> Case<Ingoing, Outgoing>(this IMatchSwitch<Ingoing, Outgoing> that, IEnumerable<Ingoing> expected, Func<Outgoing> result)
        {
            return new MatchSwitchFunc<Ingoing, Outgoing>(that, expected, (i) => result());
        }
        public static IMatchSwitch<Ingoing, Outgoing> Case<Ingoing, Outgoing>(this IMatchSwitch<Ingoing, Outgoing> that, Func<Ingoing, bool> expected, Func<Ingoing, Outgoing> result)
        {
            return new MatchSwitchFunc<Ingoing, Outgoing>(that, expected, result);
        }
        public static IMatchSwitch<Ingoing, Outgoing> Case<Ingoing, Outgoing>(this IMatchSwitch<Ingoing, Outgoing> that, Func<Ingoing, bool> expected, Func<Outgoing> result)
        {
            return new MatchSwitchFunc<Ingoing, Outgoing>(that, expected, (i) => result());
        }
        public static IMatchSwitch<Ingoing, Outgoing> Else<Ingoing, Outgoing>(this IMatchSwitch<Ingoing, Outgoing> that, Func<Ingoing, Outgoing> result)
        {
            return new MatchSwitchElse<Ingoing, Outgoing>(that, result);
        }

        public static Outgoing Result<Ingoing, Outgoing>(this IMatchSwitch<Ingoing, Outgoing> that)
        {
            return that;
        }
    }
}
