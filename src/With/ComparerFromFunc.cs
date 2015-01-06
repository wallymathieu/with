using System;
using System.Collections.Generic;
using System.Linq;
namespace With
{
    internal class ComparerFromFunc<T> : IComparer<T>
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
