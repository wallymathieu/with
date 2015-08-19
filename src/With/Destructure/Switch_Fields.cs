using System;
using System.Linq;

namespace With.Destructure
{
    using SwitchPlumbing;

    public static partial class Switch_Fields
    {
        public static T Fields<T, In, Out>(this IMatchCollector<T, In, Out> that, Action<MatchFields<In, Out>> fields)
        {
            var m = new MatchFields<In, Out>();
            m.Fields = true;
            fields(m);
            return that.Add(new SwitchMatchFields<In, Out>(m.Funcs.ToArray(), m.TypeOfFields));
        }

        public static T Properties<T, In, Out>(this IMatchCollector<T, In, Out> that, Action<MatchFields<In, Out>> fields)
        {
            var m = new MatchFields<In, Out>();
            m.Properties = true;
            fields(m);
            return that.Add( new SwitchMatchFields<In, Out>(m.Funcs.ToArray(), m.TypeOfFields));
        }

        /// <summary>
        /// 
        /// </summary>
        public static MatchFields<In, Out> IncludeAll<In, Out>(
            this MatchFields<In, Out> that)
        {
            that.Methods = true;
            that.Fields = true;
            that.Properties = true;
            return that;
        }
        public static MatchFields<In, Out> IncludeMethods<In, Out>(
            this MatchFields<In, Out> that)
        {
            that.Methods = true;
            return that;
        }
        public static MatchFields<In, Out> IncludeFields<In, Out>(
            this MatchFields<In, Out> that)
        {
            that.Fields = true;
            return that;
        }
        public static MatchFields<In, Out> IncludeProperties<In, Out>(
            this MatchFields<In, Out> that)
        {
            that.Properties = true;
            return that;
        }
        public static MatchFields<In, Out> Case<T, In, Out>(
            this MatchFields<In, Out> that, Func<T, Out> func)
        {
            that.Add(func);
            return that;
        }
        public static MatchFields<In, Out> Case<T, T2, In, Out>(
            this MatchFields<In, Out> that, Func<T, T2, Out> func)
        {
            that.Add(func);
            return that;
        }
        public static MatchFields<In, Out> Case<T, T2, T3, In, Out>(
            this MatchFields<In, Out> that, Func<T, T2, T3, Out> func)
        {
            that.Add(func);
            return that;
        }
        public static MatchFields<In, Out> Case<T, T2, T3, T4, In, Out>(
            this MatchFields<In, Out> that, Func<T, T2, T3, T4, Out> func)
        {
            that.Add(func);
            return that;
        }
        public static MatchFields<In, Out> Case<T, T2, T3, T4, T5, In, Out>(
            this MatchFields<In, Out> that, Func<T, T2, T3, T4, T5, Out> func)
        {
            that.Add(func);
            return that;
        }
        public static MatchFields<In, Out> Case<T, T2, T3, T4, T5, T6, In, Out>(
            this MatchFields<In, Out> that, Func<T, T2, T3, T4, T5, T6, Out> func)
        {
            that.Add(func);
            return that;
        }
    }
}
