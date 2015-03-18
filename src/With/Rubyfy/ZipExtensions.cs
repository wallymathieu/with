using System.Collections.Generic;
using System.Linq;

namespace With.Rubyfy
{
	public static class ZipExtensions
    {
        public static IEnumerable<T[]> Zip<T>(this IEnumerable<T> self, params IEnumerable<T>[] arrays)
        {
            var i = 0;
            foreach (var item in self)
            {
                var list = new List<T> { item };
                list.AddRange(arrays.Select(array => array.ElementAtOrDefault(i)));
                yield return list.ToArray();
                i++;
            }
        }
    }
}

