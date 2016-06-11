using System;
using System.Collections.Generic;
namespace With.Collections
{
    /// <summary>
    /// Class that holds methods related to comparers
    /// </summary>
    public partial class Comparer
    {
        /// <summary>
        /// Create a <see cref="System.Collections.Generic.IComparer{T}"/> from a comparison Func
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="compare"></param>
        /// <returns></returns>
        public static ComparerFromFunc<T> Create<T>(Func<T, T, int> compare)
        {
            return new ComparerFromFunc<T>(compare);
        }
    }
    /// <summary>
    /// An adapter class to let a compare func act as a <see cref="System.Collections.Generic.IComparer{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ComparerFromFunc<T> : IComparer<T>
    {
        private readonly Func<T, T, int> _compare;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="compare"></param>
        public ComparerFromFunc(Func<T, T, int> compare)
        {
            _compare = compare;
        }
        /// <summary>
        /// Compares two objects and returns a value indicating the ordering between the two objects.
        /// </summary>
        public int Compare(T x, T y)
        {
            return _compare(x, y);
        }
    }
}
