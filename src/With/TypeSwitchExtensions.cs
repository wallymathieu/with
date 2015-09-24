using System;
using With.SwitchPlumbing;

namespace With
{
    public static class TypeSwitchExtensions
    {
        public static T Case<T ,On, In>(
            this MatchCollector<T, In, Nothing> that, Action<On> func)
            where On : In
        {
            return that.Add(new MatchType<On, In, Nothing>(F.ReturnDefault<On, Nothing>(func)));
        }
        
    }
}
