using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace With.Collections
{
    /// <summary>
    /// Extensions on enumerables
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Used to iterate over collection and get the collection elements pairwise.
        /// Yields the result of the application of the map function over each pair.
        /// </summary>
        /// <remarks>
        /// Note that the same element will at most 2 times. For example for
        /// Enumerable.Range(0, 4).Pairwise(Tuple.Create).ToArray() you will get new[] { Tuple.Create(0, 1), Tuple.Create(1, 2), Tuple.Create(2, 3) }
        /// </remarks>
        /// <param name="collection"></param>
        /// <param name="func">The map of pairs</param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public static IEnumerable<TResult> Pairwise<T, TResult>(
            this IEnumerable<T> collection, Func<T, T, TResult> func)
        {
            using (var enumerator = collection.GetEnumerator())
            {

                if (!enumerator.MoveNext())
                {
                    yield break;
                }
                
                var last = enumerator.Current;
                for (; enumerator.MoveNext();)
                {
                    yield return func(last, enumerator.Current);
                    last = enumerator.Current;
                }
            }
        }
        /// <summary>
        /// Yields each element of collection repeatedly n times or forever if null is given.
        /// If a non-positive number is given or the collection is empty, returns an empty enumerable.
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
        
        /// <summary>
        /// Returns a new array that is a one-dimensional flattening of self (recursively).
        ///
        ///That is, for every element that is an array, extract its elements into the new array.
        ///
        ///The optional level argument determines the level of recursion to flatten.
        /// </summary>
        public static IEnumerable Flatten(this IEnumerable self, int? order = null)
        {
            if (order == null || order >= 0)
            {
                foreach (var variable in self)
                {
                    if (variable is IEnumerable enumerable && !(enumerable is string))
                    {
                        foreach (var result in Flatten(enumerable, order != null ? order - 1 : null))
                        {
                            yield return result;
                        }
                    }
                    else
                    {
                        yield return variable;
                    }
                }
            }
            else
            {
                yield return self;
            }
        }

        private class Chunks<TKey, T> : IGrouping<TKey, T>
        {
            public Chunks(TKey key, T firstItem)
            {
                Key = key;
                Enumerable = new List<T>() { { firstItem } };
            }

            public readonly IList<T> Enumerable;
            public IEnumerator<T> GetEnumerator()
            {
                return Enumerable.GetEnumerator();
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return Enumerable.GetEnumerator();
            }
            public TKey Key
            {
                get;
            }
        }
        /// <summary>
        /// Enumerates over the items, chunking them together based on the return value of the block.
        ///
        /// Consecutive elements which return the same block value are chunked together.
        ///
        /// Compare this to GroupBy <see cref="Enumerable.GroupBy{TSource,TKey}(System.Collections.Generic.IEnumerable{TSource},System.Func{TSource,TKey})"/> 
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="keySelector"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<IGrouping<TKey, T>> Chunk<TKey, T>(this IEnumerable<T> collection, Func<T, TKey> keySelector)
        {
            Chunks<TKey, T> currentChunk = null;
            foreach (var item in collection)
            {
                var currentKey = keySelector(item);
                if (null == currentKey)
                {
                    continue;
                }

                if (currentChunk == null)// first element
                {
                    currentChunk = new Chunks<TKey, T>(currentKey, item);
                }
                else
                {
                    if (currentChunk.Key.Equals(currentKey))
                    {
                        currentChunk.Enumerable.Add(item);
                    }
                    else
                    {
                        yield return currentChunk;
                        currentChunk = new Chunks<TKey, T>(currentKey, item);
                    }
                }
            }
            yield return currentChunk;
        }
    }
}
