using System;
using With.SwitchPlumbing;

namespace With
{
    public static class TypeSwitchExtensions
    {
        public static IMatchSwitch<In, Out> Case<On, In, Out>(
            this IMatchSwitch<In, Out> that, Func<On, Out> func)
            where On:In
        {
            return new PreparedTypeSwitch<On, In, Out>(that, func);
        }
    }
}
