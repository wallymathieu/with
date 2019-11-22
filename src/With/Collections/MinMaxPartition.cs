using System;
using System.Collections.Generic;

namespace With.Collections
{
    /// <summary>
    /// A partition of a sequence into minima and maxima elements
    /// </summary>
    [Obsolete("Not a common enough pattern")]
    public class MinMaxPartition<T> : IEnumerable<IEnumerable<T>>
    {
        /// <summary>
        /// Gets the minima.
        /// </summary>
        public IEnumerable<T> Minima { get; }
        
        /// <summary>
        /// Gets the minimums.
        /// </summary>
        public IEnumerable<T> Minimums => Minima;

        /// <summary>
        /// Gets the maxima.
        /// </summary>
        public IEnumerable<T> Maxima { get; }
        
        /// <summary>
        /// Gets the maximums.
        /// </summary>
        public IEnumerable<T> Maximums => Maxima;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:With.Collections.MinMaxPartition`1"/> class.
        /// </summary>
        /// <param name="minimaArray">Minima array.</param>
        /// <param name="maximaArray">Maxima array.</param>
        public MinMaxPartition(IEnumerable<T> minimaArray, IEnumerable<T> maximaArray)
        {
            Minima = minimaArray;
            Maxima = maximaArray;
        }
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        public IEnumerator<IEnumerable<T>> GetEnumerator()
        {
            return new List<IEnumerable<T>> { Minima, Maxima }.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new[] { Minima, Maxima }.GetEnumerator();
        }
    }
}
