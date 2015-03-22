using With.SwitchPlumbing;
using System;
using System.Collections.Generic;

namespace With
{
    public static class IMatchSwitchExtensions
    {
        public static ISwitch<In, NothingOrPrepared> Case<In, NothingOrPrepared>(this ISwitch<In, NothingOrPrepared> that,
            In expected, Action<In> result)
        {
            return new MatchSingle<In, NothingOrPrepared>(that,
                expected,
                F.ReturnDefault<In, NothingOrPrepared>(result));
        }

        public static ISwitch<In, Out> Case<In, Out>(this ISwitch<In, Out> that,
            In expected, Out result)
        {
            return new MatchSingle<In, Out>(that,
                expected,
                F.IgnoreInput<In, Out>(result));
        }

        public static ISwitch<In, NothingOrPrepared> Case<In, NothingOrPrepared>(this ISwitch<In, NothingOrPrepared> that,
            In expected, Action result)
        {
            return new MatchSingle<In, NothingOrPrepared>(that,
                expected,
                F.ReturnDefault<In, NothingOrPrepared>(F.IgnoreInput<In>(result)));
        }

        public static ISwitch<In, NothingOrPrepared> Case<In, NothingOrPrepared>(this ISwitch<In, NothingOrPrepared> that,
            IEnumerable<In> expected, Action<In> result)
        {
            return new Match<In, NothingOrPrepared>(that,
                expected,
                F.ReturnDefault<In, NothingOrPrepared>(result));
        }
        public static ISwitch<In, NothingOrPrepared> Case<In, NothingOrPrepared>(this ISwitch<In, NothingOrPrepared> that,
            IEnumerable<In> expected, Action result)
        {
            return new Match<In, NothingOrPrepared>(that,
                expected,
                F.ReturnDefault<In, NothingOrPrepared>(F.IgnoreInput<In>(result)));
        }

        public static ISwitch<In, Out> Case<In, Out>(this ISwitch<In, Out> that,
            IEnumerable<In> expected, Out result)
        {
            return new Match<In, Out>(that,
                expected,
                F.IgnoreInput<In, Out>(result));
        }

        public static ISwitch<In, NothingOrPrepared> Case<In, NothingOrPrepared>(this ISwitch<In, NothingOrPrepared> that,
            Func<In, bool> expected, Action<In> result)
        {
            return new Match<In, NothingOrPrepared>(that,
                expected,
                F.ReturnDefault<In, NothingOrPrepared>(result));
        }
        public static ISwitch<In, NothingOrPrepared> Case<In, NothingOrPrepared>(this ISwitch<In, NothingOrPrepared> that,
            Func<In, bool> expected, Action result)
        {
            return new Match<In, NothingOrPrepared>(that,
                expected,
                F.ReturnDefault<In, NothingOrPrepared>(F.IgnoreInput<In>(result)));
        }

        public static ISwitch<In, Out> Case<In, Out>(this ISwitch<In, Out> that,
            Func<In, bool> expected, Out result)
        {
            return new Match<In, Out>(that,
                expected,
                F.IgnoreInput<In, Out>(result));
        }

        public static void Else<In>(this ISwitch<In, Nothing> that,
            Action<In> result)
        {
            var els = new MatchElse<In, Nothing>(that,
                F.ReturnDefault<In, Nothing>(result));
            Nothing value;
            els.TryMatch(out value);
        }
        public static ISwitch<In, Prepared> Else<In>(this ISwitch<In, Prepared> that,
            Action<In> result)
        {
            return new MatchElse<In, Prepared>(that,
                F.ReturnDefault<In, Prepared>(result));
        }
        public static ISwitch<In, Out> Case<In, Out>(this ISwitch<In, Out> that,
            In expected, Func<In, Out> result)
        {
            return new MatchSingle<In, Out>(that,
                expected, result);
        }
        public static ISwitch<In, Out> Case<In, Out>(this ISwitch<In, Out> that,
            In expected, Func<Out> result)
        {
            return new MatchSingle<In, Out>(that,
                expected,
                F.IgnoreInput<In, Out>(result));
        }
        public static ISwitch<In, Out> Case<In, Out>(this ISwitch<In, Out> that,
            IEnumerable<In> expected,
            Func<In, Out> result)
        {
            return new Match<In, Out>(that,
                expected,
                result);
        }
        public static ISwitch<In, Out> Case<In, Out>(this ISwitch<In, Out> that,
            IEnumerable<In> expected,
            Func<Out> result)
        {
            return new Match<In, Out>(that,
                expected,
                F.IgnoreInput<In, Out>(result));
        }
        public static ISwitch<In, Out> Case<In, Out>(this ISwitch<In, Out> that,
            Func<In, bool> expected,
            Func<In, Out> result)
        {
            return new Match<In, Out>(that,
                expected,
                result);
        }
        public static ISwitch<In, Out> Case<In, Out>(this ISwitch<In, Out> that,
            Func<In, bool> expected,
            Func<Out> result)
        {
            return new Match<In, Out>(that,
                expected,
                F.IgnoreInput<In, Out>(result));
        }

        public static ISwitch<In, Out> Else<In, Out>(this ISwitch<In, Out> that,
            Func<In, Out> result)
        {
            return new MatchElse<In, Out>(that, result);
        }

        public static Out Result<In, Out>(this ISwitch<In, Out> that)
        {
            return that;
        }
    }
}
