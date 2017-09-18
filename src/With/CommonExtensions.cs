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
            action(value);
            return value;
        }

        /// <summary>
        /// Execute a func on the object. Return value is the result from the func.
        /// </summary>
        public static TResult Yield<T, TResult>(this T value, Func<T, TResult> func)
        {
            return func(value);
        }

        /// <summary>
        /// Concatenates the enumerable collection using the separator between each element.
        /// </summary>
        public static string Join(this IEnumerable<string> enumerable, string separator)
        {
            return string.Join(separator, enumerable);
        }

        /// <summary>
        /// Sorts the elements in ascending order by using a compare function
        /// </summary>
        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> enumerable, Func<T, T, int> compare)
        {
            return enumerable.OrderBy(t => t, Comparer.Create(compare));
        }

        /// <summary>
        /// Gets the next value in the <see cref="System.Collections.Generic.IEnumerator{T}"/> or throws an <see cref="OutOfRangeException"/> if there are no more elements.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerator"></param>
        /// <returns></returns>
        public static T GetNext<T>(this IEnumerator<T> enumerator)
        {
            if (enumerator.MoveNext())
            {
                return enumerator.Current;
            }
            throw new OutOfRangeException();
        }

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

