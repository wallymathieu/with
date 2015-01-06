using System;
using System.Collections.Generic;
using System.Linq;
using With.Collections;
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
            
        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> self, Func<T, T, int> compare)
        {
            return self.OrderBy(t => t, Comparer.Create(compare));
        }
    }

}

