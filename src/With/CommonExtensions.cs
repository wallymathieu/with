using System;
using System.Collections.Generic;
using System.Linq;
namespace With
{
    public static class CommonExtensions
    {
        /// <summary>
        /// Execute an action on the object. Return value is the object.
        /// </summary>
        public static T Tap<T>(this T that, Action<T> tapaction)
        {
            tapaction(that);
            return that;
        }
        
        /// <summary>
        /// Execute a func on the object. Return value is the result from the func.
        /// </summary>
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

