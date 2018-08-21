using System;
using System.Collections.Generic;
using System.Linq;

namespace With.Collections
{
    public static class LinqExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="n"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> Cycle<T>(this IEnumerable<T> collection, int? n = null)
        {
            while (n == null || n-- > 0)
            {
                foreach (var item in collection)
                {
                    yield return item;
                }
            }
        }
        /// <summary>
        /// The partition function takes a predicate and a collection and returns the pair of collections of elements which do and do not satisfy the predicate.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="predicate"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Partition<T> Partition<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            var groups = collection.GroupBy(predicate).ToArray();
            var trueArray = groups.SingleOrDefault(g => g.Key.Equals(true))?.ToArray() ?? new T[0];
            var falseArray = groups.SingleOrDefault(g => g.Key.Equals(false))?.ToArray() ?? new T[0];
            return new Partition<T>(trueArray, falseArray);
        }
    }
}
