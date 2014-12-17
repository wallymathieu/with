using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace With.Destructure
{
    public static class TupleExtensions
    {
        public static Tuple<T> Let<T>(
    this Tuple<T> that, Action<T> action)
        {
            action(that.Item1);
            return that;
        }
        public static Tuple<T, T1> Let<T, T1>(
this Tuple<T, T1> that, Action<T, T1> action)
        {
            action(that.Item1, that.Item2);
            return that;
        }
        public static Tuple<T, T1, T2> Let<T, T1, T2>(
this Tuple<T, T1, T2> that, Action<T, T1, T2> action)
        {
            action(that.Item1, that.Item2, that.Item3);
            return that;
        }


        public static TRet Let<T, TRet>(
this Tuple<T> that, Func<T, TRet> action)
        {
            return action(that.Item1);
        }

        public static TRet Let<T, T1, TRet>(
this Tuple<T, T1> that, Func<T, T1, TRet> action)
        {
            return action(that.Item1, that.Item2);
        }

        public static TRet Let<T, T1, T2, TRet>(
this Tuple<T, T1, T2> that, Func<T, T1, T2, TRet> action)
        {
            return action(that.Item1, that.Item2, that.Item3);
        }

    }
}
