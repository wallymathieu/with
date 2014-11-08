using System;
using With.SwitchPlumbing;

namespace With
{
    public static class TypeSwitchExtensions
    {
        public static PreparedTypeSwitch<In, Out> Case<In, Out>(
            this IPreparedTypeSwitch that, Func<In, Out> func)
        {
            return new PreparedTypeSwitch<In, Out>(that, func);
        }

        public static PreparedTypeSwitch<object, Out> Else<In,Out>(
            this  PreparedTypeSwitch<In, Out> that, Func<object, Out> func)
        {
            return new PreparedTypeSwitch<object, Out>(that, func);
        }
    }
}
