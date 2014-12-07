
using System;
using System.Collections.Generic;
using With.SwitchPlumbing;

namespace With
{
    public static partial class Switch
    {

        public static IMatchSwitch<In, Out> Match
            <In, Out>(In value, 
               IEnumerable<In> i0, Func<In, Out> f0,
               IEnumerable<In> i1, Func<In, Out> f1)
        {
            return new MatchSwitch<In, Out>().Tap(c => c.Instance = value)
               .Case(i0, f0)
               .Case(i1, f1);
        }

        public static IMatchSwitch<In, Out> Match
            <In, Out>(In value, 
               IEnumerable<In> i0, Func<In, Out> f0,
               IEnumerable<In> i1, Func<In, Out> f1,
               IEnumerable<In> i2, Func<In, Out> f2)
        {
            return new MatchSwitch<In, Out>().Tap(c => c.Instance = value)
               .Case(i0, f0)
               .Case(i1, f1)
               .Case(i2, f2);
        }

        public static IMatchSwitch<In, Out> Match
            <In, Out>(In value, 
               IEnumerable<In> i0, Func<In, Out> f0,
               IEnumerable<In> i1, Func<In, Out> f1,
               IEnumerable<In> i2, Func<In, Out> f2,
               IEnumerable<In> i3, Func<In, Out> f3)
        {
            return new MatchSwitch<In, Out>().Tap(c => c.Instance = value)
               .Case(i0, f0)
               .Case(i1, f1)
               .Case(i2, f2)
               .Case(i3, f3);
        }

        public static IMatchSwitch<In, Out> Match
            <In, Out>(In value, 
               IEnumerable<In> i0, Func<In, Out> f0,
               IEnumerable<In> i1, Func<In, Out> f1,
               IEnumerable<In> i2, Func<In, Out> f2,
               IEnumerable<In> i3, Func<In, Out> f3,
               IEnumerable<In> i4, Func<In, Out> f4)
        {
            return new MatchSwitch<In, Out>().Tap(c => c.Instance = value)
               .Case(i0, f0)
               .Case(i1, f1)
               .Case(i2, f2)
               .Case(i3, f3)
               .Case(i4, f4);
        }

        public static IMatchSwitch<In, Out> Match
            <In, Out>(In value, 
               IEnumerable<In> i0, Func<In, Out> f0,
               IEnumerable<In> i1, Func<In, Out> f1,
               IEnumerable<In> i2, Func<In, Out> f2,
               IEnumerable<In> i3, Func<In, Out> f3,
               IEnumerable<In> i4, Func<In, Out> f4,
               IEnumerable<In> i5, Func<In, Out> f5)
        {
            return new MatchSwitch<In, Out>().Tap(c => c.Instance = value)
               .Case(i0, f0)
               .Case(i1, f1)
               .Case(i2, f2)
               .Case(i3, f3)
               .Case(i4, f4)
               .Case(i5, f5);
        }

        public static IMatchSwitch<In, Out> Match
            <In, Out>(In value, 
               IEnumerable<In> i0, Func<In, Out> f0,
               IEnumerable<In> i1, Func<In, Out> f1,
               IEnumerable<In> i2, Func<In, Out> f2,
               IEnumerable<In> i3, Func<In, Out> f3,
               IEnumerable<In> i4, Func<In, Out> f4,
               IEnumerable<In> i5, Func<In, Out> f5,
               IEnumerable<In> i6, Func<In, Out> f6)
        {
            return new MatchSwitch<In, Out>().Tap(c => c.Instance = value)
               .Case(i0, f0)
               .Case(i1, f1)
               .Case(i2, f2)
               .Case(i3, f3)
               .Case(i4, f4)
               .Case(i5, f5)
               .Case(i6, f6);
        }

        public static IMatchSwitch<In, Out> Match
            <In, Out>(
               IEnumerable<In> i0, Func<In, Out> f0,
               IEnumerable<In> i1, Func<In, Out> f1)
        {
            return new MatchSwitch<In, Out>()
               .Case(i0, f0)
               .Case(i1, f1);
        }

        public static IMatchSwitch<In, Out> Match
            <In, Out>(
               IEnumerable<In> i0, Func<In, Out> f0,
               IEnumerable<In> i1, Func<In, Out> f1,
               IEnumerable<In> i2, Func<In, Out> f2)
        {
            return new MatchSwitch<In, Out>()
               .Case(i0, f0)
               .Case(i1, f1)
               .Case(i2, f2);
        }

        public static IMatchSwitch<In, Out> Match
            <In, Out>(
               IEnumerable<In> i0, Func<In, Out> f0,
               IEnumerable<In> i1, Func<In, Out> f1,
               IEnumerable<In> i2, Func<In, Out> f2,
               IEnumerable<In> i3, Func<In, Out> f3)
        {
            return new MatchSwitch<In, Out>()
               .Case(i0, f0)
               .Case(i1, f1)
               .Case(i2, f2)
               .Case(i3, f3);
        }

        public static IMatchSwitch<In, Out> Match
            <In, Out>(
               IEnumerable<In> i0, Func<In, Out> f0,
               IEnumerable<In> i1, Func<In, Out> f1,
               IEnumerable<In> i2, Func<In, Out> f2,
               IEnumerable<In> i3, Func<In, Out> f3,
               IEnumerable<In> i4, Func<In, Out> f4)
        {
            return new MatchSwitch<In, Out>()
               .Case(i0, f0)
               .Case(i1, f1)
               .Case(i2, f2)
               .Case(i3, f3)
               .Case(i4, f4);
        }

        public static IMatchSwitch<In, Out> Match
            <In, Out>(
               IEnumerable<In> i0, Func<In, Out> f0,
               IEnumerable<In> i1, Func<In, Out> f1,
               IEnumerable<In> i2, Func<In, Out> f2,
               IEnumerable<In> i3, Func<In, Out> f3,
               IEnumerable<In> i4, Func<In, Out> f4,
               IEnumerable<In> i5, Func<In, Out> f5)
        {
            return new MatchSwitch<In, Out>()
               .Case(i0, f0)
               .Case(i1, f1)
               .Case(i2, f2)
               .Case(i3, f3)
               .Case(i4, f4)
               .Case(i5, f5);
        }

        public static IMatchSwitch<In, Out> Match
            <In, Out>(
               IEnumerable<In> i0, Func<In, Out> f0,
               IEnumerable<In> i1, Func<In, Out> f1,
               IEnumerable<In> i2, Func<In, Out> f2,
               IEnumerable<In> i3, Func<In, Out> f3,
               IEnumerable<In> i4, Func<In, Out> f4,
               IEnumerable<In> i5, Func<In, Out> f5,
               IEnumerable<In> i6, Func<In, Out> f6)
        {
            return new MatchSwitch<In, Out>()
               .Case(i0, f0)
               .Case(i1, f1)
               .Case(i2, f2)
               .Case(i3, f3)
               .Case(i4, f4)
               .Case(i5, f5)
               .Case(i6, f6);
        }

    }
}