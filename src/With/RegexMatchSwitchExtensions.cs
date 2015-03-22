using With.SwitchPlumbing;
using System;
using System.Text.RegularExpressions;

namespace With
{
    public static class RegexMatchSwitchExtensions
    {
        public static ISwitch<string, NothingOrPrepared> Regex<NothingOrPrepared>(this ISwitch<string, NothingOrPrepared> that,
    Regex expected,
    Action<Match> result)
        {
            return new Match<string, NothingOrPrepared>(that,
                expected.IsMatch,
                F.ReturnDefault<string, NothingOrPrepared>(i => result(expected.Match(i))));
        }
        public static ISwitch<string, NothingOrPrepared> Regex<NothingOrPrepared>(this ISwitch<string, NothingOrPrepared> that,
            string expected,
            Action<Match> result)
        {
            var regex = new Regex(expected);
            return that.Regex(regex, result);
        }
        public static ISwitch<string, NothingOrPrepared> Regex<NothingOrPrepared>(this ISwitch<string, NothingOrPrepared> that,
            string expected,
            RegexOptions options,
            Action<Match> result)
        {
            var regex = new Regex(expected, options);
            return that.Regex(regex, result);
        }
        public static ISwitch<string, Out> Regex<Out>(this ISwitch<string, Out> that,
            Regex expected,
            Out result)
        {
            return new Match<string, Out>(that,
                expected.IsMatch,
                F.IgnoreInput<string,Out>(result));
        }
        public static ISwitch<string, NothingOrPrepared> Regex<NothingOrPrepared>(this ISwitch<string, NothingOrPrepared> that,
            string expected,
            NothingOrPrepared result)
        {
            var regex = new Regex(expected);
            return that.Regex(regex, result);
        }
        public static ISwitch<string, NothingOrPrepared> Regex<NothingOrPrepared>(this ISwitch<string, NothingOrPrepared> that,
            Regex expected,
            Action result)
        {
            return new Match<string, NothingOrPrepared>(that,
                expected.IsMatch,
                F.ReturnDefault<string, NothingOrPrepared>(F.IgnoreInput<string>(result)));
        }
        public static ISwitch<string, NothingOrPrepared> Regex<NothingOrPrepared>(this ISwitch<string, NothingOrPrepared> that,
            string expected,
            Action result)
        {
            var regex = new Regex(expected);
            return that.Regex(regex, result);
        }
        public static ISwitch<string, NothingOrPrepared> Regex<NothingOrPrepared>(this ISwitch<string, NothingOrPrepared> that,
            string expected,
            RegexOptions options,
            Action result)
        {
            var regex = new Regex(expected, options);
            return that.Regex(regex, result);
        }

        public static ISwitch<string, Out> Regex<Out>(this ISwitch<string, Out> that,
            Regex expected,
            Func<Out> result)
        {
            return new Match<string, Out>(that,
                expected.IsMatch,
                F.IgnoreInput<string, Out>(result));
        }
        public static ISwitch<string, Out> Regex<Out>(this ISwitch<string, Out> that,
            string expected,
            Func<Out> result)
        {
            var regex = new Regex(expected);
            return that.Regex(regex, result);
        }
        public static ISwitch<string, Out> Regex<Out>(this ISwitch<string, Out> that,
            string expected,
            RegexOptions options,
            Func<Out> result)
        {
            var regex = new Regex(expected, options);
            return that.Regex(regex, result);
        }

        public static ISwitch<string, Out> Regex<Out>(this ISwitch<string, Out> that,
            Regex expected,
            Func<Match, Out> result)
        {
            return new Match<string, Out>(that, expected.IsMatch, i => result(expected.Match(i)));
        }
        public static ISwitch<string, Out> Regex<Out>(this ISwitch<string, Out> that,
            string expected,
            Func<Match, Out> result)
        {
            var regex = new Regex(expected);
            return that.Regex(regex, result);
        }
        public static ISwitch<string, Out> Regex<Out>(this ISwitch<string, Out> that,
            string expected,
            RegexOptions options,
            Func<Match, Out> result)
        {
            var regex = new Regex(expected, options);
            return that.Regex(regex, result);
        }
        public static ISwitch<string, Out> Regex<Out>(this ISwitch<string, Out> that,
            string expected,
            RegexOptions options,
            Out result)
        {
            var regex = new Regex(expected, options);
            return that.Regex(regex, result);
        }

    }
}
