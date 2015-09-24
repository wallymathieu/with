using System;
using System.Collections.Generic;

namespace With.SwitchPlumbing
{
    public abstract class MatchCollector<T, In, Out>
    {
        public abstract T Add(IMatcher<In, Out> m);

        public T Case(In expected, Out result)
        {
            return this.Add(new MatchSingle<In, Out>(
                expected,
                F.IgnoreInput<In, Out>(result)));
        }

        public T Case(
            IEnumerable<In> expected, Out result)
        {
            return this.Add(new Match<In, Out>(
                expected,
                F.IgnoreInput<In, Out>(result)));
        }

        public T Case(
            Func<In, bool> expected, Out result)
        {
            return this.Add(new Match<In, Out>(
                expected,
                F.IgnoreInput<In, Out>(result)));
        }
        public T Case(
            In expected, Func<In, Out> result)
        {
            return this.Add(new MatchSingle<In, Out>(
                expected, result));
        }
        public T Case(
            In expected, Func<Out> result)
        {
            return this.Add(new MatchSingle<In, Out>(
                expected,
                F.IgnoreInput<In, Out>(result)));
        }
        public T Case(
            IEnumerable<In> expected,
            Func<In, Out> result)
        {
            return this.Add(new Match<In, Out>(
                expected,
                result));
        }
        public T Case(
            IEnumerable<In> expected,
            Func<Out> result)
        {
            return this.Add(new Match<In, Out>(
                expected,
                F.IgnoreInput<In, Out>(result)));
        }
        public T Case(
            Func<In, bool> expected,
            Func<In, Out> result)
        {
            return this.Add(new Match<In, Out>(
                expected,
                result));
        }
        public T Case(
            Func<In, bool> expected,
            Func<Out> result)
        {
            return this.Add(new Match<In, Out>(
                expected,
                F.IgnoreInput<In, Out>(result)));
        }

        public T Else(
            Func<In, Out> result)
        {
            return this.Add(new MatchElse<In, Out>(result));
        }

        public T Case<On>(Func<On, Out> func)
            where On : In
        {
            return this.Add(new MatchType<On, In, Out>(func));
        }

    }
}