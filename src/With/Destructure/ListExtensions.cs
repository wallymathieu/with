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

        public static void EachConsequtivePair<T>(
this IEnumerable<T> self, Action<T,T> action)
        {
            var enumerator = self.GetEnumerator();
            enumerator.MoveNext();
            var last = enumerator.Current;
            for (; enumerator.MoveNext(); )
            {
                action(enumerator.Current, last);
                last = enumerator.Current;
            }
        }

        public static IEnumerable<TResult> EachConsequtivePair<T,TResult>(
this IEnumerable<T> self, Func<T,T,TResult> func)
        {
            var enumerator = self.GetEnumerator();
            enumerator.MoveNext();
            var last = enumerator.Current;
            for (; enumerator.MoveNext(); )
            {
                yield return func(enumerator.Current, last);
                last = enumerator.Current;
            }
        }
    }
}
