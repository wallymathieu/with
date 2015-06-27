
namespace With
{
using System;
using System.Collections.Generic;
using With.SwitchPlumbing;

    public static partial class Switch
    {

    public static ISwitch<In, Out> Match
        <In, Out>(In value, 
               IEnumerable<In> i0, Func<In, Out> f0,
               IEnumerable<In> i1, Func<In, Out> f1)
    {
        return new Switch<In, Out>().Tap(c => c.Instance = value)
               .Case(i0, f0)
               .Case(i1, f1);
    }

    public static ISwitch<In, Out> Match
        <In, Out>(In value, 
               IEnumerable<In> i0, Func<In, Out> f0,
               IEnumerable<In> i1, Func<In, Out> f1,
               IEnumerable<In> i2, Func<In, Out> f2)
    {
        return new Switch<In, Out>().Tap(c => c.Instance = value)
               .Case(i0, f0)
               .Case(i1, f1)
               .Case(i2, f2);
    }

    public static ISwitch<In, Out> Match
        <In, Out>(In value, 
               IEnumerable<In> i0, Func<In, Out> f0,
               IEnumerable<In> i1, Func<In, Out> f1,
               IEnumerable<In> i2, Func<In, Out> f2,
               IEnumerable<In> i3, Func<In, Out> f3)
    {
        return new Switch<In, Out>().Tap(c => c.Instance = value)
               .Case(i0, f0)
               .Case(i1, f1)
               .Case(i2, f2)
               .Case(i3, f3);
    }

    public static ISwitch<In, Out> Match
        <In, Out>(In value, 
               IEnumerable<In> i0, Func<In, Out> f0,
               IEnumerable<In> i1, Func<In, Out> f1,
               IEnumerable<In> i2, Func<In, Out> f2,
               IEnumerable<In> i3, Func<In, Out> f3,
               IEnumerable<In> i4, Func<In, Out> f4)
    {
        return new Switch<In, Out>().Tap(c => c.Instance = value)
               .Case(i0, f0)
               .Case(i1, f1)
               .Case(i2, f2)
               .Case(i3, f3)
               .Case(i4, f4);
    }

    public static ISwitch<In, Out> Match
        <In, Out>(In value, 
               IEnumerable<In> i0, Func<In, Out> f0,
               IEnumerable<In> i1, Func<In, Out> f1,
               IEnumerable<In> i2, Func<In, Out> f2,
               IEnumerable<In> i3, Func<In, Out> f3,
               IEnumerable<In> i4, Func<In, Out> f4,
               IEnumerable<In> i5, Func<In, Out> f5)
    {
        return new Switch<In, Out>().Tap(c => c.Instance = value)
               .Case(i0, f0)
               .Case(i1, f1)
               .Case(i2, f2)
               .Case(i3, f3)
               .Case(i4, f4)
               .Case(i5, f5);
    }

    public static ISwitch<In, Out> Match
        <In, Out>(In value, 
               IEnumerable<In> i0, Func<In, Out> f0,
               IEnumerable<In> i1, Func<In, Out> f1,
               IEnumerable<In> i2, Func<In, Out> f2,
               IEnumerable<In> i3, Func<In, Out> f3,
               IEnumerable<In> i4, Func<In, Out> f4,
               IEnumerable<In> i5, Func<In, Out> f5,
               IEnumerable<In> i6, Func<In, Out> f6)
    {
        return new Switch<In, Out>().Tap(c => c.Instance = value)
               .Case(i0, f0)
               .Case(i1, f1)
               .Case(i2, f2)
               .Case(i3, f3)
               .Case(i4, f4)
               .Case(i5, f5)
               .Case(i6, f6);
    }

    public static ISwitch<In, Out> Match
        <In, Out>(
               IEnumerable<In> i0, Func<In, Out> f0,
               IEnumerable<In> i1, Func<In, Out> f1)
    {
        return new Switch<In, Out>()
               .Case(i0, f0)
               .Case(i1, f1);
    }

    public static ISwitch<In, Out> Match
        <In, Out>(
               IEnumerable<In> i0, Func<In, Out> f0,
               IEnumerable<In> i1, Func<In, Out> f1,
               IEnumerable<In> i2, Func<In, Out> f2)
    {
        return new Switch<In, Out>()
               .Case(i0, f0)
               .Case(i1, f1)
               .Case(i2, f2);
    }

    public static ISwitch<In, Out> Match
        <In, Out>(
               IEnumerable<In> i0, Func<In, Out> f0,
               IEnumerable<In> i1, Func<In, Out> f1,
               IEnumerable<In> i2, Func<In, Out> f2,
               IEnumerable<In> i3, Func<In, Out> f3)
    {
        return new Switch<In, Out>()
               .Case(i0, f0)
               .Case(i1, f1)
               .Case(i2, f2)
               .Case(i3, f3);
    }

    public static ISwitch<In, Out> Match
        <In, Out>(
               IEnumerable<In> i0, Func<In, Out> f0,
               IEnumerable<In> i1, Func<In, Out> f1,
               IEnumerable<In> i2, Func<In, Out> f2,
               IEnumerable<In> i3, Func<In, Out> f3,
               IEnumerable<In> i4, Func<In, Out> f4)
    {
        return new Switch<In, Out>()
               .Case(i0, f0)
               .Case(i1, f1)
               .Case(i2, f2)
               .Case(i3, f3)
               .Case(i4, f4);
    }

