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
        public static T Case<T ,On, In>(
            this IMatchCollector<T, In, Nothing> that, Action<On> func)
            where On : In
        {
            return that.Add(new MatchType<On, In, Nothing>(F.ReturnDefault<On, Nothing>(func)));
        }
        
    }
}
