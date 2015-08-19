using With.SwitchPlumbing;
using System;
using System.Text.RegularExpressions;

namespace With
{
    public static class RegexMatchSwitchExtensions
    {
        public static T Regex<T,NothingOrPrepared>(this IMatchCollector<T,string, NothingOrPrepared> that,
    Regex expected,
    Action<Match> result)
        {
            return that.Add(new Match<string, NothingOrPrepared>(
                expected.IsMatch,
                F.ReturnDefault<string, NothingOrPrepared>(i => result(expected.Match(i)))));
        }
        public static T Regex<T, NothingOrPrepared>(this IMatchCollector<T, string, NothingOrPrepared> that,
            string expected,
            Action<Match> result)
        {
            var regex = new Regex(expected);
            return that.Regex(regex, result);
        }
        public static T Regex<T, NothingOrPrepared>(this IMatchCollector<T,string, NothingOrPrepared> that,
            string expected,
            RegexOptions options,
            Action<Match> result)
        {
            var regex = new Regex(expected, options);
            return that.Regex(regex, result);
        }
        public static T Regex<T, Out>(this IMatchCollector<T, string, Out> that,
            Regex expected,
            Out result)
        {
            return that.Add(new Match<string, Out>(
                expected.IsMatch,
                F.IgnoreInput<string,Out>(result)));
        }
        public static T Regex<T, NothingOrPrepared>(this IMatchCollector<T, string, NothingOrPrepared> that,
            string expected,
            NothingOrPrepared result)
        {
            var regex = new Regex(expected);
            return that.Regex(regex, result);
        }
        public static T Regex<T, NothingOrPrepared>(this IMatchCollector<T, string, NothingOrPrepared> that,
            Regex expected,
            Action result)
        {
            return that.Add(new Match<string, NothingOrPrepared>(
                expected.IsMatch,
                F.ReturnDefault<string, NothingOrPrepared>(F.IgnoreInput<string>(result))));
        }
        public static T Regex<T, NothingOrPrepared>(this IMatchCollector<T, string, NothingOrPrepared> that,
            string expected,
            Action result)
        {
            var regex = new Regex(expected);
            return that.Regex(regex, result);
        }
        public static T Regex<T, NothingOrPrepared>(this IMatchCollector<T, string, NothingOrPrepared> that,
            string expected,
            RegexOptions options,
            Action result)
        {
            var regex = new Regex(expected, options);
            return that.Regex(regex, result);
        }

        public static T Regex<T, Out>(this IMatchCollector<T, string, Out> that,
            Regex expected,
            Func<Out> result)
        {
            return that.Add(new Match<string, Out>(
                expected.IsMatch,
                F.IgnoreInput<string, Out>(result)));
        }
        public static T Regex<T, Out>(this IMatchCollector<T, string, Out> that,
            string expected,
            Func<Out> result)
        {
            var regex = new Regex(expected);
            return that.Regex(regex, result);
        }
        public static T Regex<T, Out>(this IMatchCollector<T, string, Out> that,
            string expected,
            RegexOptions options,
            Func<Out> result)
        {
            var regex = new Regex(expected, options);
            return that.Regex(regex, result);
        }

        public static T Regex<T, Out>(this IMatchCollector<T, string, Out> that,
            Regex expected,
            Func<Match, Out> result)
        {
            return that.Add(new Match<string, Out>(expected.IsMatch, i => result(expected.Match(i))));
        }
        public static T Regex<T, Out>(this IMatchCollector<T, string, Out> that,
            string expected,
            Func<Match, Out> result)
        {
            var regex = new Regex(expected);
            return that.Regex(regex, result);
        }
        public static T Regex<T, Out>(this IMatchCollector<T, string, Out> that,
            string expected,
            RegexOptions options,
            Func<Match, Out> result)
        {
            var regex = new Regex(expected, options);
            return that.Regex(regex, result);
        }
        public static T Regex<T, Out>(this IMatchCollector<T, string, Out> that,
            string expected,
            RegexOptions options,
            Out result)
        {
            var regex = new Regex(expected, options);
            return that.Regex(regex, result);
        }

    }
}
