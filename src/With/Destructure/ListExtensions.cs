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
            var e = that.GetEnumerator();
            action(e.GetNext(), e.Yield());
        }
        public static TRet Let<T, TRet>(
this IEnumerable<T> that, Func<T, IEnumerable<T>, TRet> func)
        {
            var e = that.GetEnumerator();
            return func(e.GetNext(), e.Yield());
        }


        public static void Let<T>(
this IEnumerable<T> that, Action<T, T, IEnumerable<T>> action)
        {
            var e = that.GetEnumerator();
            action(e.GetNext(), e.GetNext(), e.Yield());
        }
        public static TRet Let<T, TRet>(
this IEnumerable<T> that, Func<T, T, IEnumerable<T>, TRet> func)
        {
            var e = that.GetEnumerator();
            return func(e.GetNext(), e.GetNext(), e.Yield());
        }

        public static void Stitch<T>(
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

        public static IEnumerable<TResult> Stitch<T,TResult>(
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
