using System;
using System.Collections.Generic;
using System.Linq;

namespace With
{
    public static class BatchExtensions
    {
        public static IEnumerable<IEnumerable<T>> BatchesOf<T>(this IEnumerable<T> xs, int count)
        {
            var enumerator = xs.GetEnumerator();
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

