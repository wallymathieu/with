using System.Collections.Generic;

namespace With
{
    public class HashSet
    {
        public static HashSet<T> Create<T>(IEnumerable<T> collectionx)
        {
            return new HashSet<T>(collectionx);
        }

        public static HashSet<T> Create<T>(IEnumerable<T> collection, IEqualityComparer<T> comparer)
        {
            return new HashSet<T>(collection, comparer);
        }
    }
}
