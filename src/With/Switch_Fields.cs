using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using With.SwitchPlumbing;

namespace With
{
    public static class Switch_Fields
    {
        public static ISwitch<In, Out> Fields<T, In, Out>(
            this ISwitch<In, Out> that, Func<T, Out> func)
        {
            return new MatchFields<In, Out>(that, func);
        }
        public static ISwitch<In, Out> Fields<T, T2, In, Out>(
            this ISwitch<In, Out> that, Func<T, T2, Out> func)
        {
            return new MatchFields<In, Out>(that, func);
        }
        public static ISwitch<In, Out> Fields<T, T2, T3, In, Out>(
            this ISwitch<In, Out> that, Func<T, T2, T3, Out> func)
        {
            return new MatchFields<In, Out>(that, func);
        }
        public static ISwitch<In, Out> Fields<T, T2, T3, T4, In, Out>(
            this ISwitch<In, Out> that, Func<T, T2, T3, T4, Out> func)
        {
            return new MatchFields<In, Out>(that, func);
        }
        public static ISwitch<In, Out> Fields<T, T2, T3, T4, T5, In, Out>(
            this ISwitch<In, Out> that, Func<T, T2, T3, T4, T5, Out> func)
        {
            return new MatchFields<In, Out>(that, func);
        }
        public static ISwitch<In, Out> Fields<T, T2, T3, T4, T5, T6, In, Out>(
            this ISwitch<In, Out> that, Func<T, T2, T3, T4, T5, T6, Out> func)
        {
            return new MatchFields<In, Out>(that, func);
        }
    }
}
