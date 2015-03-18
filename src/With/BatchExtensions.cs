using System.Collections.Generic;
using System.Linq;

namespace With
{
	public static class BatchExtensions
    {
        /// <summary>
        /// Returns sub lists of at most 'count' elements from the IEnumerable
        /// </summary>
        /// <returns>An IEnumerable of IEnumerable with Count less than 'count'</returns>
        /// <param name="enumerable"></param>
        /// <param name="count">The number of elements that should be at most found in each "batch".</param>
        public static IEnumerable<IEnumerable<T>> BatchesOf<T>(this IEnumerable<T> enumerable, int count)
        {
            var enumerator = enumerable.GetEnumerator();
            while (true)
            {
                var list = new List<T>(count);
                for (int i = 0; i < count && enumerator.MoveNext(); i++)
                {
                    list.Add(enumerator.Current);
                }
                if (!list.Any())
                {
                    break;
                }
                yield return list;
            }
        }
    }
}

