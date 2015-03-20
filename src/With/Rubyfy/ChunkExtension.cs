using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace With.Rubyfy
{
    public static class ChunkExtension
    {
        internal class Chunks<TKey, T> : IGrouping<TKey, T>
        {
            public Chunks(TKey key)
            {
                Key = key;
                Enumerable = new List<T>();
            }
            public Chunks(TKey key, T firstItem)
            {
                Key = key;
                Enumerable = new List<T>() { { firstItem } };
            }

            public IList<T> Enumerable;
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
                private set;
            }
        }

        public static IEnumerable<IGrouping<TKey, T>> Chunk<TKey, T>(this IEnumerable<T> self, Func<T, TKey> keySelector)
        {
            Chunks<TKey, T> currentChunk = null;
            foreach (var item in self)
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

