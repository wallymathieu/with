using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using With.Destructure;
using With.SwitchPlumbing;

namespace With.Destructure
{
    public static class Switch_Fields
    {
        public static ISwitch<In, Out> Fields<In, Out>(this ISwitch<In, Out> that, Action<MatchFields<In,Out>> fields)
        {
            var m = new MatchFields<In, Out>();
            fields(m);
            return new SwitchMatchFields<In, Out>(that, m.Funcs.ToArray());
        }


        public static MatchFields<In, Out> Fields<T, In, Out>(
            this MatchFields<In, Out> that, Func<T, Out> func)
        {
            that.Funcs.Add(func);
            return that;
        }
        public static MatchFields<In, Out> Fields<T, T2, In, Out>(
            this MatchFields<In, Out> that, Func<T, T2, Out> func)
        {
            that.Funcs.Add(func);
            return that;
        }
        public static MatchFields<In, Out> Fields<T, T2, T3, In, Out>(
            this MatchFields<In, Out> that, Func<T, T2, T3, Out> func)
        {
            that.Funcs.Add(func);
            return that;
        }
        public static MatchFields<In, Out> Fields<T, T2, T3, T4, In, Out>(
            this MatchFields<In, Out> that, Func<T, T2, T3, T4, Out> func)
        {
            that.Funcs.Add(func);
            return that;
        }
        public static MatchFields<In, Out> Fields<T, T2, T3, T4, T5, In, Out>(
            this MatchFields<In, Out> that, Func<T, T2, T3, T4, T5, Out> func)
        {
            that.Funcs.Add(func);
            return that;
        }
        public static MatchFields<In, Out> Fields<T, T2, T3, T4, T5, T6, In, Out>(
            this MatchFields<In, Out> that, Func<T, T2, T3, T4, T5, T6, Out> func)
        {
            that.Funcs.Add(func);
            return that;
        }
    }
}
