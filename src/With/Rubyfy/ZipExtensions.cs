using System;
using System.Collections.Generic;
using System.Linq;

namespace With.Rubyfy
{
    public static class ZipExtensions
    {
        public static IEnumerable<T[]> Zip<T>(this IEnumerable<T> self, params IEnumerable<T>[] others)
        {
            // ElementAtOrDefault: If the type of source implements IList<T>
            var lists = others.Select(other => other.ToL()); 
            var i = 0;
            foreach (var item in self)
            {
                var nextOthers = lists.Select(list => list.ElementAtOrDefault(i));
                yield return new List<T> { item }.Tap(list => list.AddRange(nextOthers)).ToArray();
                i++;
            }
        }

        public static IEnumerable<Tuple<T, TOther>> Zip<T,TOther>(this IEnumerable<T> self, IEnumerable<TOther> other)
        {
            // ElementAtOrDefault: If the type of source implements IList<T>
            var list = other.ToL();
            var i = 0;
            foreach (var item in self)
            {
                yield return Tuple.Create(item, list.ElementAtOrDefault(i));
                i++;
            }
        }
    }
}