    public static ISwitch<In, Out> Match
        <In, Out>(
               IEnumerable<In> i0, Func<In, Out> f0,
               IEnumerable<In> i1, Func<In, Out> f1,
               IEnumerable<In> i2, Func<In, Out> f2,
               IEnumerable<In> i3, Func<In, Out> f3,
               IEnumerable<In> i4, Func<In, Out> f4,
               IEnumerable<In> i5, Func<In, Out> f5)
    {
        return new Switch<In, Out>()
               .Case(i0, f0)
               .Case(i1, f1)
               .Case(i2, f2)
               .Case(i3, f3)
               .Case(i4, f4)
               .Case(i5, f5);
    }

    public static ISwitch<In, Out> Match
        <In, Out>(
               IEnumerable<In> i0, Func<In, Out> f0,
               IEnumerable<In> i1, Func<In, Out> f1,
               IEnumerable<In> i2, Func<In, Out> f2,
               IEnumerable<In> i3, Func<In, Out> f3,
               IEnumerable<In> i4, Func<In, Out> f4,
               IEnumerable<In> i5, Func<In, Out> f5,
               IEnumerable<In> i6, Func<In, Out> f6)
    {
        return new Switch<In, Out>()
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
namespace With.Destructure
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using With.Destructure;
using With.SwitchPlumbing;
    public static partial class Switch_Tuples
    {
        public static Tuple<T0> Let<T0>(
this Tuple<T0> that, Action<T0> action)
        {
            action(that.Item1);
            return that;
        }
        public static Tuple<T0, T1> Let<T0, T1>(
this Tuple<T0, T1> that, Action<T0, T1> action)
        {
            action(that.Item1, that.Item2);
            return that;
        }
        public static Tuple<T0, T1, T2> Let<T0, T1, T2>(
this Tuple<T0, T1, T2> that, Action<T0, T1, T2> action)
        {
            action(that.Item1, that.Item2, that.Item3);
            return that;
        }
        public static Tuple<T0, T1, T2, T3> Let<T0, T1, T2, T3>(
this Tuple<T0, T1, T2, T3> that, Action<T0, T1, T2, T3> action)
        {
            action(that.Item1, that.Item2, that.Item3, that.Item4);
            return that;
        }
        public static Tuple<T0, T1, T2, T3, T4> Let<T0, T1, T2, T3, T4>(
this Tuple<T0, T1, T2, T3, T4> that, Action<T0, T1, T2, T3, T4> action)
        {
            action(that.Item1, that.Item2, that.Item3, that.Item4, that.Item5);
            return that;
        }
        public static Tuple<T0, T1, T2, T3, T4, T5> Let<T0, T1, T2, T3, T4, T5>(
this Tuple<T0, T1, T2, T3, T4, T5> that, Action<T0, T1, T2, T3, T4, T5> action)
        {
            action(that.Item1, that.Item2, that.Item3, that.Item4, that.Item5, that.Item6);
            return that;
        }
        public static Tuple<T0, T1, T2, T3, T4, T5, T6> Let<T0, T1, T2, T3, T4, T5, T6>(
this Tuple<T0, T1, T2, T3, T4, T5, T6> that, Action<T0, T1, T2, T3, T4, T5, T6> action)
        {
            action(that.Item1, that.Item2, that.Item3, that.Item4, that.Item5, that.Item6, that.Item7);
            return that;
        }
        public static TRet Let<T0, TRet>(
this Tuple<T0> that, Func<T0, TRet> func)
        {
            return func(that.Item1);
        }
        public static TRet Let<T0, T1, TRet>(
this Tuple<T0, T1> that, Func<T0, T1, TRet> func)
        {
            return func(that.Item1, that.Item2);
        }
        public static TRet Let<T0, T1, T2, TRet>(
this Tuple<T0, T1, T2> that, Func<T0, T1, T2, TRet> func)
        {
            return func(that.Item1, that.Item2, that.Item3);
        }
        public static TRet Let<T0, T1, T2, T3, TRet>(
this Tuple<T0, T1, T2, T3> that, Func<T0, T1, T2, T3, TRet> func)
        {
            return func(that.Item1, that.Item2, that.Item3, that.Item4);
        }
        public static TRet Let<T0, T1, T2, T3, T4, TRet>(
this Tuple<T0, T1, T2, T3, T4> that, Func<T0, T1, T2, T3, T4, TRet> func)
        {
            return func(that.Item1, that.Item2, that.Item3, that.Item4, that.Item5);
        }
        public static TRet Let<T0, T1, T2, T3, T4, T5, TRet>(
this Tuple<T0, T1, T2, T3, T4, T5> that, Func<T0, T1, T2, T3, T4, T5, TRet> func)
        {
            return func(that.Item1, that.Item2, that.Item3, that.Item4, that.Item5, that.Item6);
        }
        public static TRet Let<T0, T1, T2, T3, T4, T5, T6, TRet>(
this Tuple<T0, T1, T2, T3, T4, T5, T6> that, Func<T0, T1, T2, T3, T4, T5, T6, TRet> func)
        {
            return func(that.Item1, that.Item2, that.Item3, that.Item4, that.Item5, that.Item6, that.Item7);
        }
    }
}
