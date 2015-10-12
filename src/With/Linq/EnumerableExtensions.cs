using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace With.Linq
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<TResult> Pairwise<T, TResult>(
       this IEnumerable<T> self, Func<T, T, TResult> func)
        {
            var enumerator = self.GetEnumerator();

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
}
