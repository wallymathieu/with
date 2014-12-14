using With.SwitchPlumbing;
using System;
using System.Text.RegularExpressions;

namespace With
{
    public static class RegexMatchSwitchExtensions
    {
        public static ISwitch<string, NothingOrPrepared> Regex<NothingOrPrepared>(this ISwitch<string, NothingOrPrepared> that,
            string expected, 
            Action<Match> result)
        {
            var regex = new Regex(expected);
            return new Match<string, NothingOrPrepared>(that,
                regex.IsMatch,
                F.ReturnDefault<string, NothingOrPrepared>(i => result(regex.Match(i))));
        }
        public static ISwitch<string, NothingOrPrepared> Regex<NothingOrPrepared>(this ISwitch<string, NothingOrPrepared> that,
            string expected, 
            RegexOptions options,
            Action<Match> result)
        {
            var regex = new Regex(expected, options);
            return new Match<string, NothingOrPrepared>(that,
                regex.IsMatch,
                F.ReturnDefault<string, NothingOrPrepared>(i => result(regex.Match(i))));
        }


        public static ISwitch<string, NothingOrPrepared> Regex<NothingOrPrepared>(this ISwitch<string, NothingOrPrepared> that, 
            string expected, 
            Action result)
        {
            var regex = new Regex(expected);
            return new Match<string, NothingOrPrepared>(that, 
                regex.IsMatch, 
                F.ReturnDefault<string, NothingOrPrepared>(F.IgnoreInput<string>(result)));
        }
        public static ISwitch<string, NothingOrPrepared> Regex<NothingOrPrepared>(this ISwitch<string, NothingOrPrepared> that, 
            string expected, 
            RegexOptions options,
            Action result)
        {
            var regex = new Regex(expected, options);
            return new Match<string, NothingOrPrepared>(that, 
                regex.IsMatch, 
                F.ReturnDefault<string, NothingOrPrepared>(F.IgnoreInput<string>(result)));
        }


        public static ISwitch<string, Out> Regex<Out>(this ISwitch<string, Out> that, 
            string expected, 
            Func<Out> result)
        {
            var regex = new Regex(expected);
            return new Match<string, Out>(that, 
                regex.IsMatch, 
                F.IgnoreInput<string,Out>(result));
        }
        public static ISwitch<string, Out> Regex<Out>(this ISwitch<string, Out> that, 
            string expected, 
            RegexOptions options,
            Func<Out> result)
        {
            var regex = new Regex(expected, options);
            return new Match<string, Out>(that, 
                regex.IsMatch, 
                F.IgnoreInput<string,Out>(result));
        }


        public static ISwitch<string, Out> Regex<Out>(this ISwitch<string, Out> that, 
            string expected, 
            Func<Match, Out> result)
        {
            var regex = new Regex(expected);
            return new Match<string, Out>(that, regex.IsMatch, i => result(regex.Match(i)));
        }
        public static ISwitch<string, Out> Regex<Out>(this ISwitch<string, Out> that, 
            string expected, 
            RegexOptions options,
            Func<Match, Out> result)
        {
            var regex = new Regex(expected, options);
            return new Match<string, Out>(that, regex.IsMatch, i => result(regex.Match(i)));
        }

    }
}
