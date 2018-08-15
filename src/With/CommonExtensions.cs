using System;
using System.Collections.Generic;
using System.Linq;
namespace With
{
    using Collections;
    /// <summary>
    /// Common extensions
    /// </summary>
    public static class CommonExtensions
    {
        /// <summary>
        /// Execute an action on the object. Return value is the object.
        /// </summary>
        public static T Tap<T>(this T value, Action<T> action)
        {
            action?.Invoke(value);
            return value;
        }

        /// <summary>
        /// Execute a func on the object. Return value is the result from the func.
        /// </summary>
        public static TResult Yield<T, TResult>(this T value, Func<T, TResult> func) => func(value);

        /// <summary>
        /// Sorts the elements in ascending order by using a compare function
        /// </summary>
        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> enumerable, Func<T, T, int> compare) => 
            enumerable.OrderBy(t => t, Comparer.Create(compare));

        /// <summary>
        /// Returns an an <see cref="System.Collections.Generic.IEnumerable{T}"/> by doing a yield return of the values of the <see cref="System.Collections.Generic.IEnumerator{T}"/>
        /// </summary>
        public static IEnumerable<T> Yield<T>(this IEnumerator<T> enumerator)
        {
            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }
    }

}

