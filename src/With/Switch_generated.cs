
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

           public static PreparedTypeSwitch<In1, Out> On
           <In0, In1, Out>(object instance, Func<In0, Out> _case0,
Func<In1, Out> _case1)
        {
           return new PreparedTypeSwitch().Tap(tc => tc.Instance = instance) .Case<In0, Out>(_case0)
.Case<In1, Out>(_case1);
        }

           public static PreparedTypeSwitch<In2, Out> On
           <In0, In1, In2, Out>(object instance, Func<In0, Out> _case0,
Func<In1, Out> _case1,
Func<In2, Out> _case2)
        {
           return new PreparedTypeSwitch().Tap(tc => tc.Instance = instance) .Case<In0, Out>(_case0)
.Case<In1, Out>(_case1)
.Case<In2, Out>(_case2);
        }

           public static PreparedTypeSwitch<In3, Out> On
           <In0, In1, In2, In3, Out>(object instance, Func<In0, Out> _case0,
Func<In1, Out> _case1,
Func<In2, Out> _case2,
Func<In3, Out> _case3)
        {
           return new PreparedTypeSwitch().Tap(tc => tc.Instance = instance) .Case<In0, Out>(_case0)
.Case<In1, Out>(_case1)
.Case<In2, Out>(_case2)
.Case<In3, Out>(_case3);
        }

           public static PreparedTypeSwitch<In4, Out> On
           <In0, In1, In2, In3, In4, Out>(object instance, Func<In0, Out> _case0,
Func<In1, Out> _case1,
Func<In2, Out> _case2,
Func<In3, Out> _case3,
Func<In4, Out> _case4)
        {
           return new PreparedTypeSwitch().Tap(tc => tc.Instance = instance) .Case<In0, Out>(_case0)
.Case<In1, Out>(_case1)
.Case<In2, Out>(_case2)
.Case<In3, Out>(_case3)
.Case<In4, Out>(_case4);
        }

           public static PreparedTypeSwitch<In5, Out> On
           <In0, In1, In2, In3, In4, In5, Out>(object instance, Func<In0, Out> _case0,
Func<In1, Out> _case1,
Func<In2, Out> _case2,
Func<In3, Out> _case3,
Func<In4, Out> _case4,
Func<In5, Out> _case5)
        {
           return new PreparedTypeSwitch().Tap(tc => tc.Instance = instance) .Case<In0, Out>(_case0)
.Case<In1, Out>(_case1)
.Case<In2, Out>(_case2)
.Case<In3, Out>(_case3)
.Case<In4, Out>(_case4)
.Case<In5, Out>(_case5);
        }

           public static PreparedTypeSwitch<In6, Out> On
           <In0, In1, In2, In3, In4, In5, In6, Out>(object instance, Func<In0, Out> _case0,
Func<In1, Out> _case1,
Func<In2, Out> _case2,
Func<In3, Out> _case3,
Func<In4, Out> _case4,
Func<In5, Out> _case5,
Func<In6, Out> _case6)
        {
           return new PreparedTypeSwitch().Tap(tc => tc.Instance = instance) .Case<In0, Out>(_case0)
.Case<In1, Out>(_case1)
.Case<In2, Out>(_case2)
.Case<In3, Out>(_case3)
.Case<In4, Out>(_case4)
.Case<In5, Out>(_case5)
.Case<In6, Out>(_case6);
        }

           public static PreparedTypeSwitch<In1, Out> On
           <In0, In1, Out>(Func<In0, Out> _case0,
Func<In1, Out> _case1)
        {
           return new PreparedTypeSwitch() .Case<In0, Out>(_case0)
.Case<In1, Out>(_case1);
        }

           public static PreparedTypeSwitch<In2, Out> On
           <In0, In1, In2, Out>(Func<In0, Out> _case0,
Func<In1, Out> _case1,
Func<In2, Out> _case2)
        {
           return new PreparedTypeSwitch() .Case<In0, Out>(_case0)
.Case<In1, Out>(_case1)
.Case<In2, Out>(_case2);
        }

           public static PreparedTypeSwitch<In3, Out> On
           <In0, In1, In2, In3, Out>(Func<In0, Out> _case0,
Func<In1, Out> _case1,
Func<In2, Out> _case2,
Func<In3, Out> _case3)
        {
           return new PreparedTypeSwitch() .Case<In0, Out>(_case0)
.Case<In1, Out>(_case1)
.Case<In2, Out>(_case2)
.Case<In3, Out>(_case3);
        }

           public static PreparedTypeSwitch<In4, Out> On
           <In0, In1, In2, In3, In4, Out>(Func<In0, Out> _case0,
Func<In1, Out> _case1,
Func<In2, Out> _case2,
Func<In3, Out> _case3,
Func<In4, Out> _case4)
        {
           return new PreparedTypeSwitch() .Case<In0, Out>(_case0)
.Case<In1, Out>(_case1)
.Case<In2, Out>(_case2)
.Case<In3, Out>(_case3)
.Case<In4, Out>(_case4);
        }

           public static PreparedTypeSwitch<In5, Out> On
           <In0, In1, In2, In3, In4, In5, Out>(Func<In0, Out> _case0,
Func<In1, Out> _case1,
Func<In2, Out> _case2,
Func<In3, Out> _case3,
Func<In4, Out> _case4,
Func<In5, Out> _case5)
        {
           return new PreparedTypeSwitch() .Case<In0, Out>(_case0)
.Case<In1, Out>(_case1)
.Case<In2, Out>(_case2)
.Case<In3, Out>(_case3)
.Case<In4, Out>(_case4)
.Case<In5, Out>(_case5);
        }

           public static PreparedTypeSwitch<In6, Out> On
           <In0, In1, In2, In3, In4, In5, In6, Out>(Func<In0, Out> _case0,
Func<In1, Out> _case1,
Func<In2, Out> _case2,
Func<In3, Out> _case3,
Func<In4, Out> _case4,
Func<In5, Out> _case5,
Func<In6, Out> _case6)
        {
           return new PreparedTypeSwitch() .Case<In0, Out>(_case0)
.Case<In1, Out>(_case1)
.Case<In2, Out>(_case2)
.Case<In3, Out>(_case3)
.Case<In4, Out>(_case4)
.Case<In5, Out>(_case5)
.Case<In6, Out>(_case6);
        }

    }
}