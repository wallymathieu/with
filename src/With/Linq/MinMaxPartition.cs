using System.Collections.Generic;
namespace With.Linq
{
    /// <summary>
    /// A partition of a sequence into minima and maxima elements
    /// </summary>
    public class MinMaxPartition<T> : IEnumerable<IEnumerable<T>>
    {
        /// <summary>
        /// Gets the minima.
        /// </summary>
        public IEnumerable<T> Min { get; private set; }
        /// <summary>
        /// Gets the maxima.
        /// </summary>
        public IEnumerable<T> Max { get; private set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:With.Linq.MinMaxPartition`1"/> class.
        /// </summary>
        /// <param name="minArray">Minima array.</param>
        /// <param name="maxArray">Maxima array.</param>
        public MinMaxPartition(IEnumerable<T> minArray, IEnumerable<T> maxArray)
        {
            Min = minArray;
            Max = maxArray;
        }
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        public IEnumerator<IEnumerable<T>> GetEnumerator()
        {
            return new List<IEnumerable<T>> { Min, Max }.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new[] { Min, Max }.GetEnumerator();
        }
    }
}
