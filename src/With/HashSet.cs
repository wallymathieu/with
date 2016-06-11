using System.Collections.Generic;

namespace With
{
    /// <summary>
    /// Class that holds methods related to <see cref="HashSet{T}"/>. The same pattern as <see cref="System.Tuple"/>
    /// </summary>
    public class HashSet
    {
        public static HashSet<T> Create<T>(IEnumerable<T> collection)
        {
            return new HashSet<T>(collection);
        }

        public static HashSet<T> Create<T>(IEnumerable<T> collection, IEqualityComparer<T> comparer)
        {
            return new HashSet<T>(collection, comparer);
        }
    }
}
