using System;
using System.Collections.Generic;
namespace With.Collections
{
    /// <summary>
    /// Class that holds methods related to comparers
    /// </summary>
    internal static class Comparer
    {
        /// <summary>
        /// Create a <see cref="System.Collections.Generic.IComparer{T}"/> from a comparison Func
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="compare"></param>
        /// <returns></returns>
        public static IComparer<T> Create<T>(Func<T, T, int> compare)
        {
            return new ComparerFromFunc<T>(compare);
        }
        public static IComparer<T> Create<T, TComparable>(Func<T, TComparable> select)
            where TComparable : IComparable
        {
            return new ComparerFromSelect<T, TComparable>(select);
        }
        private class ComparerFromSelect<T, TComparable> : IComparer<T> where TComparable : IComparable
        {
            private readonly Func<T, TComparable> _select;
            private readonly IComparer<TComparable> _comparer = Comparer<TComparable>.Default;

            public ComparerFromSelect(Func<T, TComparable> select)
            {
                _select = select;
            }

            public int Compare(T x, T y)
            {
                return _comparer.Compare(_select(x), _select(y));
            }
        }
        /// <summary>
        /// An adapter class to let a compare func act as a <see cref="System.Collections.Generic.IComparer{T}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private class ComparerFromFunc<T> : IComparer<T>
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
   
}
