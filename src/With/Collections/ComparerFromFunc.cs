using System;
using System.Collections.Generic;
namespace With.Collections
{
    public partial class Comparer
    {
        public static ComparerFromFunc<T> Create<T>(Func<T, T, int> compare)
        {
            return new ComparerFromFunc<T>(compare);
        }
    }

    public class ComparerFromFunc<T> : IComparer<T>
    {
        private readonly Func<T, T, int> _compare;
        public ComparerFromFunc(Func<T, T, int> compare)
        {
            _compare = compare;
        }

        public int Compare(T x, T y)
        {
            return _compare(x, y);
        }
    }
}
