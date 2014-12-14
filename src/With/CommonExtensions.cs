using System;
using System.Collections.Generic;

namespace With
{
    public static class CommonExtensions
    {
        public static T Tap<T>(this T that, Action<T> tapaction)
        {
            tapaction(that);
            return that;
        }

        public static TResult Yield<T, TResult>(this T self, Func<T, TResult> action)
        {
            return action(self);
        }

        public static string Join(this IEnumerable<string> that, string delimitor)
        {
            return String.Join(delimitor, that);
        }
    }
}

