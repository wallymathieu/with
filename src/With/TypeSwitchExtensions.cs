using System;
using With.SwitchPlumbing;

namespace With
{
    public static class TypeSwitchExtensions
    {
        public static T Case<T ,On, In, Out>(
            this IMatchCollector<T, In, Out> that, Func<On, Out> func)
            where On : In
        {
            return that.Add(new MatchType<On, In, Out>(func));
        }
    }
}
