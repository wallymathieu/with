using With.SwitchPlumbing;
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace With
{
    public static class RegexMatchSwitchExtensions
    {
        public static IMatchSwitch<string, NothingOrPrepared> Regex<NothingOrPrepared>(this IMatchSwitch<string, NothingOrPrepared> that,
          string expected, 
            Action<Match> result)
        {
            var regex = new Regex(expected);
            return new MatchSwitchFunc<string, NothingOrPrepared>(that,
                regex.IsMatch,
                F.ReturnDefault<string, NothingOrPrepared>((i) => result(regex.Match(i))));
        }

        public static IMatchSwitch<string, NothingOrPrepared> Regex<NothingOrPrepared>(this IMatchSwitch<string, NothingOrPrepared> that, 
            string expected, 
            Action result)
        {
            var regex = new Regex(expected);
            return new MatchSwitchFunc<string, NothingOrPrepared>(that, 
                regex.IsMatch, 
                F.ReturnDefault<string, NothingOrPrepared>(F.IgnoreInput<string>(result)));
        }

        public static IMatchSwitch<string, Out> Regex<Out>(this IMatchSwitch<string, Out> that, 
            string expected, 
            Func<Out> result)
        {
            var regex = new Regex(expected);
            return new MatchSwitchFunc<string, Out>(that, 
                regex.IsMatch, 
                F.IgnoreInput<string,Out>(result));
        }

        public static IMatchSwitch<string, Out> Regex<Out>(this IMatchSwitch<string, Out> that, 
            string expected, 
            Func<Match, Out> result)
        {
            var regex = new Regex(expected);
            return new MatchSwitchFunc<string, Out>(that, regex.IsMatch, (i) => result(regex.Match(i)));
        }
    }
}
