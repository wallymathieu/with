using System;
using System.Collections.Generic;
using With.Collections;
using System.Linq;

namespace With.Rubyfy
{
    public static class GroupByExtensions
    {
        public static IEnumerable<IGrouping<TKey,T>> GroupBy<T, TKey>(this IEnumerable<T> self, Func<T, TKey> keySelector)
        {
            return Enumerable.GroupBy(self, keySelector);
        }
    }
}

