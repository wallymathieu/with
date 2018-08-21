using System.Collections.Generic;

namespace With.Collections
{
    /// <summary>
    /// The partition contains two collections. First collection is of all entities that satisfy predicate and second collection does not satisfy predicate
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Partition<T> : IEnumerable<IEnumerable<T>>
    {
        /// <summary>
        /// The collection of all entities that do satisfy predicate 
        /// </summary>
        public IEnumerable<T> True { get; }
        /// <summary>
        /// The collection of all entities that do not satisfy predicate 
        /// </summary>
        public IEnumerable<T> False { get; }
        internal Partition(IEnumerable<T> trueArray, IEnumerable<T> falseArray)
        {
            True = trueArray;
            False = falseArray;
        }
        ///
        public IEnumerator<IEnumerable<T>> GetEnumerator()
        {
            return new List<IEnumerable<T>> { True, False }.GetEnumerator();
        }
        /// 
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new[] { True, False }.GetEnumerator();
        }
    }
}

