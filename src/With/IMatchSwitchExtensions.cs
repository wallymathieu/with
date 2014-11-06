using With.SwitchPlumbing;
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace With
{
    public static class IMatchSwitchExtensions
    {
        public static IMatchSwitch<In, NothingOrPrepared> Case<In, NothingOrPrepared>(this IMatchSwitch<In, NothingOrPrepared> that, 
            In expected, Action<In> result)
        {
            return new MatchSwitchSingle<In, NothingOrPrepared>(that, 
                expected,
                F.ReturnDefault<In, NothingOrPrepared>(result));
        }
        public static IMatchSwitch<In, NothingOrPrepared> Case<In, NothingOrPrepared>(this IMatchSwitch<In, NothingOrPrepared> that, 
            In expected, Action result)
        {
            return new MatchSwitchSingle<In, NothingOrPrepared>(that, 
                expected,
                F.ReturnDefault<In, NothingOrPrepared>(F.IgnoreInput<In>(result)));
        }

        public static IMatchSwitch<In, NothingOrPrepared> Case<In, NothingOrPrepared>(this IMatchSwitch<In, NothingOrPrepared> that, 
            IEnumerable<In> expected, Action<In> result)
        {
            return new MatchSwitchFunc<In, NothingOrPrepared>(that, 
                expected,
                F.ReturnDefault<In, NothingOrPrepared>(result));
        }
        public static IMatchSwitch<In, NothingOrPrepared> Case<In, NothingOrPrepared>(this IMatchSwitch<In, NothingOrPrepared> that, 
            IEnumerable<In> expected, Action result)
        {
            return new MatchSwitchFunc<In, NothingOrPrepared>(that, 
                expected,
                F.ReturnDefault<In, NothingOrPrepared>(F.IgnoreInput<In>(result)));
        }
        public static IMatchSwitch<In, NothingOrPrepared> Case<In, NothingOrPrepared>(this IMatchSwitch<In, NothingOrPrepared> that, 
            Func<In, bool> expected, Action<In> result)
        {
            return new MatchSwitchFunc<In, NothingOrPrepared>(that, 
                expected,
                F.ReturnDefault<In, NothingOrPrepared>(result));
        }
        public static IMatchSwitch<In, NothingOrPrepared> Case<In, NothingOrPrepared>(this IMatchSwitch<In, NothingOrPrepared> that, 
            Func<In, bool> expected, Action result)
        {
            return new MatchSwitchFunc<In, NothingOrPrepared>(that, 
                expected,
                F.ReturnDefault<In, NothingOrPrepared>((i) => result()));
        }
        public static void Else<In>(this IMatchSwitch<In, Nothing> that, 
            Action<In> result)
        {
            var els = new MatchSwitchElse<In, Nothing>(that, 
                F.ReturnDefault<In, Nothing>(result));
            Nothing value;
            els.TryMatch(out value);
        }
        public static IMatchSwitch<In, Prepared> Else<In>(this IMatchSwitch<In, Prepared> that, 
            Action<In> result)
        {
            return new MatchSwitchElse<In, Prepared>(that, 
                F.ReturnDefault<In, Prepared>(result));
        }
        public static IMatchSwitch<In, Out> Case<In, Out>(this IMatchSwitch<In, Out> that, 
            In expected, Func<In, Out> result)
        {
            return new MatchSwitchSingle<In, Out>(that, 
                expected, result);
        }
        public static IMatchSwitch<In, Out> Case<In, Out>(this IMatchSwitch<In, Out> that, 
            In expected, Func<Out> result)
        {
            return new MatchSwitchSingle<In, Out>(that, 
                expected, 
                F.IgnoreInput<In, Out>(result));
        }
        public static IMatchSwitch<In, Out> Case<In, Out>(this IMatchSwitch<In, Out> that, 
            IEnumerable<In> expected, 
            Func<In, Out> result)
        {
            return new MatchSwitchFunc<In, Out>(that, 
                expected, 
                result);
        }
        public static IMatchSwitch<In, Out> Case<In, Out>(this IMatchSwitch<In, Out> that, 
            IEnumerable<In> expected,
            Func<Out> result)
        {
            return new MatchSwitchFunc<In, Out>(that, 
                expected, 
                F.IgnoreInput<In, Out>(result));
        }
        public static IMatchSwitch<In, Out> Case<In, Out>(this IMatchSwitch<In, Out> that, 
            Func<In, bool> expected,
            Func<In, Out> result)
        {
            return new MatchSwitchFunc<In, Out>(that, 
                expected, 
                result);
        }
        public static IMatchSwitch<In, Out> Case<In, Out>(this IMatchSwitch<In, Out> that, 
            Func<In, bool> expected,
            Func<Out> result)
        {
            return new MatchSwitchFunc<In, Out>(that, 
                expected, 
                F.IgnoreInput<In, Out>(result));
        }
        public static IMatchSwitch<In, Out> Else<In, Out>(this IMatchSwitch<In, Out> that, 
            Func<In, Out> result)
        {
            return new MatchSwitchElse<In, Out>(that, result);
        }

        public static Out Result<In, Out>(this IMatchSwitch<In, Out> that)
        {
            return that;
        }
    }
}
