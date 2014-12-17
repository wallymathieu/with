using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace With.Destructure
{
    public static class ListExtensions
    {
        public static void Let<T>(
this IEnumerable<T> that, Action<T, IEnumerable<T>> action)
        {
            action(that.First(), that.Skip(1));
        }
        public static TRet Let<T, TRet>(
this IEnumerable<T> that, Func<T, IEnumerable<T>, TRet> action)
        {
            return action(that.First(), that.Skip(1));
        }


        public static void Let<T>(
this IEnumerable<T> that, Action<T, T, IEnumerable<T>> action)
        {
            action(that.First(), that.Skip(1).First(), that.Skip(2));
        }
        public static TRet Let<T, TRet>(
this IEnumerable<T> that, Func<T, T, IEnumerable<T>, TRet> action)
        {
            return action(that.First(), that.Skip(1).First(), that.Skip(2));
        }


    }
}
